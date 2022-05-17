using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    enum Color
    {
        Red, Blue, Black, Green
    }
    class Riddle
    {
        public List<Color> balls = new List<Color>();
        public Riddle(Color fst, Color snd, Color trd, Color fot)
        {
            balls.Add(fst);
            balls.Add(snd);
            balls.Add(trd);
            balls.Add(fot);
        }

        public ValueTuple<List<int>, int> Compare(Riddle judge)
        {
            bool[] isMarked = new bool[5];
            bool[] isMarkedj = new bool[5];
            int cnt = 0;
            var res = balls.Zip(judge.balls, (a, b) => a.Equals(b));
            Enumerable.Range(0, 4).ToList().ForEach(i => {
                for (int j = 0; j < 4; j++)
                {
                    if (balls.ElementAt(i).Equals(judge.balls.ElementAt(j)) && !isMarked[i] && !isMarkedj[j])
                    {
                        isMarked[i] = true;
                        isMarkedj[j] = true;
                        cnt++;
                    }
                }
            });
            var l = res.ToList().Select((item, index) => item ? index : -1).Where(index => index != -1).ToList();
            return (l, cnt - res.ToList().FindAll(i => i).Count);
        }
    }
}
