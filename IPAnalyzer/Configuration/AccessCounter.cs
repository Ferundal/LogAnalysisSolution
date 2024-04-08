using System.Net;

namespace IPAnalyzer;

public class AccessCounter
{
    public Dictionary<IPAddress, int> Accesses = new ();
    private Configuration _configuration;
    public AccessCounter(Configuration configuration)
    {
        _configuration = configuration;
    }

    public void CountAccess()
    {
        var addressChecker = new AddressChecker(_configuration);
        
        while (!_configuration.LogFile.EndOfStream)
        {
            string line = _configuration.LogFile.ReadLine();
            if (Record.TryParse(line, out var record))
            {
                if (addressChecker.IsInRange(record) && IsInTimeRange(record))
                {
                    if (Accesses.ContainsKey(record.Address))
                    {
                        Accesses[record.Address] += 1;
                    }
                    else
                    {
                        Accesses.Add(record.Address, 1);
                    }
                }
            }
        }
    }

    private bool IsInTimeRange(Record record)
    {
        if (_configuration.TimeStart != null && record.Time < _configuration.TimeStart)
        {
            return false;
        }

        if (_configuration.TimeEnd != null && record.Time > _configuration.TimeEnd)
        {
            return false;
        }

        return true;
    }

    private class AddressChecker
    {
        private IPAddress? _lowerBoundary;
        private IPAddress? _upperBoundary;
        
        public AddressChecker(Configuration configuration)
        {
            _lowerBoundary = configuration.AddressStart;
            if (configuration.AddressStart != null)
            {
                _upperBoundary = CalculateUpperBoundary(configuration.AddressStart, configuration.AddressMask);
            }
        }

        public bool IsInRange(Record record)
        {
            if (record.Address == null)
            {
                return true;
            }
            
            if(IPUtils.ToUint32(record.Address) < IPUtils.ToUint32(_lowerBoundary))
            {
                return false;
            }

            if (_upperBoundary == null)
            {
                return true;
            }
            
            return IPUtils.ToUint32(record.Address) <= IPUtils.ToUint32(_upperBoundary);
        }
        
        private IPAddress CalculateUpperBoundary(IPAddress? lowerBoundary, int subnetMask)
        {
            byte[] lowerBytes = lowerBoundary.GetAddressBytes();
            byte[] upperBytes = new byte[4];
        
            int maskBits = (int)(0xFFFFFFFF << (32 - subnetMask));
        
            for (int i = 0; i < 4; i++)
            {
                upperBytes[i] = (byte)(lowerBytes[i] | ~maskBits);
            }

            return new IPAddress(upperBytes);
        }
        
    }
}