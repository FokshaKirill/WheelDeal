using Microsoft.EntityFrameworkCore;
using WheelDeal.Database.ModelsDb;
using WheelDeal.Interfaces;

namespace WheelDeal.Database.Storage;

public class CarStorage : IBaseStorage<CarDb>
{
    public readonly ApplicationDbContext _db;

    public CarStorage(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task Add(CarDb item)
    {
        await _db.AddAsync(item);
        await _db.SaveChangesAsync();
    }
    
    public async Task Delete(CarDb item)
    {
        _db.Remove(item);
        await _db.SaveChangesAsync();
    }
    
    public async Task<CarDb> Get(Guid id)
    {
        return await _db.CarsDb.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public IQueryable<CarDb> GetAll()
    {
        return _db.CarsDb;
    }

    public async Task<CarDb> Update(CarDb item)
    {
        _db.CarsDb.Update(item);
        await _db.SaveChangesAsync();
        
        return item;
    }
}