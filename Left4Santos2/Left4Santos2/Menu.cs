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
using System.IO;

namespace Left4Santos2
{
    
    public class Menu : Script
    {
        string Path = "C://Users/Akshat/source/repos/Rugz007/Left4Santos2/location.txt";
        MenuPool zombiemod_menupool;
        UIMenu mainMenu;
        public static int zombie_spawn = 0;
        public static int survivor_spawn = 0;
        UIMenuItem spawnZombie, input_location, spawnSurvivor;
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
            if(e.KeyCode == Keys.F10)
            {
                mainMenu.Visible = !mainMenu.Visible;
            }
        }
        void Setup()
        {
            zombiemod_menupool = new MenuPool();
            mainMenu = new UIMenu("Zombie Mod", "Hello there!");
            zombiemod_menupool.Add(mainMenu);

            spawnZombie = new UIMenuItem("Spawn Zombie");
            mainMenu.AddItem(spawnZombie);
            mainMenu.OnItemSelect += onMainMenuItemSelect;

            input_location = new UIMenuItem("Input Location");
            mainMenu.AddItem(input_location);
            mainMenu.OnItemSelect += onMainMenuItemSelect;

            spawnSurvivor = new UIMenuItem("Spawn Survivor");
            mainMenu.AddItem(spawnSurvivor);
            mainMenu.OnItemSelect += onMainMenuItemSelect;
        }
        void onMainMenuItemSelect(UIMenu sender, UIMenuItem item, int index)
        {
            if(item == spawnZombie)
            {
                zombie_spawn = 1;
            }
            if(item == input_location)
            {
                string input;
                input = Game.GetUserInput();
                Vector3 loc = Game.Player.Character.Position;
                StreamWriter sw = new StreamWriter(Path);
                using(sw = File.AppendText(Path))
                {
                    sw.Write(input);
                    sw.Write(loc);
                    sw.Flush();
                }
                sw.Close();
            }
            if (item == spawnSurvivor)
            {
                survivor_spawn = 1;
            }
        }
    }
}
