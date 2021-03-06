﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PineApple
{
    public class MDate
    {
        int _dayNumber;
        int _hours;
        int _minutes;
        public MDate()
        {
            _dayNumber = 0;
            _hours = 0;
            _minutes = 0;
        }
        public MDate(int dayNumber, int hours, int minutes)
        {
            _dayNumber = dayNumber;
            _hours = hours;
            _minutes = minutes;
        }
        public override string ToString()
        {
            return string.Format("{0} {1} {2}", _dayNumber, _hours, _minutes);
        }
        public string printMDate()
        {
            return string.Format("Jour:{0} Heure {1}:{2}", _dayNumber, _hours, _minutes);
        }
        public int getDay()
        {
            return _dayNumber;
        }
        public int getHours()
        {
            return _hours;
        }
        public int getMinutes()
        {
            return _minutes;
        }
        public void ParseMDate(string mdateString)
        {
            string[] mdateTab = mdateString.Split(' ');
            _dayNumber=int.Parse(mdateTab[0]);
            _hours = int.Parse(mdateTab[1]);
            _minutes = int.Parse(mdateTab[2]);
        }
        public static bool operator <(MDate test, MDate liste)
        {
            int t = (24 * 60 + 40) * test.getDay() + test.getHours() * 60 + test.getMinutes();
            int l = (24 * 60 + 40) * liste.getDay() + liste.getHours() * 60 + liste.getMinutes();
            return t < l ? true :false;
        }
        public static bool operator >(MDate test, MDate liste)
        {
            int t = (24 * 60 + 40) * test.getDay() + test.getHours() * 60 + test.getMinutes();
            int l = (24 * 60 + 40) * liste.getDay() + liste.getHours() * 60 + liste.getMinutes();
            return t > l ? true : false;
        }
        public static bool operator <=(MDate test, MDate liste)
        {
            int t = (24 * 60 + 40) * test.getDay() + test.getHours() * 60 + test.getMinutes();
            int l = (24 * 60 + 40) * liste.getDay() + liste.getHours() * 60 + liste.getMinutes();
            return t <= l? true : false;
        }
        public static bool operator >=(MDate test, MDate liste)
        {
            int t = (24 * 60 + 40) * test.getDay() + test.getHours() * 60 + test.getMinutes();
            int l = (24 * 60 + 40) * liste.getDay() + liste.getHours() * 60 + liste.getMinutes();
            return t>= l ? true : false;
        }
    }
}
