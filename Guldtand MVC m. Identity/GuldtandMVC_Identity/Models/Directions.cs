﻿
namespace GuldtandMVC_Identity
{
    public partial class Directions
    {
        public int DirectionsId { get; set; }
        public string Description { get; set; }
        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
