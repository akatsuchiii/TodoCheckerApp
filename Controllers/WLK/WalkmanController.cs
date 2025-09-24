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
            // DBからWalkman一覧を取得してViewModelにセット
            var vm = new WalkmanViewModel
            {
                ViewList = _db.Walkman
                    .Select(x => new WalkmanRowDto
                    {
                        title = x.title,
                        artist = x.artist,
                        album = x.album,
                        track = x.track,
                        release = x.release,
                        genre = x.genre,
                        country = x.country
                    }).ToList()
            };

            return View("~/Views/WLK/Index.cshtml", vm);
        }
        [HttpPost]
        public IActionResult UpdateRows([FromBody] List<WalkmanRowDto> rows)
        {
            if (rows == null || rows.Count == 0)
                return BadRequest(new { success = false, message = "更新するデータがありません" });

            foreach (var row in rows)
            {
                var entity = _db.Walkman.FirstOrDefault(x => x.title == row.title);
                if (entity != null)
                {
                    entity.artist = row.artist;
                    entity.album = row.album;
                    entity.track = row.track;
                    entity.release = row.release;
                    entity.genre = row.genre;
                    entity.country = row.country;
                }
            }

            _db.SaveChanges();
            return Ok(new { success = true, message = "更新成功" });
        }


        [HttpPost]
        public IActionResult DeleteRows([FromBody] List<WalkmanRowDto> rows)
        {
            if (rows == null || rows.Count == 0)
                return BadRequest(new { success = false, message = "削除するデータがありません" });

            foreach (var row in rows)
            {
                var entity = _db.Walkman.FirstOrDefault(x => x.title == row.title);
                if (entity != null)
                {
                    _db.Walkman.Remove(entity);
                }
            }

            _db.SaveChanges();
            return Ok(new { success = true, message = "削除成功" });
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