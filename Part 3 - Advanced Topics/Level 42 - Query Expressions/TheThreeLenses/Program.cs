int[] array = { 1, 9, 2, 8, 3, 7, 4, 6, 5 };

Console.WriteLine("Procedural results...");
foreach (int i in TheProceduralWay(array))
    Console.Write($"{i} ");

Console.WriteLine("\n\nKeyword-based query results...");
foreach (int elem in KeywordQuery(array))
    Console.Write($"{elem} ");

Console.WriteLine("\n\nMethod-call-based query results...");
foreach (int elem in MethodQuery(array))
    Console.Write($"{elem} ");

static IEnumerable<int> TheProceduralWay(int[] array)
{
    List<int> collection = new();

    foreach (int element in array)
        if (element % 2 == 0) collection.Add(element);

    int[] backToArray = collection.ToArray();

    Array.Sort(backToArray);

    for (int i = 0; i < backToArray.Length; i++)
        backToArray[i] *= 2;

    return backToArray;
}

static IEnumerable<int> KeywordQuery(int[] array)
    => from elem in array
       where elem % 2 == 0
       orderby elem ascending
       select (elem * 2);

static IEnumerable<int> MethodQuery(int[] array)
    => array.Where(elem => elem % 2 == 0)
            .OrderBy(elem => elem)
            .Select(elem => elem * 2);

/*
 * Answer to question 1:
 * 
 * Honestly, the first one is bad, long, and messy. You could even say
 * it is not the right tool for the job. Whereas, the second and third 
 * method solutions are much more suited for this kind of problem.
 * 
 * Answer to question 2:
 * 
 * My personal favorite would be number 2 with the keyword based query, 
 * simple and right to the point.
 */