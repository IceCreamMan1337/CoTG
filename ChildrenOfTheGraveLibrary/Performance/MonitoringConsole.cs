using System;
using System.Diagnostics;
using System.IO;

public static class MonitoringConsoleClass
{
    private static readonly string LogDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
    private static readonly string TodayLogFile = Path.Combine(LogDirectory, $"{DateTime.Now:yyyy-MM-dd}.log");
    private static Process _tailProcess;

    public static void Start()
    {
        if (_tailProcess != null && !_tailProcess.HasExited)
            return;


        Directory.CreateDirectory(LogDirectory);
        if (!File.Exists(TodayLogFile))
            File.WriteAllText(TodayLogFile, $"=== Log démarré le {DateTime.Now:HH:mm:ss} ==={Environment.NewLine}");


        var psi = new ProcessStartInfo
        {
            FileName = "powershell.exe",
            Arguments = $"-NoExit -Command \"Get-Content -Path '{TodayLogFile}' -Wait\"",
            UseShellExecute = true,
            WindowStyle = ProcessWindowStyle.Normal
        };
        _tailProcess = Process.Start(psi);
    }

    public static void Log(string message)
    {
        try
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            File.AppendAllText(TodayLogFile, $"[{timestamp}] {message}{Environment.NewLine}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur écriture fichier log : " + ex.Message);
        }
    }

    public static void Stop()
    {
        try
        {
            if (_tailProcess != null && !_tailProcess.HasExited)
            {
                _tailProcess.CloseMainWindow();
                _tailProcess.WaitForExit(500);
                if (!_tailProcess.HasExited)
                    _tailProcess.Kill();
                _tailProcess = null;
            }
        }
        catch { }
    }
}
