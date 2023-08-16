

function SubmitCButtonOnclick() {

    flag = true;
    if ($.trim($("#CounsellingNo").val()) == "") {

        $("#CounsellingNo").addClass("input-validation-error");
        flag = false;
        //$("#FatherName").focus();
    }
    else {
        $("#CounsellingNo").removeClass("input-validation-error");
    }


    if ($.trim($("#Mobile").val()) == "") {
        $("#Mobile").addClass("input-validation-error");
        flag = false; //$("#Mobile").focus();
    }
    else {
        var number = $.trim($("#Mobile").val());
        if (number.match(/^-?\d+\.?\d*$/) && (number.match(/\d{10}/))) {
            $("#Mobile").removeClass("input-validation-error");
        }
        else {
            flag = false; //$("#Mobile").focus();
            if (!$("#Mobile").hasClass("input-validation-error"))
                $("#Mobile").addClass("input-validation-error");
        }
    }


    if ($.trim($("#CourseId").val()) == "") {
        $("#CourseId").addClass("input-validation-error");
        flag = false; //$("#Category").focus();
    }
    else {
        $("#CourseId").removeClass("input-validation-error");
    }

    if ($.trim($("#Amount").val()) == "" || $.trim($("#Amount").val()) == 0) {
        $("#Amount").addClass("input-validation-error");
        flag = false; //$("#ParmanentAddress").focus();
    }
    else {
        $("#Amount").removeClass("input-validation-error");
    }

    //alert($("#PState option:selected").text())

    if (flag) {
        var obj = {
            CounsellingNo: $("#CounsellingNo").val(),
            Mobile: $("#Mobile").val(),
            CourseId: $("#CourseId").val(),
            Amount: $("#Amount").val(),

        };
        // alert(JSON.stringify(obj))
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: '/Home/CounsellingData',
            data: JSON.stringify(obj),
            dataType: "json",
            beforeSend: function () {
                // setting a timeout
                $('#btnlwait').show();
                $('#btnl').hide();
            },
            success: function (response) {

                if (response == 0) {
                    $('#btnlwait').hide();
                    $('#btnl').show()
                    $('#Mobile').focus();
                    Swal.fire({
                        position: 'center',
                        icon: 'error',
                        title: 'Try Again Later',
                        showConfirmButton: false,
                        timer: 3000
                    })
                }
                else {
                    window.location.href = "/Registration/precRegistration?Mobile=" + obj.Mobile;
                }

            },
            error: function (error) {

            }
        });
    }
}