using Prism.Commands;
using Prism.Mvvm;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marimo.TuringMachineViewer.Uwp.ViewModels
{
    public class SymbolViewModel : BindableBase
    {
        bool isCurrent;
        public bool IsCurrent
        {
            get => isCurrent;
            set => SetProperty(ref isCurrent, value);
        }

        char symbol;
        public char Symbol
        {
            get => symbol;
            set => SetProperty(ref symbol, value);
        }
    }
}
