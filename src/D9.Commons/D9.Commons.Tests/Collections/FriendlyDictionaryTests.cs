// ReSharper disable InconsistentNaming
using System;
using System.Collections.Generic;
using System.Reflection;
using D9.Commons.Collections;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace D9.Commons.Tests.Collections
{
	internal class Record
	{
		public static Exception Exception(Action action)
		{
			return Exception((Delegate)action);
		}

		public static Exception Exception<TWhatever>(Func<TWhatever> action) 
		{
			return Exception((Delegate)action);
		}

		static Exception Exception(Delegate action) 
		{
			try
			{
				action.DynamicInvoke();
				Assert.Fail("should have thrown an exception");
			}
			catch (Exception ex)
			{
				if (ex is TargetInvocationException)
					ex = ex.InnerException;
				return ex;
			}
			return null;
		}
	}

	[TestFixture]
	public class FriendlyDictionaryTests
	{
		[Test]
		public void IndexerGet_MissingKey_ThroughNiceException()
		{
			var target = new FriendlyDictionary<int, string>();

			const int nonExistentKey = 1;

			var thrown = Record.Exception(() => 
				target[nonExistentKey]
			);

			Assert.That(thrown, Is.AssignableFrom(typeof(KeyNotFoundException)));
			Assert.That(thrown.Data["Key"], Is.EqualTo(nonExistentKey));
		}

		[Test]
		public void AddKeyValuePair_ExistingKey_ThroughNiceException()
		{
			var target = new FriendlyDictionary<int, string>();

			const int existingKey = 1;
			const string existingValue = "WHATEVER";

			var action = (Action) (() => 
				target.Add(new KeyValuePair<int, string>(existingKey, existingValue))
			);

			action();
			var thrown = Record.Exception(action);

			Assert.That(thrown, Is.AssignableFrom(typeof(ArgumentException)));
			Assert.That(thrown.Data["Key"], Is.EqualTo(existingKey));
			Assert.That(thrown.Data["Value"], Is.EqualTo(existingValue));
		}

		[Test]
		public void AddKeyAndValue_ExistingKey_ThroughNiceException()
		{
			var target = new FriendlyDictionary<int, string>();

			const int existingKey = 1;
			const string existingValue = "WHATEVER";

			var action = (Action)(() =>
				target.Add(existingKey, existingValue)
			);


			action();
			var thrown = Record.Exception(action);

			Assert.That(thrown, Is.AssignableFrom(typeof(ArgumentException)));
			Assert.That(thrown.Data["Key"], Is.EqualTo(existingKey));
			Assert.That(thrown.Data["Value"], Is.EqualTo(existingValue));
		}
	}
}

// ReSharper restore InconsistentNaming
