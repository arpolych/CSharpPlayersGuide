Type soupType = PickSoupType();
Console.WriteLine();

Ingredient soupIngredient = PickSoupIngredient();
Console.WriteLine();

Seasoning soupSeasoning = PickSoupSeasoning();
Console.WriteLine();

var soup = (sType: soupType, sIngredient: soupIngredient, sSeasoning: soupSeasoning);

Console.WriteLine($"Final recipe: {soup.sSeasoning} {soup.sIngredient} {soup.sType}");

Type PickSoupType()
{
    Console.WriteLine("Available soup types:");
    Console.WriteLine(Type.Soup);
    Console.WriteLine(Type.Stew);
    Console.WriteLine(Type.Gumbo);
    Console.Write("Which soup type do you want to try? ");

    switch (Console.ReadLine())
    {
        case "Soup": { Console.WriteLine("Soup? Excellent choice!"); return Type.Soup; }
        case "Stew": { Console.WriteLine("Stew? Excellent choice!"); return Type.Stew; }
        case "Gumbo": { Console.WriteLine("Gumbo? Excellent choice!"); return Type.Gumbo; }
        default: { Console.WriteLine("What? Let's go with the first one..."); return Type.Soup; }
    }
}

Ingredient PickSoupIngredient()
{
    Console.WriteLine("Available soup ingredients:");
    Console.WriteLine(Ingredient.Mushrooms);
    Console.WriteLine(Ingredient.Chicken);
    Console.WriteLine(Ingredient.Carrots);
    Console.WriteLine(Ingredient.Potatoes);
    Console.Write("Which soup ingredient do you want to try? ");

    switch (Console.ReadLine())
    {
        case "Mushrooms": { Console.WriteLine("Mushrooms? Excellent choice!"); return Ingredient.Mushrooms; }
        case "Chicken": { Console.WriteLine("Chicken? Excellent choice!"); return Ingredient.Chicken; }
        case "Carrots": { Console.WriteLine("Carrots? Excellent choice!"); return Ingredient.Carrots; }
        case "Potatoes": { Console.WriteLine("Potatoes? Excellent choice!"); return Ingredient.Potatoes; }
        default: { Console.WriteLine("What? Let's go with the first one..."); return Ingredient.Mushrooms; }
    }
}

Seasoning PickSoupSeasoning()
{
    Console.WriteLine("Available soup seasonings:");
    Console.WriteLine(Seasoning.Spicy);
    Console.WriteLine(Seasoning.Salty);
    Console.WriteLine(Seasoning.Sweet);
    Console.Write("Which soup seasoning do you want to try? ");

    switch (Console.ReadLine())
    {
        case "Spicy": { Console.WriteLine("Spicy? Excellent choice!"); return Seasoning.Spicy; }
        case "Salty": { Console.WriteLine("Salty? Excellent choice!"); return Seasoning.Salty; }
        case "Sweet": { Console.WriteLine("Sweet? Excellent choice!"); return Seasoning.Sweet; }
        default: { Console.WriteLine("What? Let's go with the first one..."); return Seasoning.Spicy; }
    }
}

enum Type { Soup, Stew, Gumbo }

enum Ingredient { Mushrooms, Chicken, Carrots, Potatoes }

enum Seasoning { Spicy, Salty, Sweet }