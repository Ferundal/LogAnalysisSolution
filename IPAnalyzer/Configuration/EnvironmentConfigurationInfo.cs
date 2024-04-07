namespace IPAnalyzer;

public class EnvironmentConfigurationInfo : ConfigurationInfo
{
    public EnvironmentConfigurationInfo()
    {
        LogFilePath.Value = Environment.GetEnvironmentVariable(LogFilePath.Name);
        OutputFilePath.Value = Environment.GetEnvironmentVariable(OutputFilePath.Name);
        AddressStart.Value = Environment.GetEnvironmentVariable(AddressStart.Name);
        AddressMask.Value = Environment.GetEnvironmentVariable(AddressMask.Name);
        TimeStart.Value = Environment.GetEnvironmentVariable(TimeStart.Name);
        TimeEnd.Value = Environment.GetEnvironmentVariable(TimeEnd.Name);
    }

    protected override void AppendTargetConfigurationInfo(ConfigurationInfo targetArgument)
    {
        
        
        targetArgument.LogFilePath.Value ??= LogFilePath.Value;
        targetArgument.OutputFilePath.Value ??= OutputFilePath.Value;
        targetArgument.AddressStart.Value ??= AddressStart.Value;
        targetArgument.AddressMask.Value ??= AddressMask.Value;
        targetArgument.TimeStart.Value ??= TimeStart.Value;
        targetArgument.TimeEnd.Value ??= TimeEnd.Value;
    }
}