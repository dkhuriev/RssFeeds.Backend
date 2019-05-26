using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Newtonsoft.Json;
using RssFeeds.Model.Domain;

namespace RssFeeds.Model.Infrastructure
{
	public class RssFeedsRepository
	{
		public RssFeedsRepository(string connectionString)
		{
			_connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
		}

		public bool TryGetByLink(string link, out RssFeed rssFeed)
		{
			using (IDbConnection connection = new SqlConnection(_connectionString))
			{
				var rssFeedDbEntity = connection.Query<RssFeedDbEntity>(
						"SELECT * FROM rssFeed Where Link = @link",
						new {link})
					.FirstOrDefault();

				if (rssFeedDbEntity == null)
					rssFeed = null;
				else
					rssFeed = JsonConvert.DeserializeObject<RssFeed>(rssFeedDbEntity.SerializedFeed);

				return rssFeedDbEntity != null;
			}
		}

		public void SaveRssFeed(RssFeed rssFeed)
		{
			using (IDbConnection connection = new SqlConnection(_connectionString))
			{
				var serializedFeed = JsonConvert.SerializeObject(rssFeed);
				var query =
					"INSERT INTO rssFeed (Link, SerializedFeed) VALUES(@Link, @SerializedFeed)";

				connection.Execute(query,
					new
					{
						Link = rssFeed.Link,
						SerializedFeed = serializedFeed
					});
			}
		}

		private readonly string _connectionString;
	}
}