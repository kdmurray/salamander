using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Salamander.Core;

namespace Salamander.Importer
{
	public class ImportEngine
	{
		public string inputDirectory { get; set; }
		public string outputDirectory { get; set; }
		public string authorName { get; set; }
		public string inputExtension { get; set; }
		public string outputExtension { get; set; }

		private List<string> errors;

		public ImportEngine (Arguments args)
		{
			// Initialization
			inputDirectory = "";
			outputDirectory = "";
			authorName = "";
			inputExtension = ".markdown";
			outputExtension = ".md";
			errors = new List<string> ();

			ReadArguments (args);

			if (!ValidateInputData ())
			{
				Utility.DisplayErrors (errors);
				return;
			}

		}

		public void ProcessInputFiles ()
		{
			string[] fileList = Directory.GetFiles (inputDirectory, "*" + inputExtension);

			foreach (string file in fileList)
			{
				FileInfo fi = new FileInfo(file);

				StreamReader sr = new StreamReader(fi.FullName);

				string outputContents = ConvertHeadersToMultiMarkdown (sr.BaseStream);

				string outputFilename = Path.Combine (outputDirectory, "source", "posts", fi.Name.Replace (inputExtension, outputExtension));

				StreamWriter sw = new StreamWriter(outputFilename);
				sw.Write (outputContents);
				sw.Close ();
			}
		}

		public void CreateDirectoryStructure ()
		{
			string baseDir = outputDirectory;
			string sourceDir = Path.Combine (baseDir, "source");
			string postsDir = Path.Combine (sourceDir, "posts");
			string pagesDir = Path.Combine (sourceDir, "pages");

			Directory.CreateDirectory (baseDir);
			Directory.CreateDirectory (sourceDir);
			Directory.CreateDirectory (postsDir);
			Directory.CreateDirectory (pagesDir);
		}

		public Stream GetFileStream (string sourceFilePath)
		{
			if (File.Exists (sourceFilePath)) 
			{
				StreamReader sr = new StreamReader(sourceFilePath);
				return sr.BaseStream;
			}

			return Stream.Null;
		}

		public string ConvertHeadersToMultiMarkdown (Stream fileData)
		{
			StreamReader reader = new StreamReader (fileData);
			bool inHeader = false;
			bool inCategories = false;
			bool inTags = false;
			StringBuilder sb = new StringBuilder (); 
			StringBuilder tagList = new StringBuilder ();
			StringBuilder catList = new StringBuilder ();

			while (!reader.EndOfStream)
			{
				string line = reader.ReadLine ();

				if (line == "---") 
				{
					inHeader = !inHeader;

					if (!inHeader) 
					{
						if (inTags) 
						{
							inTags = false;
							sb.Append ("tags: ");
							sb.Append (tagList.ToString ());
							sb.Append (System.Environment.NewLine);
						} 

						if (inCategories) 
						{
							inCategories = false;
							sb.Append ("categories: ");
							sb.Append (catList.ToString ());
							sb.Append (System.Environment.NewLine);
						}
					}
				}

				if (inHeader) 
				{
					string[] pieces = new string[] { "", "" };
					int splitPos = 0;

					if (line.Contains (":")) {
						splitPos = line.IndexOf (":");
						pieces [0] = line.Substring (0, splitPos).Trim ();
						pieces [1] = line.Substring (splitPos + 1).Trim ();
					} else {
						pieces [0] = line;
					}

					if (pieces [0].Trim () == "date") {
						sb.Append ("updated: ");
						sb.Append (pieces [1]);
						sb.Append (System.Environment.NewLine);
						sb.Append ("published: ");
						sb.Append (pieces [1]);
						sb.Append (System.Environment.NewLine);
					}

					if (pieces [0].Trim () == "slug" ||
						pieces [0].Trim () == "title") {
						sb.Append (line);
						sb.Append (System.Environment.NewLine);
					}

					if (inTags) {
						if (pieces [0].Trim ().StartsWith ("- ")) {
							if (tagList.Length > 0) {
								tagList.Append (",");
							}
							tagList.Append (pieces [0].Substring (1).Trim ());
						} else {
							inTags = false;
							sb.Append ("tags: ");
							sb.Append (tagList.ToString ());
							sb.Append (System.Environment.NewLine);
						}
					}

					if (pieces [0].Trim () == "tags") {
						inTags = true;
					}					

					if (inCategories) {
						if (pieces [0].Trim ().StartsWith ("- ")) {
							if (catList.Length > 0) {
								catList.Append (",");
							}
							catList.Append (pieces [0].Substring (1).Trim ());
						} else {
							inCategories = false;
							sb.Append ("categories: ");
							sb.Append (catList.ToString ());
							sb.Append (System.Environment.NewLine);
						}
					}

					if (pieces [0].Trim () == "categories") {
						inCategories = true;
					}

				}

				if (!inHeader &&
					!inTags &&
					!inCategories &&
					line != "---") {
					sb.AppendLine (line);
				}
			}

			return sb.ToString ();
		}

		private bool ValidateInputData ()
		{
			bool valid = true;

			if (!Directory.Exists (inputDirectory)) {
				valid = false;
			}

			if (!Directory.Exists (outputDirectory)) {
				try
				{
					Directory.CreateDirectory (outputDirectory);
				}
				catch (Exception ex)
				{
					Console.WriteLine (ex.Message);
					valid = false;
				}
			}

			return valid;
		}

		public void ReadArguments (Arguments arguments)
		{
			if (arguments.ContainsKey ("--input")) {
				inputDirectory = arguments["--input"];
			}
			
			if (arguments.ContainsKey ("-i")) {
				inputDirectory = arguments["-i"];
			}
			
			if (arguments.ContainsKey ("--output")) {
				outputDirectory = arguments["--output"];
			}
			
			if (arguments.ContainsKey ("-o")) {
				outputDirectory = arguments["-o"];
			}
			
			if (arguments.ContainsKey ("--author")) {
				authorName = arguments["--author"];
			}
			
			if (arguments.ContainsKey ("-a")) {
				authorName = arguments["-a"];
			}
		}

	}
}

