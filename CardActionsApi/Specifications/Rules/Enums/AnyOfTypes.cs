using CardActionsApi.Models;

namespace CardActionsApi.Specifications.Rules.Enums;

public class AnyOfTypes(CardType[] items) : AnyOf<CardType, CardDetails>(items)
{
    public override bool IsSatisfiedBy(CardDetails cardDetails)
    {
        return _items.Contains(cardDetails.CardType);
    }
}