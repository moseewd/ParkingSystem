using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystem
{
    public class Userclass
    {
        public   int ID
        {
            get;

            set;
          
        }
        public string Full_Name
        {
            get;

            set;

        }
        public int Balance
        {
            get;

            set;
        }
        public int Debt
        {
            get;

            set;
        }
        
        public string Drive_License
        {
            get;

            set;
        }
        public int Password
        {
            get;

            set;
        }

        public string Login
        {
            get;

            set;
        }
       
        public string Pasport
        {
            get;

            set;
        }
        public int Admin
        {
            get;

            set;
        }
        public List<Cars> carsList => Cars.GetCars(this.ID);
        public List<Penalty> penaltylist => Penalty.GetPenalty(this.ID);
        public Userclass()
        {
            ID = 0;
            Full_Name = "";
            Balance = 0;
            Debt = 0;
            Drive_License = "";
            Password = 0;
            Login = "";
            Pasport = "";
            Admin = 0;
            
        }


    }
}
