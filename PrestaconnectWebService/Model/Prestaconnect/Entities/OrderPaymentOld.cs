namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderPaymentOld")]
    public partial class OrderPaymentOld
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Pre_Id_Order_Payment { get; set; }

        public long Pre_Id_Order { get; set; }
    }
}
