using NUnit.Framework;

namespace IPAnalyzer;

[TestFixture]
public class AccessCounterTest
{
    private ConfigurationInfo _configurationInfo;
    private Configuration _configuration;

    [SetUp]
    public void SetUp()
    {
        // Arrange
        var logFilePath = "test.log";
        var outputFilePath = "output.txt";
        var addressStart = "192.168.1.1";
        var addressMask = "16";
        var timeStart = "01.01.2023";
        var timeEnd = "01.02.2023";
        
        string[] testData = {
            "192.168.1.1:2023-01-01 08:00:00",
            "192.168.1.1:2023-01-02 08:00:00",
            "192.168.1.2:2023-01-05 12:30:00",
            "192.168.1.3:2023-01-10 16:45:00",
            "192.168.1.3:2023-01-12 16:45:00",
            "192.168.1.4:2023-01-15 20:15:00",
            "192.168.1.5:2023-01-20 09:30:00",
            "192.168.1.6:2023-01-25 14:00:00",
            "192.168.1.7:2023-01-28 18:45:00",
            "192.168.1.10:2023-02-08 17:00:00",
            "192.168.1.10:2023-02-10 17:00:00"
        };

        File.WriteAllLines(logFilePath, testData);
        File.WriteAllText(outputFilePath, String.Empty);
        var fakeArgs = new[]
        {
            $"--{ConfigurationInfo.LogFilePathOptionName}", $"{logFilePath}",
            $"--{ConfigurationInfo.OutputFilePathOptionName}", $"{outputFilePath}",
            $"--{ConfigurationInfo.AddressStartOptionName}", $"{addressStart}",
            $"--{ConfigurationInfo.AddressMaskOptionName}", $"{addressMask}",
            $"--{ConfigurationInfo.TimeStartOptionName}", $"{timeStart}",
            $"--{ConfigurationInfo.TimeEndOptionName}", $"{timeEnd}"
        };

        _configurationInfo = new CommandLineConfigurationInfo(fakeArgs);
        _configuration = Configuration.Create(_configurationInfo);
        

    }

    [TearDown]
    public void TearDown()
    {
        _configuration.Dispose();
        File.Delete(_configurationInfo.LogFilePath.Value);
        File.Delete(_configurationInfo.OutputFilePath.Value);
    }

    [Test]
    public void Count_Empty()
    {
        var accessCounter= new AccessCounter(_configuration);
        Assert.Pass();
    }
    
    [Test]
    public void Count_Full()
    {
        var accessCounter= new AccessCounter(_configuration);
        accessCounter.CountAccess();
        Assert.Pass();
    }
    
}