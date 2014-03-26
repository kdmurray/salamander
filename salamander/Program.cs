using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace salamander
{
	class MainClass
	{
		static List<Post> _posts;

		public static void Main (string[] args)
		{
			SalamanderConfig cfg = new SalamanderConfig();

			cfg.ReadConfig (XmlReader.Create ("salamander.config"));

			Utility.multiMarkdownPath = cfg.MultiMarkdownPath;

			FileManager fmgr = new FileManager();

			_posts = fmgr.LoadPostsFromDisk (cfg.PostsDir);

			foreach (Post p in _posts)
			{
				Console.WriteLine (p.Title);
			}




		}
	}
}
