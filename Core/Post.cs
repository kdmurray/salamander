using System;
using System.Collections.Generic;

namespace Salamander.Core
{
	public class Post
	{
		public string Title { get; set; }
		public string Author { get; set; }
		public string BodyMarkdown { get; set; }
		public string BodyHtml { get; set; }
		public string Slug { get; set; }	
		public DateTime Published { get; set; }
		public DateTime Updated { get; set; }
		public string[] Tags { get; set; }
		public string[] Categories { get; set; }
		public string SourceFile { get; set; }
		public string DestFile { get; set; }

		public Post ()
		{
			Initialize ();
		}

		private void Initialize ()
		{
			Updated = Convert.ToDateTime ("01-Jan-1900");
			Published = Convert.ToDateTime ("01-Jan-1900");
		}

		public bool IsValid ()
		{
			bool isValid = true;

			if (Title == "" || null == Title) {
				isValid = false;
			}

			if (Author == "" || null == Author) {
				isValid = false;
			}

			if (BodyMarkdown == "" || null == BodyMarkdown) {
				isValid = false;
			}
			
			if (BodyHtml == "" || null == BodyHtml) {
				isValid = false;
			}
			
			if (Slug == "" || null == Slug) {
				isValid = false;
			}

			if (Updated == Convert.ToDateTime ("01-Jan-1900")) {
				isValid = false;
			}

			if (Published == Convert.ToDateTime ("01-Jan-1900")) {
				isValid = false;
			}

			if (Categories.Length == 0 || null == Categories) {
				isValid = false;
			}

			if (Tags.Length == 0 || null == Tags) {
				isValid = false;
			}

			return isValid;
		}
	}
}

