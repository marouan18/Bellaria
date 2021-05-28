using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Admin.ViewModel
{
    class HomeViewModel : BaseViewModel
    {
        public ObservableCollection<Immagini> Images { get; set; }
        public List<Menu> MyMenu { get; set; }
        public HomeViewModel()
        {
            MyMenu = GetMenus();
            Images = new ObservableCollection<Immagini>();
            var data = new List<Immagini>
            {
                new Immagini { Immagine = "dashboard1.png" },
                new Immagini { Immagine = "dashboard2.png" }
            };
            Images.Clear();
            foreach (var item in data)
            {
                Images.Add(item);
            }
        }
        private List<Menu> GetMenus()
        {
            return new List<Menu>
            {
                new Menu{ Name = "Home", Icon = "home.png"},
                new Menu{ Name = "Nuovi Ordini", Icon = "OrderHistory.png"},
                new Menu{ Name = "Cronologia Ordini", Icon = "Logout1.png"},
            };
        }

        public class Immagini
        {
            public string Immagine { get; set; }
        }
        public class Menu
        {
            public string Name { get; set; }
            public string Icon { get; set; }
        }

    }
}
