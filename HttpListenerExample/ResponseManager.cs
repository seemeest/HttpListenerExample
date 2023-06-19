using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpListenerExample
{
    public static class ResponseManager
    {
        public static async Task SendResponseData<T>(HttpListenerContext context, T data)
        {
            string responseData = JsonConvert.SerializeObject(data);
            byte[] buffer = Encoding.UTF8.GetBytes(responseData);

            HttpListenerResponse response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = 200;
            response.ContentLength64 = buffer.Length;

            await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
            response.OutputStream.Close();
        }


       



        public static void WriteUnauthorizedResponse(HttpListenerResponse response)
        {
            response.StatusCode = (int)HttpStatusCode.Unauthorized;
            byte[] data = Encoding.UTF8.GetBytes("Unauthorized");
            response.ContentType = "text/plain";
            response.ContentEncoding = Encoding.UTF8;
            response.ContentLength64 = data.LongLength;
            response.OutputStream.Write(data, 0, data.Length);
        }
    }
}
