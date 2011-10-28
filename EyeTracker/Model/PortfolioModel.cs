using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EyeTracker.Model
{
    public class PortfolioModel
    {
        public virtual int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Description")]
        public virtual string Description { get; set; }

        [Required]
        [DisplayName("Time Zone")]
        public short TimeZone { get; set; }
    }
}
