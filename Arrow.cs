using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FGame
{
     public class Arrow : Item
     {
          //Arrow specific values
          int flyDistance = 300;
          int flySpeed = 40;
          int flyStart = 0;
          int damage = 10;
          bool stuck = false;


          //Arrow constructor
               //direction of movement and location coors are passed in based on player location and direction
          public Arrow(Direction dir, int xloc, int yloc)
          {
               //Currently the arrow image is a simple rectangle so only two images are needed
               actionTime = 750;
               if (dir == Direction.Left || dir == Direction.Right)
               {
                    imagePath = System.Environment.CurrentDirectory + @"\images\Items\ArrowLR.png";
                    width = 9;
                    height = 2;
                    widthHB = 9;
                    heightHB = 2;
               }
               else
               {
                    imagePath = System.Environment.CurrentDirectory + @"\images\Items\ArrowUD.png";
                    width = 2;
                    height = 9;
                    widthHB = 2;
                    heightHB = 9;
               }
               
               x = xloc;
               y = yloc;
               //Arrow begins in flight
               grounded = false;
               setHitbox();
          }


          //Fly function
               //direction of movement and lists of all potential obstacles are passed in
          public void Fly(Direction dir, List<Environment> envi, List<Enemy> enemies)
          {
               //FlyStart holds the total distance covered
               flyStart += flySpeed;
               //Compare distance traveled to total possible distance, if surpassed ground the arrow
               if (flyStart > flyDistance) { grounded = true; return; };
               //Check if the arrow has hit and object and is no longer traveling
               if (stuck) return;
               //Change coordinated depending on direction of flight
               switch (dir)
               {
                    case Direction.Left:
                         x -= flySpeed;
                         //Call wait to illustrate arrow speed
                         CommonMethods.wait(150);
                         //Check if the arrow has hit an obstacle
                         CheckHit(dir, envi, enemies);
                         //Recursively call fly function to continue the arrow's flight
                         Fly(dir, envi, enemies);
                         break;
                    case Direction.Right:
                         x += flySpeed;
                         CommonMethods.wait(150);
                         CheckHit(dir, envi, enemies);
                         Fly(dir, envi, enemies);
                         break;
                    case Direction.Down:
                         y += flySpeed;
                         CommonMethods.wait(150);
                         CheckHit(dir, envi, enemies);
                         Fly(dir, envi, enemies);
                         break;
                    case Direction.Up:
                         y -= flySpeed;
                         CommonMethods.wait(150);
                         CheckHit(dir, envi, enemies);
                         Fly(dir, envi, enemies);
                         break;
                    default:
                         break;
               }
          }


          //Check Hit function
               //direction of movement and lists of all potential obstacles are passed in
               //generate path of arrow for this section of flight and compare to obstacle hitboxes
          public void CheckHit(Direction dir, List<Environment> envi, List<Enemy> enemies)
          {
               setHitbox();
               bool hit = false;
               foreach (Enemy en in enemies)
               {
                    Point enUL = new Point();
                    enUL.X = en.hitbox[1][0];
                    enUL.Y = en.hitbox[1][1];
                    Point enLR = new Point();
                    enLR.X = en.hitbox[4][0];
                    enLR.Y = en.hitbox[4][1];
                    Rectangle enBody = CommonMethods.GetRectangle(enUL, enLR);
                    Point arrowLineStart = new Point();
                    Point arrowLineEnd = new Point();
                    arrowLineEnd.X = x;
                    arrowLineEnd.Y = y;
                    switch (dir)
                    {
                         case Direction.Left:
                              arrowLineStart.X = x + flySpeed;
                              arrowLineStart.Y = y;
                              hit = CommonMethods.LineIntersectsRect(arrowLineStart, arrowLineEnd, enBody);
                              break;
                         case Direction.Right:
                              arrowLineStart.X = x - flySpeed;
                              arrowLineStart.Y = y;
                              hit = CommonMethods.LineIntersectsRect(arrowLineStart, arrowLineEnd, enBody);
                              break;
                         case Direction.Down:
                              arrowLineStart.X = x;
                              arrowLineStart.Y = y - flySpeed;
                              hit = CommonMethods.LineIntersectsRect(arrowLineStart, arrowLineEnd, enBody);
                              break;
                         case Direction.Up:
                              arrowLineStart.X = x;
                              arrowLineStart.Y = y + flySpeed;
                              hit = CommonMethods.LineIntersectsRect(arrowLineStart, arrowLineEnd, enBody);
                              break;
                         default:
                              break;
                    }
                    //If the arrow intersects with an enemy take the arrow damage from enemy health
                         //stick the arrow in the enemy and change to grounded
                    if (hit)
                    {
                         en.health -= damage;
                         stuck = true;
                         grounded = true;
                    }
               }
               //Seperate check for envi list to remove easily
               foreach (Environment en in envi)
               {
                    Point enUL = new Point();
                    enUL.X = en.hitbox[1][0];
                    enUL.Y = en.hitbox[1][1];
                    Point enLR = new Point();
                    enLR.X = en.hitbox[4][0];
                    enLR.Y = en.hitbox[4][1];
                    Rectangle enBody = CommonMethods.GetRectangle(enUL, enLR);
                    Point arrowLineStart = new Point();
                    Point arrowLineEnd = new Point();
                    arrowLineEnd.X = x;
                    arrowLineEnd.Y = y;
                    switch (dir)
                    {
                         case Direction.Left:
                              arrowLineStart.X = x + flySpeed;
                              arrowLineStart.Y = y;
                              hit = CommonMethods.LineIntersectsRect(arrowLineStart, arrowLineEnd, enBody);
                              break;
                         case Direction.Right:
                              arrowLineStart.X = x - flySpeed;
                              arrowLineStart.Y = y;
                              hit = CommonMethods.LineIntersectsRect(arrowLineStart, arrowLineEnd, enBody);
                              break;
                         case Direction.Down:
                              arrowLineStart.X = x;
                              arrowLineStart.Y = y - flySpeed;
                              hit = CommonMethods.LineIntersectsRect(arrowLineStart, arrowLineEnd, enBody);
                              break;
                         case Direction.Up:
                              arrowLineStart.X = x;
                              arrowLineStart.Y = y + flySpeed;
                              hit = CommonMethods.LineIntersectsRect(arrowLineStart, arrowLineEnd, enBody);
                              break;
                         default:
                              break;
                    }
                    if (hit)
                    {
                         stuck = true;
                         grounded = true;
                         continue;
                    }
               }
          }
     }
}
