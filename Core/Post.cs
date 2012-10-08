using System;
using System.Collections.Generic;

namespace Salamander.Core
{
	public class Post
	{
		public string Title { get; set; }
		public string Author { get; set; }
		public string Body { get; set; }
		public string Slug { get; set; }	
		public DateTime Published { get; set; }
		public DateTime Updated { get; set; }
		public List<Tag> Tags { get; set; }
		public List<Category> Categories { get; set; }

		public Post ()
		{
			Initialize ();
		}

		private void Initialize ()
		{
			Tags = new List<Tag>();
			Categories = new List<Category>();

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

			if (Body == "" || null == Body) {
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

			if (Categories.Count == 0 || null == Categories) {
				isValid = false;
			}

			if (Tags.Count == 0 || null == Tags) {
				isValid = false;
			}

			return isValid;
		}
	}
}

