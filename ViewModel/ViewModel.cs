
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Salon_samochodowy.DAL.Encje;
using Salon_samochodowy.DAL.Repozytoria;

namespace Salon_samochodowy.ViewModel
{
    using Model;
    using BaseClass;
    using DAL;

    class ViewModel
    {
        private static Model model = new Model();

        public AddEmployeeVM AddEmployeeVm { get; set; }
        public LoginVM LoginVm { get; set; }
        public AddCarVM AddCarVm { get; set; }

        public ViewModel()
        {
            AddEmployeeVm = new AddEmployeeVM(model);
            AddCarVm = new AddCarVM(model);
            LoginVm = new LoginVM(model);
        }

    }
}
