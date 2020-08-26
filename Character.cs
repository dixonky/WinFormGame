using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FGame
{
     //Enum of all character types
     public enum CharType
     {
          player, enemy, goblin
     }

     public class Character: Object
     {
          //Character specific values
          public CharType type { get; set; }
          public string walkUpPath { get; set; }
          public string walkDownPath { get; set; }
          public string walkRightPath { get; set; }
          public string walkLeftPath { get; set; }
          public int walkSpeed { get; set; }
          public int runSpeed { get; set; }
          public Direction dir { get; set; }
          public int visionCone { get; set; }


          //Character constructor
          public Character()
          {
           
          }


          //Random Walk Function
               //previously generated int (0-3) sets the direction of movement
               //walk speed changes the x and y coordinates appropriately
          public void RandomWalk(int move)
          {
               switch (move)
               {
                    case 0:
                         y += walkSpeed;
                         dir = Direction.Down;
                         imagePath = walkDownPath;
                         break;
                    case 1:
                         y -= walkSpeed;
                         dir = Direction.Up;
                         imagePath = walkUpPath;
                         break;
                    case 2:
                         x -= walkSpeed;
                         dir = Direction.Left;
                         imagePath = walkLeftPath;
                         break;
                    case 3:
                         x += walkSpeed;
                         dir = Direction.Right;
                         imagePath = walkRightPath;
                         break;
                    default:
                         break;
               }
          }
     }
}
