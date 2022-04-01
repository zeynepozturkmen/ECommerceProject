
$("#addCampaignForm").submit(function (event) {
    event.preventDefault();

    debugger;

    var formValue = (this);

    swal({
        text: "Are you sure the campaign will be added?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes',
        cancelButtonText: 'No',
        confirmButtonClass: 'btn btn-success',
        cancelButtonClass: 'btn btn-danger',
        buttonsStyling: true
    }).then(function (willDelete) {
        if (willDelete) {

            $.ajax({
                method: "POST",
                url: "/Campaign/CreateCampaign",
                //dataType: 'json',
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                data: new FormData(formValue),
                cache: false,
                contentType: false,
                processData: false,
                beforeSend: function () {

                }
            }).done(function (d) {

                debugger;

                if (d.failed == true) {
                    swal({
                        title: "Error!",
                        text: d.message,
                        type: "error",
                        showCancelButton: false,
                        confirmButtonColor: '#5cb85c',
                        confirmButtonText: 'Ok',
                        cancelButtonText: "No, cancel it!",
                        closeOnConfirm: false,
                        closeOnCancel: false,
                        allowOutsideClick: false
                    });


                }
                else {

                    debugger;

                    swal({
                        title: "Success!",
                        text: d.message,
                        type: "success",
                        showCancelButton: false,
                        confirmButtonColor: '#5cb85c',
                        confirmButtonText: 'Ok',
                        cancelButtonText: "No, cancel it!",
                        closeOnConfirm: false,
                        closeOnCancel: false,
                        allowOutsideClick: false
                    });


                    setInterval(function () { window.location.reload() }, 2000);

                }

            }).fail(function (xhr) {
                debugger;
                if (xhr.status == 403 || xhr.status == 401 || xhr.status == 404) {
                    window.location.href = "/Home/AccessDenied";
                }
                else {
                    toastr.error("Hata oluştu", "Hata");
                }
            }).always(function () {

            });
        }

    });


});