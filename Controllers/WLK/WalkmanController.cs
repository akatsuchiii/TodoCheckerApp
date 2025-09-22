using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoCheckerApp.Contexts;
using TodoCheckerApp.Contexts.WLK;
using TodoCheckerApp.Dto;
using TodoCheckerApp.ViewModels;
using TodoCheckerApp.ViewModels.WLK;
using X.PagedList;
using X.PagedList.Extensions;

namespace TodoCheckerApp.Controllers
{
    public class WalkmanController : Controller
    {
        private readonly WalkmanContext _db;

        public WalkmanController(WalkmanContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View("~/Views/WLK/Index.cshtml");
        }

        [HttpGet]
        public IActionResult GetWalkmanData()
        {
            var items = _db.Walkman
                .Select(x => new WalkmanRowDto
                {
                    title = x.title,
                    artist = x.artist,
                    album = x.album,
                    track = x.track,
                    release = x.release,
                    genre = x.genre,
                    country = x.country
                }).ToList();

            return Json(new { data = items });
        }

        [HttpPost]
        public IActionResult InsertRows([FromBody] List<WalkmanRowDto> rows)
        {
            if (rows == null || rows.Count == 0)
            {
                return BadRequest(new { success = false, message = "登録するデータがありません" });
            }

            foreach (var row in rows)
            {
                var entity = new Walkman
                {
                    title = row.title,
                    artist = row.artist,
                    album = row.album,
                    track = row.track,
                    release = row.release,
                    genre = row.genre,
                    country = row.country
                };
                _db.Walkman.Add(entity);
            }

            _db.SaveChanges();

            return Ok(new { success = true, message = "登録成功" });
        }

    }
}