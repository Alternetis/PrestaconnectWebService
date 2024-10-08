namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using static PrestaconnectWebService.Model.Prestaconnect.Repository.LogInformationRepository;

    public partial class LogInformations
    {
        [Key]
        public int IdLog { get; set; }

        public DateTime DateLog { get; set; }

        public int TypeLog { get; set; }

        [Required]
        [StringLength(50)]
        public string NameLog { get; set; }

        [Column(TypeName = "text")]
        public string DescriptionLog { get; set; }

        [StringLength(255)]
        public string SolutionLog { get; set; }

        public string TypeLogName 
        { 
            get
            {
                return ((LogType)TypeLog).ToString();
            } 
        }
    }
}
