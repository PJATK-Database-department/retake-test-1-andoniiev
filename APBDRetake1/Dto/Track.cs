using System.ComponentModel.DataAnnotations;

namespace APBDRetake1.Dto
{
    public class Track
    {
        [Required]
        public int IdTrack { get; set; }
        [Required]
        [StringLength(20)]
        public string TrackName { get; set; }
        [Required]
        private double Duration { get; set; }

        [Required]
        private int? IdMusicAlbum { get; set; }

        public Track(int id, string name, double duration, int? idAlbum)
        {
            IdTrack = id;
            TrackName = name;
            Duration = duration;
            IdMusicAlbum = idAlbum;
        }

    }
}
