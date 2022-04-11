using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.Models
{
    [Serializable]
    internal class Check
    {
        protected DateTime timeOfCheck;
        protected List<double> weights;
        protected List<double> diametersMin;
        protected List<double> diametersMax;
        protected string productCode;
        protected string lotCode;

        protected List<Defect> defects;
        private int checkNumber;
        public Check()
        {

        }
        public Check(Range workingRange,  int checkNumber)
        {
            this.weights = new List<double>();
            this.diametersMin = new List<double>();
            this.diametersMax = new List<double>();
            this.defects = new List<Defect>();
            this.checkNumber = checkNumber;


            this.timeOfCheck = Convert.ToDateTime(workingRange.Cells[2,8].Value);
           // this.huNumber = Convert.ToDouble((workingRange.Cells[1, 1].Value)).ToString();
            this.productCode = (string)(workingRange.Cells[2, 1]).Value;
            this.lotCode = (string)(workingRange.Cells[5, 1]).Value;

            //weights
            for (int i = 5; i <= 14; i++)
            {
                this.weights.Add(Convert.ToDouble(workingRange.Cells[i, 8].Value));
                this.diametersMin.Add(Convert.ToDouble(workingRange.Cells[i,10 ].Value));
                this.diametersMax.Add(Convert.ToDouble(workingRange.Cells[i, 11].Value));

            }

            //defects
            for (int i = 2; i <= 9; i++)
            {
                    this.defects.Add(new Defect((string)(workingRange.Cells[i, 14]).Value
                        , Convert.ToInt32(workingRange.Cells[i, 13].Value)));
               
            }


        }

        public DateTime TimeOfCheck { get => timeOfCheck; set => timeOfCheck = value; }
        public List<double> Weights { get => weights; set => weights = value; }
        public List<double> DiametersMin { get => diametersMin; set => diametersMin = value; }
        public List<double> DiametersMax { get => diametersMax; set => diametersMax = value; }
        public string ProductCode { get => productCode; set => productCode = value; }
        public string LotCode { get => lotCode; set => lotCode = value; }
        public List<Defect> Defects { get => defects; set => defects = value; }
        public int CheckNumber { get => checkNumber; set => checkNumber = value; }

        public double GetAverageWeight()
        {
            int divisor = 10;
            foreach (double item in weights)
            {
                if (item <= 0)
                {
                    divisor--;
                }
            }
            return divisor > 0 ? weights.Sum() / divisor : 0;
        }
        public double GetAverageDiameter()
        {

            return (diametersMin.Sum() + diametersMax.Sum()) / 20;
        }

    }
}
