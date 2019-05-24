using System;
using System.Collections.Generic;

namespace RssFeeds.Model.Domain
{
	public class RssFeed
	{
		public RssFeed(IEnumerable<RssFeedItem> items, string link)
		{
			Link = link ?? throw new ArgumentNullException(nameof(link));
			Items = items ?? throw new ArgumentNullException(nameof(items));
		}

		public string Link { get; }

		public IEnumerable<RssFeedItem> Items { get; }
	}
}