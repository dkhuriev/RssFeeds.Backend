using System;
using System.Linq;
using CodeHollow.FeedReader;
using RssFeeds.Model.Infrastructure;

namespace RssFeeds.Model.Domain
{
	public class RssFeedManager
	{
		public RssFeedManager(RssFeedsRepository feedsRepository)
		{
			_feedsRepository = feedsRepository ?? throw new ArgumentNullException(nameof(feedsRepository));
		}

		public RssFeed GetRssFeed(string link)
		{
			if (string.IsNullOrEmpty(link)) 
				throw new ArgumentException("Value cannot be null or empty.", nameof(link));

			if (_feedsRepository.TryGetByLink(link, out var existingFeed))
				return existingFeed;
			var rssFeed = FeedReader.Read(link);

			var newRssFeedItems = rssFeed.Items.Select(item =>
				new RssFeedItem(item.Title, item.Link, item.Description, item.PublishingDate));

			var newRssFeed = new RssFeed(newRssFeedItems, link);

			_feedsRepository.SaveRssFeed(newRssFeed);

			return newRssFeed;
		}

		private readonly RssFeedsRepository _feedsRepository;
	}
}