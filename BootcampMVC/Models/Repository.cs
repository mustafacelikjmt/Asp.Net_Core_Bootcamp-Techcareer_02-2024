namespace BootcampMVC.Models
{
    public class Repository
    {
        private static readonly List<Bootcamp> _bootcamp = new();
        static Repository()
        {
            _bootcamp = new List<Bootcamp>()
            {
                new Bootcamp(){Id = 1,Title = "Asp Net core Bootcamp",Description = "23 Ocak",Image="1.png",Tags = new string[]{"aspnet","web geliştirme" },isActive=true,isHome=true },
                new Bootcamp(){Id = 2,Title = "Sql Bootcamp",Description = "24 Ocak",Image="1.png",Tags = new string[]{"data","veri" },isActive=false,isHome=true},
                new Bootcamp(){Id = 3,Title = "Unity Bootcamp",Description = "25 Ocak",Image="1.png",Tags = new string[]{"unity","oyun geliştirme" },isActive=true,isHome=false},
            };
        }
        public static List<Bootcamp> Bootcamps
        {
            get
            {
                return _bootcamp;
            }
        }
        public static Bootcamp GetById(int? id)
        {
            return _bootcamp.FirstOrDefault(b => b.Id == id);
        }

    }
}
