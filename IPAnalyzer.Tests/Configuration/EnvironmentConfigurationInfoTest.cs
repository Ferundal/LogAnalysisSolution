namespace IPAnalyzer.Tests;

using NUnit.Framework;

[TestFixture]
public class EnvironmentConfigurationInfoTests
{
    [Test]
    public void EnvironmentVariables_Set_Correctly()
    {
        // Arrange
        var logFilePath = "test.log";
        var outputFilePath = "output.txt";
        var addressStart = "192.168.0.1";
        var addressMask = "255.255.255.0";
        var timeStart = "2023-01-01T00:00:00";
        var timrEnd = "2023-12-31T23:59:59";
        
        Environment.SetEnvironmentVariable(ConfigurationInfo.LogFilePathOptionName, logFilePath);
        Environment.SetEnvironmentVariable(ConfigurationInfo.OutputFilePathOptionName, outputFilePath);
        Environment.SetEnvironmentVariable(ConfigurationInfo.AddressStartOptionName, addressStart);
        Environment.SetEnvironmentVariable(ConfigurationInfo.AddressMaskOptionName, addressMask);
        Environment.SetEnvironmentVariable(ConfigurationInfo.TimeStartOptionName, timeStart);
        Environment.SetEnvironmentVariable(ConfigurationInfo.TimeEndOptionName, timrEnd);

        // Act
        var config = new EnvironmentConfigurationInfo();
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(config.LogFilePath.Value, Is.EqualTo(logFilePath));
            Assert.That(config.OutputFilePath.Value, Is.EqualTo(outputFilePath));
            Assert.That(config.AddressStart.Value, Is.EqualTo(addressStart));
            Assert.That(config.AddressMask.Value, Is.EqualTo(addressMask));
            Assert.That(config.TimeStart.Value, Is.EqualTo(timeStart));
            Assert.That(config.TimeEnd.Value, Is.EqualTo(timrEnd));
        });
    }

    [Test]
    public void EnvironmentVariables_NotSet_DefaultValues()
    {
        // Arrange
        Environment.SetEnvironmentVariable(ConfigurationInfo.LogFilePathOptionName, null);
        Environment.SetEnvironmentVariable(ConfigurationInfo.OutputFilePathOptionName, null);
        Environment.SetEnvironmentVariable(ConfigurationInfo.AddressStartOptionName, null);
        Environment.SetEnvironmentVariable(ConfigurationInfo.AddressMaskOptionName, null);
        Environment.SetEnvironmentVariable(ConfigurationInfo.TimeStartOptionName, null);
        Environment.SetEnvironmentVariable(ConfigurationInfo.TimeEndOptionName, null);

        // Act
        var config = new EnvironmentConfigurationInfo();
        Assert.Multiple(() =>
        {

            // Assert
            Assert.That(config.LogFilePath.Value, Is.Null);
            Assert.That(config.OutputFilePath.Value, Is.Null);
            Assert.That(config.AddressStart.Value, Is.Null);
            Assert.That(config.AddressMask.Value, Is.Null);
            Assert.That(config.TimeStart.Value, Is.Null);
            Assert.That(config.TimeEnd.Value, Is.Null);
        });
    }
    
    [TearDown]
    public void CleanUp()
    {
        Environment.SetEnvironmentVariable(ConfigurationInfo.LogFilePathOptionName, null);
        Environment.SetEnvironmentVariable(ConfigurationInfo.OutputFilePathOptionName, null);
        Environment.SetEnvironmentVariable(ConfigurationInfo.AddressStartOptionName, null);
        Environment.SetEnvironmentVariable(ConfigurationInfo.AddressMaskOptionName, null);
        Environment.SetEnvironmentVariable(ConfigurationInfo.TimeStartOptionName, null);
        Environment.SetEnvironmentVariable(ConfigurationInfo.TimeEndOptionName, null);
    }
}