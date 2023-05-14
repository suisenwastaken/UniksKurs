using System.Collections.Generic;

namespace l4Razor.Model
{
    public class CollectiveTheme
    {
        public int Id { get; set; }
        
        public string ThemeName { get; set; }
        public List<Collective> CollectivesList { get; set; }
    }
}