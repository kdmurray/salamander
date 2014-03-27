using System;

using NUnit.Framework;

using Salamander.Core;

namespace Salamander.Tests
{
	[TestFixture]
	public class DomainObjectTests
	{
		#region Tests for Salamander.Core.Post

		[Test]
		public void Post_ValidateObject_GoodData()
		{
			Post p = new Post();
			p.Author = "kdmurray";
			p.Body = "<b>Title</b> <br/> This is some text.";
			p.Categories.Add (new Category("Cat1", "cat1"));
			p.Published = DateTime.Now;
			p.Slug = "this-is-the-title";
			p.Tags.Add (new Tag("Tag1", "tag1"));
			p.Title = "This is the Title";
			p.Updated = DateTime.Now;

			bool result = p.IsValid();
			bool expected = true;

			Assert.AreEqual (expected, result);
		}

		[Test]
		public void Post_ValidateObject_DefaultInitialization()
		{
			Post p = new Post();

			bool result = p.IsValid();
			bool expected = false;
			
			Assert.AreEqual (expected, result);
		}

		#endregion

		#region Tests for Salamander.Core.Page
		
		[Test]
		public void Page_ValidateObject_GoodData()
		{
			Page p = new Page();
			p.Author = "kdmurray";
			p.BodyMarkdown = "**Title** <br/> This is some text.";
			p.BodyHtml = "<b>Title</b> <br/> This is some text.";
			p.Published = DateTime.Now;
			p.Slug = "this-is-the-title";
			p.Title = "This is the Title";
			p.Updated = DateTime.Now;
			
			bool result = p.IsValid();
			bool expected = true;
			
			Assert.AreEqual (expected, result);
		}
		
		[Test]
		public void Page_ValidateObject_DefaultInitialization()
		{
			Page p = new Page();
			
			bool result = p.IsValid();
			bool expected = false;
			
			Assert.AreEqual (expected, result);
		}
		
		#endregion

		#region Tests for Salamander.Core.SiteInfo
		
		[Test]
		public void SiteInfo_ValidateObject_GoodData()
		{
			SiteInfo si = new SiteInfo();
			si.Author = "kdmurray";
			si.Title = "This is the Title";

			bool result = si.IsValid();
			bool expected = true;
			
			Assert.AreEqual (expected, result);
		}
		
		[Test]
		public void SiteInfo_ValidateObject_DefaultInitialization()
		{
			SiteInfo si = new SiteInfo();

			bool result = si.IsValid();
			bool expected = false;
			
			Assert.AreEqual (expected, result);
		}
		
		#endregion

	}
}

