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
            { "data": "Category" },
            { "data": "Site" },
            { "data": "Address" },
            { "data": "Phones" },
            { "data": "City" }
        ],
        rowCallback: function (row, data) { },
        filter: false,
        info: false,
        ordering: false,
        processing: true,
        retrieve: true
    });

    $("#GetOrganizations").on("click", function (event) {

        var city = $('#city').val();
        var category = $('#category').val();
        var data = {
            city: city,
            category: category
        };

        $.ajax({
            url: "Home/GetOrganizations",
            type: "post",
            data: data,
        }).done(function (result) {
            Table.clear().draw();
            Table.rows.add($.parseJSON( result.data) ).draw();
        }).fail(function (jqXHR, textStatus, errorThrown) {
            // needs to implement if it fails
        });

    });
})