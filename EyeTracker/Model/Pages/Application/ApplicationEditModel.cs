using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeTracker.Domain.Model.BackOffice;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using EyeTracker.Domain.Model;

namespace EyeTracker.Model.Pages.Application
{
    public class ApplicationEditModel : ApplicationModel
    {
        [DisplayName("Type")]
        public new ApplicationType Type { get; set; }
    }
}