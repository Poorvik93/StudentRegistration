//Student Courses Data Table

$('#courses').DataTable({
    layout: {
        topStart: $('<div><h4>Courses </h4></div>'),
        topEnd: {
            search: {
                placeholder: 'Search..'
            }
        },
        bottomStart: {
            paging: {
                numbers: 3
            }
        },
        bottomEnd: null

    },
    ordering: false,
    responsive: true,
    pagingType: 'simple_numbers',
    pageLength: 5,

});

$(document).ready(function () {
    let coursesTable = $('#Courses').DataTable({
    });
    coursesTable.columns.adjust().draw();
});


//$("#dt-search-0").after(`<i class="bi bi-search text-secondary fs-5 position-relative student" id="data-table-search-icon"></i>`);


//$("#dt-search-0").keyup(function () {
//    if ($("#dt-search-0").val() == "") {
//        $("#data-table-search-icon").removeClass("d-none");
//    }
//    else {
//        $("#data-table-search-icon").addClass("d-none");
//    }
//});