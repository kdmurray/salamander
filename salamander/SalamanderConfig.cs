using System;
using System.Xml;
using System.Xml.Serialization;

namespace salamander
{
	public class SalamanderConfig
	{
		// System Configuration
		public string   MultiMarkdownPath;
		public DateTime LastRunTime;

		// Global Website Configuration
		public string   BaseURL;
		public string   Title;
		public string   Subtitle;
		public string   CopyrightNotice;

		// Blog Configuration
		public string   AuthorName;
		public string   AuthorURL;
		public string   FeedType;
		public int      PostsPerPage;
		public int      PostsInFeed;

		// File System Configuration
		public string   DraftsDir;
		public string   PostsDir;
		public string   PagesDir;
		public string   TemplatesDir;
		public string   OutputDir;

		XmlSerializer xmlSer;

		public SalamanderConfig ()
		{
			xmlSer = new XmlSerializer(this.GetType ());
		}

		public bool ReadConfig (XmlReader reader)
		{
			SalamanderConfig cfg = (SalamanderConfig)xmlSer.Deserialize (reader);

			this.AuthorName        = cfg.AuthorName;
			this.AuthorURL         = cfg.AuthorURL;
			this.BaseURL           = cfg.BaseURL;
			this.CopyrightNotice   = cfg.CopyrightNotice;
			this.FeedType          = cfg.FeedType;
			this.LastRunTime       = cfg.LastRunTime;
			this.MultiMarkdownPath = cfg.MultiMarkdownPath;
			this.PostsInFeed       = cfg.PostsInFeed;
			this.PostsPerPage      = cfg.PostsPerPage;
			this.Subtitle          = cfg.Subtitle;
			this.Title             = cfg.Title;
			this.DraftsDir         = cfg.DraftsDir;
			this.PostsDir          = cfg.PostsDir;
			this.PagesDir          = cfg.PagesDir;
			this.TemplatesDir      = cfg.TemplatesDir;
			this.OutputDir         = cfg.OutputDir;

			reader.Close ();
			return true;
		}

		public bool WriteConfig (XmlWriter writer)
		{
			xmlSer.Serialize (writer, this);

			writer.Close ();
			return true;
		}
	}
}

