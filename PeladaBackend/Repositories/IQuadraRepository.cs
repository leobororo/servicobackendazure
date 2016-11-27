using DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeladaBackend.Repositories
{
    interface IQuadraRepository
    {
        IEnumerable<Quadra> GetAllQuadras();

        Quadra Get(string id);
    }
}
