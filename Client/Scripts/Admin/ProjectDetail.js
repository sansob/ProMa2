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
    LoadIndexProjectMember(id);
});

function OpenUploader(){

    client = filestack.init("Aed8KWVgnRIGKmJSkSZoQz");
    options = {
        "fromSources": ["local_file_system"],
        "concurrency": 1,
        "onUploadDone": function (result) {
            var url = window.location.pathname;
            var id = url.substring(url.lastIndexOf('/') + 1);
            console.log(result),
                console.log(result.filesUploaded[0].filename);
            console.log(result.filesUploaded[0].url);
            Save(result.filesUploaded[0].filename, result.filesUploaded[0].url, id);
            swal({
                "title": "Saved!",
                "text": "File have been uploaded!",
                "type": "success"
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
        "url": '/File/InsertOrUpdate/',
        "data": file,
        "success": function (result) {
            swal({
                    "title": "Saved!",
                    "text": "That data has been save!",
                    "type": "success"
                },
                function () {
                    location.reload();
                });
            LoadIndexFileByProject();
        }
    });

}

function GetById(Id) {
    $.ajax({
        "url": "/Projects/GetById/",
        "data": {"Id": Id},
        "success": function (result) {
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
function LoadIndexFileByProject(id) {
    $.ajax({
        "type": "GET",
        "async": false,
        "url": "/File/LoadFilesFromProject/"+id,
        "dataType": "json",
        "success": function (data) {
            console.log(data);
            var html = '';
            var i = 1;
            $.each(data,
                function (index, val) {
                    html += '<tr>';
                    html += '<td>' + i + '</td>';
                    html += '<td>' + val.File_name + '</td>';
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
        "type": "GET",
        "async": false,
        "url": "/Tickets/LoadTicketFromProject/"+id,
        "dataType": "json",
        "success": function (data) {
            console.log(data);
            var html = '';
            $.each(data,
                function (index, val) {
                    html += '<div class="card p-3">\n' +
                        '    <blockquote class="blockquote mb-0 card-body">\n' +
                        '      <p>'+val.Message+'</p>\n' +
                        '      <footer class="blockquote-footer">\n' +
                        '        <small class="text-muted">\n' +
                        '          Sent on <cite title="Source Title">'+moment(val.CreateDate).format("MM/DD/YYYY")+'</cite>\n' +
                        '        </small>\n' +
                        '      </footer>\n' +
                        '    </blockquote>\n' +
                        '  </div><br>';

                });

            $('#ticketView').html(html);
        }
    });
}


function GetReply(tic_id){
    alert(tic_id);
}
function LoadStatusTicket() {
    $.ajax({
        "type": "GET",
        "async": false,
        "url": "/Status/GetTicketStatus/",
        "dataType": "json",
        "success": function (data) {
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
    var url = window.location.pathname;
    var id = url.substring(url.lastIndexOf('/') + 1);
    var ticket = new Object();
    ticket.FromMember_Id = 1;
    ticket.Status_Id = $('#status_name').val();
    ticket.Project_Id = id;
    ticket.Message = $('#message').val();
    $.ajax({
        "url": '/Tickets/InsertOrUpdate/',
        "data": ticket,
        "success": function (result) {
            swal({
                    "title": "Saved!",
                    "text": "That data has been save!",
                    "type": "success"
                },
                function () {
                    location.reload();
                });
            LoadIndexTicketByProject();
        }
    });
}


function LoadIndexProjectMember(id) {
    $.ajax({
        type: "GET",
        async: false,
        url: "/ProjectMembers/LoadProjectMemberFromProjectId/"+id,
        success: function (data) {
            console.log(data);
            var html = '';
            var i = 1;
            $.each(data,
                function (index, val) {
                    html += '<tr>';
                    html += '<td>' + i + '</td>';
                    html += '<td>' + val.User_Name + '</td>';
                    html += '<td>' + val.Rule.Rule_Name + '</td>';
                    html += '</tr>';
                    i++;
                });

            $('#tbodyprojectmember').html(html);
        }
    });
}

//INI KHUSUS TASK kebawah
function LoadIndexTaskByProject(id) {
    $.ajax({
        type: "GET",
        async: false,
        url: "/Tasks/LoadTaskFromProject/"+id,
        dataType: "json",
        success: function (data) {
            console.log(data);
            var html = '';
            var i = 1;
            $.each(data,
                function (index, val) {
                    html += '<tr>';
                    html += '<td>' + i + '</td>';
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
    var url = window.location.pathname;
    var id = url.substring(url.lastIndexOf('/') + 1);
    var task = new Object();
    task.Id = $('#id_task').val();
    task.Project_Id = id;
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
    ClearScreenTask();
}
function GetByIdTask(Id) {
    $('#UpdateTask').show();
    $('#SaveTask').hide();
    $.ajax({
        url: "/Tasks/GetById/",
        data: { Id: Id },
        success: function (result) {
            $('#id_task').val(result.Id);
            $('#task_description').val(result.Description);
            $('#start_date').val(moment(result.Start_Date).format('MM/DD/YYYY'));
            $('#due_date').val(moment(result.Due_Date).format('MM/DD/YYYY'));
            $('#task_priority').val(result.Priority);
            $('#task_by').val(result.Assigned_By_Member);
            $('#task_to').val(result.Assigned_To_Member);
            $('#status_task').val(result.Status_Id);
            $('#createtask').modal('show');
        }
    })
}
function SaveTask() {
    debugger;
    var url = window.location.pathname;
    var id = url.substring(url.lastIndexOf('/') + 1);
    var task = new Object();
    task.Project_Id = id;
    task.Description = $('#task_description').val();
    task.Start_Date = $('#start_date').val();
    task.Due_Date = $('#due_date').val();
    task.Priority = 1;
    task.Status_Id = $('#status_task').val();
    task.Assigned_By_Member = 1;
    task.Assigned_To_Member = 1;
    $.ajax({
        url: '/Tasks/InsertOrUpdate',
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
    ClearScreenTask();
}
function ValidateTask() {

    if ($('#task_description').val() == "" || $('#task_description').val() == " ") {
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
    else if ($('#id_task').val() == "" || $('#id_task').val() == " ") {
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

function LoadProjectTask() {
    $.ajax({
        type: "GET",
        async: false,
        url: "/Projects/GetProjectTask/",
        dataType: "json",
        success: function (data) {
            console.log(data);
            var html = '';

            html += '<option value="" selected disabled hidden>Choose here</option>';

            $.each(data,
                function (index, val) {
                    html += ' <option value="' + val.Id + '">' + val.Project_name + '</option>';
                });

            $('#project_task').html(html);
        }
    });
}

function ClearScreenTask() {
    $('#SaveTask').show();
    $('#id_task').val('');
    $('#project_Name').val('');
    $('#task_description').val('');
    $('#start_date').val('');
    $('#due_date').val('');
    $('#task_priority').val('');
    $('#task_status').val('');
    $('#task_by').val('');
    $('#task_to').val('');
    $('#UpdateTask').hide();
}