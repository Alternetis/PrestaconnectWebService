namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderCartRule")]
    public partial class OrderCartRule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Pre_id { get; set; }

        public int? Sag_id { get; set; }
    }
}
