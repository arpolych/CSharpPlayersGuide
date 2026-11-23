namespace TheFinalBattle
{
    /// <summary>
    /// Creates a console menu of a specified type.
    /// </summary>
    /// <typeparam name="T">A type for the console menu.</typeparam>
    public class ConsoleMenu<T>
    {
        /// <summary>
        /// The heading of the console menu, which is optional.
        /// </summary>
        private readonly string? _menuHeading;
        private readonly IEnumerable<T> _collection;

        /// <summary>
        /// Constructs a console menu.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="menuHeading"></param>
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

        /// <summary>
        /// A numbered console menu that starts from 1.
        /// </summary>
        /// <returns>The numbered console menu.</returns>
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
