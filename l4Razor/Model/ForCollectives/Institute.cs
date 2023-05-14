using System.Collections.Generic;

namespace l4Razor.Model
{
    public class Institute
    {
        public int Id { get; set; }
        
        public string InstituteName { get; set; }
        public List<Collective> ListOfCollectives { get; set; }
    }
}