namespace IPAnalyzer;

public class ConfigurationInfoGenerator
{
    public static ConfigurationInfo Generate(string [] argc)
    {
        var commandLineConfigurationInfo = new CommandLineConfigurationInfo(argc);
        if (!commandLineConfigurationInfo.HasCorrectData)
        {
            return null;
        }

        if (commandLineConfigurationInfo.HasSufficientData)
        {
            return commandLineConfigurationInfo;
        }

        var environmentConfigurationInfo = new EnvironmentConfigurationInfo();
        commandLineConfigurationInfo.Append(environmentConfigurationInfo);
        if (!commandLineConfigurationInfo.HasCorrectData)
        {
            return null;
        }

        if (commandLineConfigurationInfo.HasSufficientData)
        {
            return commandLineConfigurationInfo;
        }

        var fileConfigurationInfo = new FileConfigurationInfo();
        commandLineConfigurationInfo.Append(fileConfigurationInfo);

        if (commandLineConfigurationInfo is { HasCorrectData: true, HasSufficientData: true })
        {
            return commandLineConfigurationInfo;
        }

        return null;
    }

}