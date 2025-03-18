namespace v3.Common;

public class PastBookingDateTimeException()
    : Exception("Given date and time must be today or in the future.");