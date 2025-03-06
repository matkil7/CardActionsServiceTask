using CardActionsApi.Models;

namespace CardActionsApi.Helpers;

public static class CardStatusHelper
{
    public static CardStatus[] OrderedInactiveActiveBlocked =>
    [
        ..OrderedInactiveActive,
        CardStatus.Blocked
    ];
    public static CardStatus[] OrderedInactiveActive =>
    [
        CardStatus.Ordered,
        ..InactiveActive
    ];
    public static CardStatus[] InactiveActive =>
    [
        CardStatus.Inactive,
        CardStatus.Active,
    ];
}