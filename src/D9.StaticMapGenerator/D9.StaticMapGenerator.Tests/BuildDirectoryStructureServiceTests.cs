using D9.StaticMapGenerator.DirectoryStructure;
using D9.StaticMapGenerator.Services;
using NUnit.Framework;

namespace D9.StaticMapGenerator.Tests
{
	[TestFixture]
	public class BuildDirectoryStructureServiceTests
	{
		[Test]
		public void Execute_Always_Works()
		{
			IFileSystemAdapter fileSystem = new FakeFileSystemAdapter();
			IBuildDirectoryStructureService service = new BuildDirectoryStructureService(fileSystem);

			IDirInfo site = service.Execute("C:\\");

			Assert.AreEqual(2, site.Directories.Length);

			Assert.AreEqual("grandpa", site.Directories[0].Name);

			Assert.AreEqual("grandma", site.Directories[1].Name);

			string[] files = new string[]
			                 	{
			                 		@"script1.js" ,
			                 		@"script2.js" ,
			                 		@"style1.css",
			                 		@"image1.jpg" ,
			                 		@"image2.gif" 
			                 	};

			foreach (string file in files)
			{
				Assert.IsTrue(IsDirContainsFile(site, file), "File [{0}] is missed", file);
			}

			Assert.IsFalse(IsDirContainsFile(site, "Boggey"));


		}

		private static bool IsDirContainsFile(IDirInfo dir, string fileName)
		{
			if (dir.Files.Exists(delegate(string file)
									{
										return file == fileName;
									}))
				return true;

			foreach (IDirInfo sub in dir.Directories)
			{
				if (IsDirContainsFile(sub, fileName))
					return true;
			}
			return false;
		}
	}
}