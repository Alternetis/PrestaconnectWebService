namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompositionArticle")]
    public partial class CompositionArticle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CompositionArticle()
        {
            CompositionArticleAttribute = new HashSet<CompositionArticleAttribute>();
            ArticleImage = new HashSet<ArticleImage>();
        }

        [Key]
        public int ComArt_Id { get; set; }

        public int ComArt_ArtId { get; set; }

        public int ComArt_F_ARTICLE_SagId { get; set; }

        public int? ComArt_F_ARTENUMREF_SagId { get; set; }

        public int? ComArt_F_CONDITION_SagId { get; set; }

        public decimal ComArt_Quantity { get; set; }

        public bool ComArt_Default { get; set; }

        public bool ComArt_Sync { get; set; }

        public bool ComArt_Active { get; set; }

        public int? Pre_Id { get; set; }

        public virtual Article Article { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompositionArticleAttribute> CompositionArticleAttribute { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArticleImage> ArticleImage { get; set; }
    }
}
