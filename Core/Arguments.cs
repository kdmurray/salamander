using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Salamander.Core
{
	public class Arguments
	{
		StringDictionary Parameters;
		Hashtable pa;

		public Arguments (string[] args)
		{
			Parameters = new StringDictionary();

			foreach (string arg in args)
			{
				KeyValuePair<string, string> param = SplitArgumentString (arg);

				Parameters.Add (param.Key, param.Value);
			}
		}

		private KeyValuePair<string, string> SplitArgumentString (string arg)
		{
			string key = "";
			string value = "";

			if (arg.Contains ("=")) {
				int splitPosition = arg.IndexOf ("=");

				key = arg.Substring (0, splitPosition);
				value = arg.Substring (splitPosition + 1).Trim ("\"".ToCharArray ());
			}
			else 
			{
				key = arg;
			}

			return new KeyValuePair<string, string>(key, value);
		}

		public string this [string key]
		{
			get
			{
				return(Parameters[key]);
			}
		}

		public bool ContainsKey (string key)
		{
			return Parameters.ContainsKey(key);
		}

		public bool ContainsValue (string value)
		{
			return Parameters.ContainsValue(value);
		}
	}
}

