$(document).ready(function () {
    let n = 2; // Initial count for dynamically added date-time controls

    $("#add-date-time-controls").on('click', function () {
        let dateId = "dt-" + n;
        n++;

        $("#date-time-container").append(`
            <div class="d-flex align-items-center col-12 row mt-2" id="` + dateId + `">
                <div class="mb-3 col-5">
                    <label class="form-label" for="Date">Date</label>
                    <input class="form-control w-100 shadow-none" data-val="true" data-val-required="Start date of the course is missing" id="` + dateId + `-Date" name="Date" required="true" type="date" value="">
                    <span class="field-validation-valid" data-valmsg-for="Date" data-valmsg-replace="true"></span>
                </div>
                <div class="mb-3 col-5">
                    <label class="form-label" for="Time">Time</label>
                    <input class="form-control w-100 shadow-none" data-val="true" data-val-required="Start time of the course is missing" id="` + dateId + `-Time" name="Time" required="true" type="time" value="">
                    <span class="field-validation-valid" data-valmsg-for="Time" data-valmsg-replace="true"></span>
                </div>
                <button type="button" class="btn col-2 mt-1" onclick="removeDateTime('` + dateId + `')"><i class="bi bi-trash-fill fs-5 "></i></button>
            </div>`);
    });

    $('#createCourseForm').submit(function (event) {
        event.preventDefault(); // Prevent normal form submission

        var formData = $(this).serialize(); 

        $.ajax({
            url: '@Url.Action("SaveCourse", "Admin")', 
            type: 'POST',
            dataType: 'json', 
            data: formData,
            success: function (response) {
                // Handle success response
                window.location.href = '@Url.Action("Courses", "Admin")';
            },
            error: function (xhr, status, error) {
                // Handle error
                console.error(xhr.responseText);
                alert('An error occurred while saving the course.');
            }
        });
    });
});

function removeDateTime(dateTimeId) {
    $("#" + dateTimeId).remove();
}
