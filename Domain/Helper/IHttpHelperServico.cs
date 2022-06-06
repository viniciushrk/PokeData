using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helper
{
    public interface IHttpHelperServico
    {
        public Task<IRestResponse> ExecuteRequestAsync(string uri, string rota, object body = null, Dictionary<string, string> headers = null, Method method = Method.GET);
        public void Headers(Dictionary<string, string> headers, RestRequest request);
        public void Body(object body, RestRequest request);

    }
}
