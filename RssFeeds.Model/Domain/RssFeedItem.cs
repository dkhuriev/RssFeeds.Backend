using System;

namespace RssFeeds.Model.Domain
{
	public class RssFeedItem
	{
		public RssFeedItem(string title, string link, string description, DateTime? publicationDate)
		{
			Title = title;
			Link = link;
			Description = description;
			PublicationDate = publicationDate;
		}

		public string Title { get; }

		public string Link { get; }

		public string Description { get; }

		public DateTime? PublicationDate { get; }
	}
}