namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Attribute")]
    public partial class Attribute
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Sag_Id { get; set; }

        public int Pre_Id { get; set; }
    }
}
