using System;

namespace Salamander.Core
{
	public class Tag
	{
		public string Title { get; set; }
		public string Slug { get; set; }

		public Tag (string title, string slug)
		{
			Title = title;
			Slug = slug;
		}
	}
}

