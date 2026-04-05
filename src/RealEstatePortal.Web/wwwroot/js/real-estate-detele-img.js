document.addEventListener('click', function (e) {
    const btn = e.target.closest('.remove-existing-img');

    if (btn) {
        e.preventDefault();

        const imgUrl = btn.getAttribute('data-url');
        const wrapper = btn.closest('.existing-image-wrapper');
        const form = btn.closest('form');

        if (form && wrapper) {
            const hiddenInput = document.createElement('input');
            hiddenInput.type = 'hidden';
            hiddenInput.name = 'RemovedImageUrls';
            hiddenInput.value = imgUrl;

            form.appendChild(hiddenInput);

            wrapper.remove();
        }
    }
});