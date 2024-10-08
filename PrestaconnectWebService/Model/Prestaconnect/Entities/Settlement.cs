namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Settlement")]
    public partial class Settlement
    {
        [Key]
        public int Set_Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Set_PaymentMethod { get; set; }

        [Required]
        [StringLength(50)]
        public string Set_Journal { get; set; }

        [Required]
        [StringLength(50)]
        public string Set_Intitule { get; set; }

        public int Sag_Id { get; set; }

        [StringLength(50)]
        public string Com_SageRefArticle { get; set; }

        public decimal? Com_PourcentTTC { get; set; }

        public decimal? Com_MontantFixe { get; set; }

        public int? Set_Collaborateur { get; set; }
    }
}
