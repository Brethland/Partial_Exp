using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp2
{
    class Korper
    {
        ValueTuple<double, double> Position { get; set; }
        double Geschwindigkeit { get; set; }
        double Beschleunigung { get; set; }
        double r { get; set; }
        Polygon p;

        static Random rng = new Random();
        public Korper(double x, double y, double r)
        {
            Position = (x, y);
            Geschwindigkeit = 0;
            Beschleunigung = 100;
            this.r = r;
        }

        public void Draw(Canvas F, bool isAlted)
        {
            p = new Polygon
            {
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
            };
            PointCollection Pc = new PointCollection();

            Enumerable.Range(1, 20).ToList().ForEach(i => {
                double xs = rng.NextDouble() * Math.Sqrt(2) * r / 2;
                double ys = Math.Sqrt(r * r - xs * xs);
                Pc.Add(new System.Windows.Point(Position.Item1 + (-1 * rng.Next(-1, 2) * xs), Position.Item2 + (-1 * rng.Next(-1, 2) * ys)));
            });

            p.Points = Pc;
            p.Fill = isAlted ? Brushes.Red : Brushes.Gray;
            Canvas.SetLeft(p, Position.Item1);
            Canvas.SetTop(p, Position.Item2);
            F.Children.Add(p);
        }

        public void Move(TimeSpan t)
        {
            Geschwindigkeit += Beschleunigung * t.TotalSeconds;
            Position = (
                Position.Item1, 
                Position.Item2 + Geschwindigkeit * t.TotalSeconds + 1 / 2 * Beschleunigung * t.TotalSeconds * t.TotalSeconds
                );
        }

        public bool OutBorder()
        {
            return this.Position.Item2 > 410;
        }
    }
}
