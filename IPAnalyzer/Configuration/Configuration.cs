using System;
using System.Diagnostics;
using System.Net;

namespace IPAnalyzer;

public class Configuration : IDisposable
{
    public StreamReader? LogFile;
    public StreamWriter? OutputFile;
    public IPAddress? AddressStart;
    public int AddressMask;
    public DateTime? TimeStart;
    public DateTime? TimeEnd;
    
    private Configuration() { }

    public static Configuration Create(ConfigurationInfo configurationInfo)
    {
        if (!configurationInfo.HasSufficientData)
        {
            throw new ArgumentException("Wrong arguments");
        }
        var config = new Configuration();
        var hasRightData = true;
        
        hasRightData &= config.TryOpenLogFile(configurationInfo);
        hasRightData &= config.TryOpenOutputFile(configurationInfo);
        hasRightData &= config.TryGetAddress(configurationInfo);
        hasRightData &= config.TryGetMask(configurationInfo);
        hasRightData &= config.TryGetTimeStart(configurationInfo);
        hasRightData &= config.TryGetTimeEnd(configurationInfo);

        if (!hasRightData)
        {
            throw new ArgumentException("Wrong arguments");
        }
        return config;
    }
    
    private bool TryGetTimeEnd(ConfigurationInfo configurationInfo)
    {
        if (configurationInfo.TimeEnd.Value == null)
        {
            return true;
        }

        DateTime dateTime;
        if (DateTime.TryParseExact(configurationInfo.TimeEnd.Value, "dd.MM.yyyy", null,
                System.Globalization.DateTimeStyles.None, out dateTime))
        {
            TimeEnd = dateTime;
            return true;
        }

        return false;
    }
    
    private bool TryGetTimeStart(ConfigurationInfo configurationInfo)
    {
        if (configurationInfo.TimeStart.Value == null)
        {
            return true;
        }
        
        DateTime dateTime;
        if (DateTime.TryParseExact(configurationInfo.TimeStart.Value, "dd.MM.yyyy", null,
                System.Globalization.DateTimeStyles.None, out dateTime))
        {
            TimeStart = dateTime;
            return true;
        }

        return false;
    }
    
    private bool TryGetMask(ConfigurationInfo configurationInfo)
    {
        if (configurationInfo.AddressMask.Value == null)
        {
            return true;
        }
        
        return int.TryParse(configurationInfo.AddressMask.Value, out AddressMask) && AddressMask is >= 0 and <= 32;
    }
    
    private bool TryGetAddress(ConfigurationInfo configurationInfo)
    {
        if (configurationInfo.AddressStart.Value == null)
        {
            return true;
        }
        
        return IPAddress.TryParse(configurationInfo.AddressStart.Value, out AddressStart);
    }

    private bool TryOpenLogFile(ConfigurationInfo configurationInfo)
    {
        try
        {
            LogFile = new StreamReader(configurationInfo.LogFilePath.Value);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Can't open log file {configurationInfo.LogFilePath.Value}");
            return false;
        }

        return true;
    }
    
    private bool TryOpenOutputFile(ConfigurationInfo configurationInfo)
    {
        try
        {
            OutputFile = new StreamWriter($"Can't open output file {configurationInfo.OutputFilePath.Value}");
        }
        catch (Exception e)
        {
            Console.WriteLine("Cant ");
            return false;
        }

        return true;
    }

    public void Dispose()
    {
        if (LogFile != null)
        {
            LogFile.Close();
            LogFile.Dispose();
        }

        if (OutputFile != null)
        {
            OutputFile.Close();
            OutputFile.Dispose();
        }
    }
}