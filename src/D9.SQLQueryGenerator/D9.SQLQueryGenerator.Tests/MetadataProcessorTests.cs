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