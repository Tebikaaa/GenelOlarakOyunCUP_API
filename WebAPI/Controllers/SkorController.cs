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
    public class SkorController : ControllerBase
    {
        private readonly APIContext _context;
        public SkorController(APIContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("/siralama")]
        public async Task<IActionResult> GetAllSkorlar()
        {
            var kullanicilist = _context.Kullanicilar
                .Include(k => k.Skorlar)
                .ThenInclude(s => s.Oyun).ToList();

            var sortedList = new List<Lb>();
            foreach (var item in kullanicilist)
            {
                var newLb = new Lb(item);

                // Eğer liste boşsa veya yeni eleman en büyükse, en başa ekle
                if (sortedList.Count == 0 || newLb.ToplamSkor >= sortedList[0].ToplamSkor)
                {
                    sortedList.Insert(0, newLb);
                }
                else
                {
                    // Doğru ekleme pozisyonunu bul
                    int insertIndex = 0;
                    // Not: Bu kısım büyük listelerde performans sorunu yaratabilir, 
                    // Binary Search gibi yöntemlerle daha optimize edilebilir.
                    for (int i = 0; i < sortedList.Count; i++)
                    {
                        if (newLb.ToplamSkor >= sortedList[i].ToplamSkor)
                        {
                            insertIndex = i;
                            break;
                        }
                        insertIndex = i + 1; // Eğer sona kadar büyük eleman bulamazsak, sona ekle
                    }
                    sortedList.Insert(insertIndex, newLb);
                }
            }
            if (sortedList.Count == 0)
            {
                return NotFound("Skor bulunamadı.");
            }
            sortedList.OrderByDescending(x => x.ToplamSkor).ToList();
            return Ok(sortedList); 
        }

        [HttpPost]
        [Route("/skorlar/ekle")]
        public async Task<IActionResult> AddSkor([FromBody] SkorDto skor)
        {        
            if (skor != null)
            {
                Skor newSkor = new Skor
                {
                    KullaniciId = skor.KullaniciId,
                    OyunId = skor.OyunId,
                    KazanmaSayisi = skor.KazanmaSayisi,
                };
                _context.Skorlar.Add(newSkor);
                await _context.SaveChangesAsync();
                return Ok("Skor Başarıyla eklendi.");
            }
            else
            {
                return BadRequest("Skor bilgileri eksik veya hatalı.");
            }
        }
        [HttpPut]
        [Route("/skorlar/guncelle")]
        public async Task<IActionResult> UpdateSkor([FromBody] SkorDto skor)
        {
            Skor old_skor = await _context.Skorlar.FindAsync(skor.Id);
            if (skor != null && old_skor != null)
            {
                old_skor.OyunId = skor.OyunId;
                old_skor.KazanmaSayisi = skor.KazanmaSayisi;
                _context.Skorlar.Update(old_skor);
                await _context.SaveChangesAsync();
                return Ok("Skor Başarıyla Güncellendi.");
            }
            else
            {
                return BadRequest("Skor bilgileri hatalı.");
            }
        }
        [HttpDelete]
        [Route("/skorlar/sil/{id}")]
        public async Task<IActionResult> DeleteSkor(int id)
        {
            var skor = await _context.Skorlar.FindAsync(id);
            if (skor != null)
            {
                _context.Skorlar.Remove(skor);
                await _context.SaveChangesAsync();
                return Ok("Skor Başarıyla silindi.");
            }
            else
            {
                return BadRequest("Skor bilgileri hatalı.");
            }
        }

        [HttpDelete]
        [Route("/skorlar/sil/hepsi")]
        public async Task<IActionResult> DeleteAllSkors()
        {
            var skors = await _context.Skorlar.ToListAsync();
            if (skors.Count > 0)
            {
                _context.Skorlar.RemoveRange(skors);
                await _context.SaveChangesAsync();
                return Ok("Tüm skorlar başarıyla silindi.");
            }
            else
            {
                return NotFound("Silinecek skor bulunamadı.");
            }
        }
    }
}
