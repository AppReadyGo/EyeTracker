using System.ComponentModel;
using EyeTracker.Common.Entities;

namespace EyeTracker.Model.Pages.Application
{
    public class ApplicationEditModel : ApplicationModel
    {
        [DisplayName("Type")]
        public new ApplicationType Type { get; set; }
    }
}