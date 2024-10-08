namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Carrier")]
    public partial class Carrier
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Pre_Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Sag_Id { get; set; }

        [Required]
        [StringLength(35)]
        public string Sag_Name { get; set; }

        [Required]
        [StringLength(64)]
        public string Pre_Name { get; set; }

        public int Pre_Type { get; set; }

        public int Livraison_Type { get; set; }

        public int? Livraison_JoursSup { get; set; }
    }
}
