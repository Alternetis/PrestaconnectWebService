namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FMMRegistrationFields
    {
        [Key]
        public int Fmm_id { get; set; }

        public int Pre_id { get; set; }

        [StringLength(75)]
        public string Pre_FieldName { get; set; }

        [StringLength(50)]
        public string Fmm_F_COMPTET { get; set; }
    }
}
