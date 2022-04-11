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
    internal class CheckYear
    {
        string yearFolderPath;
        int year;
        List<CheckMonth> checkMonths;
        public CheckYear()
        {

        }
        public CheckYear(string yearFolderPath, int year)
        {
            this.yearFolderPath = yearFolderPath;
            this.year = year;
            checkMonths = new List<CheckMonth>();
            GetMonths();
        }

        public string YearFolderPath { get => yearFolderPath; set => yearFolderPath = value; }
        public int Year { get => year; set => year = value; }
        public List<CheckMonth> CheckMonths { get => checkMonths; set => checkMonths = value; }

        private void GetMonths()
        {
            for (int i = 1; i <= 12; i++)
            {
                string month = i < 10 ? "0" + i : i.ToString();
                string monthPath = yearFolderPath + $@"\{month}-{year}";
                if (Directory.Exists(monthPath))
                {
                    checkMonths.Add(new CheckMonth(monthPath, year, i));
                }
                else
                {
                    monthPath = yearFolderPath + $@"\{GetMonthByNumber(i)} {year}";
                    if (Directory.Exists(monthPath))
                    {
                        checkMonths.Add(new CheckMonth(monthPath, year, i));
                    }
                }
            }

        }
        private string GetMonthByNumber(int month)
        {
            string result = "";

            switch (month)
            {
                case 1:
                    return "January";
                case 2:
                    return "February";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                case 12:
                    return "December";
                default:
                    return result;
            }

        }
        internal List<ValueDate> GetWeightsByProductCode(string productCode)
        {
            List<ValueDate> weights = new List<ValueDate>();
            foreach (CheckMonth item in checkMonths)
            {
                weights.AddRange(item.GetWeightsByProductCode(productCode));
            }
            return weights;
        }
    }
}
