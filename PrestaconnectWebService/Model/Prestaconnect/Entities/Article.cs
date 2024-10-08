namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Article")]
    public partial class Article
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Article()
        {
            ArticleImage = new HashSet<ArticleImage>();
            Attachment = new HashSet<Attachment>();
            Characteristic = new HashSet<Characteristic>();
            ConditioningArticle = new HashSet<ConditioningArticle>();
            ArticleAdditionalField = new HashSet<ArticleAdditionalField>();
            CompositionArticle = new HashSet<CompositionArticle>();
            CompositionArticleAttributeGroup = new HashSet<CompositionArticleAttributeGroup>();
            Catalog1 = new HashSet<Catalog>();
        }

        [Key]
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArticleImage> ArticleImage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attachment> Attachment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Characteristic> Characteristic { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConditioningArticle> ConditioningArticle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArticleAdditionalField> ArticleAdditionalField { get; set; }

        public virtual Catalog Catalog { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompositionArticle> CompositionArticle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompositionArticleAttributeGroup> CompositionArticleAttributeGroup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Catalog> Catalog1 { get; set; }
    }
}
