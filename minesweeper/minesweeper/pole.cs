using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minesweeper
{
    class pole
    {
        int[,] field;

        //создание массива m на n
        public void init(int i, int j)
        {
            field = new int[i, j];
        }

        bool isBroken (int x, int y)
        {
            bool res = true;

            int xmin = x - 1;
            int ymin = y - 1;

            if (xmin < 0) xmin = 0;
            if (ymin < 0) ymin = 0;

            int xmax = x + 1;
            int ymax = y + 1;

            if (xmax > field.GetLength(0)-1) xmax = field.GetLength(0)-1;
            if (ymax > field.GetLength(1)-1) ymax = field.GetLength(1)-1;

            for (int i = xmin; i <= xmax; i++)
            {
                for (int j = ymin; j <= ymax; j++)
                {
                    if (field[i, j] == 0)
                    {
                        res = false;
                        break;
                    }

                }
                if (res == false)
                    break;
            }
                    return res;
        }

        public void plant_mines(int n)
        {
            Random rng = new Random();

            for(int i = 0; i < n; i++)
            {
                int x = rng.Next(field.GetLength(0));
                int y = rng.Next(field.GetLength(1));

                if (field[x, y] == 9)
                {
                    i--;
                    continue;
                }

                field[x, y] = 9;

                for (int i1 = 0; i1 < field.GetLength(0); i1++)
                {
                    for (int j1 = 0; j1 < field.GetLength(1); j1++)
                        if (field[i1, j1] == 9)
                            if (isBroken(i1, j1) == true)
                            {
                                i--;
                                field[x, y] = 0;
                                break;
                            }
                    if (field[x, y] == 0)
                        break;
                }
            }

        }

        public void calculate()
        {
            //смотрим соседние клетки, считаем мины, сумму пишем в текущую
            for (int i1 = 0; i1 < field.GetLength(0); i1++)
            {
                for (int j1 = 0; j1 < field.GetLength(1); j1++)
                {
                    if (field[i1, j1] == 0)
                    {
                        int xmin = i1 - 1;
                        int ymin = j1 - 1;
                        if (xmin < 0) xmin = 0;
                        if (ymin < 0) ymin = 0;

                        int xmax = i1 + 1;
                        int ymax = j1 + 1;
                        if (xmax > field.GetLength(0) - 1) xmax = field.GetLength(0) - 1;
                        if (ymax > field.GetLength(1) - 1) ymax = field.GetLength(1) - 1;

                        int sum = 0;

                        for (int i = xmin; i <= xmax; i++)
                            for (int j = ymin; j <= ymax; j++)
                            {
                                if (field[i, j] == 9) sum++;
                            }

                        field[i1, j1] = sum;
                    }
                }
            }
        }
        public int getCell(int i, int j)
        {
            return field[i, j];
        }


    }
}
