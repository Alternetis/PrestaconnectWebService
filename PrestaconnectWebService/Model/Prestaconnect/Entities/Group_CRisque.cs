namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Group_CRisque
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Sag_CRisque { get; set; }

        public int Grp_Pre_Id { get; set; }

        public int Grp_PreId_Default { get; set; }

        public bool Grp_LockCondition { get; set; }
    }
}
