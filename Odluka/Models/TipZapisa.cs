//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Odluka.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TipZapisa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipZapisa()
        {
            this.Dnevniks = new HashSet<Dnevnik>();
        }
    
        public int id { get; set; }
        public string naziv { get; set; }
        public string tekst { get; set; }
        public string urlSlike { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dnevnik> Dnevniks { get; set; }
    }
}
