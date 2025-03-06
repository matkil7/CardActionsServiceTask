using CardActionsApi.Models;
using Utils;

namespace CardActionsApi.Services;

public interface IActionService
{
    Task<Result<IEnumerable<string>>> GetCardActions(string userId, string cardNumber);
}

public class ActionService : IActionService
{
    public Task<Result<IEnumerable<string>>> GetCardActions(string userId, string cardNumber)
    {
        throw new NotImplementedException();
    }
}