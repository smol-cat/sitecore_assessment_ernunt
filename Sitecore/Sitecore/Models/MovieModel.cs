using Sitecore.Models;
using System;
using System.ComponentModel;


namespace Sitecore.Models
{
    public class MovieModel
    {
        public string id { get; set; }
        [DisplayName("Name")]
        public string name { get; set; }
        [DisplayName("Release Date")]
        public string releaseDate { get; set; }

        [DisplayName("Director name")]
        public string directorName { get; set; }

        public string directorId { get; set; }

        public MovieModel(string id, string name, string releaseDate, string directorName, string directorId)
        {
            this.id = id;
            this.name = name;
            this.releaseDate = releaseDate;
            this.directorName = directorName;
            this.directorId = directorId;
        }
    }
}
