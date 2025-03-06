namespace CardActionsApi.Specifications.Rules;

public abstract class IsSatisfiableEnum<T, TModel> : ISpecification<TModel> where T : Enum
{
    protected T[] _items { get; init; }

    public IsSatisfiableEnum(T[] items)
    {
        _items = items;
    }

    public abstract bool IsSatisfiedBy(TModel cardDetails);
}