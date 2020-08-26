using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FGame
{
     public class Enemy : Character
     {
          //Enemy Character specific values
          public int sightDist;
          public bool foundPlayer;
          public int attackDamage { get; set; }
          public int attackDistance { get; set; }
          public int attackAccuracy { get; set; }


          //Set initial found player value to false
          public Enemy()
          {
               foundPlayer = false;
          }


          //Find Player Function
               //Called after every random walk character function call
          public void FindPlayer(Character p)
          {
               //Create a rectangle referencing player hitbox
               Point pUL = new Point();
               pUL.X = p.hitbox[1][0];
               pUL.Y = p.hitbox[1][1];
               Point pLR = new Point();
               pLR.X = p.hitbox[4][0];
               pLR.Y = p.hitbox[4][1];
               Rectangle pBody = CommonMethods.GetRectangle(pUL, pLR);
               //Create three points referencing enemy vision cone
               Point eyeLineStart = new Point();
               Point eyeLineEnd1 = new Point();
               Point eyeLineEnd2 = new Point();
               //Starting vision point starts between the two upper hitbox points and down the same value from the top hitbox points
               int eyeMod = (hitbox[2][0] - hitbox[1][0]) / 2;
               int eyeX = hitbox[1][0] + eyeMod;
               int eyeY = hitbox[1][1] + eyeMod;
               eyeLineStart.X = eyeX;
               eyeLineStart.Y = eyeY;
               switch (dir)
               {
                    case Direction.Left:
                         //Vision cone ends in two points, enemy sight distance value away from beginning vision point and spread apart by 2 * vision cone value
                         eyeLineEnd1.X = eyeX - sightDist;
                         eyeLineEnd1.Y = eyeY - visionCone;
                         //Check if player rectange crosses any line which connects vision cone points
                         foundPlayer = CommonMethods.LineIntersectsRect(eyeLineStart, eyeLineEnd1, pBody);
                         if (!foundPlayer)
                         {
                              eyeLineEnd2.X = eyeX - sightDist;
                              eyeLineEnd2.Y = eyeY + visionCone;
                              foundPlayer = CommonMethods.LineIntersectsRect(eyeLineStart, eyeLineEnd2, pBody);
                         }
                         if (!foundPlayer)
                         {
                              foundPlayer = CommonMethods.LineIntersectsRect(eyeLineEnd1, eyeLineEnd2, pBody);
                         }
                         break;
                    case Direction.Right:
                         eyeLineEnd1.X = eyeX + sightDist;
                         eyeLineEnd1.Y = eyeY - visionCone;
                         foundPlayer = CommonMethods.LineIntersectsRect(eyeLineStart, eyeLineEnd1, pBody);
                         if (!foundPlayer)
                         {
                              eyeLineEnd2.X = eyeX + sightDist;
                              eyeLineEnd2.Y = eyeY + visionCone;
                              foundPlayer = CommonMethods.LineIntersectsRect(eyeLineStart, eyeLineEnd2, pBody);
                         }
                         if (!foundPlayer)
                         {
                              foundPlayer = CommonMethods.LineIntersectsRect(eyeLineEnd1, eyeLineEnd2, pBody);
                         }
                         break;
                    case Direction.Down:
                         eyeLineEnd1.X = eyeX - visionCone;
                         eyeLineEnd1.Y = eyeY + sightDist;
                         foundPlayer = CommonMethods.LineIntersectsRect(eyeLineStart, eyeLineEnd1, pBody);
                         if (!foundPlayer)
                         {
                              eyeLineEnd2.X = eyeX + visionCone;
                              eyeLineEnd2.Y = eyeY + sightDist;
                              foundPlayer = CommonMethods.LineIntersectsRect(eyeLineStart, eyeLineEnd2, pBody);
                         }
                         if (!foundPlayer)
                         {
                              foundPlayer = CommonMethods.LineIntersectsRect(eyeLineEnd1, eyeLineEnd2, pBody);
                         }
                         break;
                    case Direction.Up:
                         eyeLineEnd1.X = eyeX - visionCone;
                         eyeLineEnd1.Y = eyeY - sightDist;
                         foundPlayer = CommonMethods.LineIntersectsRect(eyeLineStart, eyeLineEnd1, pBody);
                         if (!foundPlayer)
                         {
                              eyeLineEnd2.X = eyeX + visionCone;
                              eyeLineEnd2.Y = eyeY - sightDist;
                              foundPlayer = CommonMethods.LineIntersectsRect(eyeLineStart, eyeLineEnd2, pBody);
                         }
                         if (!foundPlayer)
                         {
                              foundPlayer = CommonMethods.LineIntersectsRect(eyeLineEnd1, eyeLineEnd2, pBody);
                         }
                         break;
                    default:
                         break;
               }
          }

          //Triggered when player char is found by enemy
               //enemy char is moved by reference to player hitbox
               //running speed is used for movement value
          public virtual void runTowardsPlayer(Character p)
          {
               //f val is used to change direction value only once
               bool f = false;
               int run = Math.Abs(p.hitbox[1][0] - hitbox[1][0]);
               int rise = Math.Abs(p.hitbox[1][1] - hitbox[1][1]);
               if (run > rise) f = true;
               if (p.hitbox[1][0] > hitbox[1][0])
               {
                    x += runSpeed;
                    if (x > p.hitbox[1][0]) x = p.hitbox[1][0];
                    if (f) dir = Direction.Right;
                    if (f) imagePath = walkRightPath;
                    //call to make sure the enemy doesn't overlap the player
                    CheckBoundary(p);
               }
               else if (p.hitbox[1][0] < hitbox[1][0])
               {
                    x -= runSpeed;
                    if (x < p.hitbox[1][0]) x = p.hitbox[1][0];
                    if (f) dir = Direction.Left;
                    if (f) imagePath = walkLeftPath;
                    CheckBoundary(p);
               }
               if (p.hitbox[1][1] > hitbox[1][1])
               {
                    y += runSpeed;
                    if (y > p.hitbox[1][1]) y = p.hitbox[1][1];
                    if (!f) dir = Direction.Down;
                    if (!f) imagePath = walkDownPath;
                    CheckBoundary(p);
               }
               else if (p.hitbox[1][1] < hitbox[1][1])
               {
                    y -= runSpeed;
                    if (y < p.hitbox[1][1]) y = p.hitbox[1][1];
                    if (!f) dir = Direction.Up;
                    if (!f) imagePath = walkUpPath;
                    CheckBoundary(p);
               }
               //Update hitbox from running movement
               setHitbox();
               //Check distance from enemy to player hitbox compared to enemy's attack distance value
               if (CommonMethods.DistancePoints(new Point(hitbox[1][0], hitbox[1][1]), new Point(p.hitbox[1][0], p.hitbox[1][1])) <= attackDistance
                    || CommonMethods.DistancePoints(new Point(hitbox[1][0], hitbox[1][1]), new Point(p.hitbox[2][0], p.hitbox[2][1])) <= attackDistance
                    || CommonMethods.DistancePoints(new Point(hitbox[1][0], hitbox[1][1]), new Point(p.hitbox[3][0], p.hitbox[3][1])) <= attackDistance
                    || CommonMethods.DistancePoints(new Point(hitbox[1][0], hitbox[1][1]), new Point(p.hitbox[4][0], p.hitbox[4][1])) <= attackDistance)
               {
                    //If within range call attack
                    Attack(p);
               }
               if (CommonMethods.DistancePoints(new Point(hitbox[1][0], hitbox[1][1]), new Point(p.hitbox[1][0], p.hitbox[1][1])) > (sightDist * 2))
               {
                    foundPlayer = false;
               }
          }

          public virtual void Attack(Character p)
          {
               //Generate random value and compare to enemy's attack chance
               Random random = new Random();
               int chance = random.Next(100);
               if (chance < attackAccuracy)
               {
                    //If passed, attack is successful and reduce player health by enemy attack damage
                    p.health -= attackDamage;
                    //Change player image to indicate damage taken
                    p.imagePath = System.Environment.CurrentDirectory + @"\images\Characters\MainChar\Damaged.png";
               }
          }


          //Check Boundary function for enemy
               //Called when enemy is running towards player, stops character overlap
          public void CheckBoundary(Character p)
          {
               Point pointL1 = new Point();
               pointL1.X = p.hitbox[1][0];
               pointL1.Y = p.hitbox[1][1];
               Point pointR1 = new Point();
               pointR1.X = p.hitbox[4][0];
               pointR1.Y = p.hitbox[4][1];
               Point pointL2 = new Point();
               pointL2.X = hitbox[1][0];
               pointL2.Y = hitbox[1][1];
               Point pointR2 = new Point();
               pointR2.X = hitbox[4][0];
               pointR2.Y = hitbox[4][1];
               //If there is overlap then undo most recent running movement
               if (CommonMethods.doOverlap(pointL1, pointR1, pointL2, pointR2))
               {
                    switch (dir)
                    {
                         case Direction.Left:
                              x += runSpeed;
                              break;
                         case Direction.Right:
                              x -= runSpeed;
                              break;
                         case Direction.Down:
                              y -= runSpeed;
                              break;
                         case Direction.Up:
                              y += runSpeed;
                              break;
                         default:
                              break;
                    }
               }
               //update enemy hitbox
               setHitbox();
          }
     }
}
