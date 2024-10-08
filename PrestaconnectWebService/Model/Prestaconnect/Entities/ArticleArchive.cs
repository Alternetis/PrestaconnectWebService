namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ArticleArchive")]
    public partial class ArticleArchive
    {
        [Key]
        public int Arch_Id { get; set; }

        public int Art_Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Art_Name { get; set; }

        [Column(TypeName = "text")]
        public string Art_Description { get; set; }

        [Column(TypeName = "text")]
        public string Art_Description_Short { get; set; }

        [StringLength(128)]
        public string Art_LinkRewrite { get; set; }

        [StringLength(128)]
        public string Art_MetaTitle { get; set; }

        [StringLength(255)]
        public string Art_MetaKeyword { get; set; }

        [StringLength(255)]
        public string Art_MetaDescription { get; set; }

        [Required]
        [StringLength(19)]
        public string Art_Ref { get; set; }

        [StringLength(13)]
        public string Art_Ean13 { get; set; }

        public bool Art_Pack { get; set; }

        public bool? Art_Solde { get; set; }

        public bool Art_Active { get; set; }

        public bool Art_Sync { get; set; }

        public DateTime Art_Date { get; set; }

        [Required]
        [StringLength(3)]
        public string Art_RedirectType { get; set; }

        public int Art_RedirectProduct { get; set; }

        public int Art_Manufacturer { get; set; }

        public int Art_Supplier { get; set; }

        public int Sag_Id { get; set; }

        public int? Pre_Id { get; set; }

        public int Cat_Id { get; set; }

        public int Art_Type { get; set; }

        public bool Art_SyncPrice { get; set; }
    }
}
