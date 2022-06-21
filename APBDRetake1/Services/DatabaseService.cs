using APBDRetake1.Dto;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace APBDRetake1.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly string _connectionString;
        public DatabaseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task DeleteMusicianAsync(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                SqlTransaction tran = con.BeginTransaction();
                try
                {
                   if(IsInvolvedInSong(id))
                    {
                        var cmdTasks = new SqlCommand("DELETE FROM Musician WHERE IdMusician = @IdMusician", con, tran);
                        cmdTasks.Parameters.AddWithValue("IdMusician", id);
                        await cmdTasks.ExecuteNonQueryAsync();

                    }

                    var cmd = new SqlCommand("DELETE FROM Musician WHERE IdMusician = @IdMusician", con, tran);
                    cmd.Parameters.AddWithValue("IdMusician", id);
                    await cmd.ExecuteNonQueryAsync();
                    tran.CommitAsync();
                }
                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                }
            }
        }

        public async Task<Album> GetInfoAlbumAsync(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("SELECT IdAlbum, AlbumName, PublishDate, IdMusicLabel FROM Album where IdAlbum = @IdAlbum");
                cmd.Parameters.AddWithValue("IdAlbum", id);
                await con.OpenAsync();
                var dr = await cmd.ExecuteReaderAsync();
                await dr.ReadAsync();
                var listOfSongs = GetSongsInAlbum(id);

                return new Album(Convert.ToInt32(dr[0]), dr[1].ToString(), Convert.ToDateTime(dr[2]), Convert.ToInt32(dr[3]),listOfSongs);
            }
        }
        public List<Track>GetSongsInAlbum(int id)
        {
            var result = new List<Track>();
            using (var con = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("SELECT t.TrackName, t.Duration,  FROM Track t, Album a WHERE IdAlbum = @IdAlbum AND t.IdMusic = a.IdAlbum ORDER BY Duration DESC", con);
                cmd.Parameters.AddWithValue("IdAlbum", id);
                 con.Open();
                var dr =  cmd.ExecuteReader();
                while ( dr.Read())
                {
                    var singleTask = new Track(Convert.ToInt32(dr[0]), dr[1].ToString(), Convert.ToDouble(dr[2]), Convert.ToInt32(dr[3]));
                    result.Add(singleTask);
                }
                return result;
            }
        }

        public Task<bool> DoesAlbumExistAsync(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("SELECT IIF(COUNT(1) >0,1,0 FROM Album WHERE IdAlbum = @IdAlbum", con);
                cmd.Parameters.AddWithValue("IdAlbum", id);
                con.OpenAsync();
                var result = cmd.ExecuteScalarAsync();
                return Task.FromResult(Convert.ToBoolean(result));
            }
        }

        public Task<bool> DoesMusicianExistAsync(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("SELECT IIF(COUNT(1) >0,1,0 FROM Musician WHERE IdMusician = @IdMusician", con);
                cmd.Parameters.AddWithValue("IdMusician", id);
                con.OpenAsync();
                var result = cmd.ExecuteScalarAsync();
                return Task.FromResult(Convert.ToBoolean(result));
            }
        }

        

        public bool IsInvolvedInSong(int idMusician)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("SELECT TOP 1 FROM Musician_Track WHERE IdMusician = @IdMusician",  con);
                cmd.Parameters.AddWithValue("IdMusician", idMusician);
                con.OpenAsync();
                var result = cmd.ExecuteScalarAsync();
                return Convert.ToBoolean(result);
            }
        }
    }
}
