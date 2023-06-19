using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpListenerExample.Model
{
    internal static class RequestManager
    {
        public static async Task<T> GetRequestData<T>(HttpListenerRequest request)
        {
            // Проверка, что содержимое запроса является JSON
            if (!request.ContentType.StartsWith("application/json"))
            {
                throw new ArgumentException("Invalid content type. Expected application/json.");
            }

            // Чтение тела запроса
            using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
            {
                string requestBody = await reader.ReadToEndAsync();

                // Десериализация JSON в объект указанного типа
                T requestData = JsonConvert.DeserializeObject<T>(requestBody);

                return requestData;
            }
        }
    }
}
