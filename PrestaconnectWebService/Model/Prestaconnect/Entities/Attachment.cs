namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Attachment")]
    public partial class Attachment
    {
        [Key]
        public int Att_Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Att_File { get; set; }

        [Required]
        [StringLength(128)]
        public string Att_FileName { get; set; }

        [Required]
        [StringLength(128)]
        public string Att_Mime { get; set; }

        [Required]
        [StringLength(32)]
        public string Att_Name { get; set; }

        [Column(TypeName = "text")]
        public string Att_Description { get; set; }

        public int Art_Id { get; set; }

        public int? Pre_Id { get; set; }

        public int? Sag_Id { get; set; }

        public virtual Article Article { get; set; }
    }
}
