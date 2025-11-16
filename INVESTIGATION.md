# Investigations on the Energy system
The Energy system is a new system added to Stacklands 2000 by SokPop, which enables cards to both
generate Energy and Sewage as well as transport items, and consume Energy and Sewage.

Adding cards using this system is a little more complex than normal cards,
but mostly boils down to ensuring you use the correct classes and set the right connections.

# Card Classes
Internally, Stacklands uses the following classes for different types of generator:
* `ConsumingEnergyGenerator` - an energy producer that requires items.
* `PassiveEnergyGenerator` - an energy producer that does not require items.

# Defining Connections
Connections are managed by the `CardConnectorData` class, which are attached to a list
to a given card. An example of a custom card that emits energy (as defined in this repository) is below:

```csharp
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
```

And an example of an energy consumer is below - please note this is a partial definition,
as managed by the `CardLoader.cs` class.
```csharp
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
            EnergyConnectionStrength = ConnectionType.HV,
            EnergyConnectionAmount = 1
        }
    } //Connections
```

# Energy Consumer Breakdown
Energy consumers are hidden through a series of inheritance. For example, the chain of inheritance for the Industrial Smelter is:
* `IndustrialSmelter` inherits from `EnergyConsumer` =>
* `EnergyConsumer` inherits from `SewerCard`, and implements `IEnergyConsumer` =>
* `SewerCard` inherits from `CardData`

`IEnergyConsumer` only implements `GetEnergyConsumptionString()`.

`EnergyConsumer` implements the primary logic for energy consumption:
* `CanToggleOnOff`
* `CanSelectOutput`
* `CanHaveCardsWhileHasStatus`
* `UpdateCard`

The actual energy connections are managed by the `CardConnectorData` definitions attached to the card.
NOTE: It is currently unknown if LV/HV consumption can be combined,
as well as if a card can both consume and generate energy - as the inheritance is
not using interfaces, it's implicit that they cannot.
SewerCard is the implementation that adds the 'generates waste without sewer hookup' logic.
This does mean without some fancy reflection overrides, you need to implement a sewer connection on any energy consumer card.