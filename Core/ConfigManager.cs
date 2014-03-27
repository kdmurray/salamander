using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Salamander.Core
{
	public class ConfigManager
	{
		/// <summary>
		/// Loads configuration values from the Broiled config file
		/// </summary>
		/// <returns>
		/// Populated instance of Config based on the file contents
		/// </returns>
		/// <param name='filePath'>
		/// Path to the configuration file
		/// </param>
		public static Config LoadConfiguration (string filePath)
		{
			//TODO: Error handling
			XmlSerializer xs = new XmlSerializer(typeof(Config));			
			StreamReader sr = new StreamReader(filePath);
			
			return (Config) xs.Deserialize (sr.BaseStream);
		}
		
		
		/// <summary>
		/// Generates the default configuration file for Broiled
		/// </summary>
		public static Config GenerateDefaultConfig ()
		{			
			Config config = new Config();
			
			//TODO: change default folder location before deployment, or publication
			// Default folder location
			config.BlogRoot = "/Users/kdmurray/Dropbox/Projects/BroiledBlog";
			
			// Default installation location for the Multimarkdown path
			config.MultiMarkdownPath = "/usr/local/bin/mmd";
			
			// Default author display name
			config.DefaultAuthor = "Keith Murray";
			
			// Default URL for author
			config.DefaultAuthorUrl = "http://kdmurray.net";
			
			// Default number of posts in RSS feed
			config.NumberOfPostsInFeed = 20;
			
			// Default number of posts per page (incl front page)
			config.NumberOfPostsPerPage = 10;
			
			// Base URL for the website once published
			config.BaseUrl = "http://sandbox.kdmurray.net";
			
			// Default "last-run" time
			config.LastRunTime = Convert.ToDateTime ("1994-05-23");
			
			// Default title
			config.BlogTitle = "kdmurray.blog";
			
			// Default subtitle
			config.BlogSubtitle = "at the crossroads of life and tech";
			
			// Default Copyright statement
			config.BlogCopyright = "Copyright 2005 - " + DateTime.Now.Year.ToString () + " " + config.DefaultAuthor + ". All rights reserved.";
			
			// Default Copyright statement
			config.BlogCopyrightHtml = "&copy; 2005 - " + DateTime.Now.Year.ToString () + " <a href=\"" + config.DefaultAuthorUrl + "\">" + config.DefaultAuthor + "</a>. All rights reserved.";

			return config;
		}
		
		public static void WriteConfig (string filePath, Config config)
		{
			XmlSerializer xs = new XmlSerializer(typeof(Config));
			StreamWriter sw = new StreamWriter(filePath);
			xs.Serialize (sw.BaseStream, config);
			sw.Close ();
			sw.Dispose ();
		}
	}
}

