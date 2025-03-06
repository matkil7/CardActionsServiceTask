namespace CardActionsApi.Specifications;

public interface ISpecification<T>
{
    bool IsSatisfiedBy(T details);
}