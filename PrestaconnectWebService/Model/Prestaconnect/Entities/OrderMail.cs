namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderMail")]
    public partial class OrderMail
    {
        [Key]
        public int OrdMai_Id { get; set; }

        public int OrdMai_Type { get; set; }

        [Required]
        [StringLength(255)]
        public string OrdMai_Header { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string OrdMai_Content { get; set; }

        public bool OrdMai_Active { get; set; }

        [StringLength(255)]
        public string OrdMai_Expediteur { get; set; }

        [StringLength(2000)]
        public string OrdMai_CC { get; set; }

        [StringLength(255)]
        public string OrdMai_ExpPwd { get; set; }
    }
}
