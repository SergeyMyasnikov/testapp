using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace clientapp
{
    public static class ServerConnection
    {
        private static string _uri = ConfigurationSettings.AppSettings["serverUri"];

        public static List<Client> GetAll()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(_uri + "api/client");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                List<Client> result = new List<Client>();

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    result = JsonConvert.DeserializeObject<List<Client>>(reader.ReadToEnd());
                }

                return result;
            }
            catch
            {
                return null;
            }
        }

        public static List<Client> Update(Client client)
        {
            string json = JsonConvert.SerializeObject(client);
            UTF8Encoding encoding = new UTF8Encoding();

            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(_uri + "api/client");
                request.Method = "PUT";
                request.ContentType = "application/json";
                request.ContentLength = encoding.GetByteCount(json);

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(encoding.GetBytes(json), 0, encoding.GetByteCount(json));
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                
                return GetAll();
            }
            catch
            {
                return null;
            }
        }

        public static int Add(Client client)
        {
            string json = JsonConvert.SerializeObject(client);
            UTF8Encoding encoding = new UTF8Encoding();

            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(_uri + "api/client");
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = encoding.GetByteCount(json);

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(encoding.GetBytes(json), 0, encoding.GetByteCount(json));
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                int id = 0;
                using (StreamReader responseStream = new StreamReader(response.GetResponseStream()))
                {
                    id = Convert.ToInt32(responseStream.ReadToEnd());
                }

                return id;
            }
            catch
            {
                return 0;
            }
        }

        public static List<Client> Delete(object id)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(_uri + "api/client/" + id);
                request.Method = "DELETE";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                return GetAll();
            }
            catch
            {
                return null;
            }
        }
    }
}
