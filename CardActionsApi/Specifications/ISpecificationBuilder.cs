namespace CardActionsApi.Specifications;

public interface ISpecificationBuilder<T> where T: class
{
    public ISpecificationBuilder<T> Rule(ISpecification<T> spec);
    public ISpecificationBuilder<T> Rule(Func<T, bool> condition);
    public bool IsValid(T details);

}