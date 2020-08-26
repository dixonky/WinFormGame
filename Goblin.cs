using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGame
{
     class Goblin : Enemy
     {
          //Goblin Constructor
               //Starting location is previously randomly generated and passed in
          public Goblin(int xloc, int yloc)
          {
               type = CharType.goblin;
               imagePath = System.Environment.CurrentDirectory + @"\images\Characters\Goblin\WalkDown.png";
               walkDownPath = System.Environment.CurrentDirectory + @"\images\Characters\Goblin\WalkDown.png";
               walkUpPath = System.Environment.CurrentDirectory + @"\images\Characters\Goblin\WalkUp.png";
               walkRightPath = System.Environment.CurrentDirectory + @"\images\Characters\Goblin\WalkRight.png";
               walkLeftPath = System.Environment.CurrentDirectory + @"\images\Characters\Goblin\WalkLeft.png";
               x = xloc;
               y = yloc;
               width = 15;
               height = 25;
               walkSpeed = 1;
               runSpeed = 8;
               sightDist = 150;
               dir = Direction.Down;
               health = 20;
               visionCone = 25;
               attackDamage = 5;
               attackDistance = 20;
               attackAccuracy = 20;
          }
     }
}
