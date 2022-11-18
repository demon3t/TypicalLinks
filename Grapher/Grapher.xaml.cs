using Grapher.Interfaces;
using Grapher.ViewElements;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Grapher
{
    /// <summary>
    /// Логика взаимодействия для Grapher.xaml
    /// </summary>
    public partial class Grapher : UserControl
    {
        public Grapher()
        {
            InitializeComponent();
        }

        private const double c_Step = 20;

        internal delegate void ContextMenuOperation(IViewModel model);

        internal ContextMenuOperation Delete = delegate (IViewModel model)
        {
            
        };

        internal ContextMenuOperation Edit = delegate (IViewModel model)
        {

        };

        private Mode Mode
        {
            get { return _mode; }
            set
            {
                if (value == Mode.Watch)
                {
                    HorizontalLine.Visibility = Visibility.Collapsed;
                    VerticalLine.Visibility = Visibility.Collapsed;
                }
                if (value == Mode.Edit)
                {
                    HorizontalLine.Visibility = Visibility.Visible;
                    VerticalLine.Visibility = Visibility.Visible;
                }
                _mode = value;
            }
        }
        private Mode _mode = Mode.Watch;

        private IViewModel Model;

        

        private void Graph_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mode == Mode.Watch)
                return;

            VerticalLine.X1 = Math.Round(Mouse.GetPosition(Graph).X / c_Step, 0) * c_Step ;
            VerticalLine.X2 = Math.Round(Mouse.GetPosition(Graph).X / c_Step, 0) * c_Step;

            HorizontalLine.Y1 = Math.Round(Mouse.GetPosition(Graph).Y / c_Step, 0) * c_Step;
            HorizontalLine.Y2 = Math.Round(Mouse.GetPosition(Graph).Y / c_Step, 0) * c_Step;

            Model.Move(VerticalLine.X1, HorizontalLine.Y1);
        }

        private void Graph_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_mode == Mode.Watch)
                return;

            Model.SetContextMenu(Graph);
            Mode = Mode.Watch;
            Model = null;
        }

        private void Adder_Click(object sender, RoutedEventArgs e)
        {
            Model = new AdderView();
            Graph.Children.Add(Model.Model);
            Mode = Mode.Edit;
        }
    }
    internal enum Mode
    {
        Watch,
        Edit
    }
}
