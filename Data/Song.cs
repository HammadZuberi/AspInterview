namespace AspInterview.Data
{
    public class Song
    {
        public int Id { get; set; }
        public Album Album { get; set; }
        public string Title { get; set; }
        public int LengthInSeconds { get; set; }
    }
}
