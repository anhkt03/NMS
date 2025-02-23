using System;
using System.Collections.Generic;

namespace NMS.Models;

public partial class Tag
{
    public int TagId { get; set; }

    public string? TagName { get; set; }

    public string? Note { get; set; }

    public virtual ICollection<NewsArticle> NewArticles { get; set; } = new List<NewsArticle>();
}
