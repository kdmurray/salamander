using System;

namespace Salamander.Core
{
	public class Category
	{
		public string Title { get; set; }
		public string Slug { get; set; }

		public Category (string title, string slug)
		{
			Title = title;
			Slug = slug;
		}
	}
}

