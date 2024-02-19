using System.Windows;
using System.Windows.Media;

namespace Homework_13.Infrastructure.Extensions
{
    public static class DependencyObjectExtensions
    {
        public static DependencyObject FindVisualRoot(this DependencyObject obj)
        {
            do
            {
                var parent = VisualTreeHelper.GetParent(obj);
                if (parent is null) return obj;
                obj = parent;
            } while (true);
        }
    }
}
