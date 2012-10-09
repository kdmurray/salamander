using System;

using Salamander.Core;
using Salamander.Importer;

using NUnit.Framework;

namespace Salamander.Tests
{
	[TestFixture]
	public class ImportEngineTests
	{

		[Test]
		public void ImportEngine_ReadArguments()
		{
			string[] args = new string[] {"-i=/path/to/folder", "-o=/dev/opt/path" };

			Arguments arguments = new Arguments(args);

			ImportEngine engine = new ImportEngine(arguments);

			string expectedInputFolder = "/path/to/folder";
			string expectedOutputFolder = "/dev/opt/path";

			Assert.AreEqual (expectedInputFolder, engine.inputDirectory);
			Assert.AreEqual (expectedOutputFolder, engine.outputDirectory);
		}

	}
}

