namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderMacketplace")]
    public partial class OrderMacketplace
    {
        [Key]
        public int Ord_MacketplaceId { get; set; }

        [StringLength(50)]
        public string Ord_ColoumName { get; set; }

        [StringLength(50)]
        public string Ord_ReplaceText { get; set; }

        [Column(TypeName = "text")]
        public string Ord_MySQLRequest { get; set; }

        public byte? Ord_TransfertLigne { get; set; }
    }
}
