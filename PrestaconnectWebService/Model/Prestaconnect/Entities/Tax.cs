namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tax")]
    public partial class Tax
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Sag_Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Pre_Id { get; set; }

        [StringLength(35)]
        public string Sag_Name { get; set; }

        [StringLength(32)]
        public string Pre_Name { get; set; }

        [StringLength(20)]
        public string Sag_ArticleRemise { get; set; }

        [StringLength(20)]
        public string Sag_ArticleRemplacement { get; set; }

        [StringLength(20)]
        public string Sag_ArticleReduction { get; set; }
    }
}
