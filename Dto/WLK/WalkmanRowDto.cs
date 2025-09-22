using System.ComponentModel.DataAnnotations;

namespace TodoCheckerApp.Dto
{
    public class WalkmanRowDto
    {

        /// <summary>
        /// タイトル
        /// </summary>
        [Key]
        public string? title { get; set; }

        /// <summary>
        /// アーティスト
        /// </summary>
        public string? artist { get; set; }

        /// <summary>
        /// アルバム
        /// </summary>
        public string? album { get; set; }

        /// <summary>
        /// トラック
        /// </summary>
        public int? track { get; set; }

        /// <summary>
        /// リリース
        /// </summary>
        public DateTime? release { get; set; }

        /// <summary>
        /// ジャンル
        /// </summary>
        public string? genre { get; set; }

        /// <summary>
        /// 国名
        /// </summary>
        public string? country { get; set; }
    }

    // DB 用（Entity Framework）
    public class Walkman
    {
        /// <summary>
        /// タイトル
        /// </summary>
        [Key]
        public string? title { get; set; }

        /// <summary>
        /// アーティスト
        /// </summary>
        public string? artist { get; set; }

        /// <summary>
        /// アルバム
        /// </summary>
        public string? album { get; set; }

        /// <summary>
        /// トラック
        /// </summary>
        public int? track { get; set; }

        /// <summary>
        /// リリース
        /// </summary>
        public DateTime? release { get; set; }

        /// <summary>
        /// ジャンル
        /// </summary>
        public string? genre { get; set; }

        /// <summary>
        /// 国名
        /// </summary>
        public string? country { get; set; }
    }
}
