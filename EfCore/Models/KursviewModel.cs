using EfCore.Data;
using System.ComponentModel.DataAnnotations;

namespace EfCore.Models
{
    public class KursViewModel
    {
        [Key]
        public int KursId { get; set; }
        public string? Baslik { get; set; }
        public int? OgretmenId { get; set; }
        public ICollection<KursKayit> KursKayitlari { get; set; } = new List<KursKayit>();
    }
}
