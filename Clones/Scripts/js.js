$(document).ready(function ()
{
    $(".j-save-model").click(function (event) {
        event.preventDefault();

        var id = $(this).attr("data-id");
        var field = $("#profile-" + id);

        if ($("#profile-form").valid()) {

            var data_container = {
                Id: id,
                Name: field.find("#profile-name-" + id).val()
            }

            $.ajax({
                url: "/ProfileManagement/EditProfile",
                method: "Post",
                data: data_container,
                success: function (data, p1, p2) {
                    //GetResponseMessage(p1);
                    window.location.reload();
                }
            });
        }
    });

    $(".j-save-account").click(function (event) {
        event.preventDefault();

        var id = $(this).attr("data-id");
        var profileId = $(this).attr("data-profile-id");
        var field = $("#account-" + id);

        if ($("#profile-form").valid()) {

            var data_container = {
                Id: id,
                ProfileId: profileId,
                Name: field.find("#account-name-" + id).val()
            }

            $.ajax({
                url: "/ProfileManagement/EditAccount",
                method: "Post",
                data: data_container,
                success: function (data, p1, p2) {
                    //GetResponseMessage(p1);
                    window.location.reload();
                }
            });
        }
    });

    $(".j-save-stock").click(function (event) {
        event.preventDefault();

        var id = $(this).attr("data-id");
        var field = $("#stock-" + id);

        if ($("#stock-form").valid()) {

            var data_container = {
                Id: id,
                Name: field.find("#stock-name-" + id).val()
            }

            $.ajax({
                url: "/StockManagement/EditStock",
                method: "Post",
                data: data_container,
                success: function (data, p1, p2) {
                    //GetResponseMessage(p1);
                    window.location.reload();
                }
            });
        }
    });

    var GetResponseMessage = function (message) {
        var time = Date.now();
        var response_tmpl = "<span id='" + time + "' style='padding: 10px; width: 300px; background: green;'>" + message + "</span>";

        $(".j-response").append(response_tmpl);
        setTimeout(function () { $("#" + time).remove(); }, 2000);
    };

    $(".j-delete-profile").click(function (event) {
        var id = $(this).attr("data-id");

        var message_tmpl =
            "<div id='alert-message' style='position: fixed; top: 15px; left: 50%; margin-left: -200px; width: 400px; background: white; border: 2px solid red; z-index: 5000; padding: 15px; text-align: center;'>" +
            "Profile will be delete permanently<br />" +
            "<button data-id-delete="+id+" id='confirm-delete' class='btn btn-danger' style='margin-top: 30px;'>Confirm</button>" +
            "</div>";

        $("body").append(message_tmpl);

        $("#confirm-delete").click(function () {
            var id = $(this).attr("data-id-delete");

            if ($("#profile-form").valid()) {
                $.ajax({
                    url: "/ProfileManagement/DeleteProfile/" + id,
                    method: "Post",
                    success: function (data, p1, p2) {
                        //GetResponseMessage(p1);

                        //$("#alert-message").remove();

                        window.location.reload();
                    }
                });
            }
        });
    });

    $(".j-delete-account").click(function (event) {
        var id = $(this).attr("data-id");

        var message_tmpl =
            "<div id='alert-message' style='position: fixed; top: 15px; left: 50%; margin-left: -200px; width: 400px; background: white; border: 2px solid red; z-index: 5000; padding: 15px; text-align: center;'>" +
            "Account will be delete permanently<br />" +
            "<button data-id-delete=" + id + " id='confirm-delete' class='btn btn-danger' style='margin-top: 30px;'>Confirm</button>" +
            "</div>";

        $("body").append(message_tmpl);

        $("#confirm-delete").click(function () {
            var id = $(this).attr("data-id-delete");

            if ($("#profile-form").valid()) {
                $.ajax({
                    url: "/ProfileManagement/DeleteAccount/" + id,
                    method: "Post",
                    success: function (data, p1, p2) {
                        //GetResponseMessage(p1);

                        //$("#alert-message").remove();

                        window.location.reload();
                    }
                });
            }
        });
    });

    $(".j-delete-stock").click(function (event) {
        var id = $(this).attr("data-id");

        var message_tmpl =
            "<div id='alert-message' style='position: fixed; top: 15px; left: 50%; margin-left: -200px; width: 400px; background: white; border: 2px solid red; z-index: 5000; padding: 15px; text-align: center;'>" +
            "Stock will be delete permanently<br />" +
            "<button data-id-delete=" + id + " id='confirm-delete' class='btn btn-danger' style='margin-top: 30px;'>Confirm</button>" +
            "</div>";

        $("body").append(message_tmpl);

        $("#confirm-delete").click(function () {
            var id = $(this).attr("data-id-delete");

            if ($("#stock-form").valid()) {
                $.ajax({
                    url: "/StockManagement/DeleteStock/" + id,
                    method: "Post",
                    success: function (data, p1, p2) {
                        //GetResponseMessage(p1);

                        //$("#alert-message").remove();

                        window.location.reload();
                    }
                });
            }
        });
    });
});