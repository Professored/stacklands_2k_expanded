using System;
using System.Collections.Generic;
using System.Text;

namespace Stacklands2KExpanded
{
    public class Card
    {
        public readonly string Id;
        public readonly string Name;
        public readonly string Description;
        public readonly int Value;
        public readonly CardType CardType;
        public readonly Type ScriptType;
        public readonly bool IsBuilding;
        public readonly Action<CardData> Init;
        public readonly CardPalette CustomColour;
        public readonly List<CardConnectorData> Connections;

        public Card(
            string id,
            string name,
            string description,
            int value,
            CardType cardType,
            Type scriptType = null,
            bool building = false,
            Action<CardData> init = null,
            CardPalette customColour = null,
            List<CardConnectorData> connections = null
        )
        {
            Id = id;
            Name = name;
            Description = description;
            Value = value;
            CardType = cardType;
            ScriptType = scriptType ?? typeof(CardData);
            IsBuilding = building;
            Init = init;
            CustomColour = customColour;
            Connections = connections;
        }

        public static Card Create<T>(
            string id,
            string name,
            string description,
            int value,
            CardType cardType,
            bool building = false,
            Action<T> init = null,
            CardPalette customColour = null,
            List<CardConnectorData> connections = null
        ) where T : CardData
        {
            return new Card(
                id,
                name,
                description,
                value,
                cardType,
                typeof(T),
                building,
                init == null ? null : (c) => init((T)c),
                customColour,
                connections
            );
        }

        public static bool IsAlive(CardData card)
        {
            var typ = card.MyCardType;
            return typ == CardType.Fish || typ == CardType.Mobs || typ == CardType.Humans;
        }

        public static string Currency =>
            WorldManager.instance.CurrentBoard?.Id == Consts.ISLAND ? Consts.SHELL : Consts.COIN;
    }
}
