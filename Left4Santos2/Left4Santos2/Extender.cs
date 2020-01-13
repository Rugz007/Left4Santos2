using GTA;
using GTA.Native;
namespace Left4Santos2
{
    class Extender
    {
        public bool CanHearPlayer(Ped ped)
        {
            return Function.Call<bool>(Hash.CAN_PED_HEAR_PLAYER,Game.Player.Character,ped);
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
        public void MakeZombie(Ped ped)
        {

        }
    }
}
