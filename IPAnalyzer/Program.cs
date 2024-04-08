using IPAnalyzer;

AccessCounter accessCounter;

var configurationInfo = ConfigurationInfoGenerator.Generate(args);
Configuration configuration;
try
{
    configuration = Configuration.Create(configurationInfo);
    accessCounter = new AccessCounter(configuration); 
    accessCounter.CountAccess();
}
catch (ArgumentException argumentException)
{
    return;
}

foreach (var access in accessCounter.Accesses)
{
    configuration.OutputFile.WriteLine($"{access.Key} {access.Value}");
}
configuration.Dispose();

