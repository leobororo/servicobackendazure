using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses
{
    [Serializable]
    public class Quadra
    {
        public ObjectId _id
        {
            get;
            set;
        }

        public string name
        {
            get;
            set;
        }

        public string business_adress
        {
            get;
            set;
        }

        public string ico
        {
            get;
            set;
        }

        public List<Reserva> reservas
        {
            get;
            set;
        }
    }
}
