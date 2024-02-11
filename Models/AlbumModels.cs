namespace AspInterview.Models
{
	public class AlbumListPage
	{
		public List<AlbumDto> Albums { get; set; } = new List<AlbumDto>();

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Artist { get; set; }
    }

	public class AlbumDto
	{
        public int Id { get; set; }
		public string Artist { get; set; }
		public string Title { get; set; }
		public int NumberOfSongs { get; set; }
		public DateTime ReleaseDate { get; set; }
	}
}
