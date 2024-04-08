using System.Net;

namespace IPAnalyzer;

public class Record
{
    public IPAddress? Address;
    public DateTime? Time;

    private Record(IPAddress ipAddress, DateTime dateTime)
    {
        Address = ipAddress;
        Time = dateTime;
    }

    /// <summary>
    /// Tries to parse a string and create a Record object.
    /// </summary>
    /// <param name="input">The input string containing an IP address and a date/time in the format "IP address:yyyy-MM-dd HH:mm:ss".</param>
    /// <param name="record">The parsed Record object if the operation is successful.</param>
    /// <returns>True if the parsing operation succeeds; otherwise, false.</returns>
    /// <exception cref="ArgumentException">Thrown if the input string has an invalid format.</exception>
    public static bool TryParse(string input, out Record? record)
    {
        record = null;

        string[] parts = input.Split(new char[] { ':' }, 2);
    
        if (parts.Length != 2)
        {
            return false;
        }

        if (!IPAddress.TryParse(parts[0], out IPAddress? ipAddress))
        {
            return false;
        }

        if (!DateTime.TryParseExact(parts[1], "yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out DateTime dateTime))
        {
            return false;
        }

        record = new Record(ipAddress, dateTime);
        return true;
    }
}