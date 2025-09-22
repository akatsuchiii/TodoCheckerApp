document.addEventListener("DOMContentLoaded", function () {

    // 1. DataTables 初期化（サーバーからデータ取得）
    const table = $("#foo-table").DataTable({
        ajax: '/Walkman/GetWalkmanData',   // サーバーから JSON を取得
        info: false,
        paging: true,
        searching: true,
        ordering: true,
        columns: [
            {
                data: null,
                render: function (data, type, row) {
                    return '<input type="checkbox" class="checkBox">';
                }
            },
            { data: "title" },
            { data: "artist" },
            { data: "album" },
            { data: "track" },
            { data: "release" },
            { data: "genre" },
            { data: "country" },
        ],
        columnDefs: [
            { orderable: false, targets: 0}
        ]
    });

    // 2. セル編集可能化
    $("#foo-table tbody").on("mouseenter", "td", function () {

        if (!$(this).hasClass("sorting_1")) {
            $(this).attr("contenteditable", "true");

        }

    });

    // 編集後に DataTables の内部データを更新
    $("#foo-table tbody").on("blur", "td", function () {
        const cell = table.cell(this);
        cell.data($(this).text()).draw();
    });

    // 3. 新規行追加
    window.newRow = function () {
        const newNode = table.row.add({
            title: "",
            artist: "",
            album: "",
            track: "",
            release: "",
            genre: "",
            country: ""
        }).draw(false).node();

        $(newNode).addClass("newRow");
        $('#updBtn').prop('disabled', true); // 更新ボタン無効化
    };

    // 4. 登録ボタン
    window.clickRegBtn = function () {
        let rowData = [];
        table.rows(".newRow").every(function () {
            rowData.push(this.data());
        });

        if (rowData.length === 0) {
            alert("登録する行がありません");
            return;
        }

        fetch('/Walkman/InsertRows', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                // CSRF トークンを使用している場合は以下を追加
                //'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
            },
            body: JSON.stringify(rowData)
        })
            .then(res => {
                if (res.ok) {
                    alert("登録しました！");
                    table.ajax.reload();   // 最新データを取得
                } else {
                    alert("登録に失敗しました");
                }
            })
            .catch(err => console.error(err));
    };


    //window.clickUpdBtn = function () {
    //    let 
    //}



    // 5. ボタンイベント例
    window.clickHomeButton = function () { alert("ホームボタンが押されました"); };
    window.clickRemindButton = function () { alert("通知ボタンが押されました"); };
    window.clickMenuButton = function () { alert("メニュー一覧ボタンが押されました"); };

});
