using System;
using System.Collections.Generic;
using System.Text;

namespace Stacklands2KExpanded
{
    public static class CardDefs
    {
        public static string BuildCardName(string shortName)
        {
            return $"{Consts.PREFIX}.{shortName}";
        }

        public static readonly Card[] Cards = new Card[]
        {
            Card.Create<Cards.SilverIngot>(
                Consts.SILVER_INGOT,
                "Silver Ingot",
                "A bar of pure silver, useful for crafting and trading.",
                5,
                CardType.Resources,
                false,
                null,
                null,
                null
            ),
            Card.Create<Cards.SilverOre>(
                Consts.SILVER_ORE,
                "Silver Ore",
                "A chunk of silver ore, can be smelted into a silver ingot.",
                2,
                CardType.Resources,
                false,
                null,
                null,
                null
            ),
            Card.Create<Cards.IndustrialElectrolyser>(
                Consts.INDUSTRIAL_ELECTROLYSER,
                "Industrial Electrolyser",
                "A building that uses electricity to break down ores into their base metals, or to split water into chemical components.",
                8,
                CardType.Structures,
                true,
                null,
                null,
                new List<CardConnectorData>
                {
                    new CardConnectorData
                    {
                        EnergyConnectionType = CardDirection.input,
                        EnergyConnectionStrength = ConnectionType.LV,
                        EnergyConnectionAmount = 2
                    },
                    // Provide a sewer output to satisfy SewerCard waste logic and prevent null refs.
                    new CardConnectorData
                    {
                        EnergyConnectionType = CardDirection.output,
                        EnergyConnectionStrength = ConnectionType.Sewer,
                        EnergyConnectionAmount = 1
                    }
                }
            ),
            Card.Create<Cards.ElementalHydrogen>(
                Consts.ELEMENTAL_HYDROGEN,
                "Hydrogen",
                "A highly burnable, flammable gas, but has limited energy capabilities.",
                2,
                CardType.Resources,
                false,
                null,
                null,
                null
            ),
            Card.Create<Cards.ElementalOxygen>(
                Consts.ELEMENTAL_OXYGEN,
                "Oxygen",
                "A gas essential for combustion and respiration.",
                2,
                CardType.Resources,
                false,
                null,
                null,
                null
            )
        };

        public static readonly Idea[] Ideas = new Idea[]
        {
            new Idea(
                Consts.IDEA_SILVER_SMELT,
                "Smelt Silver Ore",
                "1x Silver Ore\n\nSmelt silver ore into a silver ingot.",
                "Smelting...",
                BlueprintGroup.Resources,
                new List<Subprint>
                {
                    new Subprint
                    {
                        RequiredCards = new string[]
                        {
                            Consts.SILVER_ORE, "industrial_smelter", "any_worker"
                        },
                        CardsToRemove = new string[]
                        {
                            Consts.SILVER_ORE
                        },
                        Time = 10f,
                        ResultCard = Consts.SILVER_INGOT,
                        StatusTerm = "Smelt a Silver Ingot"
                    }
                },
                false
            ),
            new Idea(
                Consts.IDEA_ELECTROLYSIS,
                "Electrolyse Water",
                "1x Water\n\nSplit water into hydrogen and oxygen.",
                "Splitting...",
                BlueprintGroup.Resources,
                new List<Subprint>
                {
                    new Subprint
                    {
                        RequiredCards = new string[]
                        {
                            Consts.WATER, Consts.INDUSTRIAL_ELECTROLYSER
                        },
                        CardsToRemove = new string[]
                        {
                            Consts.WATER
                        },
                        Time = 12f,
                        ResultCard = Consts.ELEMENTAL_HYDROGEN,
                        ExtraResultCards = new string[]
                        {
                            Consts.ELEMENTAL_OXYGEN
                        },
                        StatusTerm = "Electrolyze Water",
                    }
                },
                false
            ),
            new Idea(
                Consts.IDEA_INDUSTRIAL_ELECTROLYSER,
                "Industrial Electrolyser",
                "1x Iron Bar\n1x Silver Ingot\n1x Gold Bar\n1x Factory Parts\n\nBuild an Industrial Electrolyser.",
                "Building...",
                BlueprintGroup.Building,
                new List<Subprint>
                {
                    new Subprint
                    {
                        RequiredCards = new string[]
                        {
                            "iron_bar", Consts.SILVER_INGOT, "gold_bar", "factory_parts"
                        },
                        CardsToRemove = new string[]
                        {
                            "iron_bar", Consts.SILVER_INGOT, "gold_bar", "factory_parts"
                        },
                        Time = 15f,
                        ResultCard = Consts.INDUSTRIAL_ELECTROLYSER,
                        StatusTerm = "Build Industrial Electrolyser"
                    }
                })
        };
    }
}