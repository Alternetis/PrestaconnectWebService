namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InformationArticle")]
    public partial class InformationArticle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Sag_InfoArt { get; set; }

        public short Inf_SendValue { get; set; }

        public short Inf_Mode { get; set; }

        public int Cha_Id { get; set; }
    }
}
