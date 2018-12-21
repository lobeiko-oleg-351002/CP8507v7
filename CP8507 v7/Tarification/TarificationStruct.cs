using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace CP8507_v7
{
    [Serializable]
    public class TarifStruct
    {
        public int IntervalsActivated;
        public TimeInterval[] Interval;

        [Serializable]
        public struct TimeInterval
        {
            public TimeSpan Start;
            public TimeSpan End;
        }

        public TarifStruct()
        {
            IntervalsActivated = 0;
            Interval = new TimeInterval[48];
            for (int i = 0; i < Interval.Length; i++) Interval[i] = new TimeInterval();
        }
    }

    [Serializable]
    public class Day
    {
        public TarifStruct[] Tarif;

        public Day()
        {
            Tarif = new TarifStruct[8];
            for (int i = 0; i < Tarif.Length; i++) Tarif[i] = new TarifStruct();
        }
    }

    [Serializable]
    public class FixedDay : Day
    {
        public bool DayActivated;
        public DateTime Date;

        public FixedDay()
        {
            DayActivated = false;
            Date = new DateTime();
        }
    }

    [Serializable]
    public class SeasonStruct
    {
        public bool SeasonActivated;
        public int SeasonNumber;
        public DateTime StartDT;
        public DateTime EndDT;

        public Day WorkDays;
        public Day Sunday;
        public Day Saturday;
        public FixedDay[] FixedDays;

        public SeasonStruct()
        {
            SeasonActivated = false;
            StartDT = new DateTime();
            EndDT = new DateTime();
            WorkDays = new Day();
            Sunday = new Day();
            Saturday = new Day();
            FixedDays = new FixedDay[25];
            for (int i = 0; i < FixedDays.Length; i++) FixedDays[i] = new FixedDay();
        }
    }

     [Serializable]
    public struct RXTXInterval
    {
        public char Start;
        public char End;
        public char Tarif;
    }

     [Serializable]
    public struct RXTXDay
    {
        public RXTXInterval[] Intervals;
        public char IntervalsActivated;
    }

     [Serializable]
    public struct RXTXFixedDate
    {
        public bool Activated;
        public char Day;
        public char Month;
        public RXTXInterval[] Intervals;
        public char IntervalsActivated;
    };

     [Serializable]
    public struct SeasonRXTXStruct
    {
        public char StartDay;
        public char StartMonth;
        public char EndDay;
        public char EndMonth;

        public RXTXDay WorkDays;
        public RXTXDay Sunday;
        public RXTXDay Saturday;
        public RXTXFixedDate[] FixedDays;
    }
}
