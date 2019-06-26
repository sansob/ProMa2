$(document).ready(function () {
    LoadIndexFile();
    $('#tableProjects').DataTable({
        "ajax": LoadIndexFile(),
        "paging": true,
        "filter": true,
        "autoWidth": false
    })
});

function LoadIndexFile() {
    $.ajax({
        type: "GET",
        async: false,
        url: "/File/LoadFiles/",
        dataType: "json",
        success: function (data) {
            console.log(data);
            var html = '';
            var i = 1;
            $.each(data,
                function (index, val) {
                    html += '<tr>';
                    html += '<td>' + i + '</td>';
                    html += '<td>' + val.File_name + '</td>';
                    html += '<td>' + val.File_url + '</td>';
                    html += '<td>' + val.File_uploaderId + '</td>';
                    html += '<td>' + val.Project.Project_name + '</td>';                     
                    html += '<td>  <a class="btn btn-outline-danger btn-sm" onclick="return Delete(' + val.Id + ')" ><i class="os-icon os-icon-cancel-square"></i><span>Delete</span></a>';

                    html += '</tr>';
                    i++;
                });

            $('.tbody').html(html);
        }
    });
}

function Edit() {
    var status = new Object();
    status.Id = $('#id').val();
    status.Status_name = $('#status_name').val();
    status.Status_module = $('#status_module').val();
    $.ajax({
        url: '/Status/InsertOrUpdate/',
        data: status,
        success: function (result) {
            swal({
                    title: "Saved!",
                    text: "That data has been save!",
                    type: "success"
                },
                LoadIndexFile());
            $('#modelAddNew').modal('hide');

        }
    });
}

function Save() {
    var status = new Object();
    status.Status_name = $('#status_name').val();
    status.Status_module = $('#status_module').val();

    $.ajax({
        url: '/Status/InsertOrUpdate/',
        data: status,
        success: function (result) {
            swal({
                    title: "Saved!",
                    text: "That data has been save!",
                    type: "success"
                },
                function () {
                    window.location.href = '/Status/';
                });
            $('#modelAddNew').modal('hide');
        }
    });
    ClearScreen();


}

function GetById(Id) {
    $('#Update').show();
    $('#Save').hide();
    $.ajax({
        url: "/Status/GetById/",
        data: {Id: Id},
        success: function (result) {
            $('#id').val(result.Id);
            $('#status_name').val(result.Status_name);
            $('#status_module').val(result.Status_module);
            $('#modelAddNew').modal('show');
        }
    })
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
            url: '/File/Delete/',
            data: {Id: Id},
            success: function (response) {
                swal({
                        title: "Deleted!",
                        text: "That data has been soft delete!",
                        type: "success"
                    },
                    LoadIndexFile());
                ClearScreen();
            },
            error: function (response) {
                swal("Oops", "We couldn't connect to the server!", "error");
            }
        });
    });
}

function ClearScreen() {
    $('#Save').show();
    $('#id').val('');
    $('#status_name').val('');
    $('#status_module').val('');
    $('#Update').hide();
}

function Validate() {
    if ($('#status_name').val() == "" || $('#status_name').val() == " ") {
        swal("Oops", "Please Insert Project Name", "error")
    }
    else if ($('#status_module').val() == "" || $('#status_module').val() == " ") {
        swal("Oops", "Please add project start", "error")
    }
    else if ($('#id').val() == "" || $('#id').val() == " ") {
        Save();
    }  else {
        Edit();
    }
}