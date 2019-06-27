$(document).ready(function () {
    var url = window.location.pathname;
    var id = url.substring(url.lastIndexOf('/') + 1);
    GetById(id);
    LoadIndexTaskByProject(id);
    LoadStatusTask();
    LoadProjectTask();
    LoadIndexFileByProject(id);
    LoadIndexTicketByProject(id);
    LoadStatusTicket();
    console.log(id);
    LoadIndexProjectMember();
});

function OpenUploader(){
    
    client = filestack.init("Aed8KWVgnRIGKmJSkSZoQz");
    options = {
        fromSources: ["local_file_system"],
        concurrency: 1,
        onUploadDone: function (result) {
            var url = window.location.pathname;
            var id = url.substring(url.lastIndexOf('/') + 1);
            console.log(result),
            console.log(result.filesUploaded[0].filename);
            console.log(result.filesUploaded[0].url);
            Save(result.filesUploaded[0].filename, result.filesUploaded[0].url, id);
            swal({
                    title: "Saved!",
                    text: "File have been uploaded!",
                    type: "success"
                });
            LoadIndexFileByProject(id);
        }
    };
    client.picker(options).open();
}

function Save(filename, fileurl, projectid) {
    var file = new Object();
    file.File_name = filename;
    file.File_url = fileurl;
    file.Project_Id = projectid;
    file.File_uploaderId = 1;
    $.ajax({
        url: '/File/InsertOrUpdate/',
        data: file,
        success: function (result) {
            swal({
                    title: "Saved!",
                    text: "That data has been save!",
                    type: "success"
                },
                LoadIndexFileByProject(id))
        }
    });

}

function GetById(Id) {
    $.ajax({
        url: "/Projects/GetById/",
        data: {Id: Id},
        success: function (result) {
            var projectStart = moment(result.Project_Deadline).format('MM/DD/YYYY');
            $('#id').val(result.Id);
            $('#project_Title').text(result.Project_name);
            $('#project_description').text(result.Project_Detail);
            $('#project_start').text(moment(result.Project_Start).format('MM/DD/YYYY'));
            $('#project_end').text(moment(result.Project_Deadline).format('MM/DD/YYYY'));
            $('#project_running').text(moment(projectStart, "MM/DD/YYYY").fromNow());
            $('#project_status').text(result.Status.Status_name);
        }
    })
}

//INI KHUSUS TASK kebawah
function LoadIndexTaskByProject(id) {
    $.ajax({
        type: "GET",
        async: false,
        url: "/Tasks/LoadTaskFromProject/" + id,
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
                    //html += '<td>' + val.Priority + '</td>';
                    html += '<td>' + val.Status.Status_name + '</td>';
                    //html += '<td>' + val.Assigned_By_Member + '</td>';
                    //html += '<td>' + val.Assigned_To_Member + '</td>';
                    html += '<td>' +
                        '<a class="btn btn-outline-info btn-sm" onclick="return GetByIdTask(' + val.Id + ')" ><i class="os-icon os-icon-edit-1"></i><span>Edit</span></a>';
                    html += '  <a class="btn btn-outline-danger btn-sm" onclick="return Delete(' + val.Id + ')" ><i class="os-icon os-icon-cancel-square"></i><span>Delete</span></a>';

                    html += '</tr>';
                    i++;
                });

            $('#ttask').html(html);
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

            $('#status_task').html(html);
        }
    });
}
function EditTask() {
    var task = new Object();
    task.Id = $('#id').val();
    task.Project_Id = val.Id;
    task.Description = $('#task_description').val();
    task.Start_Date = $('#start_date').val();
    task.Due_Date = $('#due_date').val();
    task.Priority = 1;
    task.Status_Id = $('#status_task').val();
    task.Assigned_By_Member = 1;
    task.Assigned_To_Member = 1;
    $.ajax({
        url: '/Tasks/InsertOrUpdate/',
        data: task,
        success: function (result) {
            swal({
                title: "Saved!",
                text: "That data has been save!",
                type: "success"
            },
                LoadIndexTaskByProject(id));
            $('#createtask').modal('hide');
        }
    });
}
function GetByIdTask(Id) {
    $('#UpdateTask').show();
    $('#SaveTask').hide();
    $.ajax({
        url: "/Tasks/GetById/",
        data: { Id: Id },
        success: function (result) {
            $('#id').val(result.Id);
            $('#task_description').val(result.Description);
            $('#start_date').val(moment(result.Start_Date).format('MM/DD/YYYY'));
            $('#due_date').val(moment(result.Due_Date).format('MM/DD/YYYY'));
            $('#task_priority').val(result.Priority);
            $('#task_by').val(result.Assigned_By_Member);
            $('#task_to').val(result.Assigned_To_Member);
            $('#modelAddNew').modal('show');
        }
    })
}
function SaveTask() {
    var task = new Object();
    task.Project_Id = val.Id;
    task.Description = $('#task_description').val();
    task.Start_Date = $('#start_date').val();
    task.Due_Date = $('#due_date').val();
    task.Priority = 1;
    task.Status_Id = $('#status_task').val();
    task.Assigned_By_Member = 1;
    task.Assigned_To_Member = 1;
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
            LoadIndexTaskByProject(id);
        }
    });
    ClearScreen();
}
function ValidateTask() {
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
        SaveTask();
    } else {
        EditTask();
    }
}
function Delete(Id) {
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
                    function () {
                        location.reload();
                    });
            },
            error: function (response) {
                swal("Oops", "We couldn't connect to the server!", "error");
            }
        });
    });
}
//INI KHUSUS TASK keatas

function LoadIndexFileByProject(id) {
    $.ajax({
        type: "GET",
        async: false,
        url: "/File/LoadFilesFromProject/"+id,
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
                    html += '<td>' + val.File_uploaderId + '</td>';
                    html += '<td>' + val.Project.Project_name + '</td>';
                    html += '<td>' + moment(val.CreateDate).format("MM/DD/YYYY") + '</td>';
                    html += '<td>  <a class="btn btn-outline-success btn-sm" href="'+val.File_url+'" target="_blank" ><i class="os-icon os-icon-ui-51"></i><span>Download</span></a>';

                    html += '</tr>';
                    i++;
                });

            $('#tfiles').html(html);
        }
    });
}

function LoadIndexTicketByProject(id) {
    $.ajax({
        type: "GET",
        async: false,
        url: "/Tickets/LoadTicketFromProject/"+id,
        dataType: "json",
        success: function (data) {
            console.log(data);
            var html = '';
            var i = 1;
            $.each(data,
                function (index, val) {
                    html += '<tr>';
                    html += '<td>' + i + '</td>';
                    html += '<td>' + val.Status.Status_name + '</td>';
                    html += '<td>' + moment(val.Date).format("MMM Do YY") + '</td>';
                    html += '<td>' + val.Message + '</td>';
                    html += '<td>' +
                        '<a class="btn btn-outline-info btn-sm" onclick="return GetById(' + val.Id + ')" ><i class="os-icon os-icon-edit-1"></i><span>Edit</span></a>';
                    html += '  <a class="btn btn-outline-danger btn-sm" onclick="return Delete(' + val.Id + ')" ><i class="os-icon os-icon-cancel-square"></i><span>Delete</span></a>';

                    html += '</tr>';
                    i++;
                });

            $('#ttickets').html(html);
        }
    });
}

function LoadStatusTicket() {
    $.ajax({
        type: "GET",
        async: false,
        url: "/Status/GetTicketStatus/",
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

function SaveTickets() {
    var ticket = new Object();
    ticket.FromMember_Id = 1;
    ticket.Status_Id = $('#status_name').val();
    ticket.Project_Id = 1;
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
                
            LoadIndexTicketByProject();
        }
    });
}

function GetById(Id) {
    $('#Update').show();
    $('#Save').hide();
    $.ajax({
        url: "/Status/GetById/",
        data: { Id: Id },
        success: function (result) {
            $('#id').val(result.Id);
            $('#status_name').val(result.Status_name);
            $('#status_module').val(result.Status_module);
            $('#modelAddNew').modal('show');
        }
    })
}


function LoadIndexProjectMember() {
    $.ajax({
        type: "GET",
        async: false,
        url: "/ProjectMembers/LoadProjectMember/",
        success: function (data) {
            console.log(data);
            var html = '';
            var i = 1;
            $.each(data,
                function (index, val) {
                    html += '<tr>';
                    html += '<td>' + i + '</td>';
                    html += '<td>' + val.Project.Project_name + '</td>';
                    html += '<td>' + val.User_Name + '</td>';
                    html += '<td>' + val.Rule.Rule_Name + '</td>';                   
                    html += '</tr>';
                    i++;
                });

            $('#tbodyprojectmember').html(html);
        }
    });
}

