﻿using GTA;
using GTA.Math;
using GTA.Native;
namespace Left4Santos2
{
    class Extender
    {
        public bool CanHearPlayer(Ped ped)
        {
            return Function.Call<bool>(Hash.CAN_PED_HEAR_PLAYER, Game.Player.Character, ped);
        }
        public bool CanSeePlayer(Ped ped)
        {
            return Function.Call<bool>(Hash.HAS_ENTITY_CLEAR_LOS_TO_ENTITY_IN_FRONT, ped, Game.Player.Character);
        }
        public void GiveZombieLook(Ped ped)
        {
            if (ped != Game.Player.Character)
            {
                Function.Call(Hash.STOP_PED_SPEAKING, new InputArgument[]
                {
                    ped.Handle,true
                });
                Function.Call(Hash.DISABLE_PED_PAIN_AUDIO, new InputArgument[]
                {
                    ped.Handle,true
                });
                ped.AlwaysKeepTask = true;
                ped.IsEnemy = true;
                ped.Health = 3000;
                Function.Call(Hash.SET_BLOCKING_OF_NON_TEMPORARY_EVENTS, ped, 1);
                Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, ped, 0, 0);
                Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, ped, 5, 1);
                Function.Call(Hash.APPLY_PED_DAMAGE_PACK, ped, "BigHitByVehicle", 0.0, 9.0);
                Function.Call(Hash.APPLY_PED_DAMAGE_PACK, ped, "SCR_Dumpster", 0.0, 9.0);
                Function.Call(Hash.APPLY_PED_DAMAGE_PACK, ped, "SCR_Torture", 0.0, 9.0);
            }
        }
        public void SetPopulation(float multiplier)
        {
            Function.Call(Hash.SET_PED_DENSITY_MULTIPLIER_THIS_FRAME, multiplier);
        }
        public void MakeZombie(Ped ped, RelationshipGroup relationshipGroup)
        {
            GiveZombieLook(ped);
            ped.RelationshipGroup = relationshipGroup;
            if (!Function.Call<bool>(Hash.HAS_CLIP_SET_LOADED, new InputArgument[]
                    {"move_m@drunk@verydrunk"}))
            {
                Function.Call(Hash.REQUEST_CLIP_SET, new InputArgument[]
                {
                        "move_m@drunk@verydrunk"
                });
            }
            if (Function.Call<bool>(Hash.HAS_CLIP_SET_LOADED, new InputArgument[]
            {
                    "move_m@drunk@verydrunk"
            }))
            {
                Function.Call(Hash.SET_PED_MOVEMENT_CLIPSET, new InputArgument[]
                {
                        ped.Handle,"move_m@drunk@verydrunk",1048576000
                });
            }
            Function.Call(Hash.STOP_PED_SPEAKING, new InputArgument[]
            {
                    ped.Handle,true
            });

            ped.AlwaysKeepTask = true;
            ped.IsEnemy = true;
            Function.Call(Hash.SET_BLOCKING_OF_NON_TEMPORARY_EVENTS, ped, 1);
            Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, ped, 0, 0);
            Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, ped, 46, 1);
        }
        public void MakeFriendly(Ped ped, RelationshipGroup relationshipGroup)
        {
            ped.RelationshipGroup = relationshipGroup;
            ped.Health = 100;
            Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, ped, Game.Player.Character.PedGroup);
            ped.Weapons.Give(WeaponHash.CarbineRifle, 100, true, true);
            ped.Task.FightAgainstHatedTargets(30f);
            ped.AlwaysKeepTask = true;
            Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, ped, 46, true);
            Blip blip = ped.AddBlip();
            blip.Sprite = BlipSprite.Friend;
            blip.Color = BlipColor.Blue;
        }
        public void MakeZombieGoToPed(Ped ped, Vector3 target)
        {
            if (!Function.Call<bool>(Hash.HAS_CLIP_SET_LOADED, new InputArgument[]
                    {"move_m@drunk@verydrunk"}))
            {
                Function.Call(Hash.REQUEST_CLIP_SET, new InputArgument[]
                {
                        "move_m@drunk@verydrunk"
                });
            }
            if (Function.Call<bool>(Hash.HAS_CLIP_SET_LOADED, new InputArgument[]
            {
                    "move_m@drunk@verydrunk"
            }))
            {
                Function.Call(Hash.SET_PED_MOVEMENT_CLIPSET, new InputArgument[]
                {
                        ped.Handle,"move_m@drunk@verydrunk",1048576000
                });
            }
            Function.Call(Hash.STOP_PED_SPEAKING, new InputArgument[]
            {
                    ped.Handle,true
            });
            Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, ped, target.X, target.Y, target.Z, 1.0f, -1, 0.0f, 0f);
        }
    }
    
}
