namespace CardActionsApi.Specifications.Rules;

public abstract class AnyOf<T, TModel> : ISpecification<TModel> where T :  Enum{
    protected T[] _items { get; init; }
    
    public AnyOf (T[] items)
    {
        _items = items;
    }

    public abstract bool IsSatisfiedBy(TModel cardDetails);

}