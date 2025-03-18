namespace v3.MedicalSlots.Exceptions;

public class NonExistentMedicalSlotException(string id)
    : Exception($"Medical slot whose id is {id} was not found.");