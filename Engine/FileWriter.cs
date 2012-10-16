using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Salamander.Core;

namespace Salamander.Engine
{
	public class FileWriter
	{
		Config config; 

		public FileWriter (Config configuration)
		{
			config = configuration;
		}

		public void WritePostSingle(Post post, List<Page> pages)
		{
				
			string headerHtml = Utility.MergeToTemplate (config.TemplatesDir + "/header.html", post, config, PageType.Post);
			headerHtml = Utility.InsertPagesBar (config, headerHtml, pages);
			string postHtml = Utility.MergeToTemplate (config.TemplatesDir + "/post_single.html", post, config, PageType.Post);
			string sidebarHtml = Utility.MergeToTemplate (config.TemplatesDir + "/sidebar.html", post, config, PageType.Post);
			string footerHtml = Utility.MergeToTemplate (config.TemplatesDir + "/footer.html", post, config, PageType.Post);
			
			string postDirectory = config.BlogDir + post.Published.ToString ("/yyyy/MM/dd/") + post.Slug;
			
			if (!Directory.Exists (postDirectory))
			{
				Directory.CreateDirectory (postDirectory);
			}
			
			Console.WriteLine ("Writing {0} to disk...", post.PostFilename);
			File.WriteAllText (post.PostFilename, headerHtml + postHtml + sidebarHtml + footerHtml);
		}

	}
}

