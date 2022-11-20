using Microsoft.Extensions.Configuration;

namespace Common
{
    public static class Configuration //Konfigürasyonları tek yerden yönetme işlemi
    {
        public static string SqlServerConnectionString
        {
            get
            {
                var path = WebApiPathString();

                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(path);
                configurationManager.AddJsonFile("appsettings.json");

                return configurationManager.GetConnectionString("SqlServerConnectionString");
            }
        }

        public static string EmailConfirmationLink
        {
            get
            {
                var path = WebApiPathString();

                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(path);
                configurationManager.AddJsonFile("appsettings.json");

                return configurationManager["EmailConfirmationLink"];
            }
        }

        static string WebApiPathString() //WebApi içinde bulunan appsettings.json dosyasına erişim yolu
        {
            var path = "";
            var directory = Directory.GetCurrentDirectory();
            var name = directory.Split("\\").Last();

            switch(name) // O anda çalışan projenin yolu farklı olduğu için böyle bir işleme başvuruldu. Farklı projeden buraya ulaşılmak istendiğinde buraya yeni bir 'case' eklemek gerekir.
            {
                case "WebApi":
                    path = directory; break;
                case "FavoriteWorkerService":
                case "VoteWorkerService":
                case "UserWorkerService":
                    path = Path.Combine(directory, "../../Api/WebApi/WebApi"); break;
                case "Persistence":
                    path = Path.Combine(directory, "../../WebApi/WebApi"); break;
                default:
                    break;
            }

            return path;
        }
    }
}
