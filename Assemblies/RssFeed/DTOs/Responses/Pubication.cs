namespace RssFeed.DTOs.Responses
{
    public class Publication
    {
        public string Title { get; set; }
        public Uri Link { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
        public DateTimeOffset PublicationDate { get; set; }
        public string Description { get; set; }


    }
}
