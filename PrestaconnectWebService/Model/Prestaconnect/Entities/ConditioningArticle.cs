namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ConditioningArticle")]
    public partial class ConditioningArticle
    {
        [Key]
        public int ConArt_Id { get; set; }

        public bool ConArt_Default { get; set; }

        public bool ConArt_Sync { get; set; }

        public int Con_Id { get; set; }

        public int Art_Id { get; set; }

        public int Sag_Id { get; set; }

        public int? Pre_Id { get; set; }

        public virtual Article Article { get; set; }
    }
}
