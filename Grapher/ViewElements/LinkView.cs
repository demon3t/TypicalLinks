using Grapher.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Grapher.ViewElements
{
    internal class LinkView : IViewModel
    {
        private Border border;

        UIElement IViewModel.Model { get { return this; } }

        public LinkView()
        {
            border = LinkRendering();
        }

        void IViewModel.Binding(Grapher grapher)
        {
            ContextMenu menu = new ContextMenu();

            MenuItem del = new MenuItem() { Header = "Удалить" };
            del.Click += Del_Click;
            menu.Items.Add(del);

            MenuItem edi = new MenuItem() { Header = "Переместить" };
            edi.Click += Edi_Click;
            menu.Items.Add(edi);

            border.ContextMenu = menu;

            void Del_Click(object sender, RoutedEventArgs e)
            {
                grapher.Graph.Children.Remove(this);
            }

            void Edi_Click(object sender, RoutedEventArgs e)
            {
                grapher.SetEditMode(this);
            }
        }

        private Border LinkRendering()
        {
            return new Border()
            {
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1.25),
                Width = 60,
                Height = 40,
            };
        }

        void IViewModel.Move(double X, double Y)
        {
            Canvas.SetLeft(this, X - this.border.ActualWidth / 2);
            Canvas.SetTop(this, Y - this.border.ActualHeight / 2);
        }

        public static implicit operator Border(LinkView link)
        {
            return link.border;
        }
    }
}
