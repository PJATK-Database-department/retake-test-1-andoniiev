using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APBDRetake1.Dto
{
    public class Album
    {

        [Required]
        public int IdAlbum { get; set; }


        [Required]
        [StringLength(30)]
        public string AlbumName { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        [Required]
        public int IdMusicLable { get; set; }

        List<Track> Tracks { get; set; }

        public Album( int id, string name, DateTime date, int labelId, List<Track> tracks)
        {
            IdAlbum = id;
            AlbumName = name;
            PublishDate = date;
            IdMusicLable = labelId;
            Tracks = tracks;
        }
    }
}
