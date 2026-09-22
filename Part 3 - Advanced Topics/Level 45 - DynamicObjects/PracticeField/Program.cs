using System.Dynamic;

RunChapterTopic("Dynamic Type Checking", DynamicTypeChecking);
RunChapterTopic("Emulating Dynamic Objects with Dictionaries", EmulatingDynamicObjectsWithDict);
RunChapterTopic("Using ExpandoObject", ExpandoObject);
RunChapterTopic("Extending DynamicObject", ExtendingDynamicObject);

static void RunChapterTopic(string topicName, Action topicFunc)
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine($"\t->> Topic: {topicName}\n");
    Console.ForegroundColor = ConsoleColor.White;

    topicFunc();

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("\n\t\t-<< End\n");
    Console.ForegroundColor = ConsoleColor.White;
}

static void DynamicTypeChecking()
{
    dynamic mystery = "Hello, World!";

    Console.WriteLine(mystery.Length);

    mystery = 2;
    mystery += 13;
    mystery *= 13;

    Console.WriteLine(mystery);
}

static void EmulatingDynamicObjectsWithDict()
{
    // Today that I'm doing this chapter is my actual birthday, haha.
    // What a coincidence!
    Dictionary<string, object> flexible = new();

    flexible["Name"] = "Ariks";
    flexible["Age"] = 25;
    flexible["HappyABirthday"] = new Action(
        () => flexible["Age"] = (int)flexible["Age"] + 1);

    Console.WriteLine($"Yesterday, {flexible["Name"]} was {flexible["Age"]} years old.");

    ((Action)flexible["HappyABirthday"])(); // Invoking the delegate...

    Console.WriteLine($"And with today's birthday, {flexible["Name"]} is {flexible["Age"]} years old.");
    Console.WriteLine("Happy birthday to me ;)");
}

static void ExpandoObject()
{
    // Part A
    dynamic expando = new ExpandoObject();

    expando.Name = "Ariks";
    expando.Age = 26;
    expando.HaveABirthday = new Action(() => expando.Age++);

    Console.WriteLine($"Today, {expando.Name} is {expando.Age} years old.");

    expando.HaveABirthday(); // Invoking the method...

    Console.WriteLine($"And with next year's birthday, {expando.Name} will be {expando.Age} years old.");

    // Part B
    var expandoAsDictionary = (IDictionary<string, object>)expando;

    Console.WriteLine("\nExpando's keys when casted to a dictionary:");

    foreach (string memberName in expandoAsDictionary.Keys)
        Console.WriteLine(memberName);

    expandoAsDictionary.Remove("Age");

    Console.WriteLine("\nExpando dictionary keys when key removed:");

    foreach (string memberName in expandoAsDictionary.Keys)
        Console.WriteLine(memberName);

    // expando.HaveABirthday(); // Will throw exception, since the Age property is gone...
}

static void ExtendingDynamicObject()
{
    dynamic item = new CustomObject(new string[] { "Name", "Age" },
                                    new string[] { "HAL", "9001" });

    Console.WriteLine($"{item.Name} is {item.Age} years old.");
}

public class CustomObject : DynamicObject
{
    private Dictionary<string, string> _data;

    public CustomObject(string[] names, string[] values)
    {
        _data = new Dictionary<string, string>();

        for (int i = 0; i < names.Length; i++)
            _data[names[i]] = values[i];
    }

    public override bool TryGetMember(GetMemberBinder binder, out object? result)
    {
        if (_data.ContainsKey(binder.Name))
        {
            result = _data[binder.Name];
            return true;
        }
        else
        {
            result = null;
            return false;
        }
    }

    public override bool TrySetMember(SetMemberBinder binder, object? value)
    {
        if (!_data.ContainsKey(binder.Name)) return false;

        _data[binder.Name] = value!.ToString()!;
        return true;
    }
}