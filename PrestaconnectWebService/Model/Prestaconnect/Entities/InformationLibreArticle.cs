namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InformationLibreArticle")]
    public partial class InformationLibreArticle
    {
        [Key]
        [StringLength(50)]
        public string Sag_InfoLibreArticle { get; set; }

        public int Inf_Catalogue { get; set; }

        [StringLength(128)]
        public string Inf_Parent { get; set; }

        public bool Inf_IsStat { get; set; }

        public int? Inf_Stat { get; set; }
    }
}
