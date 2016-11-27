using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses
{
    [Serializable]
    public class PedidoReservaQuadra
    {
        public string deviceId
        {
            get;
            set;
        }

        public Quadra quadra
        {
            get;
            set;
        }
    }
}
