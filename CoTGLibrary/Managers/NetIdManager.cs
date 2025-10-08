namespace CoTG.CoTGServer;

public static class NetIdManager
{
    private static uint _dwStart = 0x40000000; //new netid
    private static readonly object _lock = new();

    /// <summary>
    /// Generates a new unique net identifier for a GameObject.
    /// </summary>
    public static uint GenerateNewNetId()
    {
        lock (_lock)
        {
            _dwStart++;
            return _dwStart;
        }
    }
}