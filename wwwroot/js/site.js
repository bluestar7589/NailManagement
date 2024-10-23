// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// JavaScript for index page

function showDetails(appointmentId) {
    const appointment = appointments.find(app => app.appointmentID === appointmentId);
    if (appointment) {
        const appointmentDate = new Date(appointment.appointmentDate);
        const formattedDate = appointmentDate.toLocaleDateString();
        const formattedTime = appointmentDate.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
        const formattedPrice = parseFloat(appointment.price).toLocaleString('en-US', { style: 'currency', currency: 'USD' });

        document.getElementById('modalCustomerName').innerText = appointment.customerName;
        document.getElementById('modalTechnicianName').innerText = appointment.technicianName;
        document.getElementById('modalAppointmentDate').innerText = `${formattedDate} ${formattedTime}`;
        document.getElementById('modalService').innerText = appointment.service;
        document.getElementById('modalPrice').innerText = formattedPrice;

        $('#detailsModal').modal('show');
    }
}

