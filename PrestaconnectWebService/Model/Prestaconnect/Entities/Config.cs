namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Config")]
    public partial class Config
    {
        [Key]
        public int Con_Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Con_Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Con_Value { get; set; }
    }
}
