﻿using System;
using System.Linq;
using System.Threading.Tasks;
using GuldtandMVC_Identity.Data.Queries;
using GuldtandMVC_Identity.Data.Repositories;
using GuldtandMVC_Identity.Functions;

namespace GuldtandMVC_Identity.Models
{
    public class AddHtmlToRecipe : IAddHtmlToRecipe
    {
        public async Task<string> ShowRecipeFullView(string words, double antal, Prj4databaseContext context)
        {
            string initString = "" +
                                "<html>";
            string endString = "</html>";

            string bodystring = "";

            antal /= 4;

            RecipeQuery query = new RecipeQuery
            {
                LoadIngredientList = true,
                LoadRecipeCategory = true,
                SearchRecipe = words,
                NumberOfRecipes = 1,

            };

            RecipeRepository recipeRepository = new RecipeRepository(context);

            var result = await recipeRepository.Get(query);

            foreach (var recipe in result)
            {
                bodystring = "";
                string ingrediensstring = "";
                string directionsstring = "";
                bodystring += "<h1>" + recipe.Name + "</h1>" +
                "<div class='recipe'>" +
                    "<div class='ingredienser'>" +
                    "<p class='p2'><span class='s1'>" + recipe.CookTime + " min tilberednings tid" +
                "<div class='image'>" +
                    "<img src = '" + recipe.ImgSrc + "' height='400' width='700'/>" +
                "</div>" +
                    "<br style='clear: both' />" +
                    "<h3 class='p3'><strong>Fremgangsmåde</strong></h3>" +
                "<div class='i1'>" + "<ul>";

                foreach (var direction in recipe.Directions)
                {
                    directionsstring += "<li class='p5'>" + direction.Description + "</li>";
                }

                bodystring += directionsstring;
                bodystring += "</ul>" +
                              "<h3><strong>Ingredienser</strong></h3>" +
                              "<ul>";
                foreach (var ingredient in recipe.IngredientList.Ingredient)
                {
                    ingrediensstring += "<li class='p6'>" + (ingredient.Amount * antal) + ingredient.AmountUnit + " " + ingredient.Name +
                                        "</li>";
                }

                bodystring += ingrediensstring;
                bodystring += "</ul>" +
                              "</div>" +
                              "<br style='clear:both' />" +
                              "<ul>" +
                              "</div>";
            }
            return initString + bodystring + endString;
        }

        public async Task<string> GenerateShoppingCart(string words, string stores, Prj4databaseContext context)
        {
            string initString = "" +
                    "<html>";
            string endString = "</html>";

            string bodystring = "";

            string[] storeSplit = new string[8];

            if (stores != null)
            {
                storeSplit = stores.Split(';', StringSplitOptions.RemoveEmptyEntries);
            }

            RecipeQuery query = new RecipeQuery
            {
                LoadIngredientList = true,
                LoadRecipeCategory = true,
                SearchRecipe = words,
                NumberOfRecipes = 1,
                Stores = storeSplit

            };

            RecipeRepository recipeRepository = new RecipeRepository(context);

            var result = await recipeRepository.Get(query);

            foreach (var recipe in result)
            {
                bodystring = "";
                string ingrediensstring = "";
                bodystring += "<h3><strong>Indkøbsliste</strong></h3>";

                bodystring += "<ul>";
                foreach (var ingredient in recipe.IngredientList.Ingredient)
                {
                    ingrediensstring += "<li class='p6'>" + ingredient.Name + " - " + " Købes i " + ingredient.Product.RetailChain.Name + " for " +
                                        ingredient.Product.Price + " kr. " + "</li>";
                }

                bodystring += ingrediensstring;
                bodystring += "</ul>" +
                              "</div>" +
                              "<br style='clear:both' />" +
                              "<ul>" +
                              "</div>";
            }
            return initString + bodystring + endString;
        }

        public async Task<string> ShowRecipeSmallViewAsync(string stores, int count, Prj4databaseContext context)
        {

            string initString = "" + "<html>";
            string style = "<head>" +
                           "<style>" +

                           ".viewOfRecipe{" +
                           "width: 60%;" +
                           "height: 200px;" +
                           "border: 2px solid;" +
                           "padding: 2px;" +
                           "margin: 20px;}" +

                           ".img1{" +
                           "display: block;" +
                           "position: absolute;" +
                           "width: 350px;" +
                           "height: 200px;}" +

                           ".textForPrice{" +
                           "display: block;" +
                           "position: relative;" +
                           "float: left;" +
                           "margin-left: 355px;" +
                           "font-size: 20px;}" +

                           "</style>" +
                           "</head> ";
            string endString = "</html>";

            string bodystring = "";

            HtmlCalculator calculator = new HtmlCalculator();

            string[] storeSplit = new string[8];
            string[] storeSplitfake = new string[8];

            if (stores != null)
            {
                storeSplit = stores.Split(';', StringSplitOptions.RemoveEmptyEntries);
            }


            RecipeQuery query = new RecipeQuery
            {
                NumberOfRecipes = count,
                LoadIngredientList = true,
            };

            RecipeRepository recipeRepository = new RecipeRepository(context);

            var result = await recipeRepository.Get(query);

            foreach (var recipe in result)
            {
                double originalPrice = 0;
                double salePrice = 0;
                double lowestPrice = 0;
                if (stores == null)
                {
                    if (recipe.Price != null)
                    {
                        originalPrice = (double)recipe.Price;
                    }
                    if (recipe.SavingsAbsolute != null)
                    {
                        salePrice = (double)recipe.SavingsAbsolute;
                        lowestPrice = (double)recipe.SavingsAbsolute;
                    }
                }
                else
                {
                    originalPrice = await calculator.NormalPrice(recipe, recipe.Name, storeSplit, context);
                    salePrice = await calculator.TotalPrice(recipe, recipe.Name, storeSplit, context);
                    lowestPrice = await calculator.TotalPrice(recipe, recipe.Name, storeSplitfake, context);
                }

                bodystring += "<div class='viewOfRecipe'>" +
                              "<div class='imageOfRecipe'>" +
                              "<a href='/#/Recipe/" + recipe.Name.Replace(" ", string.Empty).Replace("æ", string.Empty).Replace("ø", string.Empty).Replace("å", string.Empty) + "'>" +
                              "<img class='img1' src='" + recipe.ImgSrc + "' alt='recpieImg'></a>" +
                              "</div>" +
                              "<div class='textForPrice'>" +
                              "<div style='font-size: 25px;'>" +
                              "<a href='/#/Recipe/" + recipe.Name.Replace(" ", string.Empty).Replace("æ", string.Empty).Replace("ø", string.Empty).Replace("å", string.Empty) + "'>" +
                              recipe.Name +
                              "</a>" +
                              "<br />" +
                              "</div>" +
                              "Original pris: " + originalPrice + "kr." + " <br />" +
                              "Pris med rabat: " + salePrice + "kr." + "<br />" +
                              "Laveste mulige pris: " + lowestPrice + "kr." + "<br />" +
                              "</div>" +
                              "</div>";
            }
            return initString + style + bodystring + endString;
        }

        public async Task<string> ShowRecipeSmallViewSearchAsync(string word, string stores, Prj4databaseContext context)
        {

            string initString = "" + "<html>";
            string style = "<head>" +
                           "<style>" +

                           ".viewOfRecipe{" +
                           "width: 60%;" +
                           "height: 200px;" +
                           "border: 2px solid;" +
                           "padding: 2px;" +
                           "margin: 20px;}" +

                           ".img1{" +
                           "display: block;" +
                           "position: absolute;" +
                           "width: 350px;" +
                           "height: 200px;}" +

                           ".textForPrice{" +
                           "display: block;" +
                           "position: relative;" +
                           "float: left;" +
                           "margin-left: 355px;" +
                           "font-size: 20px;}" +
                           "</style>" +
                           "</head> ";
            string endString = "</html>";

            string bodystring = "";

            HtmlCalculator calculator = new HtmlCalculator();

            string[] storeSplit = new string[8];
            string[] storeSplitfake = new string[8];

            if (stores != null)
            {
                storeSplit = stores.Split(';', StringSplitOptions.RemoveEmptyEntries);
            }

            RecipeQuery query = new RecipeQuery
            {
                SearchRecipe = word,
                NumberOfRecipes = 5,
                LoadIngredientList = true,
            };

            RecipeRepository recipeRepository = new RecipeRepository(context);

            var recipeQuery = await recipeRepository.Get(query);

            if (recipeQuery.Count() == 0)
            {
                return "Ingen opskrift fundet";
            }

            foreach (var recipe in recipeQuery)
            {
                double originalPrice = 0;
                double salePrice = 0;
                double lowestPrice = 0;
                if (stores == null)
                {
                    if (recipe.Price != null)
                    {
                        originalPrice = (double)recipe.Price;
                    }
                    if (recipe.SavingsAbsolute != null)
                    {
                        salePrice = (double)recipe.SavingsAbsolute;
                        lowestPrice = (double)recipe.SavingsAbsolute;
                    }
                }
                else
                {
                    originalPrice = await calculator.NormalPrice(recipe, recipe.Name, storeSplit, context);
                    salePrice = await calculator.TotalPrice(recipe, recipe.Name, storeSplit, context);
                    lowestPrice = await calculator.TotalPrice(recipe, recipe.Name, storeSplitfake, context);
                }


                bodystring += "<div class='viewOfRecipe'>" +
                              "<div class='imageOfRecipe'>" +
                              "<a href='/#/Recipe/" + recipe.Name.Replace(" ", string.Empty).Replace("æ", string.Empty).Replace("ø", string.Empty).Replace("å", string.Empty) + "'>" +
                              "<img class='img1' src='" + recipe.ImgSrc + "' alt='recpieImg'></a>" +
                              "</div>" +
                              "<div class='textForPrice'>" +
                              "<div style='font-size: 25px;'>" +
                              "<a href='/#/Recipe/" + recipe.Name.Replace(" ", string.Empty).Replace("æ", string.Empty).Replace("ø", string.Empty).Replace("å", string.Empty) + "'>" +
                              recipe.Name +
                              "</a>" +
                              "<br />" +
                              "</div>" +
                              "Original pris: " + originalPrice + "kr." + " <br />" +
                              "Pris med rabat: " + salePrice + "kr." + "<br />" +
                              "Laveste mulige pris: " + lowestPrice + "kr." + "<br />" +
                              "</div>" +
                              "</div>";
            }
            return initString + style + bodystring + endString;
        }
    }
}