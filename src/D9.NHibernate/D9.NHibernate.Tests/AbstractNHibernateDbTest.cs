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