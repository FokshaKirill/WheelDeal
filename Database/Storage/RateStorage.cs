using Microsoft.EntityFrameworkCore;
using WheelDeal.Database.ModelsDb;
using WheelDeal.Interfaces;

namespace WheelDeal.Database.Storage;

public class RateStorage : IBaseStorage<RateDb>
{
    public readonly ApplicationDbContext _db;

    public RateStorage(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task Add(RateDb item)
    {
        await _db.AddAsync(item);
        await _db.SaveChangesAsync();
    }
    
    public async Task Delete(RateDb item)
    {
        _db.Remove(item);
        await _db.SaveChangesAsync();
    }
    
    public async Task<RateDb> Get(Guid id)
    {
        return await _db.RatesDb.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public IQueryable<RateDb> GetAll()
    {
        return _db.RatesDb;
    }

    public async Task<RateDb> Update(RateDb item)
    {
        _db.RatesDb.Update(item);
        await _db.SaveChangesAsync();
        
        return item;
    }
}