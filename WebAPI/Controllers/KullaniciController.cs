using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Dto;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciController : ControllerBase
    {
        private readonly APIContext _context;
        public KullaniciController(APIContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("/kullanicilar")]
        public async Task<IActionResult> GetAllKullanicilar()
        {
            var kullanicilar = await _context.Kullanicilar.ToListAsync();
            if (kullanicilar.Count == 0)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }
            return Ok(kullanicilar);
        }
        [HttpPost]
        [Route("/kullanicilar/ekle")]
        public async Task<IActionResult> AddKullanici([FromBody] KullaniciDto kullanici)
        {
            if (kullanici != null)
            {
                var newkullanici = new Kullanici()
                {
                    KullaniciAdi = kullanici.KullaniciAdi,
                    Ad = kullanici.Ad,
                    Soyad = kullanici.Soyad,
                    Sifre = kullanici.Sifre,
                    Email = kullanici.Email
                };

                _context.Kullanicilar.Add(newkullanici);
                await _context.SaveChangesAsync();
                return Ok("Kullanıcı Başarıyla eklendi.");
            }
            else
            {
                return BadRequest("Kullanici bilgileri eksik veya hatalı.");
            }
        }

        [HttpPut]
        [Route("/kullanicilar/guncelle")]
        public async Task<IActionResult> UpdateKullanici([FromBody] KullaniciDto kullanici)
        {
            if (kullanici != null)
            {
                var existingKullanici = _context.Kullanicilar.Find(kullanici.Id);
                if (existingKullanici == null)
                {
                    return NotFound("Kullanıcı bulunamadı.");
                }
                existingKullanici.KullaniciAdi = kullanici.KullaniciAdi;
                existingKullanici.Ad = kullanici.Ad;
                existingKullanici.Soyad = kullanici.Soyad;
                existingKullanici.Sifre = kullanici.Sifre;
                existingKullanici.Email = kullanici.Email;
                _context.Kullanicilar.Update(existingKullanici);
                await _context.SaveChangesAsync();
                return Ok("Kullanıcı başarıyla güncellendi.");
            }
            return NotFound("Kullanıcı bilgileri eksik veya hatalı.");
        }

        [HttpDelete]
        [Route("/kullanicilar/sil/{id}")]
        public async Task<IActionResult> DeleteKullanici(int id)
        {
            var kullanici = await _context.Kullanicilar.FindAsync(id);
            if (kullanici == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }
            _context.Kullanicilar.Remove(kullanici);
            await _context.SaveChangesAsync();
            return Ok("Kullanıcı başarıyla silindi.");
        }

        [HttpDelete]
        [Route("/kullanicilar/sil/hepsi")]
        public async Task<IActionResult> DeleteAllKullanicilar()
        {
            var kullanicilar = await _context.Kullanicilar.ToListAsync();
            if (kullanicilar.Count == 0)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }
            _context.Kullanicilar.RemoveRange(kullanicilar);
            await _context.SaveChangesAsync();
            return Ok("Tüm kullanıcılar başarıyla silindi.");
        }
    }
}
