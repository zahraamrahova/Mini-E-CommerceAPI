using E_CommerceAPI.Application.Repositories;
using E_CommerceAPI.Persistence.Contexts;
using File = E_CommerceAPI.Domain.Entities.File;

namespace E_CommerceAPI.Persistence.Repositories
{
    public class FileWriteRepository : WriteRepository<E_CommerceAPI.Domain.Entities.File>, IFileWriteRepository
    {
        public FileWriteRepository(E_CommerceAPIDbContext context) : base(context)
        {
        }

    }
}
