namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InformationLibre")]
    public partial class InformationLibre
    {
        [Key]
        [StringLength(50)]
        public string Sag_InfoLibre { get; set; }

        public short Inf_Mode { get; set; }

        public int Cha_Id { get; set; }
    }
}
