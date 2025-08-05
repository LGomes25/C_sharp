using Moq;

namespace ControleEstoque.Tests;

public class UnitTest1
{
    public interface ITestMoq
    {
        string DizerTestMoqPassou();
    }

    //Teste nativo vazio
    [Fact]
    public void Test1()
    {
    }
    //Test funcional moq
    [Fact]
    public void DeveDizerTestMoqPassou()
    {
        var mock = new Mock<ITestMoq>();
        mock.Setup(s => s.DizerTestMoqPassou()).Returns("Olá, mundo! O teste de moq pasou!!");

        var resultado = mock.Object.DizerTestMoqPassou();

        Assert.Equal("Olá, mundo! O teste de moq pasou!!", resultado);
    }
}
