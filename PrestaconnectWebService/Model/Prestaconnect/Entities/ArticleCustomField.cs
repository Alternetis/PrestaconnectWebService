namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ArticleCustomField")]
    public partial class ArticleCustomField
    {
        [Key]
        public int Arc_Id { get; set; }

        public string Arc_ColonnePrestashop { get; set; }

        public string Arc_TablePrestashop { get; set; }

        [StringLength(69)]
        public string Arc_ChampSage { get; set; }

        public byte? Arc_TypeValeur { get; set; }

        [StringLength(69)]
        public string Arc_ValeurSi { get; set; }
    }
}
