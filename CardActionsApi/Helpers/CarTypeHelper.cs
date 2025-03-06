using CardActionsApi.Models;

namespace CardActionsApi.Helpers;

public static class CarTypeHelper
{
    public static CardType[] CardTypes =>
        Enum.GetValues<CardType>(); 
    
}