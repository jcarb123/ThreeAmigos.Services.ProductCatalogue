namespace ThreeAmigos.Services.ProductCatalogue.UnitTests;

public class UnitTest1
{
    [Fact]
    public void TestAddition()
    {
        // Arrange
        const int number1 = 1;
        const int number2 = 1;

        // Act
        const int result = number1 + number2;

        // Assert
        Assert.Equal(2, result);
    }
}