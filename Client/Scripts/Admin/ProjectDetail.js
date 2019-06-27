$(document).ready(function () {
    var url = window.location.pathname;
    var id = url.substring(url.lastIndexOf('/') + 1);
    GetById(id);
    LoadIndexFileByProject(id);
    LoadIndexTicketByProject(id);
    LoadStatusTicket();
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
    ticket.Project_Id = 77;
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