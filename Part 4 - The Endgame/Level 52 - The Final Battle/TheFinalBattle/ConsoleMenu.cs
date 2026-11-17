namespace TheFinalBattle
{
    public class ConsoleMenu<T>
    {
        private readonly string? _menuHeading;
        private readonly IEnumerable<T> _collection;

        public ConsoleMenu(IEnumerable<T> collection, string? menuHeading = null)
        {
            _menuHeading = menuHeading;
            _collection = collection;
        }

        /// <summary>
        /// Retrieves an element from the console menu.
        /// </summary>
        /// <param name="elementNum">The number of the element in the console menu.</param>
        /// <returns>The element of T type in the console menu.</returns>
        public T RetrieveElement(int elementNum) => _collection.ElementAt(elementNum - 1);

        public override string ToString()
        {
            string consoleText = (_menuHeading != null) ? $"{_menuHeading}\n" : string.Empty;
            int elementNum = 1;

            foreach (var element in _collection)
            {
                consoleText += $"{elementNum++}. {element}\n";
            }

            return consoleText;
        }
    }
}
