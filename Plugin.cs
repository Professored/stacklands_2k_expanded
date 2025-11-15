using System;
using System.Collections.Generic;
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
            Harmony.PatchAll();
        }

        public override void Ready()
        {
            Logger.Log("Stacklands 2K Expanded mod is ready!");
        }
    }
}
