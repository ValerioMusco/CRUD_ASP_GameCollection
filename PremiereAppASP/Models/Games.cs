using System.ComponentModel.DataAnnotations;

namespace PremiereAppASP.Models {
    public class Games {

        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }

        public Games(){ }

        public Games( string name, string description, string genre, DateTime releaseDate, int id = 0 ) {
            Id = id;
            Name = name;
            Description = description;
            Genre = genre;
            ReleaseDate = releaseDate;
            ;
        }
    }
}
