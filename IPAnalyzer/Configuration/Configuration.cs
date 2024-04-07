namespace IPAnalyzer;

public class Configuration
{
    private Configuration()
    {
        
    }

    public static Configuration Create(ConfigurationInfo configurationInfo)
    {
        return new Configuration();
    }
}