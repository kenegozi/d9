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

// ReSharper disable AccessToStaticMemberViaDerivedType
using System;
using System.Collections.Generic;
using System.Reflection;
using D9.SQLQueryGenerator.DatabaseMetadataProviders;
using D9.SQLQueryGenerator.Descriptors;
using NUnit.Framework;
using NUnit.Framework.ExtensionMethods;

namespace D9.SQLQueryGenerator.Tests
{
	[TestFixture]
	public class MetadataProcessorTests
	{
		private MetadataProcessor processor;

		[SetUp]
		public void SetUp()
		{
			processor = new MetadataProcessor();
			CreateTables();
		}

		[Test]
		public void GetTableDescriptorFrom_Always_CreatesTheTableDescriptor()
		{
			var propertyMetadata = new DbPropertyMetadata(
				"dbo", "MyTable", "Id", "int", false);

			TableDescriptor table = processor.GetTableDescriptorFrom(propertyMetadata, true);

			Assert.AreEqual(table.Name, "MyTable");
			Assert.AreEqual(table.Schema, "dbo");
		}

		[Test]
		public void GetTableDescriptorFrom_WhenCalledWithExistingTableData_ReturnsTheExistingTable()
		{
			var property1 = new DbPropertyMetadata(
				"dbo", "MyTable", "Id", "int", false);

			var property2 = new DbPropertyMetadata(
				"dbo", "MyTable", "Name", "string", false);

			TableDescriptor table1 = processor.GetTableDescriptorFrom(property1, true);
			TableDescriptor table2 = processor.GetTableDescriptorFrom(property2, true);

			Assert.AreEqual(table1, table2);
		}

		[Test]
		public void Process_WhenFindsNewTable_AddsTheTable()
		{
			GetTables().Add("Initial", new TableDescriptor("Initial", true));

			GetTables().Keys.Contains("Initial").Should(Be.True);

			processor.Process(new DbPropertyMetadata(
			                  	"dbo", "MyTable", "Id", "int", false), true);

			Assert.AreEqual(2, GetTables().Keys.Count);

			GetTables().Keys.Contains("dbo_MyTable").Should(Be.True);
		}

		[Test]
		public void Process_WhenFindsExistingTable_WillNotAddTheTable()
		{
			GetTables().Add("Initial", new TableDescriptor("Initial", true));
			GetTables().Keys.Contains("Initial").Should(Be.True);

			processor.Process(new DbPropertyMetadata(
			                  	null, "Initial", "Id", "int", false), true);

			Assert.AreEqual(1, GetTables().Keys.Count);
		}

		#region helpers

		private FieldInfo GetTablesField()
		{
			return processor.GetType().GetField("tables", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField);
		}

		private void CreateTables()
		{
			GetTablesField().SetValue(processor, new Dictionary<string, TableDescriptor>());
		}

		private IDictionary<string, TableDescriptor> GetTables()
		{
			return GetTablesField().GetValue(processor) as IDictionary<string, TableDescriptor>;
		}

		#endregion
	}
}