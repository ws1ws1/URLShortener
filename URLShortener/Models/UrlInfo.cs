using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace URLShortener.Models
{
    public class UrlInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Url адрес не может быть пустым")]
        [Url]
        public string? LongUrl { get; set; }

        public string? ShortUrl { get; set; }

        public DateTime DateCreate { get; set; }

        public int NumberOfClicks { get; set; }
    }
}
