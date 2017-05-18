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
    
    public partial class Korisnik
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Korisnik()
        {
            this.AktivacijskiKods = new HashSet<AktivacijskiKod>();
            this.Dnevniks = new HashSet<Dnevnik>();
            this.Projekts = new HashSet<Projekt>();
        }
    
        public int id { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string nadimak { get; set; }
        public string mail { get; set; }
        public string lozinka { get; set; }
        public Nullable<int> status { get; set; }
        public Nullable<int> tipKorisnika { get; set; }
        public Nullable<bool> aktivan { get; set; }
        public Nullable<System.DateTime> vrijemeRegistracije { get; set; }
        public Nullable<bool> zakljucan { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AktivacijskiKod> AktivacijskiKods { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dnevnik> Dnevniks { get; set; }
        public virtual TipKorisnika TipKorisnika1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Projekt> Projekts { get; set; }
    }
}