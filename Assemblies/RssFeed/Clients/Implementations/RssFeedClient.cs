using RssFeed.Clients.Interfaces;
using RssFeed.DTOs.Responses;
using System.ServiceModel.Syndication;
using System.Xml;

namespace RssFeed.Clients.Implementations
{
    public class RssFeedClient : IRssFeedClient
    {
        public Task<List<Publication>> GetFeedNewsAsync(Uri link)
        {
            using var reader = XmlReader.Create(link.ToString());

            // I don't know why this library doesn't use loading asynchronously
            var feedNews = SyndicationFeed.Load(reader);

            
            List<Publication> publications= new List<Publication>();

            foreach (var item in feedNews.Items) 
            {
                
                publications.Add(new Publication
                {
                    Title = item.Title.Text,
                    Link = item.Links.FirstOrDefault().Uri,
                    Category = item.Categories.FirstOrDefault()?.Name ?? string.Empty,
                    PublicationDate = item.PublishDate,
                    Description = item.Summary.Text,
                    Author = item.Authors.FirstOrDefault()?.Name ?? string.Empty

                });
            }
          
            return Task.FromResult(publications);
        }
    }
}
