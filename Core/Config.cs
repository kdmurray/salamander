using System;
using System.IO;

namespace Salamander.Core
{
	public class Config
	{
		
		/// <summary>
		/// The root of the BroiledBlog data file tree.
		/// </summary>
		public string BlogRoot { get; set; }
		
		/// <summary>
		/// The directory where all outward-facing files will be stored. Derived from BlogRoot.
		/// </summary>
		public string BlogDir 
		{
			get
			{
				return Path.Combine(BlogRoot,"/blog");
			}
		}
		
		/// <summary>
		/// The directory where draft files will be stored. Not an outward-facing directory. Derived from BlogRoot.
		/// </summary>
		public string DraftsDir
		{
			get
			{
				return Path.Combine(BlogRoot, "/drafts");
			}
		}
		
		/// <summary>
		/// The directory where publishable blog posts will be stored. Material will be outward-facing after conversion. Derived from BlogRoot.
		/// </summary>
		public string PostsDir
		{
			get
			{
				return Path.Combine(BlogRoot, "/posts");
			}
		}
		
		/// <summary>
		/// The directory where template files will be stored. Not an outward-facing directory. Derived from BlogRoot.
		/// </summary>
		public string TemplatesDir
		{
			get
			{
				return Path.Combine(BlogRoot, "/templates");
			}
		}
		
		public string PagesDir
		{
			get
			{
				return Path.Combine(BlogRoot, "/pages");
			}
			
		}
		
		
		/// <summary>
		/// Represents the path to the MultiMarkdown executable
		/// </summary>
		public string MultiMarkdownPath { get; set; }
		
		public string BaseUrl { get; set; }
		
		public string BlogTitle { get; set; }
		
		public string BlogSubtitle { get; set; }
		
		public string BlogCopyright { get; set; }
		
		public string BlogCopyrightHtml { get; set; }
		
		public string DefaultAuthor { get; set; }
		
		public string DefaultAuthorUrl { get; set; }
		
		public int NumberOfPostsInFeed { get; set; }
		
		public int NumberOfPostsPerPage { get; set; }
		
		public DateTime LastRunTime { get; set; }
	}
}

