namespace v3.Common;

public static class PastDateTimeHandler
{

    public static void Handle(DateTime dateTime)
    {
        if (PastDateTimeChecker.IsBeforeToday(dateTime))
        {
            throw new PastBookingDateException();
        }
    }
}