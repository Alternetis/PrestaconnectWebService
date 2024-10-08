namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDepotBy")]
    public partial class OrderDepotBy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Ord_IdDepot { get; set; }

        public int Ord_TypeBy { get; set; }

        [Required]
        [StringLength(50)]
        public string Ord_Choise { get; set; }
    }
}
