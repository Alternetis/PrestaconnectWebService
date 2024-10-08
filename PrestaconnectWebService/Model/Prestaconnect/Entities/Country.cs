namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Country")]
    public partial class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Pre_IdCountry { get; set; }

        public int Sag_CatCompta { get; set; }

        public int Sag_CatComptaPro { get; set; }

        public bool Replace_ISOCode { get; set; }
    }
}
