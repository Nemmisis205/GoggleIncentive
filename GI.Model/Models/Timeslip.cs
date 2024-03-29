﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public long StartTime { get; set; }
        public long EndTime { get; set; }
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
        public long HoursRan { get; set; }
        public double Efficiency { get; set; }
        public int IncentiveAchieved { get; set; }
        public int? Override { get; set; } = null;
        public string OperatorName { get; set; }
        public string GoggleName { get; set; }
        public string DateString { get; set; }
        public string IncentiveString { get; set; }
        public string StartTimeString { get; set; }
        public string EndTimeString { get; set; }
        public string HoursRanString { get; set; }

        public Timeslip()
        {

        }

        public Timeslip(int id, int operatorId, int goggleId, long sTime, long eTime, int sCounter, int eCounter, int sBox, int sPiece, int eBox, int ePiece, int cycleCount,
                        int goodParts, double cyclePerHour,  int scrap, double scrapPercent, long hoursRan, double efficiency, int incentiveAchieved, int overrider)
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
    }
}
