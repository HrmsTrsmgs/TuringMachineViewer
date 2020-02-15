using Marimo.TuringMachineViewer.TuringMachine;
using System;
using System.Linq;

class Program
{
    static void Main(string[] args)

    {
        var machine =
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
        do
        {
            Console.WriteLine();
            Console.WriteLine(machine.Tape);
            Console.WriteLine(string.Join("", Enumerable.Range(0, machine.Tape.DistanceFromLeftEnd).Select(_ => " ")) + "_");
            Console.WriteLine(machine.StateString);
            Console.WriteLine(machine);
            Console.ReadKey();
        }
        while (machine.MoveNext());

        foreach(var i in Enumerable.Range(1, 5).Reverse())
        {
            Console.WriteLine(i);
            Console.ReadKey();
        }
    }
}