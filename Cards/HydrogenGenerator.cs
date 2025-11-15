using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Stacklands2KExpanded.Cards
{
    public class HydrogenGenerator : ConsumingEnergyGenerator
    {
        public new List<string> AcceptedCards = new List<string>
        {
            Consts.ELEMENTAL_HYDROGEN
        };

        public new List<CardAmountPair> CardsToConsume = new List<CardAmountPair>
        {
            new CardAmountPair
            {
                CardId = Consts.ELEMENTAL_HYDROGEN,
                Amount = 1
            }
        };


        public override bool CanHaveCard(CardData otherCard)
        {
            if (otherCard.Id == Consts.ELEMENTAL_HYDROGEN)
            {
                return true;
            }
            else
            {
                return base.CanHaveCard(otherCard);
            }
        }
    }
}
