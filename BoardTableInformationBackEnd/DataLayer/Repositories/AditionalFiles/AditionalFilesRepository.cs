using DataLayer.DBContext;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.AditionalFiles
{
    public class AditionalFilesRepository : IAditionalFilesRepository
    {
        private readonly DataBaseContext _dbContext;

        public AditionalFilesRepository(DataBaseContext dbContext)
        {
            _dbContext = dbContext;


        }
        public async Task<AditionalFileEntity> AddFile(AditionalFileEntity file)
        {
            _dbContext.AditionalFiles.Add(file);
            await _dbContext.SaveChangesAsync();
            return file;
        }
    }
}
