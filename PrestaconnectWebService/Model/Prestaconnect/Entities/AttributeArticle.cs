namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AttributeArticle")]
    public partial class AttributeArticle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AttributeArticle()
        {
            ArticleImage = new HashSet<ArticleImage>();
        }

        [Key]
        public int AttArt_Id { get; set; }

        public bool AttArt_Default { get; set; }

        public bool AttArt_Sync { get; set; }

        public bool AttArt_Active { get; set; }

        public int Art_Id { get; set; }

        public int Att_IdFirst { get; set; }

        public int? Att_IdSecond { get; set; }

        public int Sag_Id { get; set; }

        public int? Pre_Id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArticleImage> ArticleImage { get; set; }
    }
}
