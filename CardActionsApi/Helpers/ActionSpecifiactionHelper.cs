using CardActionsApi.Models;
using CardActionsApi.Specifications;
using CardActionsApi.Specifications.Actions;
using CardActionsApi.Specifications.Rules.Enums;

namespace CardActionsApi.Helpers;

public class ActionSpecifiactionHelper
{
    public static ISpecificationBuilder<CardDetails> AccessibleCardTypeAndAnyOfState(CardStatus[] statuses) =>
        new ActionSpecificationBuilder().Rule(x=> Enum.GetValues<CardType>().Contains(x.CardType)).Rule(new AnyOfStatuses(statuses));
    public static ISpecificationBuilder<CardDetails> AccessibleCardTypeAndAccessibleState() =>
        new ActionSpecificationBuilder().Rule(x => Enum.GetValues<CardType>().Contains(x.CardType)).Rule(x => Enum.GetValues<CardStatus>().Contains(x.CardStatus));
    public static ISpecificationBuilder<CardDetails> AnyOfCardTypeAndAccessibleState(CardType[] types) =>
        new ActionSpecificationBuilder().Rule(new AnyOfTypes(types)).Rule(x => Enum.GetValues<CardStatus>().Contains(x.CardStatus));
}