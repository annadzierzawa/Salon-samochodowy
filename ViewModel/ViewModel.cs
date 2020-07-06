
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Salon_samochodowy.DAL.Encje;
using Salon_samochodowy.DAL.Repozytoria;
using Salon_samochodowy.View;

namespace Salon_samochodowy.ViewModel
{
    using Model;
    using BaseClass;
    using DAL;

    class ViewModel
    {
        UserContext userContext;

        private static Model model = new Model();

       
        public LoginVM LoginVm { get; set; }
        public AboutAccVM AboutAccVm { get; set; }
        public AddCarVM AddCarVm { get; set; }
        public AddEmployeeVM AddEmployeeVm { get; set; }
        public ChangePasswordVM ChangePasswordVm { get; set; }
        public SellersStatsVM SellersStatsVm { get; set; }
        public SellingVM SellingVM { get; set; }

        public ViewModel()
        {
            userContext = UserContext.Instance;
            AddEmployeeVm = new AddEmployeeVM(model);
            AddCarVm = new AddCarVM(model);
            AboutAccVm = new AboutAccVM(model);
            LoginVm = new LoginVM(model);
            ChangePasswordVm = new ChangePasswordVM(model);
            SellersStatsVm = new SellersStatsVM(model);
            SellingVM = new SellingVM(model);
        }

    }
}
