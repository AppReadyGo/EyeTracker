using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EyeTracker.Common.Entities;
using EyeTracker.Model.Filter;

namespace EyeTracker.Model.Pages.Application
{
    public class ApplicationModel : FilterModel
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

        public ApplicationViewModel ViewData { get; set; }
    }
}