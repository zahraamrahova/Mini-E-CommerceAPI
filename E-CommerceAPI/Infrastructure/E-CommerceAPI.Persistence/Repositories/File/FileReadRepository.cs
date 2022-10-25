using E_CommerceAPI.Application.Repositories;
using E_CommerceAPI.Persistence.Contexts;


namespace E_CommerceAPI.Persistence.Repositories.File
{
    public class FileReadRepository : ReadRepository<E_CommerceAPI.Domain.Entities.File>, IFileReadRepository
    {
        public FileReadRepository(E_CommerceAPIDbContext context) : base(context)
        {
        }

    }
}
