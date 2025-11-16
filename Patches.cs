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
            try
            {
                CardLoader.AddCards(__result);
            }
            catch (Exception e)
            {
                Plugin.StaticLogger.LogException("Failed to load mod cards: " + e);
            }
        }

        /// <summary>
        /// Patch the industrial smelter to accept additional resource cards for smelting.
        /// </summary>
        /// <param name="__instance">Industrial Smelter instance</param>
        /// <param name="card">The checked card.</param>
        /// <returns>True if the card is valid, or base validity check result if not.</returns>
        [HarmonyPatch(typeof(IndustrialSmelter), nameof(IndustrialSmelter.CanHaveCard))]
        [HarmonyPostfix]
        private static void CanHaveCard(ref bool __result, CardData otherCard)
        {
            if (otherCard.Id == Consts.SILVER_ORE)
            {
                __result = true;
            }
        }

        [HarmonyPatch(typeof(SokLoc), nameof(SokLoc.SetLanguage))]
        [HarmonyPostfix]
        public static void LanguageChanged(SokLoc __instance)
        {
            if (SokLoc.instance == null)
            {
                return;
            }

            foreach (var term in CardLoader.Translations)
            {
                SokLoc.instance.CurrentLocSet.TermLookup[term.Id] = term;
            }
        }

        static List<string> debugCheckCards = new List<string>
        {
            Consts.INDUSTRIAL_ELECTROLYSER
        };

        [HarmonyPatch(typeof(Subprint), nameof(Subprint.StackMatchesSubprint))]
        [HarmonyPrefix]
        private static void StackMatchesSubprintPrefix(Subprint __instance, GameCard rootCard)
        {
            if (rootCard == null) return;

            if (!debugCheckCards.Contains(rootCard.CardData.Id))
            {
                return;
            }

            var sb = new StringBuilder();
            sb.Append("[SubprintDebug] Checking stack for recipe: ")
              .Append(__instance.StatusTerm)
              .Append(" Required: [")
              .Append(string.Join(", ", __instance.RequiredCards))
              .Append("] RootCard: ").Append(rootCard.CardData.Id);

            // Build current stack snapshot (top to bottom)
            var stackIds = new List<string>();
            var cursor = rootCard;
            while (cursor != null)
            {
                stackIds.Add(cursor.CardData.Id);
                cursor = cursor.Child;
            }
            sb.Append(" Stack: [").Append(string.Join(" -> ", stackIds)).Append("]");

            Plugin.StaticLogger.Log(sb.ToString());
        }

        [HarmonyPatch(typeof(Subprint), nameof(Subprint.StackMatchesSubprint))]
        [HarmonyPostfix]
        private static void StackMatchesSubprintPostfix(Subprint __instance, GameCard rootCard, ref SubprintMatchInfo matchInfo, bool __result)
        {
            if (rootCard == null) return;

            if (!debugCheckCards.Contains(rootCard.CardData.Id))
            {
                return;
            }

            if (__result)
            {
                Plugin.StaticLogger.Log($"[SubprintDebug] SUCCESS for '{__instance.StatusTerm}'");
            }
            else
            {
                // missingCards was populated inside the method; copy current state.
                var missing = (__instance.missingCards != null && __instance.missingCards.Count > 0)
                    ? string.Join(", ", __instance.missingCards)
                    : "(none or early exit)";
                Plugin.StaticLogger.Log($"[SubprintDebug] FAIL for '{__instance.StatusTerm}' Missing/Unmatched: {missing}");
            }
        }
    }
}
