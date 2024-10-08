namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Invoice")]
    public partial class Invoice
    {
        [Key]
        public int Inv_Id { get; set; }

        public int Sag_Id { get; set; }

        public DateTime? Inv_Date { get; set; }

        public byte? Inv_SentMail { get; set; }

        public int? Pre_Id { get; set; }
    }
}
