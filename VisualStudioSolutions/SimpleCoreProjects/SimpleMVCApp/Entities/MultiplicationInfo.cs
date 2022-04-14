using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMVCApp.Entities
{
    public class MultiplicationInfo
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public int Result { get; set; }
    }

    public class MultiplicationTable
    {
        public int Number { get; set; }
        public int HightestMultiplier { get; set; } = 10;

        public List<MultiplicationInfo> Rows { get; set; } = new List<MultiplicationInfo>();

        public void Generate()
        {
            Rows.Clear();

            for(int i=1;i<=HightestMultiplier;i++)
            {
                Rows.Add(new MultiplicationInfo()
                {
                    Number1 = Number,
                    Number2 = i,
                    Result = Number * i
                });
            }
            
        }
    }
}
