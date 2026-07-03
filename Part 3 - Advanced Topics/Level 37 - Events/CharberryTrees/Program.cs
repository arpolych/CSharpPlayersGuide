using CharberryTrees;

CharberryTree tree = new();
Notifier fruitNotifier = new(tree);
Harvester fruitHarvester = new(tree);

while (true)
    tree.MaybeGrow();

namespace CharberryTrees
{
    public class CharberryTree
    {
        private Random _random = new Random();
        public bool Ripe { get; set; }
        public event Action? Ripened;

        public void MaybeGrow()
        {
            // Only a tiny chance of ripening each time, but we try a lot!
            if (_random.NextDouble() < 0.00000001 && !Ripe)
            {
                Ripe = true;
                Ripened?.Invoke();
            }
        }
    }

    public class Notifier
    {
        private readonly CharberryTree _tree;

        public Notifier(CharberryTree tree)
        {
            _tree = tree;
            tree.Ripened += OnTreeRipened;
        }

        private void OnTreeRipened()
            => Console.WriteLine("A charberry fruit has ripened!");
    }

    public class Harvester
    {
        private readonly CharberryTree _tree;
        private int _harvestCount = 0;

        public Harvester(CharberryTree tree)
        {
            _tree = tree;
            tree.Ripened += OnTreeRipened;
        }

        private void OnTreeRipened()
        {
            _harvestCount++;
            Console.WriteLine($"The charberry fruit has been harvested {_harvestCount} times.!");
            _tree.Ripe = false;
        }
    }
}