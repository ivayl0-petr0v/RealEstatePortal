document.addEventListener("DOMContentLoaded", function () {
    const galleryBox = document.getElementById('galleryBox');
    const imageInput = document.getElementById('imageInput');
    const previewContainer = document.getElementById('previewContainer');

    if (galleryBox && imageInput && previewContainer) {

        galleryBox.addEventListener('click', function () {
            imageInput.click();
        });

        imageInput.addEventListener('change', function (e) {
            previewContainer.innerHTML = '';

            Array.from(e.target.files).forEach(file => {
                const reader = new FileReader();
                reader.onload = function (event) {
                    const img = document.createElement('img');
                    img.src = event.target.result;
                    previewContainer.appendChild(img);
                }
                reader.readAsDataURL(file);
            });
        });
    }
});