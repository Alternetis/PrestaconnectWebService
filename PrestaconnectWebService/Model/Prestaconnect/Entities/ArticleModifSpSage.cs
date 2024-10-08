namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ArticleModifSpSage")]
    public partial class ArticleModifSpSage
    {
        [StringLength(19)]
        public string AR_REF { get; set; }

        [StringLength(11)]
        public string FA_CODEFAMILLE { get; set; }

        [Key]
        [StringLength(17)]
        public string CT_NUM { get; set; }
    }
}
