using DomainClasses;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using PeladaBackend.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Web.Http;

namespace PeladaBackend.Controllers
{
    public class QuadrasController : ApiController
    {
        private static readonly IQuadraRepository _quadraRepository = new QuadraRepository();

        // GET api/<controller>
        public IEnumerable<Quadra> Get()
        { 
            return _quadraRepository.GetAllQuadras();
        }

        // GET api/<controller>/5
        public Quadra Get(string id)
        {
            return _quadraRepository.Get(id);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }
    }
}