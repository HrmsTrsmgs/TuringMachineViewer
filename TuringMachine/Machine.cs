using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Marimo.TuringMachineViewer.TuringMachine
{
    public class Machine
    {
        public string State { get; private set; }

        public Tape Tape { get; }

        public Machine(string blankSymble, string startState, IEnumerable<string> tape, params (string CurrentState, string ScannedSymbol, string PrintSymbol, Direction MoveTape, string NextState)[] table)
        {
            State = startState;
            Tape = new Tape(blankSymble, tape);
            this.table = table.ToDictionary(x => (x.CurrentState, x.ScannedSymbol), x => (x.PrintSymbol, x.MoveTape, x.NextState));
        }
        private Dictionary<(string CurrentState, string ScannedSymbol), (string PrintSymbol, Direction MoveTape, string NextState)> table { get; }

        public IEnumerable<(string CurrentState, string ScannedSymbol, string PrintSymbol, Direction MoveTape, string NextState)> Table =>
            table.Select(kv => (kv.Key.CurrentState, kv.Key.ScannedSymbol, kv.Value.PrintSymbol, kv.Value.MoveTape, kv.Value.NextState));
        
        public bool MoveNext()
        {
            var key = (State, Tape.Current);
            if (table.ContainsKey(key))
            {
                var next = table[key];
                Tape.Current = next.PrintSymbol;
                State = next.NextState;
                Tape.Move(next.MoveTape);
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            var key = (State, Tape.Current);
            if (table.ContainsKey(key))
            {
                return table[key].ToString();
            }
            return "End";
        }
    }
}