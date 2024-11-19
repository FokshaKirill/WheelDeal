using Microsoft.EntityFrameworkCore;
using WheelDeal.Database.ModelsDb;
using WheelDeal.Interfaces;

namespace WheelDeal.Database.Storage;

public class PostStorage : IBaseStorage<PostDb>
{
    public readonly ApplicationDbContext _db;

    public PostStorage(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task Add(PostDb item)
    {
        await _db.AddAsync(item);
        await _db.SaveChangesAsync();
    }
    
    public async Task Delete(PostDb item)
    {
        _db.Remove(item);
        await _db.SaveChangesAsync();
    }
    
    public async Task<PostDb> Get(Guid id)
    {
        return await _db.PostsDb.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public IQueryable<PostDb> GetAll()
    {
        return _db.PostsDb;
    }

    public async Task<PostDb> Update(PostDb item)
    {
        _db.PostsDb.Update(item);
        await _db.SaveChangesAsync();
        
        return item;
    }
}