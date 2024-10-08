namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderSoucheBy")]
    public partial class OrderSoucheBy
    {
        [Key]
        public int Ord_Id { get; set; }

        public int Ord_IdSouche { get; set; }

        public int Ord_TypeBy { get; set; }

        [Required]
        [StringLength(50)]
        public string Ord_Choise { get; set; }
    }
}
