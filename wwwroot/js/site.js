$(document).ready(function () {
    $('#search').click(function () {
        var fullName = $('#name').val();
        console.log('success')
        $.get('/Admin/Search', { personName: fullName }, function (result) {
            $('#list').html(result);
        });
    });
});


//function submitBookingForm() {
//    var data = $("#bookingForm").serialize();
//    var token = $('input[name="__RequestVerificationToken"]').val();
//    data += "&__RequestVerificationToken=" + token;

//    console.log(data);  // for debugging purposes
//    alert(data);  // for debugging purposes

//    $.ajax({
//        type: 'POST',
//        url: '/User/UpdateBooking',  // adjust the URL if necessary
//        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
//        data: data,
//        success: function (response) {
//            if (response.success) {
//                alert(response.message);
//                window.location.href = response.redirectUrl;
//            } else {
//                alert(response.message);
//                console.log(response.errors);  // log the errors to the console for debugging
//            }
//        },
//        error: function (xhr, status, error) {
//            alert('Failed to receive the Data');
//            console.log('Failed: ' + status + ' - ' + error);
//            console.log('Response: ' + xhr.responseText);

//            // Additional debugging
//            console.log(xhr.responseJSON);
//            console.log(xhr.status);

//            // Log detailed errors if available
//            if (xhr.responseJSON && xhr.responseJSON.errors) {
//                console.log('Errors: ' + xhr.responseJSON.errors.join(', '));
//            }
//        }
//    });
//}
