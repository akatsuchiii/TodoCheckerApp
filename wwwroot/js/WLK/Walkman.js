document.addEventListener("DOMContentLoaded", function () {

    const table = $("#foo-table").DataTable({
        //ordering: false,
        info: false // これで "Showing 1 to 10 of 10 entries" が消える
    });

    const $updBtn = $("#updBtn");
    const $dltBtn = $("#dltBtn");
    $updBtn.prop("disabled", true);
    $dltBtn.prop("disabled", true);

    // 行クリックで選択・解除
    $("#foo-table tbody").on("click", "tr", function () {
        $(this).toggleClass("selected");

        // 選択行が1行でもあればボタン有効化
        const hasSelection = $("#foo-table tbody tr.selected").length > 0;
        $updBtn.prop("disabled", !hasSelection);
        $dltBtn.prop("disabled", !hasSelection);
    });

    // セル編集可能化
    $("#foo-table tbody").on("mouseenter", "td", function () {
        if (!$(this).hasClass("sorting_1")) $(this).attr("contenteditable", "true");
    });

    $("#foo-table tbody").on("blur", "td", function () {
        const cell = table.cell(this);
        cell.data($(this).text()).draw(false);
    });

    // 新規行追加
    window.newRow = function () {
        const newRowData = ["", "", "", "", "", "", ""];
        const currentData = table.rows().data().toArray();
        currentData.unshift(newRowData);
        table.clear();
        table.rows.add(currentData).draw(false);
        $(table.row(0).node()).addClass("newRow selected");

        // ボタン有効化
        $updBtn.prop("disabled", false);
        $dltBtn.prop("disabled", false);
    };

    // 登録ボタン（newRow のみ）
    window.clickRegBtn = function () {
        const rows = [];
        $("#foo-table tbody tr.newRow").each(function () {
            const tds = $(this).find("td");
            rows.push({
                title: tds.eq(0).text(),
                artist: tds.eq(1).text(),
                album: tds.eq(2).text(),
                track: tds.eq(3).text(),
                release: tds.eq(4).text(),
                genre: tds.eq(5).text(),
                country: tds.eq(6).text()
            });
        });
        if (rows.length === 0) { alert("登録する行がありません"); return; }

        fetch('/Walkman/InsertRows', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
            },
            body: JSON.stringify(rows)
        }).then(res => {
            if (res.ok) location.reload();
            else alert("登録失敗");
        });
    };

    // 更新ボタン（選択行）
    window.clickUpdBtn = function () {
        const rows = [];
        $("#foo-table tbody tr.selected").each(function () {
            const tds = $(this).find("td");
            rows.push({
                title: tds.eq(0).text(),
                artist: tds.eq(1).text(),
                album: tds.eq(2).text(),
                track: tds.eq(3).text(),
                release: tds.eq(4).text(),
                genre: tds.eq(5).text(),
                country: tds.eq(6).text()
            });
        });
        if (rows.length === 0) { alert("更新する行がありません"); return; }

        fetch('/Walkman/UpdateRows', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
            },
            body: JSON.stringify(rows)
        }).then(res => {
            if (res.ok) location.reload();
            else alert("更新失敗");
        });
    };

    // 削除ボタン（選択行）
    window.clickDltBtn = function () {
        const rowsToDelete = [];
        $("#foo-table tbody tr.selected").each(function () {
            const tds = $(this).find("td");
            rowsToDelete.push({
                title: tds.eq(0).text()
            });
        });
        if (rowsToDelete.length === 0) { alert("削除する行がありません"); return; }
        if (!confirm("選択した行を削除しますか？")) return;

        fetch('/Walkman/DeleteRows', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
            },
            body: JSON.stringify(rowsToDelete)
        }).then(res => {
            if (res.ok) location.reload();
            else alert("削除失敗");
        });
    };

});
