namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ArticleLang")]
    public partial class ArticleLang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Atl_Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Atl_IdLang { get; set; }

        public int? Atl_ArtId { get; set; }

        [Required]
        [StringLength(128)]
        public string Atl_Name { get; set; }

        [Column(TypeName = "text")]
        public string Atl_Description { get; set; }

        [Column(TypeName = "text")]
        public string Atl_Description_Short { get; set; }

        [StringLength(128)]
        public string Atl_MetaTitle { get; set; }

        [StringLength(255)]
        public string Atl_MetaKeyword { get; set; }

        [StringLength(255)]
        public string Atl_MetaDescription { get; set; }
    }
}
