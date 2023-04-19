using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roommates.Data.IRepositories
{
    public interface IUnitOfWorkRepository
    {
        public IFileRepository FileRepository { get; }
        public ILocationRepository LocationRepository { get; }
        public IPostRepository PostRepository { get; }
        public IRoommateRepository RoommateRepository { get; }
    }
}
