using Salon_samochodowy.DAL.Encje;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon_samochodowy
{
    /*
     * Singleton odpowiedzialny za trzymanie informacji o aktualnym użytkowniku
    */
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

        private UserContext()
        {
            
        }
    }
}
