using System;

namespace Salamander.Core
{
	public abstract class Content
	{
		public string Title { get; set; }
		public string Author { get; set; }
		public string Body { get; set; }
		public string Slug { get; set; }

		public DateTime Published { get; set; }
		public DateTime Updated { get; set; }

	}
}

