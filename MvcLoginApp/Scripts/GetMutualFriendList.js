//$(function () {
    $("a[data-modal='add']").on("click", function (event) {
        event.stopPropagation();
        event.stopImmediatePropagation();
        $("#divMutualFriendContent").load(this.href, function () {
            $("#divMutualFriendModal").modal({ keyboard: true }, "show");
            return false;
        });
        return false;
    });
    //$("#divMutualFriendList").dialog({
    //    autoOpen: false,
    //    modal: true,
    //    title: "View Details"
    //});
    //$(".divMutualFriend").click(function () {
    //    debugger
    //    $.ajax({
    //        type: "GET",
    //        url: "/Home/GetMutualFriendList",
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "html",
    //        success: function (response) {
    //            debugger
    //            $('#divMutualFriendList').html(response);
    //        },
    //        failure: function (response) {
    //            alert(response.responseText);
    //        },
    //        error: function (response) {
    //            alert(response.responseText);
    //        }
    //    });
    //});

    //$(".divMutualFriend").on('click', function () {
    //    debugger;
    //    alert('1');
    //    $("#divMutualFriendList").dialog('open');
            
    //});
    //$("#divMutualFriendList").dialog({
    //    autoOpen: true,
    //    position: { my: "center", at: "top+350", of: window },
    //    width: 1000,
    //    resizable: false,
    //    title: 'Mutual Friends',
    //    modal: true,
    //    open: function () {
    //        alert('2');
    //        $(this).load('@Url.Action("GetMutualFriendList", "Home")');
    //    },
    //    buttons: {
    //        Cancel: function () {
    //            $(this).dialog("close");
    //        }
    //    }
    //});
//});