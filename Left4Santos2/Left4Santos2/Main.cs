using System;
using GTA;
using GTA.UI;
using System.Windows.Forms;
using System.Linq;
using GTA.Native;
using GTA.Math;
using System.Collections.Generic;

namespace Left4Santos2 
{
    public class Main : Script
    {
        RelationshipGroup PlayerGroup, FriendlyGroup, ZombieGroup, MilitaryGroup, ThugGroup, RunnerGroup, BomberGroup;
        List<Ped> Friendlies;
        Random random;
        int rnd;
        Extender extender;
        bool enable;
        Predicate<Ped> nearZombie;
        public Main()
        {
            Function.Call(Hash.SET_MAX_WANTED_LEVEL, 0);
            Friendlies = new List<Ped>();
            World.Blackout = true;
            extender = new Extender();
            Tick += Main_Tick;
            random = new Random();
            SetRelationShips();
        }
        private void Main_Tick(object sender, EventArgs e)
        {
            if (enable)
            {
                Ped[] p = World.GetNearbyPeds(Game.Player.Character.Position, 50f);
                if (p != null)
                {
                    foreach (Ped ped in p)
                    {
                        if (ped != Game.Player.Character && ped.RelationshipGroup != PlayerGroup && !ped.IsDead && ped.RelationshipGroup != FriendlyGroup && !ped.IsDead)
                        {
                            if (ped.RelationshipGroup != ZombieGroup && ped.RelationshipGroup != FriendlyGroup)
                            {
                                extender.MakeZombie(ped, ZombieGroup);
                            }
                            if (ped.RelationshipGroup == ZombieGroup)
                            {
                                if (extender.CanHearPlayer(ped) || extender.CanSeePlayer(ped))
                                {
                                    extender.MakeZombieGoToPed(ped, Game.Player.Character.Position);
                                }
                                else
                                {
                                    ped.Task.WanderAround(World.GetNextPositionOnStreet(ped.Position), 20f); ;
                                }
                                if (ped.IsTouching(Game.Player.Character) && !ped.IsGettingUp && !ped.IsFalling && ped.IsWalking)
                                {
                                    Game.Player.Character.Health -= 1;
                                }
                                if (Friendlies.Count > 0)
                                {
                                    for (int i = 0; i < Friendlies.Count; i++)
                                    {
                                        if (Friendlies[i] != null)
                                        {
                                            if (World.GetDistance(ped.Position,Friendlies[i].Position) < 1f&& !ped.IsGettingUp && !ped.IsFalling && ped.IsWalking)
                                            {
                                                if (Friendlies[i].Health < 10)
                                                {
                                                    Friendlies[i].RelationshipGroup = ZombieGroup;
                                                    Friendlies[i].AttachedBlip.Delete();
                                                    extender.MakeZombie(Friendlies[i], ZombieGroup);
                                                    Friendlies.RemoveAt(i);
                                                }
                                                else
                                                {
                                                    Friendlies[i].Health -= 10;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (ped.RelationshipGroup == FriendlyGroup)
                        {
                            if (ped.IsDead)
                            {
                                ped.RelationshipGroup.Remove();
                                Game.Player.Character.PedGroup.Remove(ped);
                                ped.AttachedBlip.Delete();
                            }
                        }
                    }
                }
            }
            if (Menu.zombie_spawn == 1 && enable == false)
            {
                enable = true;
            }
            if (Menu.zombie_spawn == 0 && enable == true)
            {
                enable = false;
            }
            if (Menu.survivor_spawn == 1)
            {
                Menu.survivor_spawn = 0;
                Ped friendly = World.CreateRandomPed(Game.Player.Character.Position);
                extender.MakeFriendly(friendly, FriendlyGroup);
                Friendlies.Add(friendly);
            }
        }
        void SetRelationShips()
        {
            ZombieGroup = World.AddRelationshipGroup("ZombieGroup");
            FriendlyGroup = World.AddRelationshipGroup("FriendlyGroup");
            PlayerGroup = World.AddRelationshipGroup("PlayerGroup");
            RunnerGroup = World.AddRelationshipGroup("RunnerGroup");
            BomberGroup = World.AddRelationshipGroup("BomberGroup");
            PlayerGroup.SetRelationshipBetweenGroups(ZombieGroup, Relationship.Hate, true);
            PlayerGroup.SetRelationshipBetweenGroups(FriendlyGroup, Relationship.Companion, true);
            FriendlyGroup.SetRelationshipBetweenGroups(ZombieGroup, Relationship.Hate, true);
            RunnerGroup.SetRelationshipBetweenGroups(ZombieGroup, Relationship.Companion, true);
            FriendlyGroup.SetRelationshipBetweenGroups(RunnerGroup, Relationship.Hate, true);
            FriendlyGroup.SetRelationshipBetweenGroups(BomberGroup, Relationship.Hate, true);
        }
    }
}
