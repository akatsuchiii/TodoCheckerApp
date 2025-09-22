$(function () {
    let tabs = $(".tab");
    $(".tab").on("click", function () {
        $(".active").removeClass("active");
        $(this).addClass("active");
        const index = tabs.index(this);
        $(".content").removeClass("show").eq(index).addClass("show");
    });
});


// ボタンイベント
function clickHomeButton() {
    alert("ホームボタンが押されました");
}

function clickRemindButton() {
    alert("通知ボタンが押されました");
}

function clickMenuButton() {
    alert("メニュー一覧ボタンが押されました");
}
