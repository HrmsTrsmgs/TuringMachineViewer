using Marimo.TuringMachineViewer.TuringMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Marimo.TuringMachineViewer.Test.TuringMachine
{

    public class TuringMachineのテスト
    {
        [Fact]
        public void 足し算ができます()
        {
            var machine =
                new Machine("_",
                    "1",
                    "111_11".ToCharArray().Select(x => x.ToString()),
                    ("1", "1", "1", Direction.Right, "1"),
                    ("1", "_", "1", Direction.Right, "2"),
                    ("2", "1", "1", Direction.Right, "2"),
                    ("2", "_", "_", Direction.Left, "3"),
                    ("3", "1", "_", Direction.NotMove, "4"));

            while (machine.MoveNext()) ;

            machine.Tape.ToString().Is("11111_");
        }

        [Fact]
        public void 掛け算ができます()
        {
            var machine =
                new Machine("_",
                "1",
                "111X11".ToCharArray().Select(x => x.ToString()),
                ("1", "1", "1", Direction.Left, "2"),
                ("2", "_", "*", Direction.Right, "3"),
                ("3", "_", "_", Direction.Left, "4"),
                ("3", "*", "*", Direction.Right, "3"),
                ("3", "1", "1", Direction.Right, "3"),
                ("3", "X", "X", Direction.Right, "3"),
                ("3", "A", "A", Direction.Right, "3"),
                ("4", "1", "_", Direction.Left, "5"),
                ("4", "X", "X", Direction.Left, "10"),
                ("5", "1", "1", Direction.Left, "5"),
                ("5", "X", "X", Direction.Left, "6"),
                ("6", "*", "*", Direction.Right, "9"),
                ("6", "1", "A", Direction.Left, "7"),
                ("6", "A", "A", Direction.Left, "6"),
                ("7", "_", "1", Direction.Right, "8"),
                ("7", "*", "*", Direction.Left, "7"),
                ("7", "1", "1", Direction.Left, "7"),
                ("8", "*", "*", Direction.Right, "8"),
                ("8", "1", "1", Direction.Right, "8"),
                ("8", "X", "X", Direction.Left, "6"),
                ("8", "A", "A", Direction.Right, "8"),
                ("9", "_", "_", Direction.Left, "4"),
                ("9", "1", "1", Direction.Right, "9"),
                ("9", "X", "X", Direction.Right, "9"),
                ("9", "A", "1", Direction.Right, "9"),
                ("10", "*", "1", Direction.Right, "11"),
                ("10", "1", "1", Direction.Left, "10"),
                ("11", "_", "_", Direction.NotMove, "12"),
                ("11", "1", "_", Direction.Right, "11"),
                ("11", "X", "_", Direction.Right, "11"),
                ("11", "A", "_", Direction.Right, "11"));

            while (machine.MoveNext()) ;

            machine.Tape.ToString().Is("1111111_____");
        }
    }
}
