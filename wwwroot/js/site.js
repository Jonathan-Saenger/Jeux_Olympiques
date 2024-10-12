// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener('DOMContentLoaded', () => {
    const toggleButton = document.getElementById('toggleSearch');
    const searchForm = document.getElementById('searchForm');
    const sportSelect = document.getElementById('entitled');
    const siteSelect = document.getElementById('site');
    const dateInput = document.getElementById('date');
    const events = document.querySelectorAll('.container.my-5');

    toggleButton.addEventListener('click', () => {
        const isHidden = searchForm.style.display === 'none';
        searchForm.style.display = isHidden ? 'block' : 'none';
        toggleButton.textContent = isHidden
            ? 'Masquer le formulaire de recherche'
            : 'Rechercher un évènement selon vos critères';
    });

    const filterEvents = () => {
        const selectedSport = sportSelect.value;
        const selectedSite = siteSelect.value;
        const selectedDate = dateInput.value ? new Date(dateInput.value) : null;

        events.forEach(event => {
            const eventSport = event.querySelector('.text-body-emphasis').innerText.trim();
            const eventSite = event.querySelector('.lead:last-of-type').innerText.trim();
            const eventDateText = event.querySelector('.lead.fw-bold').innerText.trim();

            let eventDate = null;
            if (eventDateText !== "Date non spécifiée") {
                const [day, month, yearAndTime] = eventDateText.split(' ');
                const [year] = yearAndTime.split(',');
                const monthNames = ["jan.", "fév.", "mar.", "avr.", "mai", "juin", "juil.", "août", "sept.", "oct.", "nov.", "déc."];
                const monthIndex = monthNames.indexOf(month.toLowerCase());
                eventDate = new Date(year, monthIndex, parseInt(day));
            }

            const isSportMatch = !selectedSport || eventSport === sportSelect.selectedOptions[0].text;
            const isSiteMatch = !selectedSite || eventSite === siteSelect.selectedOptions[0].text;
            const isDateMatch = !selectedDate || (eventDate && selectedDate.toDateString() === eventDate.toDateString());

            event.style.display = (isSportMatch && isSiteMatch && isDateMatch) ? 'block' : 'none';
        });
    };

    [sportSelect, siteSelect, dateInput].forEach(el => el.addEventListener('change', filterEvents));
});
