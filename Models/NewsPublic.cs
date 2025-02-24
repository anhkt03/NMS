namespace NMS.Models
{
    public class NewsPublic
    {
        public int NewsArticleId { get; set; }

        public string? NewsTitle { get; set; }

        public string? Headline { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? NewsContent { get; set; }

        public string? NewsSource { get; set; }

        public string? CategoryName { get; set; }

        public string? Author {  get; set; }

        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();


    }
}
