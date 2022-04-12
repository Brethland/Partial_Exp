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

namespace WpfApp2
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer t = new DispatcherTimer();
        List<Korper> k = new List<Korper>();
        bool isAlted = false;
        Random rng = new Random();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            t.Interval = TimeSpan.FromMilliseconds(17);
            t.Start();
            t.Tick += FrameFresh;
        }

        private bool OutBorder(Korper k)
        {
            return k.OutBorder();
        }

        private void FrameFresh(object sender, EventArgs e)
        {
            Flaeche.Children.Clear();
            k.RemoveAll(OutBorder);
            k.ForEach(i => { i.Move(TimeSpan.FromMilliseconds(17)); i.Draw(Flaeche, isAlted); });
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Space)) isAlted = !isAlted;
            else if (e.Key.Equals(Key.K)) k.Add(new Korper(rng.NextDouble() * 800, 15, 6));
            else if (e.Key.Equals(Key.S)) k.Add(new Korper(rng.NextDouble() * 800, 15, 12));
            else if (e.Key.Equals(Key.F)) k.Add(new Korper(rng.NextDouble() * 800, 15, 24));
        }
    }
}
