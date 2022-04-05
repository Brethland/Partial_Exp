using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Input;

namespace Stau
{
    public partial class MainWindow : Window
    {
        DispatcherTimer t = new DispatcherTimer();
        Queue<Auto> a = new Queue<Auto>();
        public MainWindow()
        {
            InitializeComponent();

            t.Interval = TimeSpan.FromMilliseconds(17);
            t.Start();
            t.Tick += FrameFresh;

            Enumerable.Range(1, 10).ToList().ForEach(i => a.Enqueue(new Auto(Convert.ToInt32(F.Width), Convert.ToInt32(F.Height))));
        }

        public void FrameFresh(object sender, EventArgs e)
        {
            F.Children.Clear();
            a.ToList<Auto>().ForEach(x => { x.Move(TimeSpan.FromMilliseconds(17), F); x.Draw(F); });
        }

        public void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Up)) a.ToList<Auto>().ForEach(x => x.Acc());
            else if (e.Key.Equals(Key.Down)) a.ToList<Auto>().ForEach(x => x.DeAcc());
            else if (e.Key.Equals(Key.Space)) a.ToList<Auto>().ForEach(x => x.Stop());
        }
    }
}
