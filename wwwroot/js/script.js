const navbar = document.querySelector('.navbar');

// Check if navbar exists
if (navbar) {
    // Add an event listener for scrolling
    window.addEventListener('scroll', () => {
        if (window.scrollY > 50) { // Change to 50px to trigger the effect slightly below the top
            // Add solid background when not at the top
            navbar.classList.add('solid');
            navbar.classList.remove('transparent');
        } else {
            // Transparent background when at the top
            navbar.classList.add('transparent');
            navbar.classList.remove('solid');
        }
    });

    // Initial state: Transparent navbar
    navbar.classList.add('transparent');
}