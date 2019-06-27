$(document).ready(function () {
    LoadIndexRule();
    $('#tableProjects').DataTable({
        "ajax": LoadIndexRule(),
        "paging": true,
        "filter": true,
        "autoWidth": false
    })
});


function LoadIndexRule() {
    $.ajax({
        type: "GET",
        async: false,
        url: "/Tickets/LoadTicket/",
        dataType: "json",
        success: function (data) {
            console.log(data);
            var html = '';
            var i = 1;
            $.each(data,
                function (index, val) {
                    html += '<tr>';
                    html += '<td>' + i + '</td>';
                    html += '<td>' + val.Project.Project_name + '</td>';
                    html += '<td>' + val.Status.Status_name + '</td>';
                    html += '<td>' + moment(val.Date).format("MMM Do YY") + '</td>';
                    html += '<td>' + val.Message + '</td>';
                    html += '<td>' + '  <a class="btn btn-outline-danger btn-sm" onclick="return Delete(' + val.Id + ')" ><i class="os-icon os-icon-cancel-square"></i><span>Delete</span></a>';
                    html += '</tr>';
                    i++;
                });

            $('.tbody').html(html);
        }
    });
}



function Save() {
    var ticket = new Object();
    ticket.Status_Id = $('#status_name').val();
    ticket.Project_Id = $('#project_name').val();
    ticket.Message = $('#message').val();
    ticket.Date = $('#date').val();

    $.ajax({
        url: '/Tickets/InsertOrUpdate/',
        data: ticket,
        success: function (result) {
            swal({
                title: "Saved!",
                text: "That data has been save!",
                type: "success"
            },
                function () {
                    location.reload();
                });
            LoadIndexProject();
        }
    });
    ClearScreenTicket();
}

function Delete(Id) {
    debugger;
    swal({
        title: "Are you sure?",
        text: "You will not be able to recover this imaginary file!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, delete it!",
        closeOnConfirm: false
    }, function () {
        $.ajax({
            url: '/Tickets/Delete/',
            data: { Id: Id },
            success: function (response) {
                swal({
                    title: "Deleted!",
                    text: "That data has been soft delete!",
                    type: "success"
                },
                    function () {
                        window.location.href = '/Tickets/Index/';
                    });
                ClearScreenTicket();
            },
            error: function (response) {
                swal("Oops", "We couldn't connect to the server!", "error");
            }
        });
    });
}

function GetById(Id) {
    debugger;
    $('#Update').show();
    $('#Save').hide();
    $.ajax({
        url: "/Tickets/GetById/",
        data: { Id: Id },
        success: function (result) {
            $('#id').val(result.Id);
            $('#project_name').val(result.Project.Project_name);
            $('#status_name').val(result.Status.Status_name);            
            $('#date').val(moment(result.Date).format("MMM Do YY"));
            $('#message').val(result.Message);
            $('#modelAddNew').modal('show');
        }
    })
}

function LoadStatusTicket() {
    $.ajax({
        type: "GET",
        async: false,
        url: "/Tickets/GetTicketStatus/",
        dataType: "json",
        success: function (data) {
            console.log(data);
            var html = '';
            html += '<option value="" selected disabled hidden>Choose here</option>';

            $.each(data,
                function (index, val) {
                    html += ' <option value="' + val.Id + '">' + val.Status_name + '</option>';
                });

            $('#status_name').html(html);
        }
    });
}

    

function ClearScreenTicket() {
    $('#Save').show();
    $('#id').val('');
    $('#status_name').val('');
    $('#message').val('');
    $('#date').val('');
    $('#Update').hide();