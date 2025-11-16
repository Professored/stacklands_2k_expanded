using System.Collections.Generic;

namespace Stacklands2KExpanded.Cards
{
    public class IndustrialElectrolyser : EnergyConsumer
    {
        // Hide base AcceptedCards if it exists (pattern used in generators).
        public new List<string> AcceptedCards = new List<string>
        {
            Consts.WATER
        };

        public override bool CanHaveCard(CardData otherCard)
        {
            if (otherCard.Id == Consts.WATER)
                return true;
            return base.CanHaveCard(otherCard);
        }

        // Keep base UpdateCard unless you intentionally suppress sewer logic.
        // public override void UpdateCard() { base.UpdateCard(); }
    }
}
