//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarResale.DBModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Car
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Car()
        {
            this.Orders = new HashSet<Order>();
        }
    
        public int ID { get; set; }
        public int ModelID { get; set; }
        public int ReceiptInvoiceID { get; set; }
        public string VIN { get; set; }
        public string TRIM { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public double Mileage { get; set; }
        public string Transmission { get; set; }
        public string FuelType { get; set; }
    
        public virtual ReceiptInvoice ReceiptInvoice { get; set; }
        public virtual Model Model { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
