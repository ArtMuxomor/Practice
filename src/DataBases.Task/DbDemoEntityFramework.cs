using Microsoft.EntityFrameworkCore;

namespace DataBases.PracticeTask
{
    public class DbDemoEntityFramework : DbDemo
    {
        private readonly GameDbContext _dbContext;
        public DbDemoEntityFramework(string connectionStr) : base(connectionStr)
        {
            _dbContext = new GameDbContext(connectionStr);
        }

        public override async Task Create(string objName)
        {
            var newStat = new Stat { StatName = objName };
            _dbContext.Stats.Add(newStat);
            await _dbContext.SaveChangesAsync();
        }

        public override async Task<List<Stat>> Read(int selectLimit = 1000)
        {
            var statList = await _dbContext.Stats
                .OrderBy(s => s.StatId)
                .Take(selectLimit)
                .ToListAsync();

            return statList;
        }

        public override async Task Update(string newObjName)
        {
            newObjName += " (EF)";

            var stat = await _dbContext.Stats
                .OrderByDescending(s => s.StatId)
                .FirstOrDefaultAsync();

            if (stat != null)
            {
                stat.StatName = newObjName;
                await _dbContext.SaveChangesAsync();
            }
        }

        public override async Task Delete()
        {
            var stat = await _dbContext.Stats
                .OrderByDescending(s => s.StatId)
                .FirstOrDefaultAsync();

            if (stat != null)
            {
                _dbContext.Stats.Remove(stat);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}