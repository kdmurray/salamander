using System;
using System.IO;
using System.Text;

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

		[Test]
		public void ImportEngine_ConvertHeadersToMultiMarkdown ()
		{
			StringBuilder sourceBuilder = new StringBuilder ();
			StringBuilder expectedBuilder = new StringBuilder ();

			sourceBuilder.AppendLine ("---");
			sourceBuilder.AppendLine ("comments: true");
			sourceBuilder.AppendLine ("date: 2012-04-30 16:00:21");
			sourceBuilder.AppendLine ("layout: post");
			sourceBuilder.AppendLine ("slug: ubuntu-school-gui-xubuntu-desktop-for-ubuntu-server");
			sourceBuilder.AppendLine ("title: Ubuntu School - GUI (xubuntu-desktop) for Ubuntu Server");
			sourceBuilder.AppendLine ("wordpress_id: 1097");
			sourceBuilder.AppendLine ("categories:");
			sourceBuilder.AppendLine ("- Lessons");
			sourceBuilder.AppendLine ("- Tech Tips");
			sourceBuilder.AppendLine ("series:");
			sourceBuilder.AppendLine ("- Ubuntu School");
			sourceBuilder.AppendLine ("tags:");
			sourceBuilder.AppendLine ("- boot");
			sourceBuilder.AppendLine ("- cli");
			sourceBuilder.AppendLine ("- command line");
			sourceBuilder.AppendLine ("- gnome desktop");
			sourceBuilder.AppendLine ("- grub");
			sourceBuilder.AppendLine ("- gui");
			sourceBuilder.AppendLine ("- lightdm");
			sourceBuilder.AppendLine ("- server");
			sourceBuilder.AppendLine ("- startup");
			sourceBuilder.AppendLine ("- Ubuntu");
			sourceBuilder.AppendLine ("---");

			expectedBuilder.AppendLine ("updated: 2012-04-30 16:00:21");
			expectedBuilder.AppendLine ("published: 2012-04-30 16:00:21");
			expectedBuilder.AppendLine ("slug: ubuntu-school-gui-xubuntu-desktop-for-ubuntu-server");
			expectedBuilder.AppendLine ("title: Ubuntu School - GUI (xubuntu-desktop) for Ubuntu Server");
			expectedBuilder.AppendLine ("categories: Lessons,Tech Tips");
			expectedBuilder.AppendLine ("tags: boot,cli,command line,gnome desktop,grub,gui,lightdm,server,startup,Ubuntu");

			string[] expectedLines = expectedBuilder.ToString ().Split (System.Environment.NewLine.ToCharArray ());
			string[] actualLines = engine.ConvertHeadersToMultiMarkdown (new MemoryStream (Encoding.UTF8.GetBytes (sourceBuilder.ToString ()))).Split (System.Environment.NewLine.ToCharArray ());

			Assert.AreEqual (expectedLines.Length, actualLines.Length);

			for (int i = 0; i < actualLines.Length; i++) 
			{
				Assert.AreEqual (expectedLines[i], actualLines[i]);
			}
		}

	}
}

