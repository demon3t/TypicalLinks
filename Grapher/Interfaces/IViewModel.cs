using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Grapher.Interfaces
{
    internal interface IViewModel
    {
        internal UIElement Model { get; }
        internal void Move(double X, double Y);
        internal void SetContextMenu(Canvas graph);
    }
}
