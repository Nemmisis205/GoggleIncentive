using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GI.Model.Models
{
    public class Timeslip
    {
        public int Id { get; set; }
        public int OperatorId { get; set; }
        public int GoggleId { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public int StartCounter { get; set; }
        public int EndCounter { get; set; }
        public int StartBoxCount { get; set; }
        public int EndBoxCount { get; set; }
        public int StartPieceCount { get; set; }
        public int EndPieceCount { get; set; }
        public double CyclesPerHour { get; set; }
        public int Scrap { get; set; }
        public double ScrapPercent { get; set; }
        public int GoodParts { get; set; }
        public int CycleCount { get; set; }
        public int HoursRan { get; set; }
        public double Efficiency { get; set; }
        public int IncentiveAchieved { get; set; }
        public int Override { get; set; }
        
        public Timeslip()
        {

        }

        public Timeslip(int id, int operatorId, int goggleId, int sTime, int eTime, int sCounter, int eCounter, int sBox, int sPiece, int eBox, int ePiece, int cycleCount,
                        int goodParts, double cyclePerHour,  int scrap, double scrapPercent, int hoursRan, double efficiency, int incentiveAchieved, int overrider)
        {
            Id = id;
            OperatorId = operatorId;
            GoggleId = goggleId;
            StartTime = sTime;
            EndTime = eTime;
            StartCounter = sCounter;
            EndCounter = eCounter;
            StartBoxCount = sBox;
            EndBoxCount = eBox;
            StartPieceCount = sPiece;
            EndPieceCount = ePiece;
            CyclesPerHour = cyclePerHour;
            CycleCount = cycleCount;
            GoodParts = goodParts;
            Scrap = scrap;
            ScrapPercent = scrapPercent;
            HoursRan = hoursRan;
            Efficiency = efficiency;
            IncentiveAchieved = incentiveAchieved;
            Override = overrider;
        }

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
