using AmandsSense.Components;
using AmandsSense.Helpers;
using AmandsSense.Models;
using EFT;
using HarmonyLib;
using SPT.Reflection.Patching;
using System.Reflection;

namespace AmandsSense.Patches
{
    public class AmandsKillPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(Player), nameof(Player.OnDead));
        }

        // Solo keeps the old rule: a body counts when it has an aggressor, i.e. when OnDead calls OnBeenKilledByAggressor.
        // On a Fika client every death counts, because ObservedPlayer only passes OnBeenKilledByAggressor on to the base
        // when we were the killer, and bodies that were already dead when we joined have no aggressor at all.
        [PatchPostfix]
        public static void PatchPostFix(Player __instance, IPlayer ___LastAggressor)
        {
            if (___LastAggressor == null && !FikaCompat.IsFika) return;

            AmandsSenseClass.DeadPlayers.Add(new SenseDeadPlayer(__instance, ___LastAggressor as Player));
        }
    }
}
