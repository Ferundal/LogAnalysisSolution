using NUnit.Framework;

namespace IPAnalyzer.Tests;

[TestFixture]
public class CommandLineConfigurationInfoTests
{
    [Test]
    public void CommandLineConfigurationInfo_Sets_Values_Correctly()
    {
        // Arrange
        var logFilePath = "test.log";
        var outputFilePath = "output.txt";
        var addressStart = "192.168.0.1";
        var addressMask = "255.255.255.0";
        var timeStart = "2023-01-01T00:00:00";
        var timeEnd = "2023-12-31T23:59:59";

        
        var fakeArgs = new[] {
            $"--{ConfigurationInfo.LogFilePathOptionName}", $"{logFilePath}",
            $"--{ConfigurationInfo.OutputFilePathOptionName}", $"{outputFilePath}",
            $"--{ConfigurationInfo.AddressStartOptionName}", $"{addressStart}", 
            $"--{ConfigurationInfo.AddressMaskOptionName}", $"{addressMask}",
            $"--{ConfigurationInfo.TimeStartOptionName}", $"{timeStart}",
            $"--{ConfigurationInfo.TimeEndOptionName}", $"{timeEnd}"
        };

        var configInfo = new CommandLineConfigurationInfo(fakeArgs);

        Assert.Multiple(() =>
        {
            Assert.That(configInfo.LogFilePath.Value, Is.EqualTo(logFilePath));
            Assert.That(configInfo.OutputFilePath.Value, Is.EqualTo(outputFilePath));
            Assert.That(configInfo.AddressStart.Value, Is.EqualTo(addressStart));
            Assert.That(configInfo.AddressMask.Value, Is.EqualTo(addressMask));
            Assert.That(configInfo.TimeStart.Value, Is.EqualTo(timeStart));
            Assert.That(configInfo.TimeEnd.Value, Is.EqualTo(timeEnd));
        });
    }
}