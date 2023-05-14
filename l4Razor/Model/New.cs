using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace l4Razor.Model
{
    public class New
    {
        public int Id { get; set; }
        
        public int NewsType { get; set; }
        [Required(ErrorMessage = "Не указано название!")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Не указано описание!")]
        public string Description { get; set; } = string.Empty;
        public List<Tag>? TagsList { get; set; } = new List<Tag>();
        public DateTime? Created { get; set; }
        public DateTime? TakePlace { get; set; }
        
        /*public List<NewXTag> NewXTags { get; set; }*/

    }
}