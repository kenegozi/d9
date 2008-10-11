#region License

// Copyright (c) 2008 Ken Egozi (ken@kenegozi.com)
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
// 
//     * Redistributions of source code must retain the above copyright notice,
//     this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice,
//     this list of conditions and the following disclaimer in the documentation
//     and/or other materials provided with the distribution.
//     * Neither the name of the D9 project nor the names of its
//     contributors may be used to endorse or promote products derived from this
//     software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
// THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion License

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace D9.NHibernate.Tests
{
	/// <summary>
	/// 
	/// </summary>
	[TestFixture]
	public abstract class AbstractNHibernateDbTest
	{
		/// <summary>
		/// The configuration object
		/// </summary>
		protected Configuration cfg;

		/// <summary>
		/// Mapping files used in the Fixture
		/// </summary>
		protected abstract IList Mappings { get; }


		///<summary>
		///
		///</summary>
		protected ISessionFactory sessionFactory;
		
		/// <summary>
		/// Creates the tables used in this test
		/// </summary>
		[TestFixtureSetUp]
		public virtual void TestFixtureSetUp()
		{
			Configure();
		}

		[SetUp]
		public virtual void SetUp()
		{
			File.Delete(DbFileName);
			CreateSchema();
		}

		[TearDown]
		public virtual void TearDown()
		{
			File.Delete(DbFileName);
		}

		/// <summary>
		/// 
		/// </summary>
		[TestFixtureTearDown]
		public void TestFixtureTearDown()
		{
			CleanUp();
		}

		static string ConnectionString
		{
			get { return string.Format("Data Source={0};Version=3", DbFileName); }
		}

		private static string DbFileName
		{
			get { return "TEST_DB.db"; }
		}


		private void Configure()
		{
			cfg = new Configuration();

			var props = new Dictionary<string,string>
			            	{
			            		{"dialect", "NHibernate.Dialect.SQLiteDialect"},
			            		{"connection.provider", "NHibernate.Connection.DriverConnectionProvider"},
			            		{"connection.driver_class", "NHibernate.Driver.SQLite20Driver"},
			            		{"connection.connection_string", ConnectionString},
			            		{"query.substitutions", "true=1;false=0"}
			            	};
			cfg.AddProperties(props);

			var assembly = Assembly.GetExecutingAssembly();
			var assemblyName = "D9.NHibernate.Tests";

			foreach (string file in Mappings)
			{
				cfg.AddResource(assemblyName + "." + file, assembly);
			}

			sessionFactory = cfg.BuildSessionFactory();
		}

		private void CreateSchema()
		{
			new SchemaExport(cfg).Create(false, true);
		}

		private void CleanUp()
		{
			if (sessionFactory.IsClosed == false)
				sessionFactory.Close();
			sessionFactory.Dispose();
			sessionFactory = null;
			cfg = null;
		}

		///<summary>
		///
		///</summary>
		///<param name="sql"></param>
		///<returns></returns>
		protected int ExecuteNonQuery(string sql)
		{
			return Execute(cmd => cmd.ExecuteNonQuery(), sql);
		}

		///<summary>
		///
		///</summary>
		///<param name="sql"></param>
		///<typeparam name="T"></typeparam>
		///<returns></returns>
		protected T ExecuteScalar<T>(string sql)
		{
			return (T)Execute(cmd => cmd.ExecuteScalar(), sql);
		}

		///<summary>
		///
		///</summary>
		///<param name="sql"></param>
		///<param name="mapper"></param>
		///<typeparam name="T"></typeparam>
		///<returns></returns>
		protected T ExecuteAndExtractFirstFromReader<T>(string sql, Func<IDataReader, T> mapper)
		{
			return Execute(cmd =>
			                	{
			                		using (var reader = cmd.ExecuteReader())
			                		{
			                			if (reader.Read())
			                				return mapper(reader);
			                			return default(T);
			                		}
			                	}, sql);
		}

		private T Execute<T>(Func<IDbCommand, T> action, string sql)
		{
			using (var prov = ConnectionProviderFactory.NewConnectionProvider(cfg.Properties))
			{
				var conn = prov.GetConnection();

				try
				{
					using (var tran = conn.BeginTransaction())
					using (var cmd = conn.CreateCommand())
					{
						cmd.CommandText = sql;
						cmd.Transaction = tran;
						cmd.CommandType = CommandType.Text;
						var result = action(cmd);
						tran.Commit();
						return result;
					}
				}
				finally
				{
					prov.CloseConnection(conn);
				}
			}
		}
	}
}