namespace v3.Common;

public static class BlockedDateTimeHandler
{

    public static void Handle(DateTime dateTime)
    {
        if (BlockedDateTimeChecker.IsBeforeToday(dateTime))
        {
            throw new PastBookingDateTimeException();
        }
    }
}