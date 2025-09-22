using Microsoft.AspNetCore.Mvc;
using TodoCheckerApp.Contexts;
using TodoCheckerApp.Dto;
using TodoCheckerApp.ViewModels;
using X.PagedList;
using X.PagedList.Extensions;

namespace TodoCheckerApp.Controllers
{
    public class MenuController : Controller
    {
        //private readonly MenuContext _db;

        //public MenuController(MenuContext db)
        //{
        //    _db = db;
        //}

        public IActionResult Index()
        {
            //// DB から全データを取得
            //var items = _db.Walkman.ToList();

            //// 必要なら foreach で加工
            //var list = new List<MenuRowDto>();
            //foreach (var item in items)
            //{
            //    // ここで加工可能（例: 空白削除、文字列変換など）
            //    var row = new MenuRowDto
            //    {
            //        title = item.title?.Trim(),
            //        artist = item.artist?.Trim(),
            //        album = item.album?.Trim(),
            //        track = item.track,
            //        release = item.release,
            //        genre = item.genre?.Trim(),
            //        country = item.country?.Trim()
            //    };
            //    list.Add(row);

            //    Console.WriteLine(item.title);
            //}

            //// ViewModel にセット
            //var vm = new MenuViewModel
            //{
            //    ViewList = list
            //};

            return View("~/Views/MNU/Index.cshtml");
        }

    }
}
