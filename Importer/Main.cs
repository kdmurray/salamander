using System;

using Salamander.Core;

namespace Salamander.Importer
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Arguments arguments = new Arguments(args);
			ImportEngine engine = new ImportEngine(arguments);


		}
	}
}
