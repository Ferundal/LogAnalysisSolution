using Microsoft.Extensions.Configuration;

namespace IPAnalyzer;

public class FileConfigurationInfo : ConfigurationInfo
{
    private const string ConfigFileName = "appsettings.json";
    
    public FileConfigurationInfo(string configFileName = ConfigFileName)
    {
        IConfiguration? config = null;

        try
        {
            config = new ConfigurationBuilder()
                .AddJsonFile(configFileName, optional: false, reloadOnChange: true)
                .Build();
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Configuration file appsettings.json not found. Empty settings was used...");
        }
        if (config == null) return;
        
        LogFilePath.Value = config[LogFilePath.Name];
        OutputFilePath.Value = config[OutputFilePath.Name];
        AddressStart.Value = config[AddressStart.Name];
        AddressMask.Value = config[AddressMask.Name];
        TimeStart.Value = config[TimeStart.Name];
        TimeEnd.Value = config[TimeEnd.Name];
    }

    protected override void AppendTargetConfigurationInfo(ConfigurationInfo targetArgument)
    {
        if (targetArgument.LogFilePath.Value != null ||
            targetArgument.OutputFilePath.Value != null ||
            targetArgument.AddressStart.Value != null ||
            targetArgument.AddressMask.Value != null ||
            targetArgument.TimeStart.Value != null ||
            targetArgument.TimeEnd.Value != null) return;
        
        targetArgument.LogFilePath.Value = LogFilePath.Value;
        targetArgument.OutputFilePath.Value = OutputFilePath.Value;
        targetArgument.AddressStart.Value = AddressStart.Value;
        targetArgument.AddressMask.Value = AddressMask.Value;
        targetArgument.TimeStart.Value = TimeStart.Value;
        targetArgument.TimeEnd.Value = TimeEnd.Value;
    }
}