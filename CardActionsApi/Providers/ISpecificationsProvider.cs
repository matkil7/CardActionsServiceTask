using CardActionsApi.Specifications;
using CardActionsApi.Specifications.Builders;

namespace CardActionsApi.Providers;

public interface ISpecificationsProvider<T> where T : class
{
    Dictionary<int, ISpecificationBuilder<T>> GetDefinitions();
}