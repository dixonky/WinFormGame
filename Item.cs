using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FGame
{
     //Enum of all Item types
     public enum ItemType
     {
          arrow
     }


     public class Item : Object
     {
          //Items specific values
          public ItemType type { get; set; }
          public int actionTime { get; set; }
          public bool grounded;


          //Item constructor
          public Item()
          {
          
          }

     }

}
