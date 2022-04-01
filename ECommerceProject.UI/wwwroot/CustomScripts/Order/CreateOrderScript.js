

function createOrder(productOrCampaignId, isCampaign) {

    debugger;

    var quantity = $('#Quantity_' + productOrCampaignId).val();
    if (quantity == '' || quantity == null) {
        swal({
            title: "Error!",
            text: "Please enter amount",
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

        swal({
            text: "Are you sure your order will be created?",
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
                    url: "/Order/CreateOrder",
                    //dataType: 'json',
                    data: {
                        Id: productOrCampaignId,
                        Quantity: quantity,
                        IsCampaign: isCampaign
                    },
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
    }
}