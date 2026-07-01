using IField;
using McDroid;
using IFieldPig = IField.Pig;
using McDroidPig = McDroid.Pig;

namespace TheFeud
{
    internal class MainProgram
    {
        static void Main(string[] args)
        {
            // iField Family.
            Sheep ifieldsSheep = new();
            IFieldPig ifieldsPig = new();

            // McDroids Family.
            McDroidPig mcdroidsPig = new();
            Cow mcdroidsCow = new();
        }
    }
}
