﻿using System;
using System.Collections.Generic;

namespace GuldtandMVC.Models
{
    public partial class Category
    {
        public Category()
        {
            ProductCategory = new HashSet<ProductCategory>();
            RecipeCategory = new HashSet<RecipeCategory>();
        }

        public string CategoryName { get; set; }

        public virtual ICollection<ProductCategory> ProductCategory { get; set; }
        public virtual ICollection<RecipeCategory> RecipeCategory { get; set; }
    }
}
