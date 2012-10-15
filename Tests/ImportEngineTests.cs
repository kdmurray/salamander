using System;

using Salamander.Core;
using Salamander.Importer;

using NUnit.Framework;

namespace Salamander.Tests
{
	[TestFixture]
	public class ImportEngineTests
	{
		ImportEngine engine;

		[SetUp]
		public void CreateImportEngineInstance ()
		{
			string[] args = new string[] {"-i=/path/to/folder", "-o=/dev/opt/path" };
			
			Arguments arguments = new Arguments(args);
			
			engine = new ImportEngine(arguments);
		}

		[Test]
		public void ImportEngine_ReadArguments()
		{
			string expectedInputFolder = "/path/to/folder";
			string expectedOutputFolder = "/dev/opt/path";
			
			Assert.AreEqual (expectedInputFolder, engine.inputDirectory);
			Assert.AreEqual (expectedOutputFolder, engine.outputDirectory);
		}


	}
}

