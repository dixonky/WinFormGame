using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGame
{
     class Cliff : Environment
     {
          //Tree constructor
          public Cliff(int xloc, int yloc, int type)
          {
               if (type == 1)
               {
                    imagePath = System.Environment.CurrentDirectory + @"\images\Environment\Cliff\CliffHorizontal.png";
                    width = 100;
                    height = 10;
                    widthHB = 100;
                    heightHB = 10;
                    x = xloc;
                    y = yloc;
                    health = 100;
               }
               else
               {
                    imagePath = System.Environment.CurrentDirectory + @"\images\Environment\Cliff\CliffVert.png";
                    width = 10;
                    height = 100;
                    widthHB = 10;
                    heightHB = 100;
                    x = xloc;
                    y = yloc;
                    health = 100;
               }
               
          }
     }
}
