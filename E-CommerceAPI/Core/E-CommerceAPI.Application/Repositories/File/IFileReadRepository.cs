using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = E_CommerceAPI.Domain.Entities.File;

namespace E_CommerceAPI.Application.Repositories
{
    public interface IFileReadRepository: IReadRepository<File>
    {
    }
}
