using WebAPI.Models;

namespace WebAPI.Dto
{
    public class SkorDto
    {
        public int Id { get; set; }
        public int Puan { get; set; }
        public int KazanmaSayisi { get; set; }
        public int KullaniciId { get; set; }
        public int OyunId { get; set; }
    }
}
