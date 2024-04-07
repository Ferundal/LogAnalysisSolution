namespace IPAnalyzer;

public class ConfigurationInfoOption
{
    private ConfigurationInfo _configurationInfo;

    private string? _value;

    public string? Value
    {
        get => _value;
        set
        {
            _value = value;
            _configurationInfo.SetInfoChangedTrigger();
        }
    }
    public string Name { get; private set; }
    
    
    public ConfigurationInfoOption(string optionName, ConfigurationInfo configurationInfo)
    {
        Name = optionName;
        _configurationInfo = configurationInfo;
    }
}