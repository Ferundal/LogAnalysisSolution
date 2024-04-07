namespace IPAnalyzer;

public abstract class ConfigurationInfo
{
    public const string LogFilePathOptionName = "file-log";
    public const string OutputFilePathOptionName = "file-output";
    public const string AddressStartOptionName = "address-start";
    public const string AddressMaskOptionName = "address-mask";
    public const string TimeStartOptionName = "time-start";
    public const string TimeEndOptionName = "time-end";

    public ConfigurationInfoOption LogFilePath { get; }
    public ConfigurationInfoOption OutputFilePath { get; }
    public ConfigurationInfoOption AddressStart { get; }
    public ConfigurationInfoOption AddressMask { get; }
    public ConfigurationInfoOption TimeStart { get; }
    public ConfigurationInfoOption TimeEnd { get; }

    private bool? _hasSufficientData = false;
    public bool HasSufficientData
    {
        get
        {
            if (_hasSufficientData != null) return (bool)_hasSufficientData;
            
            if (LogFilePath.Value == null || OutputFilePath.Value == null)
            {
                _hasSufficientData = false;
            }
            else
            {
                _hasSufficientData = true;
            }
            return (bool)_hasSufficientData;
        }
    }

    private bool? _hasCorrectData = false;
    public bool HasCorrectData
    {
        get
        {
            if (_hasCorrectData != null) return (bool)_hasCorrectData;

            _hasCorrectData = true;
            if (LogFilePath.Value == null || OutputFilePath.Value == null)
            {
                _hasCorrectData = false;
            } else if (AddressMask.Value != null && AddressStart.Value == null)
            {
                _hasCorrectData = false;
            }
            return (bool)_hasCorrectData;
        }
    }
    

    public ConfigurationInfo()
    {
        LogFilePath = new ConfigurationInfoOption(LogFilePathOptionName, this);
        OutputFilePath = new ConfigurationInfoOption(OutputFilePathOptionName, this);
        AddressStart = new ConfigurationInfoOption(AddressStartOptionName, this);
        AddressMask = new ConfigurationInfoOption(AddressMaskOptionName, this);
        TimeStart = new ConfigurationInfoOption(TimeStartOptionName, this);
        TimeEnd = new ConfigurationInfoOption(TimeEndOptionName, this);
    }
    
    public void SetInfoChangedTrigger()
    {
        _hasSufficientData = null;
        _hasCorrectData = null;
    }

    public void Append(ConfigurationInfo configurationInfo)
    {
        configurationInfo.AppendTargetConfigurationInfo(this);
    }

    protected abstract void AppendTargetConfigurationInfo(ConfigurationInfo targetConfigurationInfo);
}