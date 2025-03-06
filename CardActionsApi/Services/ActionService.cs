using CardActionsApi.Models;
using CardActionsApi.Providers;
using CardActionsApi.Specifications;
using Utils;

namespace CardActionsApi.Services;

public interface IActionService
{
    Task<Result<IEnumerable<string>>> GetCardActions(string userId, string cardNumber);
}

public class ActionService : IActionService
{
    private readonly Dictionary<int, ISpecificationBuilder<CardDetails>> _specifications;
    private readonly ICardService _cardService;

    public ActionService(ISpecificationsProvider<CardDetails> actionDefinitionProvider, ICardService cardService)
    {
        _specifications = actionDefinitionProvider.GetDefinitions();
        _cardService = cardService; 
    }
    
    public IEnumerable<string> GetActions(CardDetails cardDetails)
    {
        return _specifications
            .Where(rule => rule.Value.IsValid(cardDetails))
            .Select(rule => $"ACTION{rule.Key}")
            .ToList();
    }
    public Task<Result<IEnumerable<string>>> GetCardActions(string userId, string cardNumber)
    {
        throw new NotImplementedException();
    }
}