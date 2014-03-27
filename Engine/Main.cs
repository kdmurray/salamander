using System;

using Salamander.Core;

namespace Salamander.Engine
{
	class MainClass
	{
		static string configFile;

		public static void Main (string[] args)
		{

			Arguments arguments = new Arguments (args);

			if (arguments.ContainsKey ("--config")) {
				configFile = arguments ["--config"];
			}

			if (arguments.ContainsKey ("-c")) {
				configFile = arguments ["-c"];
			}

			if (arguments.ContainsKey ("--generateConfig")) {
				ConfigManager.WriteConfig (configFile, ConfigManager.GenerateDefaultConfig());
			}

			Generator gen = new Generator(ConfigManager.LoadConfiguration (configFile), arguments);
		}

		static void Initialize()
		{
			configFile = "salamander.config";
		}
	}
}
