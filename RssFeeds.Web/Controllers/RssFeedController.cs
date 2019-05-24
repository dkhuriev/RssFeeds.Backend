using System;
using Microsoft.AspNetCore.Mvc;
using RssFeeds.Model.Domain;

namespace RssFeeds.Web.Controllers
{
	[ApiController]
	public class RssFeedController : ControllerBase
	{
		public RssFeedController(RssFeedManager rssFeedManager)
		{
			_rssFeedManager = rssFeedManager ?? throw new ArgumentNullException(nameof(rssFeedManager));
		}

		[HttpGet]
		[Route("rss")]
		public IActionResult GetRssFeed([FromQuery(Name = "link")] string rssFeedLink)
		{
			var feed = _rssFeedManager.GetRssFeed(rssFeedLink);

			return new JsonResult(feed.Items);
		}

		private readonly RssFeedManager _rssFeedManager;
	}
}