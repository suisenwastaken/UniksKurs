using System;
using System.ComponentModel.DataAnnotations;

namespace l4Razor.Model
{
    public class ContuctUsMessage
    {
        public int Id { get; set; }
        
        public DateTime DateOfMessage { get; set; }
        
        [Required(ErrorMessage = "Не указано имя!")]
        public string Name { get; set; } = String.Empty;
        [Required(ErrorMessage = "Не указана тема!")]
        public string Theme { get; set; } = String.Empty;
        [Required(ErrorMessage = "Пустое сообщение!")]
        public string MessageText { get; set; }
    }
}