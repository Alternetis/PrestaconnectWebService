namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ArticleImage")]
    public partial class ArticleImage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ArticleImage()
        {
            AttributeArticle = new HashSet<AttributeArticle>();
            CompositionArticle = new HashSet<CompositionArticle>();
        }

        [Key]
        public int ImaArt_Id { get; set; }

        [Required]
        [StringLength(128)]
        public string ImaArt_Name { get; set; }

        public bool ImaArt_Default { get; set; }

        public int ImaArt_Position { get; set; }

        [Required]
        [StringLength(255)]
        public string ImaArt_Image { get; set; }

        public int? Pre_Id { get; set; }

        public int? Art_Id { get; set; }

        [StringLength(255)]
        public string ImaArt_SourceFile { get; set; }

        public DateTime? ImaArt_DateAdd { get; set; }

        public virtual Article Article { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttributeArticle> AttributeArticle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompositionArticle> CompositionArticle { get; set; }
    }
}
