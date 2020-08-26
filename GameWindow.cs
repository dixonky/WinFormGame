using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace FGame
{
     //Enum for object direction
     public enum Direction
     {
          Left, Right, Up, Down
     }


     public partial class GameWindow : Form
     {
          //General game values
          public Random random = new Random();
          private double _winHeight;
          private double _winWidth;
          private bool playing;
          //Main player character
          Player mainPlayer = new Player();
          //Lists for objects
          List<Environment> envi = new List<Environment>();
          List<Item> items = new List<Item>();
          List<Enemy> enemies = new List<Enemy>();


          //Game constructor
               //get the window values and call setip
          public GameWindow()
          {
               InitializeComponent();
               _winHeight = this.Height;
               _winWidth = this.Width;
               Setup();
          }


          //Setup function
          private void Setup()
          {
               //Create the map with environment objects
               createMap();
               //Populate the map with character objects
               getStartChars();
          }


          //Create Map function
               //fill the map with environment objects
          private void createMap()
          {
               //Generate random trees
               int treeNum = 100+random.Next(100);
               List<int[]> treeList = new List<int[]>();

               for (int i = 0; i < treeNum; i++)
               {
                    MakeTree:
                    int x = -2500 + random.Next(5000);
                    int y = -2500 + random.Next(5000);
                    //Check the trees don't spawn over the starting player location
                    if ((x < Convert.ToInt32(_winWidth / 2) + 100) && (x > Convert.ToInt32(_winWidth / 2) - 100)
                              && (y < Convert.ToInt32(_winHeight / 2) + 100) && (y > Convert.ToInt32(_winHeight / 2) - 100))
                    {
                         goto MakeTree;
                    }
                    //Check the trees don't spawn on each other
                    foreach (int[] t in treeList)
                    {
                         if ((Math.Abs(x - t[0]) > 200) || (Math.Abs(y - t[1]) > 200))
                         {
                              goto MakeTree;
                         }
                    }
                    //Create the tree and add to environmental list
                    Tree tree = new Tree(x, y, 1+ random.Next(3));
                    tree.setHitbox();
                    envi.Add(tree);
               }
               //Order the envi list by x and y values
               List<Environment> SortedList = envi.OrderBy(o => o.x).ToList();
               envi = SortedList;
          }


          //Get Start Character function
          private void getStartChars()
          {
               //Set the player in the middle of the screen
               mainPlayer.x = Convert.ToInt32(_winWidth/2);
               mainPlayer.y = Convert.ToInt32(_winHeight/2);
               mainPlayer.setHitbox();

               //Add 20 goblins in random locations
               for (int i=0; i< 20; i++)
               {
                    MakeGoblin:
                    int x = -1250 + random.Next(2500);
                    int y = -1250 + random.Next(2500);
                    //Check the goblins spawn far enough away from the player
                    if ((x < Convert.ToInt32(_winWidth / 2) + 100) && (x > Convert.ToInt32(_winWidth / 2) - 100)
                              && (y < Convert.ToInt32(_winHeight / 2) + 100) && (y > Convert.ToInt32(_winHeight / 2) - 100))
                    {
                         goto MakeGoblin;
                    }
                    //Create goblin and add to enemy list
                    Goblin gob = new Goblin(x, y);
                    gob.setHitbox();
                    enemies.Add(gob);
               }
               playing = true;
          }


          //Game Paint Function
               //Adds the images to the Game window
          private void GameWindow_Paint(object sender, PaintEventArgs e)
          {
               //Display game information
               playerStatus.Text = "Health: " + mainPlayer.health;
               playerStatus.Text += System.Environment.NewLine + "Enemies Remaining: " + enemies.Count;

               //Display the player
               Image person = Image.FromFile(mainPlayer.imagePath);
               e.Graphics.DrawImage(person, mainPlayer.x, mainPlayer.y, mainPlayer.width, mainPlayer.height);

               //Display objects in respective lists
               foreach (Environment en in envi)
               {
                    //Only print the objects in the current grids viewed by window
                    if( en.x < _winWidth + 100 && en.x >= -100 && en.y >= -100 && en.y < _winHeight + 100)
                    {
                         en.setHitbox();
                         Image enviItem = Image.FromFile(en.imagePath);
                         e.Graphics.DrawImage(enviItem, en.x, en.y, en.width, en.height);
                    }
                    if (en.x > _winWidth && en.y > _winHeight)
                    {
                         break;
                    }
               }
               foreach (Item i in items)
               {
                    //Don't show arrows which are grounded
                    if (i.type == ItemType.arrow) {
                         if (i.grounded == true)
                         {
                              continue;
                         }
                    }
                    Image item = Image.FromFile(i.imagePath);
                    i.setHitbox();
                    e.Graphics.DrawImage(item, i.x, i.y, i.width, i.height);
               }
               foreach (Enemy en in enemies)
               {
                    //Only print the objects in the current grids viewed by window
                    if (en.x < _winWidth && en.x >= 0 && en.y >= 0 && en.y < _winHeight)
                    {
                         Image enemy = Image.FromFile(en.imagePath);
                         en.setHitbox();
                         e.Graphics.DrawImage(enemy, en.x, en.y, en.width, en.height);

                         //Display the enemy vision cone for debugging
                         Graphics gr = default(Graphics);
                         Pen pen_draw = new Pen(Color.Black);
                         pen_draw.DashPattern = new float[] { 4.0F, 2.0F, 1.0F, 3.0F };
                         gr = e.Graphics;
                         Point[] pnt = new Point[3];
                         int eyeMod = (en.hitbox[2][0] - en.hitbox[1][0]) / 2;
                         int eyeX = en.hitbox[1][0] + eyeMod;
                         int eyeY = en.hitbox[1][1] + eyeMod;
                         pnt[0].X = eyeX;
                         pnt[0].Y = eyeY;
                         switch (en.dir)
                         {
                              case Direction.Left:
                                   pnt[1].X = eyeX - en.sightDist;
                                   pnt[1].Y = eyeY + en.visionCone;
                                   pnt[2].X = eyeX - en.sightDist;
                                   pnt[2].Y = eyeY - en.visionCone;
                                   break;
                              case Direction.Right:
                                   pnt[1].X = eyeX + en.sightDist;
                                   pnt[1].Y = eyeY + en.visionCone;
                                   pnt[2].X = eyeX + en.sightDist;
                                   pnt[2].Y = eyeY - en.visionCone;
                                   break;
                              case Direction.Down:
                                   pnt[1].X = eyeX + en.visionCone;
                                   pnt[1].Y = eyeY + en.sightDist;
                                   pnt[2].X = eyeX - en.visionCone;
                                   pnt[2].Y = eyeY + en.sightDist;
                                   break;
                              case Direction.Up:
                                   pnt[1].X = eyeX + en.visionCone;
                                   pnt[1].Y = eyeY - en.sightDist;
                                   pnt[2].X = eyeX - en.visionCone;
                                   pnt[2].Y = eyeY - en.sightDist;
                                   break;
                              default:
                                   break;
                         }
                         gr.DrawPolygon(pen_draw, pnt);
                    }
               }
               //Clean the lists of objects with no health or otherwise unusable
               Thread thr = new Thread(CleanLists);
               thr.Start();
          }


          //Clean Lists function
               //remove objects based on health or status
          public void CleanLists()
          {
               for(int i = 0; i < items.Count; i++)
               {
                    if (items[i].type == ItemType.arrow)
                    {
                         if (items[i].grounded == true)
                         {
                              items.RemoveAt(i);
                              continue;
                         }
                    }
               }
               for (int i = 0; i < envi.Count; i++)
               {
                    if (envi[i].health <= 0)
                    {
                         envi.RemoveAt(i);
                         continue;
                    }
               }
               for (int i = 0; i < enemies.Count; i++)
               {
                    if (enemies[i].health <= 0)
                    {
                         enemies.RemoveAt(i);
                         continue;
                    }
               }

          }


          //Timer function
               //use to call functions based on elapsed time 
          private void tmrMoving_Tick(object sender, EventArgs e)
          {
               Thread thr;
               foreach (Enemy en in enemies)
               {
                    if (!en.foundPlayer)
                    {
                         thr = new Thread(o => en.FindPlayer(mainPlayer));
                         thr.Start();
                         switch (en.dir)
                         {
                              case Direction.Down:
                                   thr = new Thread(o => en.RandomWalk(0));
                                   thr.Start();
                                   break;
                              case Direction.Up:
                                   thr = new Thread(o => en.RandomWalk(1));
                                   thr.Start();
                                   break;
                              case Direction.Left:
                                   thr = new Thread(o => en.RandomWalk(2));
                                   thr.Start();
                                   break;
                              case Direction.Right:
                                   thr = new Thread(o => en.RandomWalk(3));
                                   thr.Start();
                                   break;
                         }
                         if (random.Next(60) % 15 == 0)
                         {
                              thr = new Thread(o => en.RandomWalk(random.Next(4)));
                              thr.Start();
                         }
                    }
                    else
                    {
                         thr = new Thread(o => en.runTowardsPlayer(mainPlayer));
                         thr.Start();
                    }
               }
               CheckGameState();
               Invalidate();
          }


          //Key Down function
               //use to call functions based on the keys entered by the user
          private void GameWindow_KeyDown(object sender, KeyEventArgs e)
          {
               //Movement keys
               if (e.KeyCode == Keys.Left) { mainPlayer.Walk(Direction.Left); MoveBoundary(1); CheckBoundary(mainPlayer); }
               if (e.KeyCode == Keys.Right) { mainPlayer.Walk(Direction.Right); MoveBoundary(2); CheckBoundary(mainPlayer); }
               if (e.KeyCode == Keys.Up) { mainPlayer.Walk(Direction.Up); MoveBoundary(3); CheckBoundary(mainPlayer); }
               if (e.KeyCode == Keys.Down) { mainPlayer.Walk(Direction.Down); MoveBoundary(4); CheckBoundary(mainPlayer); }
               //Action Keys
               //space bar currently fires an arrow
               if (e.KeyCode == Keys.Space) {
                    if (!mainPlayer.ActionTimer)
                    {
                         mainPlayer.ActionTimer = true;
                         Arrow arrow = new Arrow(mainPlayer.dir, mainPlayer.x, mainPlayer.y);
                         items.Add(arrow);
                         Thread thr = new Thread(o => arrow.Fly(mainPlayer.dir, envi, enemies));
                         thr.Start();
                         Thread actionTime = new Thread(o => CommonMethods.DelayedAction(arrow.actionTime, () => mainPlayer.ActionTimer = false));
                         actionTime.Start();
                    }
               }
               //Redraw the window
               Invalidate();
          }


          //Check Boundary function
               //use to check player hitbox overlap with environment
               //reverts movement values if overlap is detected
          private void CheckBoundary(Character c)
          {
               foreach (Environment en in envi)
               {
                    Point pointL1 = new Point();
                    pointL1.X = c.hitbox[1][0];
                    pointL1.Y = c.hitbox[1][1];
                    Point pointR1 = new Point();
                    pointR1.X = c.hitbox[4][0];
                    pointR1.Y = c.hitbox[4][1];
                    Point pointL2 = new Point();
                    pointL2.X = en.hitbox[1][0];
                    pointL2.Y = en.hitbox[1][1];
                    Point pointR2 = new Point();
                    pointR2.X = en.hitbox[4][0];
                    pointR2.Y = en.hitbox[4][1];
                    //If there is overlap revert the player movement by calling the opposite move boundary than initial movement
                    if(CommonMethods.doOverlap(pointL1, pointR1, pointL2, pointR2))
                    {
                         switch (c.dir)
                         {
                              case Direction.Left:
                                   MoveBoundary(2);
                                   break;
                              case Direction.Right:
                                   MoveBoundary(1);
                                   break;
                              case Direction.Down:
                                   MoveBoundary(3);
                                   break;
                              case Direction.Up:
                                   MoveBoundary(4);
                                   break;
                              default:
                                   break;
                         }
                    }
                    if (mainPlayer.totDisx > 2500 || mainPlayer.totDisx < -2500 || mainPlayer.totDisy > 2500  || mainPlayer.totDisy < -2500 )
                    {
                         switch (c.dir)
                         {
                              case Direction.Left:
                                   MoveBoundary(2);
                                   break;
                              case Direction.Right:
                                   MoveBoundary(1);
                                   break;
                              case Direction.Down:
                                   MoveBoundary(3);
                                   break;
                              case Direction.Up:
                                   MoveBoundary(4);
                                   break;
                              default:
                                   break;
                         }
                    }
               }
          }


          //Move Boundary function
               //moves all objects in the game to keep the player in the middle of the screen
          private void MoveBoundary(int dir)
          {
               int xVal = 0;
               int yVal = 0;
               switch (dir)
               {
                    case 1:
                         mainPlayer.totDisx -= mainPlayer.walkSpeed;
                         xVal = mainPlayer.walkSpeed;
                         yVal = 0;
                         break;
                    case 2:
                         mainPlayer.totDisx += mainPlayer.walkSpeed;
                         xVal = -mainPlayer.walkSpeed;
                         yVal = 0;
                         break;
                    case 3:
                         mainPlayer.totDisy -= mainPlayer.walkSpeed;
                         xVal = 0;
                         yVal = mainPlayer.walkSpeed;
                         break;
                    case 4:
                         mainPlayer.totDisy += mainPlayer.walkSpeed;
                         xVal = 0;
                         yVal = -mainPlayer.walkSpeed;
                         break;
                    default:
                         break;
               }
               foreach (Environment en in envi)
               {
                    en.x += xVal;
                    en.y += yVal;
                    en.setHitbox();
               }
               foreach (Item i in items)
               {
                    i.x += xVal;
                    i.y += yVal;
                    i.setHitbox();
               }
               foreach (Enemy en in enemies)
               {
                    en.x += xVal;
                    en.y += yVal;
                    en.setHitbox();
               }
          }


          //Check Game State Function
               //checks the player status and ends game depending on values
          private void CheckGameState()
          {
               if(mainPlayer.health <= 0 && playing)
               {
                    playing = false;
                    envi.Clear();
                    items.Clear();
                    enemies.Clear();
                    string message = "Game Over... Play Again?";
                    string title = "Game Over";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result = MessageBox.Show(message, title, buttons);
                    if (result == DialogResult.Yes)
                    {
                         mainPlayer = new Player();
                         Setup();
                    }
                    else
                    {
                         this.Close();
                    }
               }
               else if (enemies.Count == 0 && playing)
               {
                    playing = false;
                    envi.Clear();
                    items.Clear();
                    enemies.Clear();
                    string message = "Yo Won!!! Play Again?";
                    string title = "Game Over";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result = MessageBox.Show(message, title, buttons);
                    if (result == DialogResult.Yes)
                    {
                         mainPlayer = new Player();
                         Setup();
                    }
                    else
                    {
                         this.Close();
                    }
               }
          }


          //Resize End function
               //called when the form window is resized
               //changes the saved window size values and redraws the window
          private void GameWindow_ResizeEnd(object sender, System.EventArgs e)
          {
               Control control = (Control)sender;
               _winWidth = this.Width;
               _winHeight = this.Height;
               int oldx = mainPlayer.x;
               int oldy = mainPlayer.y;
               mainPlayer.x = Convert.ToInt32(_winWidth / 2);
               mainPlayer.y = Convert.ToInt32(_winHeight / 2);
               int diffx = mainPlayer.x - oldx;
               int diffy = mainPlayer.y - oldy;
               mainPlayer.setHitbox();
               foreach (Environment en in envi)
               {
                    en.x += diffx;
                    en.y += diffy;
                    en.setHitbox();
               }
               foreach (Item i in items)
               {
                    i.x += diffx;
                    i.y += diffy;
                    i.setHitbox();
               }
               foreach (Enemy en in enemies)
               {
                    en.x += diffx;
                    en.y += diffy;
                    en.setHitbox();
               }
               Invalidate();
          }
     }
}
