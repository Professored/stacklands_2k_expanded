using System;
using System.Collections.Generic;
using System.Text;

namespace Stacklands2KExpanded
{
    static class Consts
    {
        public const string PREFIX = "s2000_expansion.";
        public const string IDEA = "_blueprint";

        public const string SILVER_ORE = PREFIX + "silver_ore";
        public const string SILVER_INGOT = PREFIX + "silver_ingot";

        public const string INDUSTRIAL_ELECTROLYSER = PREFIX + "industrial_electrolyser";

        public const string WATER = "water";
        public const string ELEMENTAL_HYDROGEN = PREFIX + "hydrogen";
        public const string ELEMENTAL_OXYGEN = PREFIX + "oxygen";

        public const string IDEA_ELECTROLYSIS = PREFIX + "electrolysis" + IDEA;
        public const string IDEA_SILVER_SMELT = PREFIX + "silver_smelting" + IDEA;
        public const string IDEA_INDUSTRIAL_ELECTROLYSER = PREFIX + "industrial_electrolyser" + IDEA;

        public const string COIN = "gold";
        public const string SHELL = "shell";

        public const string TEN_DOLLARS = "ten_dollar";
        public const string FIFTY_DOLLARS = "fifty_dollar";
        public const string HUNDRED_DOLLARS = "hundred_dollar";
        public const string TWENTY_DOLLARS = "twenty_dollar";

        public const string MAINLAND = "main";
        public const string ISLAND = "island";

        public const string LV_CONNECTOR = "LV";
        public const string HV_CONNECTOR = "HV";

        public static string Idea(string id)
        {
            return id + IDEA;
        }
    }
}
