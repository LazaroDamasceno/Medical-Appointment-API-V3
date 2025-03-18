namespace v3.Common;

public class BlockedBookingDateTimeException()
    : Exception("Given date and time are already in use in an active medical slot.");