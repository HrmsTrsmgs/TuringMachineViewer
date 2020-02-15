using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Marimo.TuringMachineViewer.TuringMachine
{
    public class Machine
    {
        public string StateString { get; private set; }

        public Tape Tape { get; }

        public Machine(char blankSymble, string startState, IEnumerable<char> tape, params (string CurrentState, char ScannedSymbol, char PrintSymbol, Direction MoveTape, string NextState)[] table)
        {
            StateString = startState;
            Tape = new Tape(blankSymble, tape);
            this.table = table.ToDictionary(x => (x.CurrentState, x.ScannedSymbol), x => (x.PrintSymbol, x.MoveTape, x.NextState));
        }
        private Dictionary<(string CurrentState, char ScannedSymbol), (char PrintSymbol, Direction MoveTape, string NextState)> table { get; }

        public IEnumerable<(string CurrentState, char ScannedSymbol, char PrintSymbol, Direction MoveTape, string NextState)> Table =>
            table.Select(kv => (kv.Key.CurrentState, kv.Key.ScannedSymbol, kv.Value.PrintSymbol, kv.Value.MoveTape, kv.Value.NextState));
        
        public bool MoveNext()
        {
            var key = (StateString, Tape.Current);
            if (table.ContainsKey(key))
            {
                var next = table[key];
                Tape.Current = next.PrintSymbol;
                StateString = next.NextState;
                Tape.Move(next.MoveTape);
                return true;
            }
            else
            {
                return false;
            }
        }

        public (char PrintSymbol, Direction MoveTape, string NextState)? State
        {
            get
            {
                var key = (StateString, Tape.Current);
                if (table.ContainsKey(key))
                {
                    return table[key];
                }
                return null;
            }
        }


        public override string ToString()
        {
            return State?.ToString() ?? "End";
        }
    }
}