using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EyeTracker.Model.Pages.Application
{
    public class ScreenModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Width")]
        public int Width { get; set; }

        [Required]
        [DisplayName("Height")]
        public int Height { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Path")]
        public string Path { get; set; }

        public string FileExtention { get; set; }

        public int ApplicationId { get; set; }
    }
}