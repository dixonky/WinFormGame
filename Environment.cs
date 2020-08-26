using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGame
{
     //Enum of all the environment types
     public enum EnviType
     {
          tree, cliff
     }

     public class Environment : Object
     {
          //Environment specific values
          public EnviType type { get; set; }

          //Environment constructor
          public Environment()
          {

          }
     }
}
