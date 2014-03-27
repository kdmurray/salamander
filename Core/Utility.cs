using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

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

		public static string GetMarkdownHeaderValue(string headerLine)
		{
			return headerLine.Substring (headerLine.IndexOf (":") + 1).Trim ();
		}

		public static string MarkdownToHtml (string Markdown, string mmdExecutable)
		{
			Process p = new Process();
			ProcessStartInfo psi = new ProcessStartInfo();
			
			psi.FileName = mmdExecutable;
			psi.RedirectStandardInput = true;
			psi.RedirectStandardOutput = true;
			psi.UseShellExecute = false;
			psi.CreateNoWindow = true;
			
			p.StartInfo = psi;
			p.Start ();
			
			p.StandardInput.Write (Markdown);
			p.StandardInput.Close ();
			
			string html = p.StandardOutput.ReadToEnd ();
			
			p.WaitForExit ();
			p.Close ();
			
			return html;
		}



		public static string MergeToTemplate(string template, Post post, Config config, PageType type)
		{
			string postHtml = File.ReadAllText(template);
			
			// Do the post content first in case the other metadata has been
			// referenced within the post (ie image URLs)
			postHtml = postHtml.Replace ("$post_content$", post.PostHtml);
			
			switch (type)
			{
			case PageType.Post:
				postHtml = postHtml.Replace ("$window_title$", post.Title + " - " + config.BlogTitle);
				break;
			case PageType.Index:
				postHtml = postHtml.Replace ("$window_title$", config.BlogTitle + " - " + config.BlogSubtitle);
				break;
			default:
				break;
			}
			postHtml = postHtml.Replace ("$window_title$", post.Title + "&mdash;" + config.BlogTitle);
			postHtml = postHtml.Replace ("$post_title$", post.Title);
			postHtml = postHtml.Replace ("$post_tags$", post.TagString);
			postHtml = postHtml.Replace ("$post_tags$", post.TagString);
			postHtml = postHtml.Replace ("$post_url$", Util.GetPostUrlPath (post));
			postHtml = postHtml.Replace ("$post_pubdate$", post.Published.ToString ("MMMM dd, yyyy"));
			postHtml = postHtml.Replace ("$author_url$", post.AuthorUrl);
			postHtml = postHtml.Replace ("$post_author$", post.Author);
			postHtml = postHtml.Replace ("$blog_url$", config.BaseUrl);
			postHtml = postHtml.Replace ("$blog_title$", config.BlogTitle);
			postHtml = postHtml.Replace ("$blog_subtitle$", config.BlogSubtitle);
			postHtml = postHtml.Replace ("$blog_copyright$", config.BlogCopyrightHtml);
			
			return postHtml;
		}
		
		public static string MergePageToTemplate(string template, Page page, Config config, PageType type)
		{
			string pageHtml = File.ReadAllText(template);
			
			// Do the page content first in case the other metadata has been
			// referenced within the post (ie image URLs)
			pageHtml = pageHtml.Replace ("$post_content$", page.PostHtml);
			
			switch (type)
			{
			case PageType.Post:
				pageHtml = pageHtml.Replace ("$window_title$", page.Title + " - " + config.BlogTitle);
				break;
			case PageType.Index:
				pageHtml = pageHtml.Replace ("$window_title$", config.BlogTitle + " - " + config.BlogSubtitle);
				break;
			default:
				break;
			}
			pageHtml = pageHtml.Replace ("$window_title$", config.BlogTitle);
			pageHtml = pageHtml.Replace ("$post_title$", page.Title);
			pageHtml = pageHtml.Replace ("$post_tags$", page.TagString);
			pageHtml = pageHtml.Replace ("$post_tags$", page.TagString);
			pageHtml = pageHtml.Replace ("$post_url$", Util.GetPageUrlPath (page));
			pageHtml = pageHtml.Replace ("$post_pubdate$", page.Published.ToString ("MMMM dd, yyyy"));
			pageHtml = pageHtml.Replace ("$blog_url$", config.BaseUrl);
			pageHtml = pageHtml.Replace ("$blog_title$", config.BlogTitle);
			pageHtml = pageHtml.Replace ("$blog_subtitle$", config.BlogSubtitle);
			pageHtml = pageHtml.Replace ("$blog_copyright$", config.BlogCopyrightHtml);
			
			
			return pageHtml;
		}
		
		public static string InsertIndexFooter(Config config, string templateFile, int currentPage, int totalPages)
		{
			StringBuilder sb = new StringBuilder();
			
			if (currentPage > 1)
			{
				sb.Append("<span class=\"idx-footer-left\"><a href=\"");
				sb.Append (config.BaseUrl);
				sb.Append ("/page");
				sb.Append ((currentPage - 1) < 10 ? "0" : "");
				sb.Append ((currentPage - 1).ToString ());
				sb.Append (".html\">&lt;&lt; Previous Page</a></span>");
			}
			if (currentPage < totalPages)
			{
				sb.Append("<span class=\"idx-footer-right\"><a href=\"");
				sb.Append (config.BaseUrl);
				sb.Append ("/page");
				sb.Append ((currentPage + 1) < 10 ? "0" : "");
				sb.Append ((currentPage + 1).ToString ());
				sb.Append (".html\">Next Page &gt;&gt;</a></span<");
			}
			
			string templateHtml = File.ReadAllText(templateFile);
			templateHtml = templateHtml.Replace ("$index_nav$", sb.ToString ());
			return templateHtml;
		}
		
		public static string InsertPagesBar(Config config, string html, List<BlogPage> pages)
		{
			return InsertPagesBar (config, html, pages, "");
		}
		
		public static string InsertPagesBar(Config config, string html, List<BlogPage> pages, string currentPageSlug)
		{
			StringBuilder htmlBar = new StringBuilder();
			
			htmlBar.Append ("<li ");
			if (currentPageSlug == "~")
			{
				htmlBar.Append("class=\"selected\"");
			}
			htmlBar.Append("><a href=\"");
			htmlBar.Append(config.BaseUrl);
			htmlBar.Append("/\">Home</a></li>");
			htmlBar.Append(System.Environment.NewLine);
			
			foreach (BlogPage page in pages)
			{
				htmlBar.Append("<li ");
				
				if (page.Slug == currentPageSlug)
				{
					htmlBar.Append("class=\"selected\"");
				}
				
				htmlBar.Append("><a href=\"");
				htmlBar.Append(config.BaseUrl);
				htmlBar.Append("/page/" + page.Slug);
				htmlBar.Append(".html\">");
				htmlBar.Append(page.Title);
				htmlBar.Append("</a></li>");
				htmlBar.Append(System.Environment.NewLine);
			}
			
			return html.Replace ("$page_list$", htmlBar.ToString ());
		}


	}
}

