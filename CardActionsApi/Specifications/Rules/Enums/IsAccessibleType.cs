using CardActionsApi.Models;

namespace CardActionsApi.Specifications.Rules.Enums;


public class IsAccessibleType() : IsSatisfiableEnum<CardType, CardDetails>(Enum.GetValues<CardType>())
{
    public override bool IsSatisfiedBy(CardDetails cardDetails)
    {
        return _items.Contains(cardDetails.CardType);
    }
}