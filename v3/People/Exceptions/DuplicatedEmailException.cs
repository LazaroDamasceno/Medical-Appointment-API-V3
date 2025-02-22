namespace v3.People.Exceptions;

public class DuplicatedEmailException(): Exception("The given email is already in use.");