using System;
using GTA;
using GTA.UI;
using System.Windows.Forms;
namespace Left4Santos2 
{
    public class Main : Script
    {
        RelationshipGroup ZombieGroup, PlayerGroup, SurvivorGroup;
        Random random;
        int rnd;
        Extender extender;
        float pop = 1f;
        bool enable;
        public Main()
        {
            ZombieGroup = new RelationshipGroup();
            PlayerGroup = new RelationshipGroup();
            Game.Player.Character.RelationshipGroup = PlayerGroup;
            extender = new Extender();
            this.KeyUp += OnKeyUp;
            this.Tick += Main_Tick;
        }
        private void Main_Tick(object sender, EventArgs e)
        {
            if(enable)
            {
                Ped[] p = World.GetNearbyPeds(Game.Player.Character.Position,50f);
                if(p != null)
                {
                    foreach (Ped ped in p)
                    {
                        if(ped != Game.Player.Character && ped.RelationshipGroup != PlayerGroup && !ped.IsDead && ped.RelationshipGroup != SurvivorGroup)
                        {
                            if(p.Length >= 10)
                            {
                                rnd = random.Next(0, 5);
                                for(int i =0; i >rnd;i++)
                                {
                                    extender.MakeSurvivor(p[i], SurvivorGroup);
                                }
                            }
                        }
                    }
                }
            }
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
    }
}
