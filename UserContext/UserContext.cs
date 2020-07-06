using Salon_samochodowy.DAL.Encje;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon_samochodowy
{
    
    //Klasa odpowiadająca za utrzymywanie w programie informacji o zalogowanym Pracowniku
    public class UserContext
    {

        private static UserContext _instance = null;
        public static UserContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserContext();
                }
                return _instance;
            }
        }
        
        private Pracownik _currentUser;
        public Pracownik CurrentUser { get
            {
                return _currentUser;
            }
            set
            {
                this._currentUser = value;
            }
        }

        private UserContext() { }
    }
}
