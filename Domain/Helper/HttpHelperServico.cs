using RestSharp;
using System.Text.Json;

namespace Domain.Helper
{
    public class HttpHelperServico : IHttpHelperServico
    {
        public virtual async Task<IRestResponse> ExecuteRequestAsync(string uri, string rota, object body = null, Dictionary<string, string> headers = null, Method method = Method.GET)
        {
            if (!uri.EndsWith("/"))
                uri += "/";
            if (rota.StartsWith("/"))
                rota = rota.Remove(0, 1);

            var request = new RestRequest(rota, method) { RequestFormat = DataFormat.Json };

            Headers(headers, request);

            Body(body, request);

            var cliente = new RestClient(uri);

            var teste = await cliente.ExecuteAsync(request);

            return teste;
        }

        public void Headers(Dictionary<string, string> headers, RestRequest request)
        {
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }
        }

        public void Body(object body, RestRequest request)
        {
            if (body != null) request.AddJsonBody(JsonSerializer.Serialize(body));
        }
    }
}
