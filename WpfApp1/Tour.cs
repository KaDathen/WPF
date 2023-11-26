//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tour
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tour()
        {
            this.Hotel = new HashSet<Hotel>();
            this.Type = new HashSet<Type>();
        }
    
        public int ID { get; set; }
        public int TicketsCount { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] ImagePreveiw { get; set; }
        public decimal Price { get; set; }
        public bool IsActual { get; set; }

        public string ActualText 
        {
            get
            {
                return (IsActual) ? "Актуален" : "Завершен"; 
            } 
            
        }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hotel> Hotel { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Type> Type { get; set; }
    }
}
