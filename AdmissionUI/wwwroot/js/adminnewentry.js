
function validEmail(email) {
    const re = /^(([^<>()[\]\\.,;:\s@@"]+(\.[^<>()[\]\\.,;:\s@@"]+)*)|(".+"))@@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

function SubmitButtonOnclick() {

    flag = true;
    if ($.trim($("#CollegeCode").val()) == "") {

        $("#CollegeCode").addClass("input-validation-error");
        $("#select2-CollegeCode-container").addClass("input-validation-error");
        flag = false;
        //$("#FatherName").focus();
    }
    else {
        $("#CollegeCode").removeClass("input-validation-error");
        $("#select2-CollegeCode-container").removeClass("input-validation-error");
    }
    if ($.trim($("#Name").val()) == "") {

        $("#Name").addClass("input-validation-error");
        flag = false;
        //$("#FatherName").focus();
    }
    else {
        $("#Name").removeClass("input-validation-error");
    }
    if ($.trim($("#Roll").val()) == "") {

        $("#Roll").addClass("input-validation-error");
        flag = false;
        //$("#FatherName").focus();
    }
    else {
        $("#Roll").removeClass("input-validation-error");
    }
    if ($.trim($("#FatherName").val()) == "") {

        $("#FatherName").addClass("input-validation-error");
        flag = false;
        //$("#FatherName").focus();
    }
    else {
        $("#FatherName").removeClass("input-validation-error");
    }

    if ($.trim($("#MotherName").val()) == "") {
        $("#MotherName").addClass("input-validation-error");
        flag = false;
        //$("#MotherName").focus();
    }
    else {
        $("#MotherName").removeClass("input-validation-error");
    }
   
    if ($.trim($("#Gender").val()) == "") {
        $("#Gender").addClass("input-validation-error");
        flag = false; //$("#Gender").focus();
    }
    else {
        $("#Gender").removeClass("input-validation-error");
    }

    if ($.trim($("#DOB").val()) == "") {
        $("#DOB").addClass("input-validation-error");
        flag = false;
        //$("#DOB").focus();
    }
    else {
        $("#DOB").removeClass("input-validation-error");
    }
    if ($.trim($("#Email").val()) == "") {
        $("#Email").addClass("input-validation-error");
        flag = false;
        //$("#DOB").focus();
    }
    else {
        $("#Email").removeClass("input-validation-error");
    }

    if ($.trim($("#Email").val()) != "") {
        var email = $.trim($("#Email").val());
        if (!validEmail(email)) {
            $("#Email").addClass("input-validation-error");
            flag = false; //$("#Email").focus();
            alert(flag)
        }
        else {
            $("#Email").removeClass("input-validation-error");
        }
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
    

    if ($.trim($("#Category").val()) == "") {
        $("#Category").addClass("input-validation-error");
        flag = false; //$("#Category").focus();
    }
    else {
        $("#Category").removeClass("input-validation-error");
    }


    //alert($("#PState option:selected").text())
    
    if (flag) {
        var obj = {
            Roll: $("#Roll").val(),
            FatherName: $("#FatherName").val(),
            MotherName: $("#MotherName").val(),
            Email: $("#Email").val(),
            Mobile: $("#Mobile").val(),
            
            Gender: $("#Gender").val(),
            DOBStr: $("#DOB").val(),
           
            Category: $("#Category").val(),
           
            CollegeCode: $("#CollegeCode").val(),
           
        };
     alert(JSON.stringify(obj))
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: '/Home/Step2Profiles',
            data: JSON.stringify(obj),
            dataType: "json",
            beforeSend: function () {
                // setting a timeout
                $('#btnlwait').show();
                $('#btnl').hide();
            },
            success: function (response) {
               

                if (response == 1) {

                    $('#btnlwait').hide();
                    $('#btnl').show();
                    $('#Email').focus();
                    Swal.fire({
                        position: 'center',
                        icon: 'error',
                        title: 'This Email Already Exists!',
                        showConfirmButton: false,
                        timer: 3000
                    })

                }
                if (response == 2) {

                    $('#btnlwait').hide();
                    $('#btnl').show()
                    $('#Mobile').focus();
                    Swal.fire({
                        position: 'center',
                        icon: 'error',
                        title: 'This Mobile Already Exists!',
                        showConfirmButton: false,
                        timer: 3000
                    })
                }
                if (response == 3) {
                    window.location.href = "/admission-form-step3/" + obj.EncrptedRoll; 

                }
            },
            error: function (error) {

            }
        });
    }
}