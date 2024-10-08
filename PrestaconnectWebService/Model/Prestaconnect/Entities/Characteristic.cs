namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Characteristic")]
    public partial class Characteristic
    {
        [Key]
        public int Cha_Id { get; set; }

        public int Cha_IdFeature { get; set; }

        [Required]
        [StringLength(255)]
        public string Cha_Value { get; set; }

        public bool Cha_Custom { get; set; }

        public int Art_Id { get; set; }

        public int? Pre_Id { get; set; }

        public virtual Article Article { get; set; }
    }
}
