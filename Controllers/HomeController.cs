using AspInterview.Data;
using AspInterview.Models;
using AspInterview.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspInterview.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IMusicService _musicService;

		public HomeController(ILogger<HomeController> logger, IMusicService musicService)
		{
			_logger = logger;
			_musicService = musicService;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Albums(AlbumListPage albumviewmodel)
		{
			return View(new AlbumListPage()
			{
				Albums = _musicService.GetAlbums(albumviewmodel),
			});
		}
		public IActionResult EditAlbum(int albumId)
		{
			var albumResponse = _musicService.GetAlbumbyId(albumId);

			return View(albumResponse);
		}

		[HttpPost]
		public IActionResult EditAlbum(AlbumDto album)
		{
			var albumResponse = _musicService.EditAlbum(album);

			if (albumResponse)
			return	RedirectToAction(nameof(Albums));

			return View(album.Id);
		}

		[HttpGet]
		public IActionResult Songs(string sortBy, int lenght)
		{
			return View(new SongsListPage()
			{
				SongsList = _musicService.GetSongs(sortBy, lenght),
			});
		}

		#region Songs


		[HttpGet]
		public IActionResult SongsbyAlbum(int albumId)
		{
			return View(new SongsListPage()
			{
				SongsList = _musicService.GetSongsbyAlbum(albumId)
			});
		}

		[HttpGet]
		public IActionResult AddSong(int albumId)
		{
			SongDto songDto = new SongDto();
			songDto.AlbumId = albumId;
			return View(songDto);
		}

		[HttpPost]
		public IActionResult AddSong(SongDto song)
		{
			int response = _musicService.AddSong(song);

			if (response != null)
			{
				return RedirectToAction(nameof(SongsbyAlbum), new { albumId = song.AlbumId });
			}
			return View();
		}

		public IActionResult DeleteSong(int songid, int albumid)
		{

			var response = _musicService.DeleteSong(songid);
			//if(response)
			return RedirectToAction(nameof(SongsbyAlbum), new { albumId = albumid });


		}

		#endregion
		[HttpGet]
		public IActionResult Reports()
		{
			return View(_musicService.GetReport());
		}

	}
}