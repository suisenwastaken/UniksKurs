using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace l4Razor.Model
{
    public class Collective
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано название")]
        public string CollectiveName { get; set; }
        public string? CollevtiveDescription { get; set; }
        public string? CollectiveLeaderContuct { get; set; }
        
        public int? ThemeOfCollectiveId { get; set; }
        public int? InstituteOfCollectiveId { get; set; }
        
        public CollectiveTheme? ThemeOfCollective { get; set; }
        public Institute? InstituteOfCollective { get; set; }
        public List<User>? UsersOfCollective { get; set; }

    }
}