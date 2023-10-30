$(document).ready(function () {
    $('#avatarInput').on('change', function (event) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#avatarPreview').attr('src', e.target.result);
            $('#uploadLabel').hide(); // Hide the Upload message
            $('#resetButton').show(); // Show the reset button
            $('#previewMessage').show(); // Show the preview message
            $('#saveButton').prop('disabled', false); // Enable the upload button
        };

        if (this.files && this.files[0]) {
            reader.readAsDataURL(event.target.files[0]);
        } else {
            resetAvatarToDefault();
        }
    });

    $('#resetButton').on('click', function () {
        resetAvatarToDefault();
        // Reset the input value so the file is not uploaded when the form is submitted
        $('#avatarInput').val('');
    });

    function resetAvatarToDefault() {
        $('#avatarPreview').attr('src', $('#originalAvatarUrl').val()); // Revert to original image URL
        $('#uploadLabel').show(); // Show the Upload message
        $('#previewMessage').hide(); // Hide the preview message
        $('#saveButton').prop('disabled', true); // Keep the button disabled
        $('#resetButton').hide(); // Hide the reset button
    }
});