namespace WebAPI.Models
{
    public class Oyun
    {
        public int Id { get; set; }
        public string OyunAdi { get; set; }
        public double Puan { get; set; }
        public DateTime Olusuturuldu { get; set; } = DateTime.Now;

        public List<Skor> Skorlar { get; set; }
    }
}
