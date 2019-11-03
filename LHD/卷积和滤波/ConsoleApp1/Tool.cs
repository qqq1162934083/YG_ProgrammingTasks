using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Tool
    {
        /// <summary>
        /// 使用卷积计算转化图像矩阵得到转化后的矩阵
        /// </summary>
        /// <param name="matrixArray">图像矩阵数组</param>
        /// <param name="template">模板矩阵数组</param>
        /// <returns>返回转化后的图像矩阵</returns>
        public int[,] ConvetToJuanJiResult(int[,] matrixArray, int[,] template,CalculateType calType)
        {
            //计算图像元个数
            int calculateCount = 0;
            //模板的行数和列数
            int templateRowCount = template.GetLength(0);
            int templateColumnCount = template.GetLength(1);
            //矩阵的行数和列数
            int rowCount = matrixArray.GetLength(0);
            int columnCount = matrixArray.GetLength(1);

            int[,] result = new int[rowCount, columnCount];

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    int[,] templateResult = ExtractMatrixByTemplate(matrixArray, (templateRowCount - 1) / 2, (templateColumnCount - 1) / 2, i, j);
                    switch (calType)
                    {
                        case CalculateType.FourAreas:
                            result[i, j] = GetFourAreasResult(template, templateResult);
                            break;

                        case CalculateType.EightAreas:
                            result[i, j] = GetEightAreasResult(template, templateResult);
                            break;
                    }
                    
                    calculateCount++;
                    Console.WriteLine("计算中......数量:" + calculateCount);

                }
            }
            Console.WriteLine();

            return result;
        }

        /// <summary>
        /// 两模板计算得到中心值
        /// </summary>
        /// <param name="matrixA">模板A</param>
        /// <param name="matrixB">模板B</param>
        /// <returns>返回计算值</returns>
        public int GetEightAreasResult(int[,] matrixA, int[,] matrixB)
        {
            int result = 0;
            //两模板的行数和列数相同
            int templateRowCount = matrixA.GetLength(0);
            int templateColumnCount = matrixA.GetLength(1);
            for (int i = 0; i < templateRowCount; i++)
            {
                for(int j = 0; j < templateColumnCount; j++)
                {
                    result += matrixA[i, j] * matrixB[i, j];
                }
            }
            return result;
        }


        public int GetFourAreasResult(int[,] matrixA, int[,] matrixB)
        {
            int result = 0;

            //上下
            result += matrixA[0, 1] * matrixB[0, 1];
            result += matrixA[2, 1] * matrixB[2, 1];
            //中间
            result += matrixA[1, 1] * matrixB[1, 1];
            //左右
            result += matrixA[1, 0] * matrixB[1, 0];
            result += matrixA[1, 2] * matrixB[1, 2];
            
            return result;
        }


        /// <summary>
        /// 通过模板从图像矩阵中提取对应大小的用于计算的矩阵块
        /// </summary>
        /// <param name="matrixArray">图像矩阵</param>
        /// <param name="rowDirectionCount">模板在行方向上的扩展数(扩展列数)，例：3*4矩阵中，该数值为4</param>
        /// <param name="columnDirectionCount">模板在列方向上的扩展数(扩展行数)，例：3*4矩阵中，该数值为3</param>
        /// <param name="rowIndex">当前数在图像矩阵二维数组中的第一位Index值</param>
        /// <param name="columnIndex">当前数在图像矩阵二维数组中的第二位Index值</param>
        /// <returns>返回提取的矩阵数组</returns>
        public int[,] ExtractMatrixByTemplate(int[,] matrixArray, int rowDirectionCount, int columnDirectionCount,int rowIndex,int columnIndex)
        {
            //模板的行数和列数
            int templateRowCount = 2 * rowDirectionCount + 1;
            int templateColumnCount = 2 * columnDirectionCount + 1;

            //待输出的结果
            int[,] result = new int[templateRowCount, templateColumnCount];

            //待提取矩阵的行数和列数
            int rowCount = matrixArray.GetLength(0);
            int columnCount = matrixArray.GetLength(1);

            //模板中心坐标
            int templateRowIndex = rowDirectionCount;
            int templateColumnIndex = columnDirectionCount;


            for (int i = 0; i < templateRowCount; i++)
            {
                for (int j = 0; j < templateColumnCount; j++)
                {
                    //与中心点的偏移量
                    int rowOffset = templateRowIndex - i;
                    int columnOffset = templateColumnIndex - j;
                    //映射到待提取的矩阵中的坐标
                    int x = rowIndex - rowOffset;
                    int y = columnIndex - columnOffset;
                    if(x<0||x> rowCount - 1 || y < 0 || y > columnCount - 1)
                    {
                        result[i, j] = 0;
                    }
                    else
                    {
                        result[i, j] = matrixArray[x, y];
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 打印矩阵到控制台
        /// </summary>
        /// <param name="matrixArray">矩阵二维数组</param>
        /// <param name="gridNumber">打印每一个数所分配的格数</param>
        public void PrintMatrix(int[,] matrixArray, int gridNumber = 5)
        {
            int rowCount = matrixArray.GetLength(0);
            int columnCount = matrixArray.GetLength(1);
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    int num = matrixArray[i, j];
                    //数字所占位数
                    int digits = GetDigits(num);
                    if (gridNumber < digits + 2) { throw new Exception(string.Format("您为每个数字所分配的格数太少了,你所分配的格数是{0},当前遍历的数{1}的位数是{2}，打印需要占据{3}({4}+2)格，当前分配的格数不够导致此异常发生",gridNumber,num,digits,digits+2,digits)); }
                    //打印空位
                    for (int n = 0; n < gridNumber - digits - 1; n++)
                    {
                        Console.Write(" ");
                    }
                    if (num > 0) { Console.Write(" "); }
                    //打印数字
                    Console.Write(num);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("按下任意键以继续");
            Console.ReadKey();
        }

        /// <summary>
        /// 获取一个整数所占位数
        /// </summary>
        /// <param name="num">待操作整数</param>
        /// <returns></returns>
        public int GetDigits(int num)
        {
            int digits = 0;
            if (num < 0)
            {
                num = -num;
            }
            while (num!=0)
            {
                digits++;
                num /= 10;
            }
            return digits;
        }
    }
}
