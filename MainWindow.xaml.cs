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
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        bool isAlted = false;
        List<PKW> pkws = new List<PKW>();
        List<LKW> lkws = new List<LKW>();
        DispatcherTimer t = new DispatcherTimer();
        
        public MainWindow()
        {
            InitializeComponent();

            t.Tick += FrameFresh;

            var Rng = new Random();
            double p = 0;
            Enumerable.Range(1, 10).ToList().ForEach(a => { p += Rng.NextDouble() * 80; pkws.Add(new PKW(p, 100)); p += 10; });
            p = 0;
            Enumerable.Range(1, 10).ToList().ForEach(a => { p += Rng.NextDouble() * 80; lkws.Add(new LKW(p, 75)); p += 10; });
        }

        public void FrameFresh(object sender, EventArgs e)
        {
            Fläche.Children.Clear();
            pkws.ForEach(p =>
            {
                p.Bewegen(Fläche, TimeSpan.FromMilliseconds(17));
                p.Zeichnen(isAlted, Fläche);
            });
            lkws.ForEach(l =>
            {
                l.Bewegen(Fläche, TimeSpan.FromMilliseconds(17));
                l.Zeichnen(isAlted, Fläche);
            });
        }

        public void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Left)) pkws.ForEach(p => p.Alt());
            else if (e.Key.Equals(Key.Right)) lkws.ForEach(l => l.Alt());
            else if (e.Key.Equals(Key.Space)) isAlted = !isAlted;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            t.Interval = TimeSpan.FromMilliseconds(17);
            t.Start();
            Start.IsEnabled = false;
        }
    }
}
