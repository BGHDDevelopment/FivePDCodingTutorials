using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;

namespace YouTubeTutorial1
{
    
    [CalloutProperties("Mugging Test", "BGHDDevelopment", "0.0.2")]
    public class Mugging : Callout
    {
        Ped suspect, victim;

        public Mugging()
        {
            Random random = new Random();
            int x = random.Next(1, 100 + 1);
            if (x <= 40)
            {
                InitInfo(new Vector3(-274.567f, 272.508f, 89.877f));

            } else if (x >= 40 && x <= 60)
            {
                InitInfo(new Vector3(-552.532f, 232.701f, 82.5242f));
            }
            else if(x >= 60)
            {
                InitInfo(new Vector3(45.2834f, 268.411f,109.496f));
            }
            ShortName = "Mugging Test";
            CalloutDescription = "This is a callout test for a mugging";
            ResponseCode = 1;
            StartDistance = 120f;
        }

        public async override Task OnAccept()
        {
            InitBlip();
            UpdateData();
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