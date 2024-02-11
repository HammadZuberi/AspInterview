namespace AspInterview.Models
{
    public class AlbumEditPage
    {

        public AlbumEditPage()
        {
            SongsList = new List<SongDto>();
        }
        public AlbumDto Album { get; set; }
        public List<SongDto> SongsList { get; set; }
    }
}
