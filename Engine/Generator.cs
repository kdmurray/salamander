using System;
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Text;

using Salamander.Core;

namespace Salamander.Engine
{
	public class Generator
	{
		Config config;
		
		string[] postFiles;
		string[] pageFiles;
		
		List<Post> posts;
		List<Page> pages;

		public Generator (Config configuration, Arguments arguments)
		{
			Console.WriteLine ("Salamander Static Blog Engine");
			Console.WriteLine ("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
			Console.WriteLine ("");
			Console.WriteLine ("Starting up blog engine...");


			config = configuration;
			posts = new List<Post>();
			pages = new List<Page>();

		}

		public void EnumerateContent()
		{
			Console.WriteLine ("Salamander is searching for posts...");
			postFiles = EnumerateFiles (config.PostsDir);
			Console.WriteLine ("Salamander found {0} posts...", postFiles.Length);

			Console.WriteLine ("Salamander is searching for pages...");
			pageFiles = EnumerateFiles (config.PagesDir);
			Console.WriteLine ("Salamander found {0} pages...", pageFiles.Length);
		}

		/// <summary>
		/// Provides an array of absolute path names to each individual source file.
		/// </summary>
		/// <param name='SourceDirectory'>
		/// Source file directory
		/// </param>
		private string[] EnumerateFiles (string SourceDirectory)
		{
			return Directory.GetFiles (SourceDirectory, "*.md", SearchOption.AllDirectories);
		}

		public Post ReadPostFile(string filename)
		{
			Post post = new Post(config);
			string[] postLines = File.ReadAllLines(filename);
			
			post.SourceFile = filename;			
			
			bool inHeader = true;
			
			StringBuilder mdContent = new StringBuilder();
			
			foreach (string line in postLines)
			{
				if (inHeader)
				{
					if (line == "")
					{
						inHeader = false;
					}
					if (line.StartsWith ("title:"))
					{
						post.Title = Utility.GetMarkdownHeaderValue (line);
					}
					if (line.StartsWith ("author:"))
					{
						post.Author = Utility.GetMarkdownHeaderValue (line);
					}
					if (line.StartsWith ("published:"))
					{
						string published = Utility.GetMarkdownHeaderValue (line);
						if (published.Trim () != "")
						{
							post.Published = DateTime.ParseExact(published, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
						}
						else
						{
							post.Published = Convert.ToDateTime ("01-Jan-1900");
						}
					}
					if (line.StartsWith ("slug:"))
					{
						post.Slug = Utility.GetMarkdownHeaderValue (line);
					}
					if (line.StartsWith ("tags:"))
					{
						string tagString = Utility.GetMarkdownHeaderValue (line);
						post.Tags = tagString.Split(",".ToCharArray ());
						for (int i = 0; i < post.Tags.Length; i++)
						{
							post.Tags[i] = post.Tags[i].ToString ().Trim ();
						}
					}	
					if (line.StartsWith ("categories:"))
					{
						string catString = Utility.GetMarkdownHeaderValue (line);
						post.Categories = catString.Split(",".ToCharArray ());
						for (int i = 0; i < post.Categories.Length; i++)
						{
							post.Categories[i] = post.Categories[i].ToString ().Trim ();
						}
					}	
				}
				else
				{
					mdContent.Append (line);
					mdContent.Append (System.Environment.NewLine);
				}
			}
			
			string destFile = config.BlogDir + post.Published.ToString ("/yyyy/MM/dd/") + post.Slug + "/index.html";
			post.DestFile = destFile;
			
			post.BodyMarkdown = mdContent.ToString ();
			post.BodyHtml = Utility.MarkdownToHtml(post.BodyMarkdown, config.MultiMarkdownPath);

			return post;
		}	

	}
}

