////Admin Courses Data Table

$('#adminCourses').DataTable({
    layout: {
        topStart: $('<div><h4>Courses List</h4></div>'),
        topEnd: {
            search: {
                placeholder: 'Search...'
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
    scrollX: true,
    scrollY: true,
    columns: [{ width: '20%' }, { width: '20%' }, { width: '20%' }, { width: '20%' }, { width: '20%' }]
});

$("#dt-search-0").before(`<a class="btn btn-primary d-sm-none " id="create-new-course-btn-sm" href="/Admin/CreateCourse">Create New Course</a>`);

$("#dt-search-0").after(`<a class="btn btn-primary  d-none d-sm-inline" id="create-new-course-btn" href="/Admin/CreateCourse">Create New Course</a>`);

var searchContainer = $('<div class="position-relative d-inline-flex me-4"></div>');
var searchIcon = $('<i class="bi bi-search text-secondary fs-5 pe-2 translate-y"></i>');
searchContainer.append(searchIcon);

// Append the container with search box and icon after the specified element
$("#dt-search-0").after(searchContainer);


let adminCoursesTable = $('#adminCourses').DataTable();
$('#main-search-box').keyup(function () {
    adminCoursesTable.search($(this).val()).draw();
});


//// Ajax call
//$('#adminCourses').DataTable({
//    processing: true,
//    serverSide: true,
//    ajax: {
//        url: '/Admin/GetCourses',
//        type: 'Get',
//        data: function (d) {
//            // Additional data to send if needed
//            d.search = $('#main-search-box').val();
//        },
//        dataSrc: 'data',
//        error: function (xhr, errorType, exception) {
//            // Handle Ajax errors here
//            console.error('Ajax error:', xhr.responseText);
//            alert('Error fetching data. Please try again later.');
//        }
//    },
//    columns: [
//        { data: 'Name' },
//        { data: 'Description' },
//        { data: 'NoOfDays' },
//        { data: 'Time' },
//    ],
//    ordering: false,
//    responsive: true,
//    pagingType: 'simple_numbers',
//    pageLength: 5,
//    scrollX: true,
//    scrollY: true,
//    columns: [
//        { width: '20%' },
//        { width: '20%' },
//        { width: '20%' },
//        { width: '20%' }
//    ]
//});

//// Initialize DataTable after defining the function for fetching data
//let adminCoursesTable = $('#adminCourses').DataTable();

//// Handle main search box keyup event
//$('#main-search-box').keyup(function () {
//    adminCoursesTable.search($(this).val()).draw();
//});

//// Optional: If you need to dynamically add buttons or links
//$("#dt-search-0").before(`<a class="btn btn-primary d-sm-none" id="create-new-course-btn-sm" href="/Admin/CreateCourse">Create New Course</a>`);

//$("#dt-search-0").after(`<a class="btn btn-primary d-none d-sm-inline" id="create-new-course-btn" href="/Admin/CreateCourse">Create New Course</a>`);