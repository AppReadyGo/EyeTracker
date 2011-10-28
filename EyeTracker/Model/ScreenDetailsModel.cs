using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EyeTracker.Model
{
    public class ScreenDetailsModel
    {
        public int PortfolioId { get; set; }

        public int AppId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Width")]
        public int Width { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Height")]
        public int Height { get; set; }
    }
}