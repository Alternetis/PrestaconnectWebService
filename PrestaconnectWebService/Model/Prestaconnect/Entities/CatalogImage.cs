namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CatalogImage")]
    public partial class CatalogImage
    {
        [Key]
        public int ImaCat_Id { get; set; }

        [Required]
        [StringLength(255)]
        public string ImaCat_Image { get; set; }

        public bool ImaCat_Default { get; set; }

        public int Cat_Id { get; set; }

        public virtual Catalog Catalog { get; set; }
    }
}
