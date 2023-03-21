using System.Text;
using Moq;
using CapitalGains;

public class ProgramTests 
{
    StringBuilder _consoleOutput;
    Mock<TextReader> _consoleInput;

    public ProgramTests()
    {
        _consoleOutput = new StringBuilder();
        var consoleOutputWriter = new StringWriter(_consoleOutput);
        _consoleInput = new Mock<TextReader>();
        Console.SetOut(consoleOutputWriter);
        Console.SetIn(_consoleInput.Object);
    }

    [Fact]
    public void Program_Case_1_plus_2()
    {
        // Arrange
        var line1 = $@"[{{""operation"":""buy"",""unit-cost"":10.00, ""quantity"": 100}},
                        {{""operation"":""sell"",""unit-cost"":15.00,""quantity"": 50}},
                        {{""operation"":""sell"",""unit-cost"":15.00, ""quantity"": 50}}]";
        
        var line2 = $@"[{{""operation"":""buy"",""unit-cost"":10.00, ""quantity"": 10000}},
                        {{""operation"":""sell"",""unit-cost"":20.00, ""quantity"": 5000}},
                        {{""operation"":""sell"",""unit-cost"":5.00, ""quantity"": 5000}}]";
        var line3 = "";
        SetupUserResponse(line1, line2, line3);
        var expectedPrompt = "[{\"tax\":0.00},{\"tax\":0.00},{\"tax\":0.00}]\n[{\"tax\":0.00},{\"tax\":10000.00},{\"tax\":0.00}]\n";

        // Act
        var outputLines = RunMainAndGetConsoleOutput();

        // Assert
        Assert.Equal(expectedPrompt, outputLines[0]);
    }


    [Fact]
    public void Program_Case_7()
    {
        // Arrange
        var line1 = $@"[{{""operation"":""buy"", ""unit-cost"":10.00, ""quantity"": 10000}},
                        {{""operation"":""sell"", ""unit-cost"":2.00, ""quantity"": 5000}},
                        {{""operation"":""sell"", ""unit-cost"":20.00, ""quantity"": 2000}},
                        {{""operation"":""sell"", ""unit-cost"":20.00, ""quantity"": 2000}},
                        {{""operation"":""sell"", ""unit-cost"":25.00, ""quantity"": 1000}},
                        {{""operation"":""buy"", ""unit-cost"":20.00, ""quantity"": 10000}},
                        {{""operation"":""sell"", ""unit-cost"":15.00, ""quantity"": 5000}},
                        {{""operation"":""sell"", ""unit-cost"":30.00, ""quantity"": 4350}},
                        {{""operation"":""sell"", ""unit-cost"":30.00, ""quantity"": 650}}]";
        var line2 = "";
        SetupUserResponse(line1, line2);
        var expectedPrompt = "[{\"tax\":0.00},{\"tax\":0.00},{\"tax\":0.00},{\"tax\":0.00},{\"tax\":3000.00},{\"tax\":0.00},{\"tax\":0.00},{\"tax\":3700.00},{\"tax\":0.00}]\n";

        // Act
        var outputLines = RunMainAndGetConsoleOutput();

        // Assert
        Assert.Equal(expectedPrompt, outputLines[0]);
    }


    private string[] RunMainAndGetConsoleOutput() 
    {
        Program.Main(default);
        return _consoleOutput.ToString().Split("\r\n");
    }

    private MockSequence SetupUserResponse(params string[] userResponses)
    {
        var seq = new MockSequence();
        foreach(var response in userResponses)
        {
            _consoleInput.InSequence(seq).Setup(x => x.ReadLine()).Returns(response);
        }
        return seq;
    }
}