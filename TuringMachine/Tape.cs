using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Marimo.TuringMachineViewer.TuringMachine
{
    public class Tape
    {
        public Tape(string blank, IEnumerable<string> init)
        {
            Blank = blank;
            plusStates.AddRange(init);
        }

        public string Blank { get; private set; }

        private int CurrentIndex { get; set; } = 0;

        public int Position => CurrentIndex + minusStates.Count;

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    CurrentIndex--;
                    break;
                case Direction.Right:
                    CurrentIndex++;
                    break;
            }
            var d = this[CurrentIndex];
        }

        private List<string> plusStates = new List<string>();
        private List<string> minusStates = new List<string>();

        private string this[int index]
        {
            get
            {
                if (0 <= index)
                {
                    while (plusStates.Count <= index)
                    {
                        plusStates.Add(Blank);
                    }
                    return plusStates[index];
                }
                else
                {
                    var listIndex = -index - 1;

                    while (minusStates.Count <= listIndex)
                    {
                        minusStates.Add(Blank);
                    }
                    return minusStates[listIndex];
                }
            }
            set
            {
                if (0 <= index)
                {
                    while (plusStates.Count <= index)
                    {
                        plusStates.Add(Blank);
                    }
                    plusStates[index] = value;
                }
                else
                {
                    var listIndex = -index - 1;

                    while (minusStates.Count <= listIndex)
                    {
                        minusStates.Add(Blank);
                    }
                    minusStates[listIndex] = value;
                }
            }
        }

        public string Current
        {
            get
            {
                return this[CurrentIndex];
            }
            set
            {
                this[CurrentIndex] = value;
            }
        }

        public override string ToString()
        {
            return string.Join("", minusStates.Reverse<string>().Concat(plusStates));
        }
    }
}
