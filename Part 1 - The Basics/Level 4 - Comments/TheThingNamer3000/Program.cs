Console.WriteLine("What kind of thing are we talking about?");

string a = Console.ReadLine(); // The kind of thing the man is talking about...

Console.WriteLine("How would you describe it? Big? Azure? Tattered?");

string b = Console.ReadLine(); // The description of the thing...
string c = "Doom";
string d = "3000";

Console.WriteLine("The " + b + " " + a + " of " + c + " " + d + "!");

/*
 * Answer to question:
 * 
 * I would put good variable names, instead of letters which don't indicate 
 * what the variable actually does during my code review.
 */