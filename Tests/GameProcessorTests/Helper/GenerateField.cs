using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.GameProcessorTests.Helper
{
    public class GenerateField
    {
        public bool[,] generateField(int length, int with, List<Tuple<int, int>> trueIndexs)
        {
            bool[,] costumField = new bool[length, with];

            for (int i = 0; i < length - 1; i++)
            {
                for (int j = 0; j < with - 2; j++)
                {

                    costumField[i, j] = false;
                }
            }

            foreach (Tuple<int, int> item in trueIndexs)
            {
                costumField[item.Item1, item.Item2] = true;
            }

            return costumField;
        }
    }
}
