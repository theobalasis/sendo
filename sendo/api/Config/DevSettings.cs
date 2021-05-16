

namespace Sendo.WebApi.Config
{
    public class DevSettings : ISettings
    {
        public string DataDbConnectionString =>
            "Host=127.0.0.1;Database=testing;Username=postgres;Password=postgres";
    }
}
