using System;
using GTA;
using GTA.UI;
using System.Windows.Forms;
namespace Left4Santos2 
{
    public class Main : Script
    {
        Extender extender;
        Ped p;
        float pop = 1f;
        public Main()
        {
            extender = new Extender();
            this.KeyUp += OnKeyUp;
            this.Tick += Main_Tick;
        }

        private void Main_Tick(object sender, EventArgs e)
        {
            if(Menu.spawn == 1)
            {
                World.CreateRandomPed(Game.Player.Character.Position);
                Menu.spawn = 0;
                Notification.Show("Lauda");
            }
            extender.SetPopulation(pop);
        }

        void OnKeyUp(object sender,KeyEventArgs e)
        {
            if(e.KeyCode == Keys.I)
            {
                pop = 0f;
                Notification.Show("0");
            }
            if(e.KeyCode == Keys.K)
            {
                pop = 5f;
                Notification.Show("5");
            }
            if(e.KeyCode == Keys.L)
            {
                pop = 1f;
                Notification.Show("1");
            }
        }
        public void CreateZombie()
        {

        }
        public void MakeEveryoneZombie()
        {
            bool lol = extender.CanHearPlayer(p);
        }
    }
}
