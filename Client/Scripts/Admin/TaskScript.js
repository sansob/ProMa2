$(document).ready(function () {
    LoadIndexTask();
    LoadStatusTask();
    $('#tableProjects').DataTable({
        "ajax": LoadIndexTask(),
        "paging": true,
        "filter": true,
        "autoWidth": false
    })
});

function LoadIndexTask() {
    $.ajax({
        type: "GET",
        async: false,
        url: "/Tasks/LoadTask/",
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
                    html += '<td>' + val.Description + '</td>';
                    html += '<td>' + moment(val.Start_Date).format("MM/DD/YYYY") + '</td>';
                    html += '<td>' + moment(val.Due_Date).format("MM/DD/YYYY") + '</td>';
                    html += '<td>' + val.Priority + '</td>';
                    html += '<td>' + val.Status.Status_name + '</td>';
                    html += '<td>' +
                        '<a class="btn btn-outline-info btn-sm" onclick="return GetById(' + val.Id + ')" ><i class="os-icon os-icon-edit-1"></i><span>Edit</span></a>';
                    html += '  <a class="btn btn-outline-danger btn-sm" onclick="return Delete(' + val.Id + ')" ><i class="os-icon os-icon-cancel-square"></i><span>Delete</span></a>';

                    html += '</tr>';
                    i++;
                });

            $('.tbody').html(html);
        }
    });
}

function LoadStatusTask() {
    $.ajax({
        type: "GET",
        async: false,
        url: "/Status/GetStatusTask/",
        dataType: "json",
        success: function (data) {
            console.log(data);
            var html = '';

            html += '<option value="" selected disabled hidden>Choose here</option>';

            $.each(data,
                function (index, val) {
                    html += ' <option value="' + val.Id + '">' + val.Status_name + '</option>';
                });

            $('#task_status').html(html);
        }
    });
}

function Edit() {
    var task = new Object();
    task.Id = $('#id').val();
    task.Project_Id = $('#project_Name').val();
    task.Description = $('#task_description').val();
    task.Start_Date = $('#start_date').val();
    task.Due_Date = $('#due_date').val();
    task.Priority = $('#task_priority').val();
    task.Status_Id = $('#task_status').val();
    task.Assigned_By_Member = $('#task_by').val();
    task.Assigned_To_Member = $('#task_to').val();
    $.ajax({
        url: '/Tasks/InsertOrUpdate/',
        data: task,
        success: function (result) {
            swal({
                title: "Saved!",
                text: "That data has been save!",
                type: "success"
            },
                LoadIndexTask());
            $('#modelAddNew').modal('hide');

        }
    });
}

function Save() {
    var task = new Object();
    task.Project_Id = $('#project_Name').val();
    task.Description = $('#task_description').val();
    task.Start_Date = $('#start_date').val();
    task.Due_Date = $('#due_date').val();
    task.Priority = $('#task_priority').val();
    task.Status_Id = $('#task_status').val();
    task.Assigned_By_Member = $('#task_by').val();
    task.Assigned_To_Member = $('#task_to').val();
    $.ajax({
        url: '/Tasks/InsertOrUpdate/',
        data: task,
        success: function (result) {
            swal({
                title: "Saved!",
                text: "That data has been save!",
                type: "success"
            },
                function () {
                    location.reload();
                });
            LoadIndexTask();
        }
    });
    ClearScreen();


}

function GetById(Id) {
    $('#Update').show();
    $('#Save').hide();
    $.ajax({
        url: "/Tasks/GetById/",
        data: { Id: Id },
        success: function (result) {
            $('#id').val(result.Id);
            $('#project_Name').val(result.Project.Project_name);
            $('#task_description').val(result.Description);
            $('#start_date').val(moment(result.Start_Date).format('MM/DD/YYYY'));
            $('#due_date').val(moment(result.Due_Date).format('MM/DD/YYYY'));
            $('#task_priority').val(result.Priority);
            $('#task_status').val(result.Status.Status_name);
            $('#task_by').val(result.Assigned_By_Member);
            $('#task_to').val(result.Assigned_To_Member);
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
            url: '/Tasks/Delete/',
            data: { Id: Id },
            success: function (response) {
                swal({
                    title: "Deleted!",
                    text: "That data has been soft delete!",
                    type: "success"
                },
                    LoadIndexTask());
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
    $('#project_Name').val('');
    $('#task_description').val('');
    $('#start_date').val('');
    $('#due_date').val('');
    $('#task_priority').val('');
    $('#task_status').val('');
    $('#task_by').val('');
    $('#task_to').val('');
    $('#Update').hide();
}

function Validate() {
    if ($('#project_Name').val() == "" || $('#project_Name').val() == " ") {
        swal("Oops", "Please Insert Project Name", "error")
    }
    else if ($('#task_description').val() == "" || $('#task_description').val() == " ") {
        swal("Oops", "Please add Task Description", "error")
    }
    else if ($('#start_date').val() == "" || $('#start_date').val() == " ") {
        swal("Oops", "Please add Start Date", "error")
    }
    else if ($('#start_date').val() < moment().format("MM/DD/YYYY")) {
        swal("Oops", "Start Date is lower then today, today is " + moment().format("MM/DD/YYYY"), "error")
    }
    else if ($('#due_date').val() <= moment().format("MM/DD/YYYY")) {
        swal("Oops", "Minimum task duration is one day. ", "error")
    }
    else if ($('#id').val() == "" || $('#id').val() == " ") {
        Save();
    } else {
        Edit();
    }
}