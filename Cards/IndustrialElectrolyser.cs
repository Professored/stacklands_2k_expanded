using System;
using System.Collections.Generic;
using System.Text;
using TMPro;

namespace Stacklands2KExpanded.Cards
{
    public class IndustrialElectrolyser : EnergyConsumer
    {
        public override bool CanHaveCard(CardData otherCard)
        {
            if (otherCard.Id == Consts.WATER)
            {
                return true;
            }
            else
            {
                return base.CanHaveCard(otherCard);
            }
        }

        public override void UpdateCard()
        {
            //Remove poop generation. Electrolysis is clean!
            //We do this by overriding UpdateCard and not calling base.UpdateCard first,
            //which would go down the chain to SewerCard.UpdateCard and call CheckSpawnPoop.
            CardData data = this as CardData;

            data.UpdateCard();
        }
    }
}
