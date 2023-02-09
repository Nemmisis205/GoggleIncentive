using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoggleIncentive.Classes
{
    class Timeslip
    {
        DateTime startTime { get; set; }
        DateTime endTime { get; set; }
        int startCounter { get; set; }
        int endCounter { get; set; }
        int startBoxCount { get; set; }
        int endBoxCount { get; set; }
        int startPieceCount { get; set; }
        int endPieceCount { get; set; }
        double gogglesPerHour { get; set; }
        int scrap { get; set; }
        double scrapPercent { get; set; }
        int boxedGoggles { get; set; }
        int cycleCount { get; set; }
        TimeSpan hoursRan { get; set; }
        double percentOfQuote { get; set; }
        bool incentiveAchieved { get; set; }
        int quotePerHour { get; set; }


        public Timeslip(DateTime sTime, DateTime eTime, int sCount, int eCount, int sBox, int eBox, int sPiece, int ePiece, int boxCapacity, int quote)
        {
            startTime = sTime;
            endTime = eTime;
            startCounter = sCount;
            endCounter = eCount;
            startBoxCount = sBox;
            endBoxCount = eBox;
            startPieceCount = sPiece;
            endPieceCount = ePiece;
            quotePerHour = quote;
            //I probably want to add a Date field. I might be able to get clever with sTime and eTime to pull that information. If not, no loss.
            //I think I also need an ID field here? That might not be necessary, though, since I'm not going to do stuff with specific timeslips.

            hoursRan = endTime - startTime;
            boxedGoggles = endBoxCount * boxCapacity + endPieceCount - (startBoxCount * boxCapacity + startPieceCount);
            cycleCount = eCount - sCount;

            gogglesPerHour = Math.Round((endCounter - startCounter) / hoursRan.TotalHours, 2);
            scrap = cycleCount - boxedGoggles;
            scrapPercent = Math.Round((double)scrap / cycleCount * 100, 2);
            percentOfQuote = Math.Round(gogglesPerHour / quotePerHour * 100, 2);

            if (percentOfQuote >= 78)
            {
                incentiveAchieved = true;
            }
            else { incentiveAchieved = false; }
        }

        public void Display()
        {
            Console.WriteLine($"Hours Ran: {hoursRan.TotalHours}");
            Console.WriteLine($"Cycle Count: {cycleCount}");
            Console.WriteLine($"Boxed Goggles: {boxedGoggles}");
            Console.WriteLine($"Scrap: {scrap}");
            Console.WriteLine($"Scrap Percent: {scrapPercent}");
            Console.WriteLine($"Goggles per Hour: {gogglesPerHour}");
            Console.WriteLine($"Quoted: {quotePerHour}");
            Console.WriteLine($"Efficiency (% of quote): {percentOfQuote}");
            if (incentiveAchieved == true) { Console.WriteLine("Incentive Achieved"); }
            else { Console.WriteLine("No payout. Under 80% of quote."); }
        }
    }
}
