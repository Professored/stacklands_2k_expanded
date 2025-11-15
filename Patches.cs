using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stacklands2KExpanded
{
    public class Patches
    {
        /// <summary>
        /// Load all new cards from the CardLoader after the base mod cards are loaded.
        /// </summary>
        /// <param name="__result">Harmony injected result from the base class.</param>
        [HarmonyPatch(typeof(GameDataLoader), nameof(GameDataLoader.LoadModCards))]
        [HarmonyPostfix]
        private static void AddCards(List<CardData> __result)
        {
            CardLoader.AddCards(__result);
        }

        /// <summary>
        /// Patch the industrial smelter to accept additional resource cards for smelting.
        /// </summary>
        /// <param name="__instance">Industrial Smelter instance</param>
        /// <param name="card">The checked card.</param>
        /// <returns>True if the card is valid, or base validity check result if not.</returns>
        [HarmonyPatch(typeof(IndustrialSmelter), nameof(IndustrialSmelter.CanHaveCard))]
        [HarmonyPrefix]
        private static bool CanHaveCard(IndustrialSmelter __instance, CardData card)
        {
            if (card.Id == Consts.SILVER_ORE)
            {
                return true;
            }

            return __instance.CanHaveCard(card);
        }
    }
}
