using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;
using System.Drawing;
using System.Windows.Forms;

namespace Left4Santos2
{
    public class Menu : Script
    {
        MenuPool zombiemod_menupool;
        UIMenu mainMenu;

        public Menu()
        {
            zombiemod_menupool = new MenuPool();
            mainMenu = new UIMenu("Zombie Mod", "Hello there!");
            zombiemod_menupool.Add(mainMenu);
        }

        void onTick(object sender, EventArgs e)
        {
            if (zombiemod_menupool != null)
                zombiemod_menupool.ProcessMenus();
        }

        void onKeyDown(object sender, KeyEventArgs e)
        {
            mainMenu.Visible = !mainMenu.Visible;
        }
    }
}
