using CardActionsApi.Models;

namespace CardActionsApi.Specifications.Builders.Action;

public class ActionSpecificationBuilder : ISpecificationBuilder<CardDetails>
{
    private readonly List<ISpecification<CardDetails>> _specifications = new();
    private readonly List<Func<CardDetails, bool>> _conditions = new();    
    
    public ISpecificationBuilder<CardDetails> Rule(ISpecification<CardDetails> spec)
    {
        _specifications.Add(spec);
        return this;
    } 
    public ISpecificationBuilder<CardDetails> Rule(Func<CardDetails, bool> condition)
    {
        _conditions.Add(condition);
        return this;
    }
    public bool IsValid(CardDetails details)
    {
        return _specifications.All(spec => spec.IsSatisfiedBy(details) && _conditions.All(condition => condition(details)));
    }
}