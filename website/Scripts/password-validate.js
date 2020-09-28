$(document).ready(function () {
    $('.passwordToValidate').keyup(function () {
        var pswd = $(this).val();

        //validate the length
        if (pswd.length < 8) {
            $('#length').removeClass('password-valid').addClass('password-invalid');
        } else {
            $('#length').removeClass('password-invalid').addClass('password-valid');
        }

        //validate letter
        if (pswd.match(/[a-z]/)) {
            $('#letter').removeClass('password-invalid').addClass('password-valid');
        } else {
            $('#letter').removeClass('password-valid').addClass('password-invalid');
        }

        //validate capital letter
        if (pswd.match(/[A-Z]/)) {
            $('#capital').removeClass('password-invalid').addClass('password-valid');
        } else {
            $('#capital').removeClass('password-valid').addClass('password-invalid');
        }

        //validate number
        if (pswd.match(/\d/)) {
            $('#number').removeClass('password-invalid').addClass('password-valid');
        } else {
            $('#number').removeClass('password-valid').addClass('password-invalid');
        }
    });

    $('.passwordToValidate').focus(function () {
        $('.password-info').show();
    });

    $('.passwordToValidate').blur(function () {
        $('.password-info').hide();
    });
});