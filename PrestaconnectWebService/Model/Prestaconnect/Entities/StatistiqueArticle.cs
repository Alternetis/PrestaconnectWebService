namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StatistiqueArticle")]
    public partial class StatistiqueArticle
    {
        [Key]
        [StringLength(50)]
        public string Sag_StatArt { get; set; }

        public short Inf_Mode { get; set; }

        public int Cha_Id { get; set; }
    }
}
