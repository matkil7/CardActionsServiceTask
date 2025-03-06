using CardActionsApi.Models;

namespace CardActionsApi.Specifications.Rules.Enums;

public class IsAccessibleStatus() : IsSatisfiableEnum<CardStatus, CardDetails>(Enum.GetValues<CardStatus>())
{
    public override bool IsSatisfiedBy(CardDetails cardDetails)
    {
        return _items.Contains(cardDetails.CardStatus);
    }
}