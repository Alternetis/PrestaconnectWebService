namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Supplier")]
    public partial class Supplier
    {
        [Key]
        public int Sup_Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Sup_Name { get; set; }

        [Column(TypeName = "text")]
        public string Sup_Description { get; set; }

        [StringLength(128)]
        public string Sup_MetaTitle { get; set; }

        [StringLength(255)]
        public string Sup_MetaKeyword { get; set; }

        [StringLength(255)]
        public string Sup_MetaDescription { get; set; }

        public bool Sup_Active { get; set; }

        public bool Sup_Sync { get; set; }

        public DateTime Sup_Date { get; set; }

        public int Sag_Id { get; set; }

        public int? Pre_Id { get; set; }
    }
}
