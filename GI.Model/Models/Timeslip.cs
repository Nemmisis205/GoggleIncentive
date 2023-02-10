using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GI.Model.Models
{
    public class Timeslip
    {
        int Id { get; set; }
        int OperatorId { get; set; }
        int GoggleId { get; set; }
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
        int StartCounter { get; set; }
        int EndCounter { get; set; }
        int StartBoxCount { get; set; }
        int EndBoxCount { get; set; }
        int StartPieceCount { get; set; }
        int EndPieceCount { get; set; }
        double CyclesPerHour { get; set; }
        int Scrap { get; set; }
        double ScrapPercent { get; set; }
        int PieceCount { get; set; }
        int CycleCount { get; set; }
        TimeSpan HoursRan { get; set; }
        double Efficiency { get; set; }
        bool IncentiveAchieved { get; set; }
        bool Override { get; set; }


        //public Timeslip(DateTime sTime, DateTime eTime, int sCount, int eCount, int sBox, int eBox, int sPiece, int ePiece, int boxCapacity, int quote)
        //{
        //    StartTime = sTime;
        //    EndTime = eTime;
        //    StartCounter = sCount;
        //    EndCounter = eCount;
        //    StartBoxCount = sBox;
        //    EndBoxCount = eBox;
        //    StartPieceCount = sPiece;
        //    EndPieceCount = ePiece;
        //    //I probably want to add a separate Date field. I might be able to get clever with sTime and eTime to pull that information. If not, no loss.

        //    HoursRan = EndTime - StartTime;
        //    PieceCount = EndBoxCount * boxCapacity + EndPieceCount - (StartBoxCount * boxCapacity + StartPieceCount);
        //    CycleCount = eCount - sCount;

        //    CyclesPerHour = Math.Round((EndCounter - StartCounter) / HoursRan.TotalHours, 2);
        //    Scrap = CycleCount - PieceCount;
        //    ScrapPercent = Math.Round((double)Scrap / CycleCount * 100, 2);
        //    Efficiency = Math.Round(CyclesPerHour / quotePerHour * 100, 2);

        //    if (Efficiency >= 78)
        //    {
        //        IncentiveAchieved = true;
        //    }
        //    else { IncentiveAchieved = false; }
        //}
    }
}
