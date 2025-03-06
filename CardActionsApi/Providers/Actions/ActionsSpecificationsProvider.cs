using CardActionsApi.Helpers;
using CardActionsApi.Models;
using CardActionsApi.Specifications;
using CardActionsApi.Specifications.Actions;
using CardActionsApi.Specifications.Rules.Enums;

namespace CardActionsApi.Providers.Actions;

public class ActionsSpecificationsProvider : ISpecificationsProvider<CardDetails>
{
    public Dictionary<int, ISpecificationBuilder<CardDetails>> GetDefinitions()
    {
        return new Dictionary<int, ISpecificationBuilder<CardDetails>>
        {
            {
                1, ActionSpecifiactionHelper.IsAccessibleCardTypeAndAnyOfState([CardStatus.Active])
            },
            {
                2, ActionSpecifiactionHelper.IsAccessibleCardTypeAndAnyOfState([CardStatus.Inactive])
            },
            {
                3, ActionSpecifiactionHelper.IsAccessibleCardTypeAndAccessibleState()
            },
            { 4, ActionSpecifiactionHelper.IsAccessibleCardTypeAndAccessibleState() },
            {
                5,
                ActionSpecifiactionHelper.AnyOfCardTypeAndIsAccessibleState([CardType.Credit])
            },
            {
                6,
                new ActionSpecificationBuilder().Rule(x => x.CardType == CardType.Credit).Rule(x =>
                    x.IsPinSet && CardStatusHelper.OrderedInactiveActiveBlocked.Contains(x.CardStatus))
            },
            {
                7, new ActionSpecificationBuilder().Rule(x => CarTypeHelper.CardTypes.Contains(x.CardType)).Rule(x =>
                    !x.IsPinSet && CardStatusHelper.OrderedInactiveActiveBlocked.Contains(x.CardStatus))
            },
            {
                8,
                new ActionSpecificationBuilder().Rule(new IsAccessibleStatus())
                    .Rule(new AnyOfStatuses(CardStatusHelper.OrderedInactiveActiveBlocked))
            },
            { 9, new ActionSpecificationBuilder().Rule(new IsAccessibleType()).Rule(new IsAccessibleStatus()) },
            {
                10,
                new ActionSpecificationBuilder().Rule(new IsAccessibleType())
                    .Rule(new AnyOfStatuses(CardStatusHelper.OrderedInactiveActive))
            },
            {
                11, new ActionSpecificationBuilder().Rule(new IsAccessibleType()).Rule(new AnyOfStatuses(
                    CardStatusHelper.InactiveActive
                ))
            },
            {
                12, new ActionSpecificationBuilder().Rule(new IsAccessibleType()).Rule(new AnyOfStatuses(
                    CardStatusHelper.OrderedInactiveActive
                ))
            },
            {
                13,
                new ActionSpecificationBuilder().Rule(new IsAccessibleType())
                    .Rule(new AnyOfStatuses(CardStatusHelper.OrderedInactiveActive))
            },
        };
    }
}