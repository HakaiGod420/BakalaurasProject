using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.AditionalFiles
{
    public interface IAditionalFilesRepository
    {
        public Task<AditionalFileEntity> AddFile(AditionalFileEntity file);
    }
}
