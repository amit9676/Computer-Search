//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ThirdProject
{
    using System;
    using System.Collections.Generic;
    
    public partial class Search
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Search()
        {
            this.Results = new HashSet<Result>();
        }
    
        public int SearchID { get; set; }
        public string Search_text { get; set; }
        public System.DateTime Time_of_search { get; set; }
        public int Number_of_Results { get; set; }
        public string Search_folder { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Result> Results { get; set; }
    }
}
