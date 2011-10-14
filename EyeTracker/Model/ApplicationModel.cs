using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeTracker.Domain.Model.BackOffice;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using EyeTracker.Domain.Model;

namespace EyeTracker.Model
{
    public class ApplicationModel
    {
        public int Id { get; set; }

        [Required]
        public int PortfolioId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Type")]
        public ApplicationType Type { get; set; }
    }
}