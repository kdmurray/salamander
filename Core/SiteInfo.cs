using System;

namespace Salamander.Core
{
	public class SiteInfo
	{
		public string Author { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }

		public SiteInfo ()
		{
		}

		public bool IsValid()
		{
			bool isValid = true;

			if (Title == "" || null == Title) {
				isValid = false;
			}
			
			if (Author == "" || null == Author) {
				isValid = false;
			}

			return isValid;
		}
	}
}

