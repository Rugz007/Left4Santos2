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
        string Path = "location.txt";
        MenuPool zombiemod_menupool;
        UIMenu mainMenu;
        public static int zombie_spawn = 0;
        public static int survivor_spawn = 0;
        UIMenuItem input_location, spawnSurvivor;
        UIMenuCheckboxItem spawnZombie;
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

            spawnZombie = new UIMenuCheckboxItem("Spawn Zombie", false);
            mainMenu.AddItem(spawnZombie);
            mainMenu.OnCheckboxChange += onMainMenuCheckboxChange;

            input_location = new UIMenuItem("Input Location");
            mainMenu.AddItem(input_location);
            mainMenu.OnItemSelect += onMainMenuItemSelect;

            spawnSurvivor = new UIMenuItem("Spawn Survivor");
            mainMenu.AddItem(spawnSurvivor);
            mainMenu.OnItemSelect += onMainMenuItemSelect;
        }
        void onMainMenuItemSelect(UIMenu sender, UIMenuItem item, int index)
        {
            if(item == input_location)
            {
                string input;
                input = Game.GetUserInput();
                Vector3 loc = new Vector3();
                loc = Game.Player.Character.Position;
                string loc1 = loc.ToString();
                StreamWriter sw = new StreamWriter(Path);
                using(sw = File.AppendText(Path))
                {
                    sw.Write(input);
                    sw.Write(loc1);
                    sw.Flush();
                }
                sw.Close();
            }
            if (item == spawnSurvivor)
            {
                survivor_spawn = 1;
            }
        }
        void onMainMenuCheckboxChange(UIMenu sender, UIMenuCheckboxItem item, bool checked_)
        {
            if(item == spawnZombie)
            {
                if(checked_ == true)
                {
                    zombie_spawn = 0;
                    checked_ = false;
                }
                else
                {
                    zombie_spawn = 1;
                    checked_ = true;
                }
            }
        }
    }
}
