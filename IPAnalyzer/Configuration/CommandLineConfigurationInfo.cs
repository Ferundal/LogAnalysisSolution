using System.CommandLine;
using System.CommandLine.NamingConventionBinder;

namespace IPAnalyzer;

public class CommandLineConfigurationInfo : ConfigurationInfo
{
    public CommandLineConfigurationInfo(string [] args)
    {
        var logFilePathOption = new Option<string>($"--{LogFilePath.Name}", "Path to input log file");
        var outputFilePathOption = new Option<string>($"--{OutputFilePath.Name}", "Path to output file");
        var addressStartOption = new Option<string>($"--{AddressStart.Name}", "Lower bound of the address range");
        var addressMaskOption = new Option<string>($"--{AddressMask.Name}", "Subnet mask specifying the upper bound of the address range");
        var timeStartOption = new Option<string>($"--{TimeStart.Name}", "Lower bound of the time interval");
        var timeEndOption = new Option<string>($"--{TimeEnd.Name}", "Upper bound of the time interval");

        var rootCommand = new RootCommand(); 
        rootCommand.Add(logFilePathOption);
        rootCommand.Add(outputFilePathOption);
        rootCommand.Add(addressStartOption);
        rootCommand.Add(addressMaskOption);
        rootCommand.Add(timeStartOption);
        rootCommand.Add(timeEndOption);
        
        rootCommand.SetHandler((logFilePath, outputFilePath, addressStart, addressMask, timeStart, timeEnd) => 
            {
                LogFilePath.Value = logFilePath;
                OutputFilePath.Value = outputFilePath;
                AddressStart.Value = addressStart;
                AddressMask.Value = addressMask;
                TimeStart.Value = timeStart;
                TimeEnd.Value = timeEnd;
            },
            logFilePathOption,
            outputFilePathOption, 
            addressStartOption,
            addressMaskOption, 
            timeStartOption, 
            timeEndOption);

        rootCommand.Invoke(args);
    }

    protected override void AppendTargetConfigurationInfo(ConfigurationInfo targetArgument)
    {
        targetArgument.LogFilePath.Value = LogFilePath.Value;
        targetArgument.OutputFilePath.Value = OutputFilePath.Value;
        targetArgument.AddressStart.Value = AddressStart.Value;
        targetArgument.AddressMask.Value = AddressMask.Value;
        targetArgument.TimeStart.Value = TimeStart.Value;
        targetArgument.TimeEnd.Value = TimeEnd.Value;
    }
}