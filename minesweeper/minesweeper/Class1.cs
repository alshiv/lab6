using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using minesweeper;

namespace minesweeper_test
{
    [TestFixture]
    class Class1
    {
        [TestCase]
        public void init()
        {
             
            cpole p = new cpole();

            p.init(2, 3);

            Assert.AreEqual(2, p.field.GetLength(0) );
            Assert.AreEqual(3, p.field.GetLength(1));

            p.init(0, -3);

        }

        [TestCase]
        public void isbroken()
        {
            cpole p = new cpole();

            p.field = new int[,]
            {
                {  9,  9,  9,  0,  0},
                {  9,  9,  9,  9,  0},
                {  0,  9,  9,  9,  0},
                {  0,  9,  9,  9,  0},
                {  0,  0,  0,  0,  0}
            };

            Assert.AreEqual(true, p.isBroken(0, 0));
            Assert.AreEqual(true, p.isBroken(2, 2));
            Assert.AreEqual(false, p.isBroken(1, 3));
        }

        [TestCase]
        public void plant()
        {
            cpole p = new cpole();

            p.init(5, 5);
            p.plant_mines(10);

            int sum = 0;

            for(int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                {
                    if (p.field[i, j] == 9)
                        sum++;
                }

            Assert.AreEqual(10, sum);

            bool isbroken = false;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (p.field[i, j] == 9)
                        if (p.isBroken(i, j))
                        {
                            isbroken = true;
                            break;
                        }
                }
                if (isbroken == true) break;
            }

            Assert.AreEqual(false, isbroken);
    }
}
