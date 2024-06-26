using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giatrican.Handle
{
    public class GenaricData
    {
        public static int[,] GetData(int check)
        {
            int[,] matrix = new int[6, 6]; 

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    matrix[i, j] = i * 6 + j + 1; 
                }
            }
            return matrix; 
        }
    }
}
