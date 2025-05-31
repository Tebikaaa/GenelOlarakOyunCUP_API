namespace WebAPI.Models
{
    public class Skor
    {
        public int Id { get; set; }

        public int KazanmaSayisi { get; set; }

        public int KullaniciId { get; set; }
        public Kullanici Kullanici { get; set; }

        public int OyunId { get; set; }
        public Oyun Oyun { get; set; }

        public DateTime Olusuturuldu { get; set; } = DateTime.Now;

    }
}
