using System.Collections.Generic;
using D9.StaticMapGenerator.Services;

namespace D9.StaticMapGenerator.Tests
{
	/// <summary>
	/// Fake <see cref="IFileSystemAdapter"/> for tests
	/// </summary>
	public class FakeFileSystemAdapter : FileSystemAdapter
	{
		readonly Dictionary<string, string[]> directories = new Dictionary<string, string[]>();
		readonly Dictionary<string, string[]> scriptFiles = new Dictionary<string, string[]>();
		readonly Dictionary<string, string[]> styleFiles = new Dictionary<string, string[]>();
		readonly Dictionary<string, string[]> imageFiles = new Dictionary<string, string[]>();

		/// <summary>
		/// Creates a new FakeFileSystemAdapter
		/// </summary>
		public FakeFileSystemAdapter()
		{
			directories.Add(@"C:\", new string[] { @"C:\grandpa", @"C:\grandma" });
			directories.Add(@"C:\grandma", new string[] { @"C:\grandma\mom", @"C:\grandma\dad" });
			directories.Add(@"C:\grandma\dad", new string[] { @"C:\grandma\dad\Joe" });

			scriptFiles.Add(@"C:\grandpa", new string[] { @"script1.js" });
			scriptFiles.Add(@"C:\grandma\dad", new string[] { @"script2.js" });

			styleFiles.Add(@"C:\grandpa", new string[] { @"style1.css" });

			imageFiles.Add(@"C:\grandpa", new string[] { @"image1.jpg" });
			imageFiles.Add(@"C:\grandma\mom", new string[] { @"image2.gif" });
		}

		public override string[] GetScriptFiles(string dir)
		{
			if (scriptFiles.ContainsKey(dir))
				return scriptFiles[dir];
			return new string[0];
		}
		public override string[] GetStyleFiles(string dir)
		{
			if (styleFiles.ContainsKey(dir))
				return styleFiles[dir];
			return new string[0];
		}
		public override string[] GetImageFiles(string dir)
		{
			if (imageFiles.ContainsKey(dir))
				return imageFiles[dir];
			return new string[0];
		}
		public override string[] GetDirectories(string dir)
		{
			if (directories.ContainsKey(dir))
				return directories[dir];
			return new string[0];
		}
	}
}