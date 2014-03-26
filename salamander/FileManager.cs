using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace salamander
{
	public class FileManager
	{
		List<Post> _posts;

		public List<Post> LoadPostsFromDisk(string postDirectoryPath)
		{
			List<Post> posts = new List<Post>();
			string[] postFiles = Directory.GetFiles(postDirectoryPath, "*.md");

			foreach (string file in postFiles)
			{
				Post p = ReadPostFromDisk(file);
				posts.Add (p);
			}

			return posts;
		}

		Post ReadPostFromDisk(string filename)
		{
			Post post = new Post();
			string[] lines = File.ReadAllLines (filename);

			bool inHeader = true;
			
			StringBuilder mdContent = new StringBuilder();
			
			foreach (string line in lines)
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
					
					if (line.StartsWith ("author url:"))
					{
						post.Title = Utility.GetMarkdownHeaderValue (line);
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
					
					if (line.StartsWith ("series:"))
					{
						post.Series = Utility.GetMarkdownHeaderValue (line);
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
			
			post.BodySource = mdContent.ToString ();
			post.BodyRendered = Utility.MarkdownToHtml(post.BodySource);
			
			return post;
		}

	}
}

