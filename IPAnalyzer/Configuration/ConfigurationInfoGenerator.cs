namespace IPAnalyzer;

public static class ConfigurationInfoGenerator
{
    public static ConfigurationInfo Generate(string [] argc)
    {
        var commandLineConfigurationInfo = new CommandLineConfigurationInfo(argc);

        if (commandLineConfigurationInfo.HasSufficientData)
        {
            return commandLineConfigurationInfo;
        }

        var environmentConfigurationInfo = new EnvironmentConfigurationInfo();
        commandLineConfigurationInfo.Append(environmentConfigurationInfo);

        if (commandLineConfigurationInfo.HasSufficientData)
        {
            return commandLineConfigurationInfo;
        }

        var fileConfigurationInfo = new FileConfigurationInfo();
        commandLineConfigurationInfo.Append(fileConfigurationInfo);
        
        return commandLineConfigurationInfo;
    }

}