using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystem
{
    class Payment
    {
        public int ID
        {
            get;
            set;

        }
        public Int64 Card
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
