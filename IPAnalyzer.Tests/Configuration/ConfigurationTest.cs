using System.Net;
using NUnit.Framework;

namespace IPAnalyzer.Tests;

[TestFixture]
public class ConfigurationTest
{
    [Test]
    public void CommandLineConfigurationInfo_Sets_Values_Correctly()
    {
        // Arrange
        var logFilePath = "test.log";
        var outputFilePath = "output.txt";
        var addressStart = "192.168.0.1";
        var addressMask = "16";
        var timeStart = "01.01.2023";
        var timeEnd = "01.02.2023";

        File.WriteAllText(logFilePath, String.Empty);
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

        var configInfo = new CommandLineConfigurationInfo(fakeArgs);

        var config = Configuration.Create(configInfo);
        
        Assert.Pass();
        
        config.Dispose();
        File.Delete(logFilePath);
        File.Delete(outputFilePath);
    }
}