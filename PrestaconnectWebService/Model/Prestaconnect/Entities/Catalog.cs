namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using PrestaconnectWebService.Core;
    using PrestaconnectWebService.ViewModel;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Catalog")]
    public partial class Catalog
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Catalog()
        {
            Article = new HashSet<Article>();
            CatalogImage = new HashSet<CatalogImage>();
            Article1 = new HashSet<Article>();
        }

        public Catalog(CatalogueSelectedViewModel viewModel)
        {
            Cat_Id = (int)viewModel.CatId;
            Cat_Name = viewModel.Category.name[0].Value;
            Cat_Level = viewModel.Category.level_depth;
            Cat_Parent = (int)viewModel.Category.id_parent;
            Cat_Active = viewModel.Category.active == 1 ? true : false;
            Cat_Sync = viewModel.CatSync;
            Cat_Date = viewModel.Category.date_upd != null ? 
                                                    DateTime.Parse(viewModel.Category.date_upd) : 
                                                    viewModel.Category.date_add != null ? 
                                                        DateTime.Parse(viewModel.Category.date_upd) :
                                                        DateTime.Now;
            Sag_Id = (int)viewModel.SageId;
            Pre_Id = (int?)viewModel.PreId;
            Id_Shop = (int?)viewModel.IdShop;
        }

        [Key]
        public int Cat_Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Cat_Name { get; set; }

        [Column(TypeName = "text")]
        public string Cat_Description { get; set; }

        public int Cat_Level { get; set; }

        public int Cat_Parent { get; set; }

        [StringLength(128)]
        public string Cat_MetaTitle { get; set; }

        [StringLength(255)]
        public string Cat_MetaDescription { get; set; }

        [StringLength(255)]
        public string Cat_MetaKeyword { get; set; }

        [StringLength(128)]
        public string Cat_LinkRewrite { get; set; }

        public bool Cat_Active { get; set; }

        public bool Cat_Sync { get; set; }

        public DateTime Cat_Date { get; set; }

        public int Sag_Id { get; set; }

        public int? Pre_Id { get; set; }

        public int? Id_Shop { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Article> Article { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatalogImage> CatalogImage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Article> Article1 { get; set; }
    }
}
