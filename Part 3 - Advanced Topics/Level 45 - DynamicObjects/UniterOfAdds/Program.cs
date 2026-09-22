dynamic a, b;

a = 5; b = 2;
Console.WriteLine($"ints addition: {a} + {b} = {Add(a, b)}.");

a = 5.5; b = 2.2;
Console.WriteLine($"doubles addition: {a} + {b} = {Add(a, b)}.");

a = "abc"; b = "zxc";
Console.WriteLine($"strings addition: {a} + {b} = {Add(a, b)}.");

a = DateTime.Now; b = new TimeSpan(2, 30, 0);
Console.WriteLine($"DateTime & TimeSpan addition: {a} + {b} = {Add(a, b)}.");

// The single Add method that does the job, hahaha
dynamic Add(dynamic a, dynamic b) => a + b;

// The four great adds we want to replace:
public static class Adds
{
    public static int Add(int a, int b) => a + b;
    public static double Add(double a, double b) => a + b;
    public static string Add(string a, string b) => a + b;
    public static DateTime Add(DateTime a, TimeSpan b) => a + b;
}

/*
 * The main downside I see when using dynamic here is how 
 * easy it would be for a mistake to happen during runtime 
 * and crash the program. That is, an addition of types
 * that can't be added together.
 * 
 * ++ Plus, seen the author's answer, I realize that the 
 * compiler won't be able to tell us prior to execution 
 * what might go wrong (like it does on static variables).
 */