using System;
using GTA;
using GTA.UI;
using System.Windows.Forms;
using System.Linq;
using GTA.Native;

namespace Left4Santos2 
{
    public class Main : Script
    {
        RelationshipGroup  PlayerGroup, SurvivorGroup;
        RelationshipGroup ZombieGroup;
        Random random;
        int rnd;
        Extender extender;
        bool enable;
        public Main()
        {
            ZombieGroup = World.AddRelationshipGroup("Zombie");
            SurvivorGroup = World.AddRelationshipGroup("SurvivorGroup");
            SurvivorGroup.SetRelationshipBetweenGroups(ZombieGroup, Relationship.Hate, true);
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

                        if (ped != Game.Player.Character && ped.RelationshipGroup != PlayerGroup && !ped.IsDead && ped.RelationshipGroup != SurvivorGroup )
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
                            if (ped.RelationshipGroup != ZombieGroup )
                            { 
                                extender.MakeZombie(ped, ZombieGroup);
                            }
                            if(ped.RelationshipGroup != SurvivorGroup && ped.Weapons.Current == WeaponHash.Unarmed)
                            {
                                extender.MakeZombieGoToPed(ped, Game.Player.Character.Position);
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
            if(Menu.survivor_spawn == 1)
            {
                Menu.survivor_spawn = 0;
                Ped survivor = World.CreateRandomPed(Game.Player.Character.Position);
                MakeSurvivor(survivor, SurvivorGroup);
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
        public void MakeSurvivor(Ped ped, RelationshipGroup relationshipGroup)
        {
            ped.RelationshipGroup = relationshipGroup;
            ped.RelationshipGroup = SurvivorGroup;
            ped.Weapons.Give(WeaponHash.CarbineRifle, 100, true, true);
            ped.Task.FightAgainstHatedTargets(30f);
            ped.AlwaysKeepTask = true;
            Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, ped, 46, true);
            Blip blip = ped.AddBlip();
            blip.Sprite = BlipSprite.Friend;
            blip.Color = BlipColor.Blue;
        }
    }
}
