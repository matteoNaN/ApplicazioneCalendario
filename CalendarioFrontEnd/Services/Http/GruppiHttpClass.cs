using SharedLibrary.Models;

namespace CalendarioFrontEnd.Services.Http
{
    public class GruppiHttpClass : BaseApiService<Gruppi>
    {
        public GruppiHttpClass(HttpClient httpClient, string endpoint) : base(httpClient, endpoint)
        {

        }
    }
}
