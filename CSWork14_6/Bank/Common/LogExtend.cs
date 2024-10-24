using BankData.Logger;

public static class LogExtend
{
    public static void Log(this string message)
    {
        Logger.AddEvent(message);
    }
}
