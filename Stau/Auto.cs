using System;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Stau
{
    class Auto
    {
        public ValueTuple<double, double> speed { get; private set; }
        public ValueTuple<double, double> position { get; private set; }
        public ValueTuple<double, double> accelerate { get; private set; }
        public bool isHalted { get; private set; }

        static Random rng = new Random();

        private void Reset(double height, double width)
        {
            this.position = (rng.NextDouble() * width, rng.NextDouble() * height);
            this.speed = (rng.NextDouble() * 40 + 150 * rng.Next(-1, 2), rng.NextDouble() * 40 + 150 * rng.Next(-1, 2));
            this.accelerate = (rng.NextDouble() * 50 * rng.Next(-1, 2), rng.NextDouble() * 50 * rng.Next(-1, 2));
        }
        public Auto(double height, double width)
        {
            this.isHalted = false;
            Reset(height, width);
        }

        public void Draw(Canvas F)
        {
            Ellipse e = new Ellipse()
            {
                Width = 10,
                Height = 10,
                Fill = Brushes.Crimson,
            };
            if(position.Item1 + 5 > Convert.ToInt32(F.ActualWidth) 
                || position.Item1 < 0 
                || position.Item2 + 5 > Convert.ToInt32(F.ActualHeight) 
                || position.Item2 < 0)
                return;
            Canvas.SetTop(e, position.Item2);
            Canvas.SetLeft(e, position.Item1);
            F.Children.Add(e);
        }

        public void Move(TimeSpan t, Canvas F)
        {
            if(isHalted) return;
            //this.position = (this.position.Item1 + this.speed.Item1 * t.TotalSeconds,
              //              this.position.Item2 + this.speed.Item2 * t.TotalSeconds);

            this.position = (this.position.Item1 + this.speed.Item1 * t.TotalSeconds + 0.5 * this.accelerate.Item1 * t.TotalSeconds * t.TotalSeconds, 
                            this.position.Item2 + this.speed.Item2 * t.TotalSeconds + 0.5 * this.accelerate.Item2 * t.TotalSeconds * t.TotalSeconds);
            this.speed = (this.speed.Item1 + this.accelerate.Item1 * t.TotalSeconds, this.speed.Item2 + this.accelerate.Item2 * t.TotalSeconds);

            if (this.position.Item1 > 2 * Convert.ToDouble(F.ActualWidth)
                || this.position.Item2 > 2 * Convert.ToDouble(F.ActualHeight))
            {
                Reset(Convert.ToInt32(F.ActualHeight), Convert.ToDouble(F.ActualWidth));
                return;
            }
            
            if(this.position.Item1 >= Convert.ToInt32(F.ActualWidth))
            {
                this.position = (2 * Convert.ToDouble(F.ActualWidth) - this.position.Item1, this.position.Item2);
                this.speed = (-this.speed.Item1, this.speed.Item2);
            }
            else if(this.position.Item1 <= 0)
            {
                this.position = (-this.position.Item1, this.position.Item2);
                this.speed = (-this.speed.Item1, this.speed.Item2);
            }
            else if(this.position.Item2 >= Convert.ToInt32(F.ActualHeight))
            {
                this.position = (this.position.Item1, 2 * Convert.ToDouble(F.ActualHeight) - this.position.Item2);
                this.speed = (this.speed.Item1, -this.speed.Item2);
            }
            else if(this.position.Item2 <= 0)
            {
                this.position = (this.position.Item1, -this.position.Item2);
                this.speed = (this.speed.Item1, -this.speed.Item2);
            }
        }

        public void Collide(Auto b)
        {
            
        }

        public void Acc()
        {
            this.speed = (this.speed.Item1 * 1.2, this.speed.Item2 * 1.2);
        }
        public void DeAcc()
        {
            this.speed = (this.speed.Item1 * 0.8, this.speed.Item2 * 0.8);
        }
        
        public void Stop()
        {
            if(isHalted == false) this.isHalted = true;
            else this.isHalted = false;
        }
    }
}
