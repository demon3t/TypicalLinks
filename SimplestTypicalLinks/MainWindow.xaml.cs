using Links.Basic;
using Links.Calculate;
using Links.TypesOfLinks;
using SimplestTypicalLinks.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimplestTypicalLinks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<Vertex> vertices = new List<Vertex>();
            vertices.Add(new Vertex(0) { IsParametr = true });
            vertices.Add(new Vertex(1) { IsFunction = true });

            List<Link> links = new List<Link>();

            // интегрирующее
            //links.Add(new Integrating()
            //{
            //    K = 1,
            //    T = 0.1,
            //    Start = vertices[0],
            //    End = vertices[1]
            //});

            // дифференцирующее
            links.Add(new Differentiating()
            {
                K = 0.1,
                T = 0.1,
                Start = vertices[0],
                End = vertices[1]
            });

            // пропорциональное
            //links.Add(new Proportional()
            //{
            //    K = 1,
            //    T = 0.1,
            //    Start = vertices[0],
            //    End = vertices[1]
            //});

            // инерционное
            //links.Add(new Inertial()
            //{
            //    K = 1,
            //    T = 0.1,
            //    Start = vertices[0],
            //    End = vertices[1]
            //});

            Algorithm a = new Algorithm(new List<Link>(links), new List<Vertex>(vertices));



            PointCollection points = new PointCollection();
            Link.i = 0;
            do
            {
                points.Add(new Point(Link.i, Calculate.GetValue(a, new List<Link>(links), new List<Vertex>(vertices))));
                Link.i += 0.05;
            }
            while (Link.i < 1);

            Plot.Plot.AddScatter(points.Xs(), points.Ys());
            Plot.Refresh();
        }
    }
}
