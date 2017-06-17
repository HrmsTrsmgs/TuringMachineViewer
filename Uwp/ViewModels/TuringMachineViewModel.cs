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

        private Machine machine { get; } = 
                new Machine('_',
                "1",
                "111X11".ToCharArray(),
                ("1", '1', '1', Direction.Left, "2"),
                ("2", '_', '*', Direction.Right, "3"),
                ("3", '_', '_', Direction.Left, "4"),
                ("3", '*', '*', Direction.Right, "3"),
                ("3", '1', '1', Direction.Right, "3"),
                ("3", 'X', 'X', Direction.Right, "3"),
                ("3", 'A', 'A', Direction.Right, "3"),
                ("4", '1', '_', Direction.Left, "5"),
                ("4", 'X', 'X', Direction.Left, "10"),
                ("5", '1', '1', Direction.Left, "5"),
                ("5", 'X', 'X', Direction.Left, "6"),
                ("6", '*', '*', Direction.Right, "9"),
                ("6", '1', 'A', Direction.Left, "7"),
                ("6", 'A', 'A', Direction.Left, "6"),
                ("7", '_', '1', Direction.Right, "8"),
                ("7", '*', '*', Direction.Left, "7"),
                ("7", '1', '1', Direction.Left, "7"),
                ("8", '*', '*', Direction.Right, "8"),
                ("8", '1', '1', Direction.Right, "8"),
                ("8", 'X', 'X', Direction.Left, "6"),
                ("8", 'A', 'A', Direction.Right, "8"),
                ("9", '_', '_', Direction.Left, "4"),
                ("9", '1', '1', Direction.Right, "9"),
                ("9", 'X', 'X', Direction.Right, "9"),
                ("9", 'A', '1', Direction.Right, "9"),
                ("10", '*', '1', Direction.Right, "11"),
                ("10", '1', '1', Direction.Left, "10"),
                ("11", '_', '_', Direction.NotMove, "12"),
                ("11", '1', '_', Direction.Right, "11"),
                ("11", 'X', '_', Direction.Right, "11"),
                ("11", 'A', '_', Direction.Right, "11"));

        private Dictionary<int, SymbolViewModel> Tape { get; } = new Dictionary<int, SymbolViewModel>();

        public IEnumerable<SymbolViewModel> ScopedTape
        {
            get
            {
                for (int i = machine.Tape.CurrentIndex - 10; i <= machine.Tape.CurrentIndex + 10; i++)
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
                    Tape[index] = new SymbolViewModel { Symbol = machine.Tape[index] };
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
                machine.MoveNext();
                this[machine.Tape.CurrentIndex - 1].IsCurrent = false;
                this[machine.Tape.CurrentIndex - 1].Symbol = machine.Tape[machine.Tape.CurrentIndex - 1];
                this[machine.Tape.CurrentIndex].IsCurrent = true;
                this[machine.Tape.CurrentIndex].Symbol = machine.Tape.Current;
                this[machine.Tape.CurrentIndex + 1].IsCurrent = false;
                this[machine.Tape.CurrentIndex + 1].Symbol = machine.Tape[machine.Tape.CurrentIndex + 1];
                RaisePropertyChanged(nameof(ScopedTape));
            });
        }

        public DelegateCommand MoveCommand { get; }
    }
}
