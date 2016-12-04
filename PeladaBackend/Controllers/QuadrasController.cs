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

        // referência para a fila contida no account storage da nuvem
        static CloudQueue cloudQueue;

        // Utilizado para fazer a conexão com o storage account e acessar a fila
        const string connectionString = "DefaultEndpointsProtocol=https;AccountName=trabalhocomputacaonuvem;AccountKey=YNmHYnn5Wl0MzKJUVoKvn98KtkFhIcTYGPNS4TZEsOfPvMKuzl1OV65bv3FXxdRH2/hNluFNxONcUkBUlNnePw==";

        // id da fila fornecida pelo serviço de storage account
        const string idQueue = "demoqueue";

        static QuadrasController()
        {
            // cria a referência para a fila contida no cloud storage
            CloudStorageAccount cloudStorageAccount;

            if (!CloudStorageAccount.TryParse(connectionString, out cloudStorageAccount))
            {
                Trace.TraceInformation("Ocorreu um erro ao conectar-se com o serviço de fila na nuvem");
            }

            var cloudQueueClient = cloudStorageAccount.CreateCloudQueueClient();

            cloudQueue = cloudQueueClient.GetQueueReference(idQueue);

            cloudQueue.CreateIfNotExists();
        }

        // GET api/<controller>
        public IEnumerable<Quadra> Get()
        { 
            // obtém todas as quadras do repository
            return _quadraRepository.GetAllQuadras();
        }

        // GET api/<controller>/5
        public Quadra Get(string id)
        {
            // obtém do repository a quadra com o id especificado
            return _quadraRepository.Get(id);
        }

        // POST api/<controller>
        public void Post(PedidoReservaQuadra pedidoReservaQuadra)
        {
            // cria uma mensagem para a fila com o pedido de reserva serializado no formato JSON 
            var message = new CloudQueueMessage(JsonConvert.SerializeObject(pedidoReservaQuadra));

            // adiciona a mensagem na fila do account storage do azure
            cloudQueue.AddMessage(message);
        }
    }
}