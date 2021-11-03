using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Sitecore.Models
{
    public class DirectorModel
    {
        [DisplayName("Name")]
        public string name { get; set; }
        [DisplayName("Birth date")]
        public string birthDate { get; set; }

        public DirectorModel(string name, string birthDate)
        {
            this.name = name;
            this.birthDate = birthDate;
        }
    }
}