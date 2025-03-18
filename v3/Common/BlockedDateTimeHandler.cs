namespace v3.Common;

public static class BlockedDateTimeHandler
{

    public static void Handle(DateTime dateTime)
    {
        if (BlockedDateTimeChecker.IsBeforeToday(dateTime))
        {
            const string message = "Given date and time must today or in the future.";
            throw new BlockedBookingDateTimeException(message);
        }
    }
}