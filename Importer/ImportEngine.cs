using System;
using System.Collections.Generic;
using System.IO;

using Salamander.Core;

namespace Salamander.Importer
{
	public class ImportEngine
	{
		public string inputDirectory { get; set; }
		public string outputDirectory { get; set; }
		public string authorName { get; set; }
		public bool   normalizeTags { get; set; }

		private List<string> errors;

		public ImportEngine (Arguments args)
		{
			// Initialization
			inputDirectory = "";
			outputDirectory = "";
			authorName = "";
			normalizeTags = false;
			errors = new List<string> ();


			ReadArguments (args);

			if (!ValidateInputData ()) {
				Utility.DisplayErrors (errors);
				return;
			}


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
			
			if (arguments.ContainsKey ("--normalizetags")) {
				normalizeTags = true;
			}
			
			if (arguments.ContainsKey ("-n")) {
				normalizeTags = true;
			}
		}

	}
}

