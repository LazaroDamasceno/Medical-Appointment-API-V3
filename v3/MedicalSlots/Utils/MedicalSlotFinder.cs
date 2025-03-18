using MongoDB.Driver;
using v3.Context;
using v3.MedicalSlots.Domain;
using v3.MedicalSlots.Exceptions;

namespace v3.MedicalSlots.Utils;

public class MedicalSlotFinder(MongoDbContext context)
{

    public async Task<MedicalSlot> FindById(string id)
    {
        var filter = Builders<MedicalSlot>.Filter.Eq(x => x.Id, Guid.Parse(id));
        var medicalSlot = await context.MedicalSlotCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
        if (medicalSlot == null) throw new NonExistentMedicalSlotException(id);
        return medicalSlot;
    }
}