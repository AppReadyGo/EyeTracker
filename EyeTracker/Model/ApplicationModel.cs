using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeTracker.Domain.Model.BackOffice;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EyeTracker.Model
{
    public class ApplicationModel
    {
        public virtual int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Description")]
        public virtual string Description { get; set; }
    }
}