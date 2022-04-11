using DataCollector.Models.DataModels;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.Models
{
    [Serializable]
    internal class ProductCheck
    {
        string productCode;
        DateTime date;
        string checkedBy;
        double weightRangeMin, weightRangeMax;
        double diameterRangeMin, diameterRangeMax;
        List<Check> checks;

        private const int pageShift = 16;
        private  int year;
        private  int month;
        private  int day;
        public ProductCheck()
        {

        }
        public ProductCheck(Worksheet excelWorksheet,int year, int month,int day)
        {
            //   Excel.Range usedRange = excelWorkbookWorksheet.UsedRange;
            // var cellValue = (string)(excelWorksheet.Range[2, 3] as Excel.Range).Value;
            Range productInfoRange = excelWorksheet.Range["A1:O8"];


            productCode = (string)(productInfoRange.Cells[6,4].Value);
            date = Convert.ToDateTime((productInfoRange.Cells[7,3]).Value);
            checkedBy = (string)(productInfoRange.Cells[8,4]).Value;
            weightRangeMin = Convert.ToDouble((productInfoRange.Cells[8,10]).Value);
            weightRangeMax = Convert.ToDouble((productInfoRange.Cells[8,11]).Value);
            diameterRangeMin = Convert.ToDouble((productInfoRange.Cells[8,13]).Value);
            diameterRangeMax = Convert.ToDouble((productInfoRange.Cells[8,15]).Value);
            
          

            checks = new List<Check>();

            
            for (int i = 0; i < 3; i++)
            {
                Range checkRange = excelWorksheet.Range[$"A{10 + i * pageShift}:O{24 + i * pageShift}"];
                try
                {
                    var timeOfCheck = Convert.ToDateTime(checkRange.Cells[2, 8].Value);
                    checks.Add(new Check(checkRange, i));
                }
                catch (Exception ex)
                {

                    
                } 
            }

            this.year = year;
            this.month = month;
            this.day = day;
        }
#if DEBUG
        internal IEnumerable<ValueDate> GetWeightsByProductCode(string productCode,DateTime time)
        {
            List<ValueDate> weights = new List<ValueDate>();
            foreach (Check item in checks)
            {
                weights.Add(new ValueDate(item.CheckNumber,"Weight",item.GetAverageWeight(),item.TimeOfCheck,item.ProductCode,WeightRangeMin,weightRangeMax));
            }
            return weights;
        }
#else
        internal IEnumerable<ValueDate> GetWeightsByProductCode(string productCode)
        {
            List<ValueDate> weights = new List<ValueDate>();
            foreach (Check item in checks)
            {
                weights.Add(new ValueDate(item.CheckNumber,"Weight",item.GetAverageWeight(),new DateTime(year,month,day),item.ProductCode));
            }
            return weights;
        }
#endif

        public string ProductCode { get => productCode; set => productCode = value; }
        public DateTime Date { get => date; set => date = value; }
        public string CheckedBy { get => checkedBy; set => checkedBy = value; }
        public double WeightRangeMin { get => weightRangeMin; set => weightRangeMin = value; }
        public double WeightRangeMax { get => weightRangeMax; set => weightRangeMax = value; }
        public double DiameterRangeMin { get => diameterRangeMin; set => diameterRangeMin = value; }
        public double DiameterRangeMax { get => diameterRangeMax; set => diameterRangeMax = value; }
        public List<Check> Checks { get => checks; set => checks = value; }
        public int Year { get => year; set => year = value; }
        public int Month { get => month; set => month = value; }
        public int Day { get => day; set => day = value; }
    }
}
