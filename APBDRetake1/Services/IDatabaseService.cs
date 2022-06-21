using System.Collections.Generic;
using System.Threading.Tasks;
using APBDRetake1.Dto;

namespace APBDRetake1.Services
{
    public interface IDatabaseService
    {
        Task<Album> GetInfoAlbumAsync(int id);
        Task<bool> DoesMusicianExistAsync(int id);
        Task<bool> DoesAlbumExistAsync(int id);
        bool IsInvolvedInSong(int idMusician);
        public List<Track> GetSongsInAlbum(int id);
        Task DeleteMusicianAsync(int id);
    }
}
