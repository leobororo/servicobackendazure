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
        // busca todas as quadras
        IEnumerable<Quadra> GetAllQuadras();

        // busca a quadra cujo o id é o especificado
        Quadra Get(string id);
    }
}
