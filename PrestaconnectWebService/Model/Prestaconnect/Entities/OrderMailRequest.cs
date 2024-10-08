namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderMailRequest")]
    public partial class OrderMailRequest
    {
        [Key]
        public int Mail_MailRequestId { get; set; }

        [StringLength(50)]
        public string Mail_TagName { get; set; }

        [Column(TypeName = "text")]
        public string Mail_MySQLRequest { get; set; }

        [StringLength(50)]
        public string Mail_IdOrderReplace { get; set; }
    }
}
