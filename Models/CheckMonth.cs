using DataCollector.Models.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.Models
{
    [Serializable]
    internal class CheckMonth
    {
        List<CheckDay> checkDays;
        private string monthPath;
        private int month;
        private int year;
        public CheckMonth()
        {

        }
        public CheckMonth(string monthPath, int year, int month)
        {
            this.monthPath = monthPath;
            this.month = month;
            this.year = year;
            checkDays = new List<CheckDay>();
            GetDays();
        }

        public string MonthPath { get => monthPath; set => monthPath = value; }
        public int Month { get => month; set => month = value; }
        public List<CheckDay> CheckDays { get => checkDays; set => checkDays = value; }
        public int Year { get => year; set => year = value; }

        private void GetDays()
        {
            int daysInMonth = DateTime.DaysInMonth(year,month);
            string sMonth = month < 10 ? "0" + month : month.ToString();
            for (int i = 1; i <= daysInMonth; i++)
            {
                string day = i < 10 ? "0" + i : i.ToString();
                
                string dayPath = monthPath + $@"\{sMonth}-{day}-{year-2000}";
                if (Directory.Exists(dayPath))
                {
                    checkDays.Add(new CheckDay(dayPath,year,month,i));
                }
            }

        }
       

        internal List<ValueDate> GetWeightsByProductCode(string productCode)
        {
            List<ValueDate> weights = new List<ValueDate>();
            foreach (CheckDay item in checkDays)
            {
                weights.AddRange(item.GetWeightsByProductCode(productCode));
            }
            return weights;
        }
    }
}
