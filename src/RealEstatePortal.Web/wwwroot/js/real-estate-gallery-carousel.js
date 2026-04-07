function openGallery(index) {
	var carouselElement = document.getElementById('galleryCarousel');
	var carousel = bootstrap.Carousel.getOrCreateInstance(carouselElement);
	carousel.to(index);

	var myModal = new bootstrap.Modal(document.getElementById('imageGalleryModal'));
	myModal.show();
}