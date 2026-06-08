int[] firstArr = new int[5];

for (int i = 0; i < firstArr.Length; i++)
{
    Console.Write($"Please enter a number for the array index {i}: ");

    firstArr[i] = int.Parse(Console.ReadLine());
}

int[] secondArr = new int[5];

Console.WriteLine("\nStarting the copying process...");

for (int i = 0; i < secondArr.Length; i++)
{
    secondArr[i] = firstArr[i];
}

Console.WriteLine("\nThe results:");

for (int i = 0; i < 5; i++)
{
    Console.WriteLine($"\nArray 1 index {i} value: {firstArr[i]}");
    Console.WriteLine($"Array 2 index {i} value: {secondArr[i]}");
}
