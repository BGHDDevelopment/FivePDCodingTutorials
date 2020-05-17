using System;
using System.Threading.Tasks;
using CalloutAPI;
using CitizenFX.Core;

namespace YouTubeTutorial1
{
    
    [CalloutProperties("Mugging Test", "BGHDDevelopment", "0.0.1", Probability.High)]
    public class Mugging : Callout
    {
        Ped suspect, victim;

        public Mugging()
        {
            Random rnd = new Random();
            float offsetX = rnd.Next(100, 700);
            float offsetY = rnd.Next(100, 700);
            
            InitBase(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(new Vector3(offsetX, offsetY, 0))));

            ShortName = "Mugging Test";
            CalloutDescription = "This is a callout test for a mugging";
            ResponseCode = 1;
            StartDistance = 120f;
        }

        public async override Task Init()
        {
            OnAccept();
            suspect = await SpawnPed(GetRandomPed(), Location);
            victim = await SpawnPed(GetRandomPed(), Location);
            suspect.AlwaysKeepTask = true;
            suspect.BlockPermanentEvents = true;
            victim.AlwaysKeepTask = true;
            victim.BlockPermanentEvents = true;
        }

        public override void OnStart(Ped player)
        {
            base.OnStart(player);
            suspect.Weapons.Give(WeaponHash.Knife, 1, true, true);
            suspect.Task.FightAgainst(victim);
            victim.Task.ReactAndFlee(suspect);
            suspect.AttachBlip();
            victim.AttachBlip();
        }
    }
}