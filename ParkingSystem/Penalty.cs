using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystem
{
    class Penalty
    {
        public int ID
        {
            get;
            set;

        }
        public int Sum
        {
            get;
            set;

        }
        public int FK_User
        {
            get;
            set;
        }
    }
}
