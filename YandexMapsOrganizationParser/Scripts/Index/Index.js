"use strict";
const storage = localStorage;
const $search = $('#search'), $offers = $('#offers');
const animationEnd = 'webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend';
$.fn.extend({
    animateCss: function (animationName) {
        this.addClass('animated ' + animationName).one(animationEnd, function () {
            $(this).removeClass('animated ' + animationName);
        });
    },
    animateOut: function (animationName) {
        this.addClass('animated ' + animationName).one(animationEnd, function () {
            $(this).addClass('is-hidden').removeClass('animated ' + animationName);
        });
    },
    animateIn: function (animationName) {
        $(this).removeClass('is-hidden');
        this.animateCss(animationName);
    }
});
storage['query'] = storage['query'] || 0;
function buyApp() {
    // $search.animateOut('flipOutY')
    $search.hide();
    $offers.animateIn('flipInY');
}
function newQuery() {
    if (storage['query'] >= 2) {
        buyApp();
        return false;
    }
    storage['query']++;
    return true;
}

///////////////////////////////////////////////////////////////

$(function () {

    $("#city").kladr({
        type: $.kladr.type.city
    });


    var Table = $("#organizations").DataTable({
        dom: 'Bfrtip',
        buttons: [
             'excel'
        ],
        autoWidth: false,

        data: [],
        columns: [
            { "data": "Name" },
            { "data": "Time" },
            { "data": "Category" },
            { "data": "Address" },
            { "data": "Phones" },
            { "data": "Links" },
            { "data": "City" }
        ],
        rowCallback: function (row, data) { },
        filter: false,
        info: false,
        ordering: false,
        processing: true,
        retrieve: true,
        "scrollX": true
    });

    $("#GetOrganizations").on("click", function (event) {

        var city = $('#city').val();
        var category = $('#category').val();
        var data = {
            city: city,
            category: category
        };

        $.ajax({
            url: "/Home/GetOrganizations",
            type: "post",
            data: data,
        }).done(function (result) {
            var data = $.parseJSON(result.data);
            var reqLeft = result.ReqLeft;
            var status = result.success;

            Table.clear().draw();

            if (result.success === true) {
                Table.rows.add(data).draw();

            }
            else {
                $search.hide();
                $offers.animateIn('flipInY');
            }
            $('#ReqLeft').text(reqLeft);

        }).fail(function (jqXHR, textStatus, errorThrown) {
            // needs to implement if it fails
        });

    });


    $('#authForm').on("submit", function (e) {
        var messageBlock = $(this).find('#authMessageBlock');

        e.preventDefault();
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            //contentType: "application/json; charset=UTF-8",
            success: function (data, textStatus, exceptionThrown) {
                if (data.success === true) {

                    window.location = window.location;
                    
                }
                else {
                    var errorData = $.parseJSON(data.responseText);

                    // clear
                    messageBlock.html('');

                    // fill
                    errorData.ErrorList.forEach(function (erMessage) {
                        var div = document.createElement('div');
                        div.innerText = erMessage;
                        div.className = "text-danger";

                        messageBlock[0].appendChild(div);
                    });
                }

            },
            error: function (data) {
                alert('error');
            }
        });
    });

    $('#regForm').on("submit", function (e) {
        var messageBlock = $(this).find('#regMessageBlock');

        e.preventDefault();
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            //contentType: "application/json; charset=UTF-8",
            success: function (data, textStatus, exceptionThrown) {
                if (data.success === true) {

                    window.location = window.location;

                }
                else {
                    var errorData = $.parseJSON(data.responseText);

                    // clear
                    messageBlock.html('');

                    // fill
                    errorData.ErrorList.forEach(function (erMessage) {
                        var div = document.createElement('div');
                        div.innerText = erMessage;
                        div.className = "text-danger";

                        messageBlock[0].appendChild(div);
                    });
                }

            },
            error: function (data) {
                alert('error');
            }
        });
    });

})