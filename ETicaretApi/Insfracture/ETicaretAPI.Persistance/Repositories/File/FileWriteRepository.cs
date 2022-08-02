using ETicaretAPI.Application.Repositories;
using ETicaretDbContext.Domain.Entities;
using ETicaretDbContext.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistance.Repositories
{
    public class FileWriteRepository : WriteRepository<ETicaretDbContext.Domain.Entities.File>, IFileWriteRepository
    {
        public FileWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
