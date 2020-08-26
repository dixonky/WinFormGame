using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGame
{
     public abstract class Object
     {
          //All objects have these values
          public string imagePath { get; set; }
          public int x { get; set; }
          public int y { get; set; }
          public int width { get; set; }
          public int height { get; set; }
          public int widthHB { get; set; }
          public int heightHB { get; set; }
          public SortedDictionary<int, int[]> hitbox { get; set; }
          public int health { get; set; }


          //Object constructor
               //initialize hitbox
          public Object()
          {
               hitbox = new SortedDictionary<int, int[]>();
          }


          //Set Hitbox Function
               //creates rectangle points using the x and y coordinates with the width and height values
               //point 1 = Upper Left, 2 = Upper Right, 3 = Lower Left, 4 = Lower Right
          public void setHitbox()
          {
               if (hitbox.Count > 0) { hitbox.Clear(); }
               hitbox.Add(1, new[] { x, y });
               hitbox.Add(2, new[] { x + width, y });
               hitbox.Add(3, new[] { x, y + height });
               hitbox.Add(4, new[] { x + width, y + height });
          }
     }
}
