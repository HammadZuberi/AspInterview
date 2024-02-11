using AspInterview.Data;
using System.ComponentModel.DataAnnotations;

namespace AspInterview.Models
{
    public class SongsListPage
    {
        public SongsListPage()
        {
            SongsList = new List<SongDto>();
        }
        public List<SongDto> SongsList { get; set; }
    }
    public class SongDto
    {
        public int Id { get; set; }
        public int AlbumId { get; set; }
        public string AlbumName { get; set; }
        [Required]
        public string Artists { get; set; }
        [Required]
        public string Title { get; set; }
        public int LengthInSeconds { get; set; }
    }
}
