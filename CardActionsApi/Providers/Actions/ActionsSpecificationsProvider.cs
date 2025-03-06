using CardActionsApi.Models;
using CardActionsApi.Specifications;

namespace CardActionsApi.Providers.Actions;

public class ActionsSpecificationsProvider : ISpecificationsProvider<CardDetails>   
{
    public Dictionary<int, ISpecificationBuilder<CardDetails>> GetDefinitions()
    {
        throw new NotImplementedException();
    }
}