using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace l4Razor.Model
{
    public class Tag
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Не указано название!")]
        public string TagName { get; set; }
        public List<New> News { get; set; } = new List<New>();
        
        /*public List<NewXTag> NewXTags { get; set; }*/
    }
}