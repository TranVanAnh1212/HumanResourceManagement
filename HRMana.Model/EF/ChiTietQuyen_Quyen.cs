//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HRMana.Model.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChiTietQuyen_Quyen
    {
        public int maQuyen { get; set; }
        public int maChitietQuyen { get; set; }
        public string moTa { get; set; }
    
        public virtual Chitiet_Quyen Chitiet_Quyen { get; set; }
        public virtual Quyen Quyen { get; set; }
    }
}