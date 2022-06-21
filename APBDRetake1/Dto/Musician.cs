using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APBDRetake1.Dto
{
    public class Musician
    {
        [Required]
        public int IdMusician { get; set; }
        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
       
        [StringLength(20)]
        public string? Nickname{ get; set; }

        List<Track> TracksInvolved { get; set; }

        public Musician(int id, string name, string surname, string? nickname)
        {
            IdMusician = id;
            FirstName = name;
            LastName = surname;
            Nickname = nickname;
        }
    }
}
