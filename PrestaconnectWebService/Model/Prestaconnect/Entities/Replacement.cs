namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Replacement")]
    public partial class Replacement
    {
        [Key]
        [StringLength(500)]
        public string OriginText { get; set; }

        [Required]
        [StringLength(1000)]
        public string ReplacementText { get; set; }
    }
}
