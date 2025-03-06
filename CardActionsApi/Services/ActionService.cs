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
    private Dictionary<int, ISpecificationBuilder<CardDetails>> _specifications = new();
    private readonly ICardService _cardService;

    public ActionService(ISpecificationsProvider<CardDetails> actionDefinitionProvider, ICardService cardService)
    {
        InitializeSpecifications(actionDefinitionProvider.GetDefinitions());
        _cardService = cardService;
    }

    private void InitializeSpecifications(Dictionary<int, ISpecificationBuilder<CardDetails>>? specifications)
    {
        if (specifications != null && specifications.Any())
        {
            _specifications = specifications;
        }
    }

    public IEnumerable<string> GetActions(CardDetails cardDetails)
    {
        return _specifications.Any()
            ? _specifications
                .Where(rule => rule.Value.IsValid(cardDetails))
                .Select(rule => $"ACTION{rule.Key}")
                .ToList()
            : Enumerable.Empty<string>();
    }

    public async Task<Result<IEnumerable<string>>> GetCardActions(string userId, string cardNumber)
    {
        try
        {
            var cardDetails = await _cardService.GetCardDetails(userId, cardNumber);
            if (cardDetails == null)
            {
                return Result<IEnumerable<string>>.Failure("No card found");
            }

            return Result<IEnumerable<string>>.Success(GetActions(cardDetails));
        }
        catch (Exception e)
        {
            return Result<IEnumerable<string>>.Failure("Unexpected error");
        }
    }
}