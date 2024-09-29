// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener('DOMContentLoaded', () => {
    const toggleButton = document.getElementById('toggleSearch');
    const searchForm = document.getElementById('searchForm');

    toggleButton.addEventListener('click', () => {
        const { display } = searchForm.style;
        searchForm.style.display = display === 'none' ? 'block' : 'none';
        toggleButton.textContent = display === 'none'
            ? 'Masquer le formulaire de recherche'
            : 'Rechercher un évènement selon vos critères';
    });
});