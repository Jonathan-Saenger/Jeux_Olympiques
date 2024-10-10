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
        const { display } = searchForm.style;
        searchForm.style.display = display === 'none' ? 'block' : 'none';
        toggleButton.textContent = display === 'none'
            ? 'Masquer le formulaire de recherche'
            : 'Rechercher un évènement selon vos critères';
    });

    const filterEvents = () => {
        const selectedSport = sportSelect.value;
        const selectedSite = siteSelect.value;
        const selectedDate = dateInput.value;

        events.forEach(event => {
            const eventSport = event.querySelector('.text-body-emphasis').innerText.trim();
            const eventSite = event.querySelector('.lead:last-of-type').innerText.trim();
            const eventDateText = event.querySelector('.lead.fw-bold').innerText.trim();

            let eventDate = null;
            if (eventDateText !== "Date non spécifiée") {
                const [day, month, yearAndTime] = eventDateText.split(' ');
                const [year, time] = yearAndTime.split(',');
                const [hours, minutes] = time.trim().split(':');
                const monthNames = ["jan.", "fév.", "mar.", "avr.", "mai", "juin", "juil.", "août", "sept.", "oct.", "nov.", "déc."];
                const monthIndex = monthNames.findIndex(m => m.toLowerCase() === month.toLowerCase());

                eventDate = new Date(parseInt(year), monthIndex, parseInt(day));
            }

            const isSportMatch = selectedSport === '' || eventSport === sportSelect.options[sportSelect.selectedIndex].text;
            const isSiteMatch = selectedSite === '' || eventSite === siteSelect.options[siteSelect.selectedIndex].text;
            let isDateMatch = true;

            if (selectedDate && eventDate) {
                const selectedDateObj = new Date(selectedDate);
                isDateMatch = selectedDateObj.toDateString() === eventDate.toDateString();
            } else if (selectedDate && !eventDate) {
                isDateMatch = false;
            }
            event.style.display = (isSportMatch && isSiteMatch && isDateMatch) ? 'block' : 'none';
        });
    };

    sportSelect.addEventListener('change', filterEvents);
    siteSelect.addEventListener('change', filterEvents);
    dateInput.addEventListener('input', filterEvents);
});