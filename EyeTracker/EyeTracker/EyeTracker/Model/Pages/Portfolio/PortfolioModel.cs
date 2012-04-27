﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace EyeTracker.Model.Pages.Portfolio
{
    public class PortfolioModel : ViewModel<SelectList>
    {
        public virtual int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Description")]
        public virtual string Description { get; set; }

        [Required]
        [DisplayName("Time Zone")]
        public int TimeZone { get; set; }
    }
}