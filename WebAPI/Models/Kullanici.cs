﻿namespace WebAPI.Models
{
    public class Kullanici
    {
        public int Id { get; set; }
        public string? KullaniciAdi { get; set; }

        public string Ad { get; set; }
        public string Soyad { get; set; }

        public string? Sifre { get; set; }

        public string? Email { get; set; }

        public DateTime Olusuturuldu { get; set; } = DateTime.Now;

        public List<Skor> Skorlar { get; set; }
    }
}
