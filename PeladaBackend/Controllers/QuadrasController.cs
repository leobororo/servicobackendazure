using DomainClasses;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using PeladaBackend.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        static CloudQueue cloudQueue;

        static QuadrasController()
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=trabalhocomputacaonuvem;AccountKey=YNmHYnn5Wl0MzKJUVoKvn98KtkFhIcTYGPNS4TZEsOfPvMKuzl1OV65bv3FXxdRH2/hNluFNxONcUkBUlNnePw==";

            CloudStorageAccount cloudStorageAccount;

            if (!CloudStorageAccount.TryParse(connectionString, out cloudStorageAccount))
            {
                Trace.TraceInformation("Ocorreu um erro ao conectar-se com o serviço de fila na nuvem");
            }

            var cloudQueueClient = cloudStorageAccount.CreateCloudQueueClient();
            cloudQueue = cloudQueueClient.GetQueueReference("demoqueue");

            // Note: Usually this statement can be executed once during application startup or maybe even never in the application.
            //       A queue in Azure Storage is often considered a persistent item which exists over a long time.
            //       Every time .CreateIfNotExists() is executed a storage transaction and a bit of latency for the call occurs.
            cloudQueue.CreateIfNotExists();
        }

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
        public void Post(PedidoReservaQuadra pedidoReservaQuadra)
        {
            var message = new CloudQueueMessage(JsonConvert.SerializeObject(pedidoReservaQuadra));

            cloudQueue.AddMessage(message);
        }
    }
}