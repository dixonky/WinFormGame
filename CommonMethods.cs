using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FGame
{
     //Common Methods are general functions which can be called anywhere in the FGame namespace
     public class CommonMethods
     {
          //stackoverflow.com/questions/5514366/how-to-know-if-a-line-intersects-a-rectangle
          //Current way to calculate intersection
          public static bool LineIntersectsRect(Point p1, Point p2, Rectangle r)
          {
               return LineIntersectsLine(p1, p2, new Point(r.X, r.Y), new Point(r.X + r.Width, r.Y)) ||
                      LineIntersectsLine(p1, p2, new Point(r.X + r.Width, r.Y), new Point(r.X + r.Width, r.Y + r.Height)) ||
                      LineIntersectsLine(p1, p2, new Point(r.X + r.Width, r.Y + r.Height), new Point(r.X, r.Y + r.Height)) ||
                      LineIntersectsLine(p1, p2, new Point(r.X, r.Y + r.Height), new Point(r.X, r.Y)) ||
                      (r.Contains(p1) && r.Contains(p2));
          }


          public static bool LineIntersectsLine(Point l1p1, Point l1p2, Point l2p1, Point l2p2)
          {
               float q = (l1p1.Y - l2p1.Y) * (l2p2.X - l2p1.X) - (l1p1.X - l2p1.X) * (l2p2.Y - l2p1.Y);
               float d = (l1p2.X - l1p1.X) * (l2p2.Y - l2p1.Y) - (l1p2.Y - l1p1.Y) * (l2p2.X - l2p1.X);

               if (d == 0)
               {
                    return false;
               }

               float r = q / d;

               q = (l1p1.Y - l2p1.Y) * (l1p2.X - l1p1.X) - (l1p1.X - l2p1.X) * (l1p2.Y - l1p1.Y);
               float s = q / d;

               if (r < 0 || r > 1 || s < 0 || s > 1)
               {
                    return false;
               }

               return true;
          }


          public static Rectangle GetRectangle(Point topLeft, Point bottomRight)
          {
               var size = new Size(bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y);
               return new Rectangle(topLeft, size);
          }


          //source: www.geeksforgeeks.org/find-two-rectangles-overlap/
          public static bool doOverlap(Point l1, Point r1, Point l2, Point r2)
          {
               if (l1.X >= r2.X || l2.X >= r1.X)
                    return false;
               if (l1.Y >= r2.Y || l2.Y >= r1.Y)
                    return false;

               return true;
          }


          //Source: stackoverflow.com/questions/10458118/wait-one-second-in-running-program
          public static void wait(int milliseconds)
          {
               var timer1 = new System.Windows.Forms.Timer();
               if (milliseconds == 0 || milliseconds < 0) return;
               timer1.Interval = milliseconds;
               timer1.Enabled = true;
               timer1.Start();
               timer1.Tick += (s, e) =>
               {
                    timer1.Enabled = false;
                    timer1.Stop();
               };
               while (timer1.Enabled)
               {
                    Application.DoEvents();
               }
          }


          //Source: stackoverflow.com/questions/14126035/how-to-use-a-timer-to-wait
          public static void DelayedAction(int milliseconds, Action action)
          {
               Timer timer = new Timer();
               timer.Interval = milliseconds;
               timer.Enabled = true;
               timer.Start();
               timer.Tick += (s, e) =>
               {
                    timer.Enabled = false;
                    timer.Stop();
                    action();
               };
               while (timer.Enabled)
               {
                    Application.DoEvents();
               }
          }


          //Use to find the distance between the two passed points
          public static int DistancePoints(Point A, Point B)
          {
               int distance = Convert.ToInt32(Math.Sqrt(((B.X - A.X) * (B.X - A.X)) + ((B.Y - A.Y) * (B.Y - A.Y))));
               return distance;
          }

     }
}
