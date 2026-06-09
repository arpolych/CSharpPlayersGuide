Arrowhead customArrowHead = PickArrowHead();
Console.WriteLine();

float customArrowLength = PickArrowLength();
Console.WriteLine();

Fletching customArrowFletching = PickFletching();
Console.WriteLine();

Arrow customArrow = new(customArrowHead, customArrowFletching, customArrowLength);

Console.WriteLine($"The cost for this custom arrow is: {customArrow.GetCost():0.00} gold.");

Arrowhead PickArrowHead()
{
    Console.WriteLine("These are the available arrowheads:");
    Console.WriteLine($"{Arrowhead.Steel} (10 gold)");
    Console.WriteLine($"{Arrowhead.Wood} (3 gold)");
    Console.WriteLine($"{Arrowhead.Obsidian} (5 gold)");
    Console.Write("Which arrowhead do you want to use? ");

    switch (Console.ReadLine())
    {
        case "Steel": { Console.WriteLine("Steel? Excellent choice!"); return Arrowhead.Steel; }
        case "Wood": { Console.WriteLine("Wood? Excellent choice!"); return Arrowhead.Wood; }
        case "Obsidian": { Console.WriteLine("Obsidian? Excellent choice!"); return Arrowhead.Obsidian; }
        default: { Console.WriteLine("What?? Let's just go with the cheapest..."); return Arrowhead.Wood; }
    }
}

float PickArrowLength()
{
    float length;

    do
    {
        Console.Write("What arrow length do you want (60 cm - 100 cm)? ");

        length = float.Parse(Console.ReadLine());
    } while (length < 60 || length > 100);

    return length;
}

Fletching PickFletching()
{
    Console.WriteLine("These are the available fletching types:");
    Console.WriteLine($"{Fletching.Plastic} (10 gold)");
    Console.WriteLine($"{Fletching.TurkeyFeathers} (5 gold)");
    Console.WriteLine($"{Fletching.GooseFeathers} (3 gold)");
    Console.Write("Which fletching type do you want to use? ");

    switch (Console.ReadLine())
    {
        case "Plastic": { Console.WriteLine("Plastic? Excellent choice!"); return Fletching.Plastic; }
        case "TurkeyFeathers": { Console.WriteLine("Turkey Feathers? Excellent choice!"); return Fletching.TurkeyFeathers; }
        case "GooseFeathers": { Console.WriteLine("Goose Feathers? Excellent choice!"); return Fletching.GooseFeathers; }
        default: { Console.WriteLine("What?? Let's just go with the cheapest..."); return Fletching.GooseFeathers; }
    }
}

class Arrow
{
    public Arrowhead _arrowHead;
    public Fletching _fletchingType;
    public float _length;

    public Arrow(Arrowhead arrowHead, Fletching fletchingType, float length)
    {
        _arrowHead = arrowHead;
        _fletchingType = fletchingType;
        _length = length;
    }

    public float GetCost()
    {
        float cost = _arrowHead switch
        {
            Arrowhead.Steel => 10,
            Arrowhead.Wood => 3,
            Arrowhead.Obsidian => 5,
        };

        cost += _fletchingType switch
        {
            Fletching.Plastic => 10,
            Fletching.TurkeyFeathers => 5,
            Fletching.GooseFeathers => 3
        };

        cost += (_length * 0.05F);

        return cost;
    }
}

enum Arrowhead { Steel, Wood, Obsidian }

enum Fletching { Plastic, TurkeyFeathers, GooseFeathers }