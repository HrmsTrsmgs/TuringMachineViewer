using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Marimo.TuringMachineViewer.TuringMachine
{
    public class Tape
    {
        public char Blank { get; }

        private int CurrentIndex { get; set; } = 0;

        private Dictionary<int, char> symbols = new Dictionary<int, char>();

        public Tape(char blank, IEnumerable<char> init)
        {
            Blank = blank;
            foreach(var (value, index) in init.Select((v, i) => (v, i)))
            {
                this[index] = value;
            }
        }

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
        }

        public int DistanceFromLeftEnd => CurrentIndex - Math.Min(CurrentIndex, symbols.Any() ? symbols.Keys.Min() : CurrentIndex);


        private char this[int index]
        {
            get
            {
                if(symbols.ContainsKey(index))
                {
                    return symbols[index];
                }
                else
                {
                    return Blank;
                }
            }
            set
            {
                if(value == Blank)
                {
                    symbols.Remove(index);
                }
                else
                {
                    symbols[index] = value;
                }
            }
        }

        public char Current
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

        private IEnumerable<char> SymbolList
        {
            get
            {
                var min = Math.Min(CurrentIndex, symbols.Any() ? symbols.Keys.Min() : CurrentIndex);
                var max = Math.Max(CurrentIndex, symbols.Any() ? symbols.Keys.Max() : CurrentIndex);
                for (int i = min; i <=
                    
                    max; i++)
                {
                    yield return this[i];
                }
            }
        }

        public override string ToString() => new string(SymbolList.ToArray());
    }
}
