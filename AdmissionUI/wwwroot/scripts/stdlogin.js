
    document.getElementById("captcha").addEventListener("keydown", function (e) {
        if (!e) { var e = window.event; }
    // e.preventDefault(); // sometimes useful

    // Enter is pressed
    if (e.keyCode == 13) {SubmitLoginForm(); }
    }, false);

    // step-1
    const captcha = new Captcha($('#canvas'), {
        length: 4
    });
    // api


    $('#btnlwait').hide();
    $('#btnl').show();
    function SubmitLoginForm() {
        flag = true;
    if ($.trim($("#Enrollment").val()) == "") {
        $("#Enrollment").addClass("input-validation-error");
    flag = false;
        }
    else {
        $("#Enrollment").removeClass("input-validation-error");
        }
    if ($.trim($("#Dob").val()) == "") {
        $("#Dob").addClass("input-validation-error");
    flag = false;
        }
    else {
        $("#Dob").removeClass("input-validation-error");
        }


    const ans = captcha.valid($('input[name="code"]').val());
    if (ans == false) {
        flag = false;
    $("#captcha").addClass("input-validation-error");
        } else {
        $("#captcha").removeClass("input-validation-error");
        }

    if (flag) {
            var obj = {
        Enrollment: $("#Enrollment").val(),
    Dob: $("#Dob").val(),
            };
    //alert(JSON.stringify(obj))
    $.ajax({
        type: "POST",
    contentType: "application/json; charset=utf-8",
    url: '@Url.Action("Login", "Home")',
    data: JSON.stringify(obj),
    dataType: "json",
    beforeSend: function () {
        // setting a timeout
        $('#btnlwait').show();
    $('#btnl').hide();
                },
    success: function (response) {

                    if (response.res == "success") {
                        var d = JSON.stringify(response.data);
    var d = response.data;
    var name = "";
    var EncrptedRoll = "";
    $.each(d, function (index, d) {
        name = response.data.Name;
    EncrptedRoll = response.data.EncrptedRoll;
    window.location.href = "/admission-form-step1/" + EncrptedRoll;

                        });

    $("#Enrollment").val("");

                    }
    else {

        Swal.fire({
            position: 'center',
            icon: 'error',
            title: 'Invalid Login Details!',
            showConfirmButton: false,
            timer: 3000
        })
    }
                },
    error: function (result) {
        alert('Service call failed: ' + result.status + ' Type :' + result.statusText);
                },
    complete: function () {
        setTimeout(function () {
            $('#btnlwait').hide();
            $('#btnl').show();
        }, 1000);
                },
            });
        }
    }