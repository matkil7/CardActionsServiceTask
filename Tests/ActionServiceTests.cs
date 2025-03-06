using CardActionsApi.Models;
using CardActionsApi.Providers;
using CardActionsApi.Services;
using CardActionsApi.Specifications;
using CardActionsApi.Specifications.Actions;
using Moq;

namespace Tests;

public class ActionServiceTests
{
   private Mock<ICardService> _mockCardService;
    private  Mock<ISpecificationsProvider<CardDetails>> _mockSpecificationProvider;
    private  IActionService _service;

    private void Create_SUT()
    {
        _service = new ActionService( _mockSpecificationProvider.Object, _mockCardService.Object);
    }
    public ActionServiceTests()
    {
        _mockCardService = new Mock<ICardService>();
        _mockSpecificationProvider = new Mock<ISpecificationsProvider<CardDetails>>();
    }

    [Fact]
    public async Task GetCardActions_ValidUserAndCard_ReturnsActions()
    {
        // Arrange
        var userId = "user123";
        var cardNumber = "1234567890";
        var cardDetails = new CardDetails(cardNumber, It.IsAny<CardType>(), It.IsAny<CardStatus>(),
            It.IsAny<bool>());
        var dict = new Dictionary<int, ISpecificationBuilder<CardDetails>>();
        
        dict.Add(1, new ActionSpecificationBuilder());
        dict.Add(2, new ActionSpecificationBuilder());
       
        var parsedToActionNames = dict.Select(rule => $"ACTION{rule.Key}");
        
        _mockCardService.Setup(s => s.GetCardDetails(userId, cardNumber))
            .ReturnsAsync(cardDetails);
        _mockSpecificationProvider.Setup(x => x.GetDefinitions()).Returns(dict); // Act
        Create_SUT();
        var result = await _service.GetCardActions(userId, cardNumber);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(parsedToActionNames, result.Value);
    }

    [Fact]
    public async Task GetCardActions_CardNotFound_ReturnsFailure()
    {
        // Arrange
        var userId = "user123";
        var cardNumber = "1234567890";

        _mockCardService.Setup(s => s.GetCardDetails(userId, cardNumber))
            .ReturnsAsync((CardDetails)null);

        // Act
        var result = await _service.GetCardActions(userId, cardNumber);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("No card found", result.Errors);
    }

    [Fact]
    public async Task GetCardActions_EmptyActionsList_ReturnsEmptyList()
    {
        // Arrange
        var userId = "user123";
        var cardNumber = "1234567890";
        var cardDetails = new CardDetails(cardNumber, It.IsAny<CardType>(), It.IsAny<CardStatus>(),
            It.IsAny<bool>());
        var expectedActions = new Dictionary<int, ISpecificationBuilder<CardDetails>>();

        _mockCardService.Setup(s => s.GetCardDetails(userId, cardNumber))
            .ReturnsAsync(cardDetails);
        _mockSpecificationProvider.Setup(p => p.GetDefinitions())
            .Returns(expectedActions);

        // Act
        var result = await _service.GetCardActions(userId, cardNumber);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Value);
    }

    [Fact]
    public async Task GetCardActions_ExceptionThrown_ReturnsFailure()
    {
        // Arrange
        var userId = "user123";
        var cardNumber = "1234567890";

        _mockCardService.Setup(s => s.GetCardDetails(userId, cardNumber))
            .ThrowsAsync(new Exception("Unexpected error"));

        // Act
        var result = await _service.GetCardActions(userId, cardNumber);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Unexpected error", result.Errors);
    }
    
    
}