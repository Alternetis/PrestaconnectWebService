namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ImportSageFilter")]
    public partial class ImportSageFilter
    {
        [Key]
        public int Imp_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Imp_Value { get; set; }

        public int Imp_TypeSearchValue { get; set; }

        public int Imp_TargetData { get; set; }

        public bool Imp_Active { get; set; }

        [StringLength(20)]
        public string Sag_Infolibre { get; set; }
    }
}
