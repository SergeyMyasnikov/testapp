using System;
using System.Collections.Generic;
using System.Web.Http;
using testapp.Models;

namespace testapp.Controllers
{
    public class ClientController : ApiController
    {
        public IEnumerable<Client> GetAllClients()
        {
            return DatabaseConnection.GetAll();
        }

        [HttpPut]
        public void UpdateClient([FromBody]Client client)
        {
            DatabaseConnection.Update(client);
        }

        [HttpPost]
        public int AddClient([FromBody]Client client)
        {
            return DatabaseConnection.Add(client);
        }

        public void DeleteClient(int id)
        {
            DatabaseConnection.Remove(id);
        }
    }
}