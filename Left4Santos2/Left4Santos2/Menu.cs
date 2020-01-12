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
using Main;

namespace Left4Santos2
{
    int spawn = 0;
    public class Menu : Script
    {
        MenuPool zombiemod_menupool;
        UIMenu mainMenu;

        UIMenuItem spawnZombie;

        public Menu()
        {
            Setup();

            Tick += onTick;
            KeyDown += onKeyDown;
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
        void Setup()
        {
            zombiemod_menupool = new MenuPool();
            mainMenu = new UIMenu("Zombie Mod", "Hello there!");
            zombiemod_menupool.Add(mainMenu);

            spawnZombie = new UIMenuItem("Spawn Zombie");
            mainMenu.AddItem(spawnZombie);
            mainMenu.OnItemSelect += onMainMenuItemSelect;
        }
        void onMainMenuItemSelect(UIMenu sender, UIMenuItem item, int index)
        {
            if(item == spawnZombie)
            {
                spawn = 1;
            }
        }
    }
}
