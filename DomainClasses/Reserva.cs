using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses
{
    [Serializable]
    public class Reserva
    {
        public string hour
        {
            get;
            set;
        }

        public bool select
        {
            get;
            set;
        }
    }
}
