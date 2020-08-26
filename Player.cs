using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGame
{
     public class Player : Character
     {
          public int totDisx { get; set; }
          public int totDisy { get; set; }
          public bool ActionTimer { get; set; }
          //Player Constuctor
          public Player()
          {
               type = CharType.player;
               imagePath = System.Environment.CurrentDirectory + @"\images\Characters\MainChar\WalkDown.png";
               walkDownPath = System.Environment.CurrentDirectory + @"\images\Characters\MainChar\WalkDown.png";
               walkUpPath = System.Environment.CurrentDirectory + @"\images\Characters\MainChar\WalkUp.png";
               walkRightPath = System.Environment.CurrentDirectory + @"\images\Characters\MainChar\WalkRight.png";
               walkLeftPath = System.Environment.CurrentDirectory + @"\images\Characters\MainChar\WalkLeft.png";
               x = 0;
               y = 0;
               totDisx = 0;
               totDisy = 0;
               width = 16;
               height = 32;
               walkSpeed = 10;
               dir = Direction.Down;
               health = 100;
               ActionTimer = false;
          }


          //Walk Function
               //changes x and y values based on direction player is facing and the player walkspeed value
          public void Walk(Direction walkDir)
          {
               switch (walkDir)
               {
                    case Direction.Down:
                         dir = Direction.Down;
                         imagePath = walkDownPath;
                         break;
                    case Direction.Up:
                         dir = Direction.Up;
                         imagePath = walkUpPath;
                         break;
                    case Direction.Left:
                         dir = Direction.Left;
                         imagePath = walkLeftPath;
                         break;
                    case Direction.Right:
                         dir = Direction.Right;
                         imagePath = walkRightPath;
                         break;
                    default:
                         break;
               }
          }
     }
}
