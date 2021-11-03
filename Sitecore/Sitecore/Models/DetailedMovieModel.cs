using Sitecore.Models;
using System;
using System.ComponentModel;

namespace Sitecore.Models
{
    public class DetailedMovieModel : MovieModel
    {
        [DisplayName("Short description")]
        public string shortDescription { get; set; }
        [DisplayName("Description")]
        public string description { get; set; }
        public string image { get; set; }
        [DisplayName("Director's birth date")]
        public string birthDate { get; set; }

        public int likes { get; set; }

        public DetailedMovieModel(string id, 
            string name,
            string releaseDate, 
            string shortDescription, 
            string description, 
            string image, 
            string directorName,
            string birthDate, 
            string directorId,
            int likes)
            : base(id, name, releaseDate, directorName, directorId)
        {
            this.shortDescription = shortDescription;
            this.description = description;
            this.image = image;
            this.birthDate = birthDate;
            this.likes = likes;
        }
    }
}