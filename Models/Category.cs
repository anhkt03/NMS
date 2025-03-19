﻿using System;
using System.Collections.Generic;

namespace NMS.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public string? CategoryDescription { get; set; }

    public int ParentCategoryId { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Category> InverseParentCategory { get; set; } = new List<Category>();

    public virtual ICollection<NewsArticle> NewsArticles { get; set; } = new List<NewsArticle>();

    public virtual Category ParentCategory { get; set; } = null!;
}
