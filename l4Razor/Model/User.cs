using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace l4Razor.Model
{
    public class User
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Не указан логин")]
        public string UserName { get; set; } = String.Empty;
        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; } = String.Empty;
        [Required(ErrorMessage = "Не указано имя")]
        public string FirstName { get; set; } = "Аноним";
        
        public DateTime DateOfRegistration { get; set; }

        public string PictureUrl { get; set; } = String.Empty;
            
        
        public int? RoleId { get; set; } = 1;
        public Role? Role { get; set; }

        public List<Collective> CollectiveList { get; set; } = new List<Collective>();
    }
}