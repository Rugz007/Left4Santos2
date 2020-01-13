using System;
using GTA;
using GTA.UI;
using System.Windows.Forms;
using System.Linq;
namespace Left4Santos2 
{
    public class Main : Script
    {
        RelationshipGroup ZombieGroup, PlayerGroup, SurvivorGroup;
        Random random;
        int rnd;
        Extender extender;
        bool enable;
        public Main()
        {
            ZombieGroup = new RelationshipGroup();
            PlayerGroup = new RelationshipGroup();
            SurvivorGroup = new RelationshipGroup();
            SurvivorGroup.SetRelationshipBetweenGroups(ZombieGroup, Relationship.Hate, true);
            PlayerGroup.SetRelationshipBetweenGroups(ZombieGroup, Relationship.Hate, true);
            Game.Player.Character.RelationshipGroup = PlayerGroup;
            extender = new Extender();
            this.KeyUp += OnKeyUp;
            this.Tick += Main_Tick;
            random = new Random();
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
                        if (ped != Game.Player.Character && ped.RelationshipGroup != PlayerGroup && !ped.IsDead && ped.RelationshipGroup != SurvivorGroup)
                        {
                           /* #region Make Survivor
                            if (p.Length >= 10)
                            {
                                rnd = random.Next(1, 5);
                                for (int i = 0; i > rnd; i++)
                                {
                                    if(p[i].RelationshipGroup != ZombieGroup)
                                    {
                                        extender.MakeSurvivor(p[i], SurvivorGroup);
                                    }
                                    //p = p.Where(val => val != p[i]).ToArray();
                                }
                            }
                            #endregion*/
                            if (ped.RelationshipGroup != SurvivorGroup && ped.RelationshipGroup != ZombieGroup)
                            {
                                extender.MakeZombie(ped, ZombieGroup);
                            }
                            if(ped.RelationshipGroup != SurvivorGroup)
                            {
                                extender.MakeZombieGoToPed(ped, Game.Player.Character.Position);
                                Notification.Show("Wuhu");
                            }
                        }
                    }
                }
            }
            if(Menu.zombie_spawn == 1)
            {
                Menu.zombie_spawn = 0;
                enable = true;
            }
        }
        void OnKeyUp(object sender,KeyEventArgs e)
        {
            if(e.KeyCode == Keys.I)
            {
                Notification.Show("0");
            }
            if(e.KeyCode == Keys.K)
            {
                Notification.Show("5");
            }
            if(e.KeyCode == Keys.L)
            {
                Notification.Show("1");
            }
        }
    }
}
