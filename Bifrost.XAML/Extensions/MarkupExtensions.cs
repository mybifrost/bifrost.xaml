using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Bifrost.XAML.Extensions
{
    public static class MarkupExtensions
    {
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
    }
}
