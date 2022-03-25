using System.Collections.Generic;

namespace FlavorTreat.Models
{
    public class Flavor
    {
        public Flavor()
        {
            this.JoinEntities = new HashSet<FlavorTreat>();
        }

        public int FlavorId { get; set; }
        public string FlavorName { get; set; }
        public virtual ApplicationUser User { get; set; } //new line

        public ICollection<FlavorTreat> JoinEntities { get;}
    }
}