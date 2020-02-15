using Marimo.TuringMachineViewer.TuringMachine;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace Marimo.TuringMachineViewer.Uwp.ViewModels
{
    public class TuringMachineViewModel : BindableBase
    {

        public Machine Machine { get; } = 
                new Machine(' ',
                "1",
                "***X**",
                ("1", '*', '*', Direction.Left, "2"),
                ("2", ' ', '-', Direction.Right, "3"),
                ("3", ' ', ' ', Direction.Left, "4"),
                ("3", '-', '-', Direction.Right, "3"),
                ("3", '*', '*', Direction.Right, "3"),
                ("3", 'X', 'X', Direction.Right, "3"),
                ("3", 'A', 'A', Direction.Right, "3"),
                ("4", '*', ' ', Direction.Left, "5"),
                ("4", 'X', 'X', Direction.Left, "10"),
                ("5", '*', '*', Direction.Left, "5"),
                ("5", 'X', 'X', Direction.Left, "6"),
                ("6", '-', '-', Direction.Right, "9"),
                ("6", '*', 'A', Direction.Left, "7"),
                ("6", 'A', 'A', Direction.Left, "6"),
                ("7", ' ', '*', Direction.Right, "8"),
                ("7", '-', '-', Direction.Left, "7"),
                ("7", '*', '*', Direction.Left, "7"),
                ("8", '-', '-', Direction.Right, "8"),
                ("8", '*', '*', Direction.Right, "8"),
                ("8", 'X', 'X', Direction.Left, "6"),
                ("8", 'A', 'A', Direction.Right, "8"),
                ("9", ' ', ' ', Direction.Left, "4"),
                ("9", '*', '*', Direction.Right, "9"),
                ("9", 'X', 'X', Direction.Right, "9"),
                ("9", 'A', '*', Direction.Right, "9"),
                ("10", '-', '*', Direction.Right, "11"),
                ("10", '*', '*', Direction.Left, "10"),
                ("11", ' ', ' ', Direction.NotMove, "12"),
                ("11", '*', ' ', Direction.Right, "11"),
                ("11", 'X', ' ', Direction.Right, "11"),
                ("11", 'A', ' ', Direction.Right, "11"));

        private Dictionary<int, SymbolViewModel> Tape { get; } = new Dictionary<int, SymbolViewModel>();

        public IEnumerable<SymbolViewModel> ScopedTape
        {
            get
            {
                for (int i = Machine.Tape.CurrentIndex - 10; i <= Machine.Tape.CurrentIndex + 10; i++)
                {
                    yield return this[i];
                }
            }
        }

        private SymbolViewModel this[int index]
        {
            get
            {
                if(!Tape.ContainsKey(index))
                {
                    Tape[index] = new SymbolViewModel { Symbol = Machine.Tape[index] };
                }
                return Tape[index];
            }
        }

        public TuringMachineViewModel()
        {
            if(DesignMode.DesignModeEnabled)
            {

            }

            MoveCommand = new DelegateCommand(() =>
            {
                Machine.MoveNext();
                this[Machine.Tape.CurrentIndex - 1].IsCurrent = false;
                this[Machine.Tape.CurrentIndex - 1].Symbol = Machine.Tape[Machine.Tape.CurrentIndex - 1];
                this[Machine.Tape.CurrentIndex].IsCurrent = true;
                this[Machine.Tape.CurrentIndex].Symbol = Machine.Tape.Current;
                this[Machine.Tape.CurrentIndex + 1].IsCurrent = false;
                this[Machine.Tape.CurrentIndex + 1].Symbol = Machine.Tape[Machine.Tape.CurrentIndex + 1];
                RaisePropertyChanged(nameof(ScopedTape));
            });
        }

        public DelegateCommand MoveCommand { get; }
    }
}
