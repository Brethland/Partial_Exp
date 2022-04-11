using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp1
{
    abstract class Fahrzeug
    {
        public double Position { get; protected set; }
        public double Geschwindigkeit { get; protected set; }

        protected Ellipse ep { get; set; }

        protected Fahrzeug(double p, double g)
        {
            this.Position = p;
            this.Geschwindigkeit = g;
            ep = new Ellipse()
            {
                Width = 10,
                Height = 10,
            };
        }
        public abstract void Zeichnen(bool isAlted, Canvas F);
        public void Bewegen(Canvas F, TimeSpan T)
        {
            this.Position += this.Geschwindigkeit * T.TotalSeconds;
            if (this.Position > Convert.ToDouble(F.ActualWidth))
            {
                this.Position = 0 + this.Position - Convert.ToDouble(F.ActualWidth);
            }
            else if (this.Position < 0)
            {
                this.Position = Convert.ToDouble(F.ActualWidth) + this.Position;
            }
        }

        public void Alt()
        {
            this.Geschwindigkeit = -this.Geschwindigkeit;
        }
    }

    class PKW : Fahrzeug
    {
        public PKW(double p, double g) : base(p, g) { }

        public override void Zeichnen(bool isAlted, Canvas F)
        {
            if(isAlted)
            {
                ep.Fill = Brushes.Gray;
            }else
            {
                ep.Fill = Brushes.Blue;
            }
            Canvas.SetTop(ep, 100);
            Canvas.SetLeft(ep, Position);
            F.Children.Add(ep);
        }
    }

    class LKW : Fahrzeug
    {
        public LKW(double p, double g) : base(p, g) { }

        public override void Zeichnen(bool isAlted, Canvas F)
        {
            if (isAlted)
            {
                ep.Fill = Brushes.Blue;
            }
            else
            {
                ep.Fill = Brushes.Gray;
            }
            Canvas.SetTop(ep, 300);
            Canvas.SetLeft(ep, Position);
            F.Children.Add(ep);
        }
    }
}
