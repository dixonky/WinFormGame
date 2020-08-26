using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGame
{
     public class Tree: Environment
     {
          //Tree constructor
          public Tree(int xloc, int yloc, int size)
          {
               if (size == 1)
               {
                    imagePath = System.Environment.CurrentDirectory + @"\images\Environment\Tree\Tree.png";
                    width = 30;
                    height = 90;
                    widthHB = 25;
                    heightHB = 80;
                    x = xloc;
                    y = yloc;
                    health = 100;
               }
               else
               {
                    imagePath = System.Environment.CurrentDirectory + @"\images\Environment\Tree\Tree.png";
                    width = 40;
                    height = 120;
                    widthHB = 35;
                    heightHB = 110;
                    x = xloc;
                    y = yloc;
                    health = 100;
               }
          }
     }
}
