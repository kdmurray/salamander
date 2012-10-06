using System;
using System.Collections.Generic;

namespace Salamander.Core
{
	public class Post : Content
	{
		public List<Tag> Tags { get; set; }
		public List<Category> Categories { get; set; }

		public Post ()
		{
		}
	}
}

