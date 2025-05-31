using WebAPI.Services;

namespace WebAPI.Models
{
    public class Lb
    {
        public int ToplamSkor { get; set; }
        public string KullaniciAdi { get; set; }
        public List<KullaniciOyun> KullaniciOyun { get; set; }
        public Lb(Kullanici kullanici)
        {
            LbService lbService = new LbService();
            ToplamSkor = lbService.CalcUserScore(kullanici.Skorlar);
            KullaniciOyun = lbService.CalcGameSkor(kullanici.Skorlar);
            KullaniciAdi = kullanici.Ad + " " + kullanici.Soyad;    
        }
    }
}
