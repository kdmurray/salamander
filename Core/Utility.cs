using System;
using System.Collections.Generic;

namespace Salamander.Core
{
	public class Utility
	{
		public static void DisplayErrors (List<string> errors)
		{
			Console.WriteLine ("Errors were encountered when processing your request. See below for details.");
			Console.WriteLine ("");

			foreach (string error in errors) {
				Console.WriteLine ("--------------------------------------------------------------------------------");
				Console.WriteLine (error);
			}
		}
	}
}

