/* ------------------------------------------------------------------------------
*
*  # Datatables API
*
*  Demo JS code for datatable_api.html page
*
* ---------------------------------------------------------------------------- */

document.addEventListener('DOMContentLoaded', function () {
    // Table setup
    // ------------------------------

    // Setting datatable defaults
    $.extend($.fn.dataTable.defaults, {
        lengthChange: true,
        searching: true,
        autoWidth: false,
        columnDefs: [{}],
        order: [],
        dom: '<"datatable-header"fBl><"datatable-scroll-wrap"t><"datatable-footer"ip>',
        language: {
            search: '<span>Filter:</span> _INPUT_',
            searchPlaceholder: 'Type to filter...',
            lengthMenu: '<span>Show:</span> _MENU_',
            info: "Showing _PAGES_ to _PAGE_ of _MAX_ entries",
            paginate: { 'first': 'First', 'last': 'Last', 'next': $('html').attr('dir') == 'rtl' ? '&larr;' : '&rarr;', 'previous': $('html').attr('dir') == 'rtl' ? '&rarr;' : '&larr;' }
        },
        drawCallback: function () {
            $(this).find('tbody tr').slice(-3).find('.dropdown, .btn-group').addClass('dropup');
        },
        preDrawCallback: function () {
            $(this).find('tbody tr').slice(-3).find('.dropdown, .btn-group').removeClass('dropup');
        }
    });
});