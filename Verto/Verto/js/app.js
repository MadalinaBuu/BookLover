$(document).foundation()

$(document).ready(function () {
    var slider = $("#lightSlider").lightSlider({
        item: 4,
        speed: 700,
        pause: 3000,
        loop: true,
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

    slider.play();
    $('.errorMessage').hide();
    $('.btnViewAllProducts').click(function () {
        ('#divProducts').toggle();
    });
});

function validate()
{
    var validCategoryName = $("#MainContent_txtCategoryName").validationEngine('validate');
    var validCategorySource = $("#MainContent_txtCategoryImage").validationEngine('validate');

    return validCategoryName && validCategorySource;
}

function SaveCategory(e) {
    e.preventDefault();
    var name = $('#MainContent_txtCategoryName').val();
    var source = $('#MainContent_txtCategoryImage').val();

    if (validate()) {
        var categoryObj = {
            name: name,
            source: source
        };

        var category = JSON.stringify(categoryObj);
        $.ajax({
            type: "POST",
            url: "Home.aspx/SaveCategory",
            data: '{category:' + category + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.d == "succes") {
                    $('.close-button').click();
                    window.location.reload(true);
                }
                else {
                    $('.errorMessage').show();
                    $('.errorMessage').append("<span>" + result.d + "</span>");
                }
            },
            error: function (result) {
                console.log("error" + result);
            }
        });
        return false;
    }
}