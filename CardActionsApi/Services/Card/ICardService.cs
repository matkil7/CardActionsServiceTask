using CardActionsApi.Models;

namespace CardActionsApi.Services.Card;

public interface ICardService
{
    Task<CardDetails?> GetCardDetails(string userId, string cardNumber);
}