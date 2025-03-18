namespace v3.Common;

public static class PastDateTimeChecker
{
    
    public static bool IsBeforeToday(DateTime dateTime)
    {
        var now = DateTime.Now;
        var isDayLessThanToday = now.Day < dateTime.Day;
        var areMonthsAndYearsEqual = now.Month == dateTime.Month && now.Year == dateTime.Year;
        return isDayLessThanToday && areMonthsAndYearsEqual;
    }
}