using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class LbService
    {
        double FullSkor;
        public int CalcUserScore(List<Skor> skor)
        {

            FullSkor = 0;
            foreach (var item in skor)
            {

                FullSkor += item.KazanmaSayisi * item.Oyun.Puan;
            }

            return (int)Math.Ceiling(FullSkor);
        }
        public List<KullaniciOyun> CalcGameSkor(List<Skor> skor)
        {
            List<KullaniciOyun> kullaniciOyunList = new List<KullaniciOyun>();
            foreach (var item in skor)
            {
                kullaniciOyunList.Add(new KullaniciOyun
                {
                    OyunAdi = item.Oyun.OyunAdi,
                    ToplamOyunPuani = (int)Math.Ceiling(item.KazanmaSayisi * item.Oyun.Puan)
                });
            }
            return kullaniciOyunList;
        }
    }
}
