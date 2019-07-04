$(document).foundation()

$(document).ready(function () {
    $("#lightSlider").lightSlider({
        item: 4,
        responsive: [
            {
                breakpoint: 1024,
                settings: {
                    item: 2,
                    slideMove: 1,
                }
            }
        ]
    });
    //$('#btnSaveCategory').click(function () {
    //    var category = {};
    //    category.Name = $('#txtCategoryName').val();
    //    category.Source = $('#txtCategoryImage').val();
    //    $.ajax({
    //        type: "POST",
    //        url: "Home.aspx/SaveCategory",
    //        data: '{category: ' + JSON.stringify(category) + '}',
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        success: function (response) {
    //            alert("User has been added successfully.");
    //            window.location.reload();
    //        }
    //    });
    //    return false;
    //});
});
function clicker(e) {
    e.preventDefault();
    var categoryObj = {
        name: $('#MainContent_txtCategoryName').val(),
        source: $('#MainContent_txtCategoryImage').val()
    };

    var category = JSON.stringify(categoryObj);
    $.ajax({
        type: "POST",
        url: "Home.aspx/SaveCategory",
        data: '{category:' + category + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $("#commentForm").validate();
            alert("User has been added successfully.");
            //window.location.reload();
        },
        error: function (result) {
            console.log("error" + result);
        }
    });
    return false;
}