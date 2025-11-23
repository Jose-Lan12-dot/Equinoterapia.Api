window.inicializarFullCalendar = (elementId, events) => {
    if (!window.FullCalendar) {
        console.error("FullCalendar no está definido aún. Revisa que la librería se haya cargado correctamente.");
        return;
    }

    var calendarEl = document.getElementById(elementId);
    if (!calendarEl) return;

    // Si ya existe un calendario previo, destrúyelo
    if (calendarEl._fullCalendarInstance) {
        calendarEl._fullCalendarInstance.destroy();
    }

    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'dayGridMonth',
        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay'
        },
        events: events,
        eventColor: '#378006' // color por defecto, luego puedes cambiar según instructor
    });

    calendar.render();
    calendarEl._fullCalendarInstance = calendar;
};
