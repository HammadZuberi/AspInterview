using AspInterview.Data;
using AspInterview.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AspInterview.Services
{
    public interface IMusicService
    {
        public List<AlbumDto> GetAlbums(AlbumListPage albumviewmodel);
        int CreateAlbum(AlbumDto album);
        public AlbumDto GetAlbumbyId(int albumId);
        public bool EditAlbum(AlbumDto album);
        public List<SongDto> GetSongs(string filter, int lenght);

        public List<SongDto> GetSongsbyAlbum(int albumId);
        public int AddSong(SongDto songDto);
        public bool DeleteSong(int songId);
		public List<ReportModel> GetReport();
    }

    public class MusicService : IMusicService
    {
        private readonly DataContext _context;

        public MusicService(DataContext context)
        {
            _context = context;
        }

        //public List<AlbumDto> GetAlbums()
        //{
        //    return _context.Albums.Include(n => n.Songs).Select(album => new AlbumDto
        //    {
        //        Id = album.Id,
        //        Artist = album.Artist,
        //        Title = album.Title,
        //        ReleaseDate = album.ReleaseDate,
        //        NumberOfSongs = album.Songs.Count,

        //    }).ToList();
        //}
        public List<AlbumDto> GetAlbums(AlbumListPage albumviewmodel)
        {
            IQueryable<AlbumDto> listAlbums = _context.Albums.Include(n => n.Songs).Select(album => new AlbumDto
            {
                Id = album.Id,
                Artist = album.Artist,
                Title = album.Title,
                ReleaseDate = album.ReleaseDate.Date,
                NumberOfSongs = album.Songs.Count,

            });

            if (albumviewmodel.Artist != null)
            {
                //listAlbums = listAlbums.Where(a => a.Artist.ToLower() == albumviewmodel.Artist.ToLower());

                listAlbums = listAlbums.Where(a => EF.Functions.Like(a.Artist, $"%{albumviewmodel.Artist}%"));
            }

            if (albumviewmodel.StartDate.HasValue)

                listAlbums = listAlbums.Where(s => s.ReleaseDate >= albumviewmodel.StartDate);

            if (albumviewmodel.EndDate.HasValue)

                listAlbums = listAlbums.Where(s => s.ReleaseDate <= albumviewmodel.EndDate);


            return listAlbums.ToList();
        }


        public int CreateAlbum(AlbumDto albumDto)
        {
            var album = new Album
            {
                Title = albumDto.Title,
                Artist = albumDto.Artist,
                ReleaseDate = albumDto.ReleaseDate,
            };
            _context.Albums.Add(album);
            _context.SaveChanges();
            return album.Id;
        }

        public AlbumDto GetAlbumbyId(int albumId)
        {
            AlbumDto AlbumDto = _context.Albums.Select(album => new AlbumDto
            {
                Id = album.Id,
                Artist = album.Artist,
                Title = album.Title,
                ReleaseDate = album.ReleaseDate

            }).Where(a => a.Id == albumId).FirstOrDefault();


            if (AlbumDto == null)
                return null;

            return AlbumDto;
        }

        public bool EditAlbum(AlbumDto albumDto)
        {
            try
            {
                var album = new Album
                {
                    Id = albumDto.Id,
                    Title = albumDto.Title,
                    Artist = albumDto.Artist
                };

                _context.Albums.Update(album);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public List<SongDto> GetSongs(string filter, int lenght)
        {
            IQueryable<Song> songs = _context.Songs.Include(a => a.Album);

            switch (filter)
            {
                case "artist":

                    songs = songs.OrderBy(a => a.Album.Artist);
                    break;
                case "releasedate":

                    songs = songs.OrderBy(a => a.Album.ReleaseDate);
                    break;
                case "lenght":

                    songs = songs.OrderBy(a => a.LengthInSeconds);
                    break;
                default:
                    songs = songs.OrderBy(s => s.Id);
                    break;

            }

            if (lenght > 0)
            {
                songs = songs.Where(s => s.LengthInSeconds > lenght);

            }
            else if (lenght > 0)
            {
                songs = songs.Where(s => s.LengthInSeconds < -lenght);

            }


            return songs.Select(song => new SongDto
            {
                Id = song.Id,
                Title = song.Title,
                Artists = song.Album.Artist,
                AlbumName = song.Album.Title,
                LengthInSeconds = song.LengthInSeconds,
            }).ToList();



        }

        public bool DeleteSong(int songId)
        {
            Song song = _context.Songs.First(s => s.Id == songId);
            if (song != null)
            {
                _context.Songs.Remove(song);
                _context.SaveChanges();
                return true;
            }

            return false;

        }


        public List<SongDto> GetSongsbyAlbum(int albumId)
        {
            return _context.Songs.Where(s => s.Album.Id == albumId)
                .Select(song => new SongDto
                {
                    Id = song.Id,
                    Title = song.Title,
                    Artists = song.Album.Artist,
                    AlbumId = song.Album.Id,
                    AlbumName = song.Album.Title,
                    LengthInSeconds = song.LengthInSeconds,
                }).ToList();

        }

        public int AddSong(SongDto songDto)
        {
            Album album = _context.Albums.First(a => a.Id == songDto.AlbumId);

            Song song = new Song()
            {
                Id = songDto.Id,
                Title = songDto.Title,
                LengthInSeconds = songDto.LengthInSeconds,
                Album = album
            };

            _context.Songs.Add(song);
            _context.SaveChanges(true);
            return song.Id;
        }

        public List<ReportModel> GetReport()
        {

            return _context.Albums.GroupBy(g => g.ReleaseDate.Year)
                  .Select(x => new ReportModel()
                  {
                      Year = x.Key,
                      Album = x.Select(a => a.Title).ToList(),
                  }).ToList();

        }
    }
}
