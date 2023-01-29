using RssFeed.Clients.Interfaces;
using RssFeed.DTOs.Responses;
using RssFeedAPI.DataAccessLayer.Repositories.RepositoryInterfaces;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;

namespace RssFeed.Clients.Implementations
{
    public class RssFeedClient : IRssFeedClient
    {
        private readonly INewsRepository _newsRepository;
public RssFeedClient(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task<List<Publication>> GetFeedNewsAsync(Uri feedLink, DateTimeOffset date)
        {
            using var reader = XmlReader.Create(feedLink.ToString());

            // I don't know why this library doesn't use loading asynchronously
            var feedNews = SyndicationFeed.Load(reader);

            var alreadyReadNews = await _newsRepository.GetAllReadPublicationsAsync();
            HashSet<Uri> linksOfReadNews = alreadyReadNews.Select(arn => arn.Link).ToHashSet();
            
            List<Publication> publications = new List<Publication>();

            foreach (var item in feedNews.Items) 
            {
                if (item.PublishDate >= date)
                {
                    Uri linkOfPost = item.Links.FirstOrDefault()?.Uri;

                    if (!linksOfReadNews.Contains(linkOfPost)) 
                    {
                        publications.Add(new Publication
                        {
                            Title = item.Title.Text,
                            Link = linkOfPost,
                            Category = item.Categories.FirstOrDefault()?.Name ?? string.Empty,
                            PublicationDate = item.PublishDate,
                            Description = item.Summary.Text,
                            Author = item.Authors.FirstOrDefault()?.Name ?? string.Empty

                        });
                    }
                }
           
            }
          
            return publications;
        }
    }
}
