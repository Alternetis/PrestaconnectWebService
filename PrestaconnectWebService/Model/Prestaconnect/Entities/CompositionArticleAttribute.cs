namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompositionArticleAttribute")]
    public partial class CompositionArticleAttribute
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Caa_ComArtId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Caa_AttributeGroup_PreId { get; set; }

        public int Caa_Attribute_PreId { get; set; }

        public virtual CompositionArticle CompositionArticle { get; set; }
    }
}
