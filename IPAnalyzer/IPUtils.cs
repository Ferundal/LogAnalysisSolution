using System.Net;

namespace IPAnalyzer;

public static class IPUtils
{
    public static uint ToUint32(this IPAddress ipAddress)
    {
        var bytes = ipAddress.GetAddressBytes();

        return ((uint)(bytes[0] << 24)) |
               ((uint)(bytes[1] << 16)) |
               ((uint)(bytes[2] << 8)) |
               ((uint)(bytes[3]));
    }
}