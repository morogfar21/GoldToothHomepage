﻿using System;
using System.Collections.Generic;

namespace GuldtandMVC.Models
{
    public partial class Ingredient
    {
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public double? Amount { get; set; }
        public string AmountUnit { get; set; }
        public int? ProductId { get; set; }
        public int? IngredientListId { get; set; }

        public virtual IngredientList IngredientList { get; set; }
        public virtual Product Product { get; set; }
    }
}
