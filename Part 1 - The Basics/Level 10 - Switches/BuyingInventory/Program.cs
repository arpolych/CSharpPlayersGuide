Console.WriteLine
(@"The following items are available:
1 - Rope
2 - Torches
3 - Climbing Equipment
4 - Clean Water
5 - Machete
6 - Canoe
7 - Food Supplies");
Console.Write("What number do you want to see the price of? ");

string userInput = Console.ReadLine();
int choice = int.Parse(userInput);

switch (choice)
{
    case 1: { Console.WriteLine("Ropes cost 10 gold."); break; }
    case 2: { Console.WriteLine("Torches cost 15 gold."); break; }
    case 3: { Console.WriteLine("Climbing Equipment cost 25 gold."); break; }
    case 4: { Console.WriteLine("Clean Water cost 1 gold."); break; }
    case 5: { Console.WriteLine("Machetes cost 20 gold."); break; }
    case 6: { Console.WriteLine("Canoes cost 200 gold."); break; }
    case 7: { Console.WriteLine("Food Supplies cost 1 gold."); break; }
    default: { Console.WriteLine("Item was not found!"); break; }
}
