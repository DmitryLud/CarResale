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
    
    public partial class SpecificPeriodReport_Result
    {
        public string VIN { get; set; }
        public string Mark { get; set; }
        public string Model { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public System.DateTime Date_of_acquisition { get; set; }
        public decimal Acquisistion_price { get; set; }
        public Nullable<decimal> Other_costs { get; set; }
        public decimal Total_acquisistion_price { get; set; }
        public System.DateTime Sale_date { get; set; }
        public decimal Sale_price { get; set; }
        public Nullable<decimal> Financial_result { get; set; }
    }
}
