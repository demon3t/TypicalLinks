using Grapher.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Grapher.ViewElements
{
    internal class AdderView : IViewModel
    {
        private Border border;

        public bool Top { get { return TopSection.IsAdded; } }
        private AdderSection TopSection = new AdderSection(0);

        public bool Right { get { return RigthSection.IsAdded; } }
        private AdderSection RigthSection = new AdderSection(90);

        public bool Bottom { get { return BottomSection.IsAdded; } }
        private AdderSection BottomSection = new AdderSection(180);

        public bool Left { get { return LeftSection.IsAdded; } }
        private AdderSection LeftSection = new AdderSection(270);

        private class AdderSection
        {
            private Border border;
            public bool IsAdded { get; set; }

            public AdderSection(double _angle)
            {
                border = SectionRendering(_angle);
                border.MouseLeftButtonDown += Border_MouseLeftButtonDown;
            }

            private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
            {
                border.Background = IsAdded ? Brushes.DarkGray : Brushes.White;
                IsAdded = !IsAdded;
            }

            private Border SectionRendering(double angle)
            {
                return new Border()
                {
                    Background = Brushes.White,
                    CornerRadius = new CornerRadius(8.5, 0, 0, 0),
                    Width = 8.5,
                    Height = 8.5,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(1),
                    RenderTransform = new RotateTransform()
                    {
                        Angle = angle + 45,
                        CenterX = 9,
                        CenterY = 9,
                    }
                };
            }

            public static implicit operator Border(AdderSection section)
            {
                return section.border;
            }
        }

        UIElement IViewModel.Model { get { return this; } }

        public AdderView()
        {
            border = AdderRendering();
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


        private Border AdderRendering()
        {
            Grid grid = new Grid();

            grid.Children.Add(TopSection);
            grid.Children.Add(RigthSection);
            grid.Children.Add(BottomSection);
            grid.Children.Add(LeftSection);

            return new Border()
            {
                BorderBrush = Brushes.Black,
                Width = 20,
                Height = 20,
                CornerRadius = new CornerRadius(10),
                Background = Brushes.Black,
                Child = grid,
            };
        }

        void IViewModel.Move(double X, double Y)
        {
            Canvas.SetLeft(this, X - this.border.ActualWidth / 2);
            Canvas.SetTop(this, Y - this.border.ActualHeight / 2);
        }

        public static implicit operator Border(AdderView adder)
        {
            return adder.border;
        }
    }
}
