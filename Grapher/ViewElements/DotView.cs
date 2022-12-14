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
    internal class DotView : IViewModel
    {
        private Ellipse ellipse;
        UIElement IViewModel.Model { get { return this; } }

        public DotView()
        {
            ellipse = DotRendering();
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

            ellipse.ContextMenu = menu;

            void Del_Click(object sender, RoutedEventArgs e)
            {
                grapher.Graph.Children.Remove(this);
            }

            void Edi_Click(object sender, RoutedEventArgs e)
            {
                grapher.SetEditMode(this);
            }
        }

        private Ellipse DotRendering()
        {
            return new Ellipse()
            {
                Fill = Brushes.Black,
                Stroke = Brushes.Black,
                Width = 8,
                Height = 8,
            };
        }

        void IViewModel.Move(double X, double Y)
        {
            Canvas.SetLeft(this, X - this.ellipse.ActualWidth / 2);
            Canvas.SetTop(this, Y - this.ellipse.ActualHeight / 2);
        }

        public static implicit operator Ellipse(DotView dot)
        {
            return dot.ellipse;
        }
    }
}
