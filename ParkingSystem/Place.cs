using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystem
{
    class Place
    {
        public int ID
        {
            get;
            set;
        }
        public int FK_Parking
        {
            get;
            set;

        }
        public int Status
        {
            get;
            set;
        }
    }
}
