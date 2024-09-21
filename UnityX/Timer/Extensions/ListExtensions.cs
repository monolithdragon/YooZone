using System.Collections.Generic;

namespace UnityX.Timer {
    public static class ListExtensions {
        /// <summary>
        /// Replaces the contents of the list with the specified items from an IEnumerable<T>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="list">The list to refresh.</param>
        /// <param name="items">The collection of items to add to the list.</param>
        public static void RefreshWith<T>(this List<T> list, IEnumerable<T> items) {
            list.Clear();
            list.AddRange(items);
        }
    }
}
