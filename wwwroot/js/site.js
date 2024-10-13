// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(() => {
    const $toggleButton = $('#toggleSearch');
    const $searchForm = $('#searchForm');
    const $sportSelect = $('#entitled');
    const $siteSelect = $('#site');
    const $dateInput = $('#date');
    const $events = $('.container.my-5');

    $toggleButton.click(() => {
        const isHidden = $searchForm.css('display') === 'none';
        $searchForm.css('display', isHidden ? 'block' : 'none');
        $toggleButton.text(isHidden ? 'Masquer le formulaire de recherche' : 'Rechercher un évènement selon vos critères');
    });

    const filterEvents = () => {
        const selectedSport = $sportSelect.val();
        const selectedSite = $siteSelect.val();
        const selectedDate = $dateInput.val() ? new Date($dateInput.val()) : null;

        $events.each(function () {
            const $event = $(this);
            const eventSport = $event.find('.text-body-emphasis').text().trim();
            const eventSite = $event.find('.lead:last-of-type').text().trim();
            const eventDateText = $event.find('.lead.fw-bold').text().trim();
            let eventDate = null;

            if (eventDateText !== "Date non spécifiée") {
                const [day, month, yearAndTime] = eventDateText.split(' ');
                const [year] = yearAndTime.split(',');
                const monthNames = ["jan.", "fév.", "mar.", "avr.", "mai", "juin", "juil.", "août", "sept.", "oct.", "nov.", "déc."];
                const monthIndex = monthNames.indexOf(month.toLowerCase());
                eventDate = new Date(year, monthIndex, parseInt(day));
            }

            const isSportMatch = !selectedSport || eventSport === $sportSelect.find('option:selected').text();
            const isSiteMatch = !selectedSite || eventSite === $siteSelect.find('option:selected').text();
            const isDateMatch = !selectedDate || (eventDate && selectedDate.toDateString() === eventDate.toDateString());

            $event.css('display', (isSportMatch && isSiteMatch && isDateMatch) ? 'block' : 'none');
        });
    };

    $sportSelect.change(filterEvents);
    $siteSelect.change(filterEvents);
    $dateInput.on('input', filterEvents);
});

$(document).ready(() => {
    const formatCardNumber = (e) => {
        const input = e.target.value.replace(/\D/g, '').substring(0, 16);
        $(e.target).val(input.match(/.{1,4}/g)?.join('-') || '');
    };

    const formatExpirationDate = (e) => {
        const input = e.target.value.replace(/\D/g, '').substring(0, 4);
        $(e.target).val(input.length >= 2 ? `${input.substring(0, 2)}/${input.substring(2, 4)}` : input);
    };

    const validateForms = () => {
        const $forms = $('.needs-validation');
        $forms.each(function () {
            $(this).on('submit', (event) => {
                if (!this.checkValidity()) {
                    event.preventDefault();
                    event.stopPropagation();
                }
                $(this).addClass('was-validated');
            });
        });
    };

    $('#cardNumber').on('input', formatCardNumber);
    $('#expirationDate').on('input', formatExpirationDate);

    validateForms();
});
