﻿using GuldtandMVC_Identity.Data.Queries;
using GuldtandMVC_Identity.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using GuldtandMVC_Identity;
using GuldtandMVC_Identity.Controllers;
using GuldtandMVC_Identity.Models;
using Microsoft.EntityFrameworkCore;

namespace Til_test
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            using (var db = new prj4databaseContext())
            {
                RecipeController ctrl = new RecipeController();
                var result = await ctrl.viewForSmallRecipeSearch("lasagne", "bilka;aldi;netto");

                //var Stores = new string[] {"Netto", "rema", "bilka", "aldi"};
                //HTMLCalculator calculator = new HTMLCalculator();
                //for (int i = 0; i < 20; i++)
                //{

                //    var result = await calculator.NormalPrice("lasagne", Stores);
                //}


                //RecipeQuery recipeQuery = new RecipeQuery
                //{

                //            SearchRecipe = "lasagne",
                //            LoadIngredientList = true,
                //            Stores = new string[] {"Netto", "rema", "bilka", "aldi"}
                //        };
                //        var recipes = await recipeQuery.Execute(db);
                //        foreach (var recipe in recipes)
                //        {
                //            Console.WriteLine($"{recipe.Name}");
                //            foreach (var ingredient in recipe.IngredientList.Ingredient)
                //            {
                //                Console.WriteLine($"{ingredient.Name} - {ingredient.Product.Name} - {ingredient.Product.Price} - {ingredient.Product.RetailChain.Name}");
                //            }
                //        }


                //ProductQuery query = new ProductQuery
                //{
                //    LoadRetailChain = true
                //};
                //query.ValidToDate = "2050";
                //var result = await query.Execute(db);
                //foreach (var VARIABLE in result)
                //{
                //    //Console.WriteLine($"{VARIABLE.ValidTo.ToString()}");
                //    Console.WriteLine($"{VARIABLE.RetailChain.Name}");
                //    Console.WriteLine($"{VARIABLE.Name}");
                //    Console.WriteLine($"{VARIABLE.Price + "kr"}");
                //}


                //RecipeQuery query = new RecipeQuery
                //{
                //    LoadIngredientList = true,
                //    SearchIngredient = "feta"
                //};
                //RecipeRepository repo = new RecipeRepository(db);

                //var data = await repo.GetRecipes(query);

                //foreach (var VARIABLE in data)
                //{
                //    Console.WriteLine($"{VARIABLE.Name} - {VARIABLE.Price}");
                //}
                //Recipe recipe = new Recipe
                //{
                //    Name = "One Pot Pasta med chorizo",
                //    Price = 85,
                //    Servings = 4,
                //    SavingsAbsolute = 30,
                //    CookTime = 25,
                //    ImgSrc = "https://www.valdemarsro.dk/wp-content/2017/03/one-pot-pasta-1.jpg"

                //};
                //db.Recipe.Add(recipe);
                //db.SaveChanges();

                //var directions = new Directions[]
                //{
                //    new Directions{Description = "Skær ting", RecipeId = recipe.RecipeId},
                //    new Directions{Description = "Put det i en gryde", RecipeId = recipe.RecipeId}
                //};

                //foreach (var dir in directions)
                //{
                //    db.Directions.Add(dir);
                //}

                //db.SaveChanges();

                //Category category = new Category { CategoryName = "Dansk klassisk" };
                //if (!db.Category.Contains(category))
                //{
                //    db.Category.Add(category);
                //    db.SaveChanges();
                //}

                ////RecipeCategory recipeCategory = new RecipeCategory
                ////{
                ////    RecipeId = recipe.RecipeId,
                ////    CategoryName = "Pastaret"
                ////};
                ////db.RecipeCategory.Add(recipeCategory);
                ////db.SaveChanges();

                //IngredientList ingList = new IngredientList
                //{
                //    RecipeId = recipe.RecipeId
                //};
                //db.IngredientList.Add(ingList);
                //db.SaveChanges();
                //var produkt = await db.Set<Product>().Where(p => p.Name.Contains("a")).Take(1).ToListAsync();
                //var ingredients = new Ingredient[]
                //{
                //    new Ingredient{Amount = 400, AmountUnit = "g", Name = "Spaghetti", ProductId = db.Product.Where(p => p.Name.Contains("pasta")).Take(1).ToList()[0].ProductId, IngredientListId = ingList.IngredientListId},
                //    new Ingredient{Amount = 1, AmountUnit = "dåse", Name = "Hakkede tomater", ProductId = db.Product.Where(p => p.Name.Contains("tomat")).Take(1).ToList()[0].ProductId, IngredientListId = ingList.IngredientListId},
                //    new Ingredient{Amount = 1, AmountUnit = " ", Name = "Løg", ProductId = db.Product.Where(p => p.Name.Contains("løg")).Take(1).ToList()[0].ProductId, IngredientListId = ingList.IngredientListId}
                //};
                //foreach (Ingredient i in ingredients)
                //{
                //    db.Ingredient.Add(i);
                //}
                //db.SaveChanges();
            }

    //using (var context = new prj4databaseContext())
    //{
    //    insert insert = new insert(context);
    //    await insert.insertdataAsync();
    //    RecipeRepository repo = new RecipeRepository(context);
    //    RecipeQuery query = new RecipeQuery
    //    {
    //        LoadIngredientList = true,
    //        LoadRecipeCategory = true,
    //        SearchRecipe = ""
    //    };
    //    var opskrifter = await repo.GetRecipes(query);

    //    foreach (var opskrift in opskrifter.Where(o => o.Name.Contains("marcus")))
    //    {
    //        System.Console.WriteLine($"{opskrift.Name}");
    //        Console.WriteLine($"{opskrift.Price}");
    //    }
    //}
}
    }
}
