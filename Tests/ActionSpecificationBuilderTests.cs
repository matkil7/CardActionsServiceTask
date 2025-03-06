using CardActionsApi.Models;
using CardActionsApi.Specifications;
using CardActionsApi.Specifications.Actions;
using Moq;

namespace Tests;

public class ActionSpecificationBuilderTests
{
    [Fact]
    public void IsValid_NoRulesDefined_ReturnsTrue()
    {
        // Arrange
        var builder = new ActionSpecificationBuilder();
        var details = new CardDetails(It.IsAny<string>(), It.IsAny<CardType>(), It.IsAny<CardStatus>(),
            It.IsAny<bool>());

        // Act
        var result = builder.IsValid(details);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsValid_AllSpecificationsAndConditionsPass_ReturnsTrue()
    {
        // Arrange
        var mockSpec = new Mock<ISpecification<CardDetails>>();
        mockSpec.Setup(s => s.IsSatisfiedBy(It.IsAny<CardDetails>())).Returns(true);

        var builder = new ActionSpecificationBuilder()
            .Rule(mockSpec.Object)
            .Rule(details => true);

        var details = new CardDetails(It.IsAny<string>(), It.IsAny<CardType>(), It.IsAny<CardStatus>(),
            It.IsAny<bool>());

        // Act
        var result = builder.IsValid(details);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsValid_AnySpecificationFails_ReturnsFalse()
    {
        // Arrange
        var passingSpec = new Mock<ISpecification<CardDetails>>();
        passingSpec.Setup(s => s.IsSatisfiedBy(It.IsAny<CardDetails>())).Returns(true);

        var failingSpec = new Mock<ISpecification<CardDetails>>();
        failingSpec.Setup(s => s.IsSatisfiedBy(It.IsAny<CardDetails>())).Returns(false);

        var builder = new ActionSpecificationBuilder()
            .Rule(passingSpec.Object)
            .Rule(failingSpec.Object)
            .Rule(details => details.IsPinSet);

        var details = new CardDetails(It.IsAny<string>(), It.IsAny<CardType>(), It.IsAny<CardStatus>(),
            It.IsAny<bool>());

        // Act
        var result = builder.IsValid(details);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsValid_AnyConditionFails_ReturnsFalse()
    {
        // Arrange
        var mockSpec = new Mock<ISpecification<CardDetails>>();
        mockSpec.Setup(s => s.IsSatisfiedBy(It.IsAny<CardDetails>())).Returns(true);

        var builder = new ActionSpecificationBuilder()
            .Rule(mockSpec.Object)
            .Rule(details => details.IsPinSet);

        var details = new CardDetails(It.IsAny<string>(), It.IsAny<CardType>(), It.IsAny<CardStatus>(), false);

        // Act
        var result = builder.IsValid(details);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsValid_MultipleSpecificationsAndConditionsPass_ReturnsTrue()
    {
        // Arrange
        var spec1 = new Mock<ISpecification<CardDetails>>();
        spec1.Setup(s => s.IsSatisfiedBy(It.IsAny<CardDetails>())).Returns(true);

        var spec2 = new Mock<ISpecification<CardDetails>>();
        spec2.Setup(s => s.IsSatisfiedBy(It.IsAny<CardDetails>())).Returns(true);

        var builder = new ActionSpecificationBuilder()
            .Rule(spec1.Object)
            .Rule(spec2.Object)
            .Rule(details => details.IsPinSet)
            .Rule(details => details.CardStatus == CardStatus.Active);
        var details = new CardDetails(It.IsAny<string>(), It.IsAny<CardType>(), CardStatus.Active, true);

        // Act
        var result = builder.IsValid(details);

        // Assert
        Assert.True(result);
    }
}