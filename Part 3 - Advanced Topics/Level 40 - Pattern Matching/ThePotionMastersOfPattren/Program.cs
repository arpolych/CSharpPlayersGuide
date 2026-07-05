Potion currentPotion = Potion.Water;

while (true)
{
    DisplayPotionState();

    Ingredient ingredientChoice = GetIngredientChoice("\nEnter your choice: ");

    currentPotion = AddIngredientToPotion(currentPotion, ingredientChoice);

    if (currentPotion == Potion.Ruined)
    {
        Console.WriteLine("\nThe potion was ruined!");
        Console.WriteLine("You'll have to start again with water...");
        currentPotion = Potion.Water;
        continue;
    }
    else
    {
        Console.WriteLine($"\nThe resulted potion is: {currentPotion}.");
        {
            if (WannaContinue("Would you like to add another ingredient? ")) continue;
            else
            {
                Console.WriteLine("Okay, we're ending this...");
                return;
            }
        }
    }
}

bool WannaContinue(string message)
{
    Console.Write(message);
    return Console.ReadLine() switch
    {
        "yes" => true,
        "no" => false,
        _ => WannaContinue($"Unrecognized input...\n\nWould you like to add another ingredient? ")
    };
}
Ingredient GetIngredientChoice(string message)
{
    Console.Write($"{message}");

    // Pattern used: constant pattern.
    return Console.ReadLine() switch
    {
        "Stardust" => Ingredient.Stardust,
        "Snake Venom" => Ingredient.SnakeVenom,
        "Dragon Breath" => Ingredient.DragonBreath,
        "Shadow Glass" => Ingredient.ShadowGlass,
        "Eyeshine Gem" => Ingredient.EyeshineGem,
        _ => GetIngredientChoice($"Unrecognized choice, try again...\n\nEnter your choice: ")
    };
}

Potion AddIngredientToPotion(Potion potion, Ingredient ingredient)
    // Pattern used: positional pattern.
    => (potion, ingredient) switch
    {
        (Potion.Water, Ingredient.Stardust) => Potion.Elixir,
        (Potion.Elixir, Ingredient.SnakeVenom) => Potion.Poison,
        (Potion.Elixir, Ingredient.DragonBreath) => Potion.Flying,
        (Potion.Elixir, Ingredient.ShadowGlass) => Potion.Invisibility,
        (Potion.Elixir, Ingredient.EyeshineGem) => Potion.NightSight,
        (Potion.NightSight, Ingredient.ShadowGlass) => Potion.CloudyBrew,
        (Potion.Invisibility, Ingredient.EyeshineGem) => Potion.CloudyBrew,
        (Potion.CloudyBrew, Ingredient.Stardust) => Potion.Wraith,
        (_, _) => Potion.Ruined
    };

void DisplayPotionState()
{
    Console.WriteLine($"You currently have a {currentPotion} potion.");
    Console.WriteLine("Available ingredients choices to add:");
    Console.WriteLine($" - Stardust\n - Snake Venom\n - Dragon Breath\n - Shadow Glass\n - Eyeshine Gem");
}

public enum Potion { Water, Elixir, Poison, Flying, Invisibility, NightSight, CloudyBrew, Wraith, Ruined }

public enum Ingredient { Stardust, SnakeVenom, DragonBreath, ShadowGlass, EyeshineGem }
