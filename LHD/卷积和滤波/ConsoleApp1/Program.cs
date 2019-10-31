using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Tool tool = new Tool();

            //像素矩阵
            int[,] matrixArray = new int[,]
            {
                {86,85,87,88,90 },
                {86,87,87,87,85 },
                {88,87,85,86,83 },
                {89,89,86,86,87 },
                {89,90,89,87,87 }
            };

            //模板矩阵
            int[,] template = new int[,]
            {
                {-1,-1,-1 },
                {-1, 8,-1 },
                {-1,-1,-1 }
            };

            Console.WriteLine("像素矩阵：");
            Console.WriteLine();
            tool.PrintMatrix(matrixArray);
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("模板矩阵：");
            Console.WriteLine();
            tool.PrintMatrix(template);
            Console.WriteLine();
            Console.WriteLine();

            int[,] resultMatrixArray = tool.ConvetToJuanJiResult(matrixArray, template);

            Console.WriteLine("计算结果：");
            Console.WriteLine();
            tool.PrintMatrix(resultMatrixArray);
            Console.WriteLine();
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
