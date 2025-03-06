using CardActionsApi.Specifications;

namespace CardActionsApi.Providers;

public interface ISpecificationsProvider<T> where T : class
{
    Dictionary<int, ISpecificationBuilder<T>> GetDefinitions();
}