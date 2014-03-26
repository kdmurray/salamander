using System;

namespace salamander
{
	public class Post
	{

		public string   Title;
		public string   Author;
		public string   AuthorUrl;
		public string   Slug;
		public string[] Tags;
		public string[] Categories;
		public string   Series;
		public string   BodySource;
		public string   BodyRendered;

		public DateTime Published;
		public DateTime Updated;


		public Post ()
		{

		}
	}
}

