using Utils;

namespace CardActionsApi.Services.Action;

public interface IActionService
{
    Task<Result<IEnumerable<string>>> GetCardActions(string userId, string cardNumber);
}