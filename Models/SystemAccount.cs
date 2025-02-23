using System;
using System.Collections.Generic;

namespace NMS.Models;

public partial class SystemAccount
{
    public int AccountId { get; set; }

    public string? AccountName { get; set; }

    public string? AccountEmail { get; set; }

    public int? AccountRole { get; set; }

    public string? AccountPassword { get; set; }

    public virtual ICollection<NewsArticle> NewsArticleCreatedBies { get; set; } = new List<NewsArticle>();

    public virtual ICollection<NewsArticle> NewsArticleUpdateBies { get; set; } = new List<NewsArticle>();
}
