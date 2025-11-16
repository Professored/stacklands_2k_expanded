using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Stacklands2KExpanded
{
    internal class Plugin : Mod
    {
        public static Plugin Instance;
        public static ModLogger StaticLogger;
        private void Awake()
        {
            Instance = this;
            StaticLogger = Logger;
            Logger.Log("Stacklands 2K Expanded mod loaded.");
            Harmony.PatchAll(typeof(Patches));
        }

        public override void Ready()
        {
            Logger.Log("Adding Stacklands 2K Expanded mod cards to set card bags...");

            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(
                SetCardBagType.Cities_Basic_Smeltables, Consts.IDEA_SILVER_SMELT, 1);

            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(
                SetCardBagType.Cities_Ideas_Industry, Consts.IDEA_ELECTROLYSIS, 1);

            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(
                SetCardBagType.Cities_BasicResources, Consts.SILVER_ORE, 2);

            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(
                SetCardBagType.Cities_Ideas_Industry, Consts.INDUSTRIAL_ELECTROLYSER, 1);

            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(
                SetCardBagType.Cities_Basic_Smeltables, Consts.SILVER_INGOT, 1);

            Logger.Log("Stacklands 2K Expanded mod finished adding cards to set card bags.");
            Logger.Log("Stacklands 2K Expanded mod is ready.");
        }
    }
}
