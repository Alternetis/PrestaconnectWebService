namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Supply")]
    public partial class Supply
    {
        [Key]
        public int Sup_Id { get; set; }

        [Required]
        [StringLength(35)]
        public string Sup_Name { get; set; }

        public bool Sup_Active { get; set; }

        public int Sag_Id { get; set; }
    }
}
