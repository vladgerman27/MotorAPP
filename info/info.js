function handleNextButtonClick() {
    const slideWidth = document.querySelector('.slide').clientWidth;
    const slidesContainer = document.getElementById('slides-container');
    slidesContainer.scrollLeft += slideWidth;
  }
  
  function handlePrevButtonClick() {
    const slideWidth = document.querySelector('.slide').clientWidth;
    const slidesContainer = document.getElementById('slides-container');
    slidesContainer.scrollLeft -= slideWidth;
  }