using System;
using System.Collections.Generic;
using System.Text;

namespace Stacklands2KExpanded
{
    public static class CardDefs
    {
        public static readonly Card[] Cards = new Card[]
        {
            Card.Create<Cards.SilverIngot>(
                Consts.SILVER_INGOT, //ID
                "Silver Ingot", //Name
                "A bar of pure silver, useful for crafting and trading.", //Description
                5, //Value
                CardType.Resources, //CardType
                false, //IsBuilding
                null, //Init
                null, //CustomColour
                null //Connections
            ),
            Card.Create<Cards.SilverOre>(
                Consts.SILVER_ORE, //ID
                "Silver Ore", //Name
                "A chunk of silver ore, can be smelted into a silver ingot.", //Description
                2, //Value
                CardType.Resources, //CardType
                false, //IsBuilding
                null, //Init
                null, //CustomColour
                null //Connections
            ),
            Card.Create<Cards.IndustrialElectrolyser>(
                Consts.INDUSTRIAL_ELECTROLYSER, //ID,
                "Industrial Electrolyser", //Name,
                "A building that uses electricity to break down ores into their base metals, or to split water into chemical components.", //Description,
                8,
                CardType.Structures, //CardType,
                true, //IsBuilding,
                null, //Init,
                null, //CustomColour,
                new List<CardConnectorData>
                {
                    new CardConnectorData
                    {
                        EnergyConnectionType = CardDirection.input,
                        EnergyConnectionStrength = ConnectionType.LV,
                        EnergyConnectionAmount = 2
                    }
                } //Connections
            ),
            Card.Create<Cards.ElementalHydrogen>(
                Consts.ELEMENTAL_HYDROGEN, //ID
                "Hydrogen", //Name
                "A highly burnable, flammable gas, but has limited energy capabilities.", //Description
                2, //Value
                CardType.Resources, //CardType
                false, //IsBuilding
                null, //Init
                null, //CustomColour
                null //Connections
            ),
            Card.Create<Cards.ElementalOxygen>(
                Consts.ELEMENTAL_OXYGEN, //ID
                "Oxygen", //Name
                "A gas essential for combustion and respiration.", //Description
                2, //Value
                CardType.Resources, //CardType
                false, //IsBuilding
                null, //Init
                null, //CustomColour
                null //Connections
            )
        };

        public static readonly Idea[] Ideas = new Idea[]
        {
            new Idea(
                "silver_ingot_blueprint", //Name,
                BlueprintGroup.Resources, //Group,
                new List<Subprint>
                {
                    new Subprint
                    {
                        RequiredCards = new string[]
                        {
                            "silver_ore", "industrial_smelter" 
                        },
                        CardsToRemove = new string[]
                        {
                            "silver_ore"
                        },
                        Time = 10f,
                        ResultCard = "silver_ingot"

                    }
                }
            )
        };
    }
}