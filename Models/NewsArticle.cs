using System;
using System.Collections.Generic;

namespace NMS.Models;

public partial class NewsArticle
{
    public int NewsArticleId { get; set; }

    public string? NewsTitle { get; set; }

    public string? Headline { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? NewsContent { get; set; }

    public string? NewsSource { get; set; }

    public int? CategoryId { get; set; }

    public bool? NewsStatus { get; set; }

    public int? CreatedById { get; set; }

    public int? UpdateById { get; set; }

    public DateTime? ModifyDate { get; set; }

    public virtual Category? Category { get; set; }

    public virtual SystemAccount? CreatedBy { get; set; }

    public virtual SystemAccount? UpdateBy { get; set; }

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
