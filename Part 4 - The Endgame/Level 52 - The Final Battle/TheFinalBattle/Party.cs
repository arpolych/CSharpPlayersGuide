namespace TheFinalBattle
{
    public class Party
    {
        public List<Character> Units { get; } = new List<Character>();
        public IPlayer Player { get; }
        public List<ConsumableItem> ConsumablesInventory { get; private set; } = new();
        public List<EquippableGearItem> UnequippedGear { get; private set; } = new();

        public Party(IPlayer player) => Player = player;

        public bool TransferConsumables(List<ConsumableItem> otherInventory, string? message = null)
            => TransferInventoryElements(ConsumablesInventory, otherInventory, message);

        public bool TransferGearItems(List<EquippableGearItem> otherInventory, string? message = null)
            => TransferInventoryElements(UnequippedGear, otherInventory, message);

        public static bool TransferInventoryElements<T>(List<T> from, List<T> to, string? message = null)
        {
            // Inventory transfer will execute if there are elements to transfer.
            if (from.Count == 0) return false;
            if (message != null) Console.WriteLine(message);

            foreach (var element in from)
            {
                to.Add(element);
            }
            from.Clear();

            return true;
        }
    }
}
