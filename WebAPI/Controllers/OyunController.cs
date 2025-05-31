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
    public class OyunController : ControllerBase
    {
        private readonly APIContext _context;
        public OyunController(APIContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("/oyunlar")]
        public async Task<IActionResult> GetAllOyunlar()
        {
            var oyunlar = await _context.Oyunlar.ToListAsync();
            if (oyunlar.Count == 0)
            {
                return NotFound("Oyun bulunamadı.");
            }
            return Ok(oyunlar);
        }
        [HttpPost]
        [Route("/oyunlar/ekle")]
        public async Task<IActionResult> AddOyun([FromBody] OyunDto oyun)
        {
            if (oyun != null)
            {
                var newoyun = new Oyun()
                {
                    OyunAdi = oyun.OyunAdi,
                    Puan = oyun.Puan
                };
                _context.Oyunlar.Add(newoyun);
                await _context.SaveChangesAsync();
                return Ok("Oyun Başarıyla eklendi.");
            }
            else
            {
                return BadRequest("Oyun bilgileri eksik veya hatalı.");
            }
        }


        [HttpPut]
        [Route("/oyunlar/guncelle")]
        public async Task<IActionResult> UpdateOyun([FromBody] OyunDto oyun)
        {
            if (oyun == null || oyun.Id <= 0)
            {
                return BadRequest("Oyun bilgileri eksik veya hatalı.");
            }
            var existingOyun = await _context.Oyunlar.FindAsync(oyun.Id);
            if (existingOyun == null)
            {
                return NotFound("Oyun bulunamadı.");
            }
            existingOyun.OyunAdi = oyun.OyunAdi;
            existingOyun.Puan = oyun.Puan;
            _context.Oyunlar.Update(existingOyun);
            await _context.SaveChangesAsync();
            return Ok("Oyun başarıyla güncellendi.");
        }

        [HttpDelete]
        [Route("/oyunlar/sil/{id}")]
        public async Task<IActionResult> DeleteOyun(int id)
        {
            var oyun = await _context.Oyunlar.FindAsync(id);
            if (oyun == null)
            {
                return NotFound("Oyun bulunamadı.");
            }
            _context.Oyunlar.Remove(oyun);
            await _context.SaveChangesAsync();
            return Ok("Oyun başarıyla silindi.");
        }
        [HttpDelete]
        [Route("/oyunlar/sil/hepsi")]
        public async Task<IActionResult> DeleteAllOyun()
        {
            try
            {
                _context.Oyunlar.RemoveRange(_context.Oyunlar);
                _context.SaveChanges();
                return Ok("Tüm oyunlar başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Oyunlar silinirken hata oluştu: {ex.Message}");
            }
        }
    }
}
