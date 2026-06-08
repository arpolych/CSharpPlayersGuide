string userInput;

Console.WriteLine("What's the traingle's base size?");
userInput = Console.ReadLine();
double triangleBase = Convert.ToDouble(userInput);

Console.WriteLine("What's the traingle's height size?");
userInput = Console.ReadLine();
double triangleHeight = Convert.ToDouble(userInput);

double triangleArea = (triangleBase * triangleHeight) / 2.0;

Console.WriteLine("The area of the triangle is: " + triangleArea);