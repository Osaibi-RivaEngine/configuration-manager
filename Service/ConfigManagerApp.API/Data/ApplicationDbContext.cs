using ConfigManagerApp.API.Models;

namespace ConfigManagerApp.API.Data
{
    public class ApplicationDbContext
    {
        public static List<KeyValuePairModel> Configs { get; set; } = new List<KeyValuePairModel>();
    }
}
