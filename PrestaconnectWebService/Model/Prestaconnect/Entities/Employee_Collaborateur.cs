namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employee_Collaborateur
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Sage_CO_No { get; set; }

        public int IdEmployee { get; set; }
    }
}
