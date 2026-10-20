using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace WpfApp1.view.utils
{
    public static class VisualTreeExtensions
    {
        public static T? FindAncestor<T>(this DependencyObject child) where T : DependencyObject
        {
            var current = VisualTreeHelper.GetParent(child);
            while (current is not null && current is not T)
                current = VisualTreeHelper.GetParent(current);
            return current as T;
        }
    }
}
