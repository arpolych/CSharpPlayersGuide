Pack myPack = new(3, 9.5, 11.2);
Console.WriteLine("Packing Inventory Program!");

while (true)
{
    DisplayMenu();

    string userInput = Console.ReadLine() ?? "error";

    InventoryItem? newItem = userInput switch
    {
        "Arrow" => new Arrow(),
        "Bow" => new Bow(),
        "Rope" => new Rope(),
        "Water" => new Water(),
        "Food" => new Food(),
        "Sword" => new Sword(),
        _ => null
    };

    if (myPack.Add(newItem)) { Console.WriteLine($"{newItem!.ToString()} was added to the pack!"); }
    else { Console.WriteLine($"{newItem?.ToString() ?? "Whatever that is..."} was not added to the pack..."); }
}

void DisplayMenu()
{
    Console.WriteLine("\nWhich of these items would you like to add to your pack?");
    Console.WriteLine("1. Arrow");
    Console.WriteLine("2. Bow");
    Console.WriteLine("3. Rope");
    Console.WriteLine("4. Water");
    Console.WriteLine("5. Food");
    Console.WriteLine("6. Sword");
    Console.WriteLine($"Current pack stats:");
    Console.WriteLine($"  => Items: {myPack.CurrentItemCount}/{myPack.MaxItemNumber}");
    Console.WriteLine($"  => Weight: {myPack.CurrentWeight:0.00}/{myPack.MaxWeight:0.00}");
    Console.WriteLine($"  => Volume: {myPack.CurrentVolume:0.00}/{myPack.MaxVolume:0.00}");
    Console.Write("Answer: ");
}

public class Pack
{
    public InventoryItem[] Items { get; }
    public int MaxItemNumber { get; }
    public double MaxWeight { get; }
    public double MaxVolume { get; }
    public int CurrentItemCount { get; private set; } = 0;
    public double CurrentWeight { get; private set; } = 0.0;
    public double CurrentVolume { get; private set; } = 0.0;

    public Pack(int itemNumber, double maxWeight, double maxVolume)
    {
        Items = new InventoryItem[itemNumber];
        MaxItemNumber = itemNumber;
        MaxWeight = maxWeight;
        MaxVolume = maxVolume;
    }

    public bool Add(InventoryItem? item)
    {
        if (item != null
            && CurrentItemCount < MaxItemNumber
            && (CurrentWeight + item.Weight) <= MaxWeight
            && (CurrentVolume + item.Volume) <= MaxVolume)
        {
            Items[CurrentItemCount++] = item;
            CurrentWeight += item.Weight;
            CurrentVolume += item.Volume;

            return true;
        }

        return false;
    }
}

public class InventoryItem
{
    public double Weight { get; }
    public double Volume { get; }

    public InventoryItem(double weight, double volume)
    {
        Weight = weight;
        Volume = volume;
    }
}

public class Arrow : InventoryItem { public Arrow() : base(0.1, 0.05) { } }

public class Bow : InventoryItem { public Bow() : base(1.0, 4.0) { } }

public class Rope : InventoryItem { public Rope() : base(1.0, 1.5) { } }

public class Water : InventoryItem { public Water() : base(2.0, 3.0) { } }

public class Food : InventoryItem { public Food() : base(1.0, 0.5) { } }

public class Sword : InventoryItem { public Sword() : base(5.0, 3.0) { } }
