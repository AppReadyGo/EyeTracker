using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace EyeTracker.Model.Pages.Home
{

    public class ContentModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
