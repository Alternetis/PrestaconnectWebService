namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ArticleAdditionalField")]
    public partial class ArticleAdditionalField
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Art_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string FieldName { get; set; }

        public string FieldValue { get; set; }

        public string FieldValue2 { get; set; }

        public virtual Article Article { get; set; }
    }
}
