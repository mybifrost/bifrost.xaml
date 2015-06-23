using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Bifrost.XAML.Extensions
{
    public static class MarkupExtensions
    {
        /// <summary>
        /// Searches down the UI tree to find all anscestors of a given type.
        /// </summary>
        /// <typeparam name="T">Type of ancestors to be found.</typeparam>
        /// <param name="root">Element to begin searching down from.</param>
        /// <returns>List of all ancestors of the given type.  </returns>
        public static IEnumerable<T> GetAnscestors<T>(this DependencyObject root) where T : DependencyObject
        {
            var list = new List<T>();

            int count = VisualTreeHelper.GetChildrenCount(root);
            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(root, i);
                if (child is T)
                    list.Add(child as T);

                list.AddRange(GetAnscestors<T>(child));
            }

            return list;
        }

        /// <summary>
        /// Finds the first parent of a given type.
        /// </summary>
        /// <typeparam name="T">Type of parent to be found.</typeparam>
        /// <param name="root">Element to begin searching up from.</param>
        /// <returns>First parent of given type, or null if no parents match.</returns>
        public static T GetParent<T>(this DependencyObject root) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(root);
            if (parent != null)
            {
                if (parent is T)
                    return parent as T;
                else
                    return GetParent<T>(parent);
            }
            else
                return null;
        }
    }
}
