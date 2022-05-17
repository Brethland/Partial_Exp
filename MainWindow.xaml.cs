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

namespace WpfApp1
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Ellipse> eps = new List<Ellipse>();
        TextBox LastTp;
        Riddle ori = new Riddle(Color.Black, Color.Green, Color.Red, Color.Black);
        Random rng = new Random();
        int cnt = 0;
        public MainWindow()
        {
            InitializeComponent();
            Enumerable.Range(1, 5).ToList().ForEach(i => {
                ColumnDefinition cm = new ColumnDefinition();
                cm.Width = new GridLength(1, GridUnitType.Star);
                Vi.ColumnDefinitions.Add(cm);
            });
            ori = new Riddle((Color)rng.Next(0, 4), (Color)rng.Next(0, 4), (Color)rng.Next(0, 4), (Color)rng.Next(0, 4));
        }

        void New()
        {
            RowDefinition rm = new RowDefinition();
            rm.Height = new GridLength(100, GridUnitType.Pixel);
            Vi.RowDefinitions.Add(rm);

            Enumerable.Range(1, 4).ToList().ForEach(i =>
            {
                Ellipse ep = new Ellipse()
                {
                    Stroke = Brushes.Gray,
                    Fill = Brushes.LightGray,
                };
                Grid.SetRow(ep, cnt);
                Grid.SetColumn(ep, i - 1);
                Vi.Children.Add(ep);
                eps.Add(ep);

                ContextMenu ctx = new ContextMenu();
                MenuItem mi = new MenuItem()
                {
                    Header = "Red",
                    FontSize = 10,
                    Background = Brushes.White,
                };
                mi.Click += new RoutedEventHandler(SelectMenu1);
                MenuItem mi2 = new MenuItem()
                {
                    Header = "Black",
                    FontSize = 10,
                    Background = Brushes.White,
                };
                mi2.Click += new RoutedEventHandler(SelectMenu2);
                MenuItem mi3 = new MenuItem()
                {
                    Header = "Blue",
                    FontSize = 10,
                    Background = Brushes.White,
                };
                mi3.Click += new RoutedEventHandler(SelectMenu3);
                MenuItem mi4 = new MenuItem()
                {
                    Header = "Green",
                    FontSize = 10,
                    Background = Brushes.White,
                };
                mi4.Click += new RoutedEventHandler(SelectMenu4);
                ctx.Items.Add(mi);
                ctx.Items.Add(mi2);
                ctx.Items.Add(mi3);
                ctx.Items.Add(mi4);
                ep.ContextMenu = ctx;
            });
            TextBox tp = new TextBox()
            {
                Background = Brushes.LightGray,
                IsReadOnly = true,
                FontSize = 30,
                TextAlignment = TextAlignment.Center,
            };
            Grid.SetRow(tp, cnt);
            Grid.SetColumn(tp, 4);
            Vi.Children.Add(tp);
            LastTp = tp;
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            New();
            cnt++;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (LastTp == null || eps.Count() == 0)
            {
                MessageBox.Show("Add a new guess!");
                return;
            }
            Riddle rl = new Riddle((Color)ColortoNum(eps.ElementAt(0).Fill), (Color)ColortoNum(eps.ElementAt(1).Fill),
                                   (Color)ColortoNum(eps.ElementAt(2).Fill), (Color)ColortoNum(eps.ElementAt(3).Fill));
            var res = ori.Compare(rl);
            int cc = res.Item2;
            List<string> text = new List<string>();
            Enumerable.Range(0, 4).ToList().ForEach(i => {
                if (res.Item1.Contains(i)) text.Add("O");
                else if (cc > 0)
                {
                    text.Add("X");
                    cc--;
                }
                else text.Add("N");
            });
            text = text.OrderBy(x => rng.Next()).ToList();
            LastTp.Text = string.Join(" ", text);
            eps.Clear();
        }

        int ColortoNum(Brush c)
        {
            if (c.Equals(Brushes.Red)) return 0;
            else if (c.Equals(Brushes.Blue)) return 1;
            else if (c.Equals(Brushes.Black)) return 2;
            else if (c.Equals(Brushes.Green)) return 3;
            return -1;
        }
        private void SelectMenu1(object sender, RoutedEventArgs e)
        {
            ((Ellipse)((ContextMenu)((MenuItem)e.Source).Parent).PlacementTarget).Fill = Brushes.Red;
        }
        private void SelectMenu2(object sender, RoutedEventArgs e)
        {
            ((Ellipse)((ContextMenu)((MenuItem)e.Source).Parent).PlacementTarget).Fill = Brushes.Black;
        }
        private void SelectMenu3(object sender, RoutedEventArgs e)
        {
            ((Ellipse)((ContextMenu)((MenuItem)e.Source).Parent).PlacementTarget).Fill = Brushes.Blue;
        }
        private void SelectMenu4(object sender, RoutedEventArgs e)
        {
            ((Ellipse)((ContextMenu)((MenuItem)e.Source).Parent).PlacementTarget).Fill = Brushes.Green;
        }
    }
}
