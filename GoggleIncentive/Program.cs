using System;
using System.Globalization;
using GoggleIncentive.Classes;

namespace GoggleIncentive
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime startTime;
            DateTime endTime;
            int sCount;
            int eCount;
            int sBox;
            int eBox;
            int sPiece;
            int ePiece;
            int boxCapacity;
            int quote;

            Console.WriteLine("Welcome to Kohle's New, Incredible Goggle Incentive Calculator");
            Console.Write("Run Date (MM/DD/YY): ");
            string date = Console.ReadLine();
            Console.Write("Start Time (HH:mm): ");
            startTime = DateTime.Parse(date+ " " + Console.ReadLine());
            Console.Write("End Time (HH:mm): ");
            endTime = DateTime.Parse(date + " " + Console.ReadLine());
            Console.Write("Starting Counter: ");
            sCount = int.Parse(Console.ReadLine());
            Console.Write("Ending Counter: ");
            eCount = int.Parse(Console.ReadLine());
            Console.Write("Starting Boxes: ");
            sBox = int.Parse(Console.ReadLine());
            Console.Write("Starting Pieces: ");
            sPiece = int.Parse(Console.ReadLine());
            Console.Write("Ending Boxes: ");
            eBox = int.Parse(Console.ReadLine());
            Console.Write("Ending Pieces: ");
            ePiece = int.Parse(Console.ReadLine());
            Console.Write("Box Capacity: ");
            boxCapacity = int.Parse(Console.ReadLine());
            Console.Write("Quoted Cycles per Hour: ");
            quote = int.Parse(Console.ReadLine());

            Timeslip testees = new Timeslip(startTime, endTime, sCount, eCount, sBox, eBox, sPiece,
               ePiece, boxCapacity, quote);

            testees.Display();

        }
    }
}
