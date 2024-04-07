using NUnit.Framework;

namespace IPAnalyzer.Tests;

[TestFixture]
public class FileConfigurationInfoTests
{
    [Test]
    public void FileConfigurationInfo_ConfigFileExists_ConfigLoaded()
    {
        // Arrange
        string tempConfigPath = "tempconfig.json";

        var logFilePath = "test.log";
        var outputFilePath = "output.txt";
        var addressStart = "192.168.0.1";
        var addressMask = "255.255.255.0";
        var timeStart = "2023-01-01T00:00:00";
        var timrEnd = "2023-12-31T23:59:59";

        File.WriteAllText(tempConfigPath, 
            $"{{\"{ConfigurationInfo.LogFilePathOptionName}\":\"{logFilePath}\"," +
            $"\"{ConfigurationInfo.OutputFilePathOptionName}\":\"{outputFilePath}\"," +
            $"\"{ConfigurationInfo.AddressStartOptionName}\":\"{addressStart}\"," +
            $"\"{ConfigurationInfo.AddressMaskOptionName}\":\"{addressMask}\"," +
            $"\"{ConfigurationInfo.TimeStartOptionName}\":\"{timeStart}\"," +
            $"\"{ConfigurationInfo.TimeEndOptionName}\":\"{timrEnd}\"}}");

        // Act
        FileConfigurationInfo fileConfigInfo = new FileConfigurationInfo(tempConfigPath);
        Assert.Multiple(() =>
        {

            // Assert
            Assert.That(fileConfigInfo.LogFilePath.Value, Is.EqualTo(logFilePath));
            Assert.That(fileConfigInfo.OutputFilePath.Value, Is.EqualTo(outputFilePath));
            Assert.That(fileConfigInfo.AddressStart.Value, Is.EqualTo(addressStart));
            Assert.That(fileConfigInfo.AddressMask.Value, Is.EqualTo(addressMask));
            Assert.That(fileConfigInfo.TimeStart.Value, Is.EqualTo(timeStart));
            Assert.That(fileConfigInfo.TimeEnd.Value, Is.EqualTo(timrEnd));
        });

        // Clean up
        File.Delete(tempConfigPath);
    }

    [Test]
    public void FileConfigurationInfo_ConfigFileDoesNotExist_NoExceptionThrown()
    {
        // Arrange & Act
        FileConfigurationInfo fileConfigInfo = new FileConfigurationInfo("nonexistent.json");

        // Assert
        Assert.Pass(); // If no exception is thrown, the test passes
    }

    [Test]
    public void FileConfigurationInfo_ConfigFileDoesNotExist_VariablesAreNull()
    {
        // Arrange & Act
        FileConfigurationInfo fileConfigInfo = new FileConfigurationInfo("nonexistent.json");
        
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(fileConfigInfo.LogFilePath.Value, Is.Null);
            Assert.That(fileConfigInfo.OutputFilePath.Value, Is.Null);
            Assert.That(fileConfigInfo.AddressStart.Value, Is.Null);
            Assert.That(fileConfigInfo.AddressMask.Value, Is.Null);
            Assert.That(fileConfigInfo.TimeStart.Value, Is.Null);
            Assert.That(fileConfigInfo.TimeEnd.Value, Is.Null);
        });
    }
}