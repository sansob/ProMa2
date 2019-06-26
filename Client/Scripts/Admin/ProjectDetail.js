$(document).ready(function () {
    var url = window.location.pathname;
    var id = url.substring(url.lastIndexOf('/') + 1);
    GetById(id);
    LoadIndexFileByProject(id);
    LoadIndexTicketByProject(id);
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
                    html += '<td>' + val.Project.Project_name + '</td>';
                    html += '<td>' + val.Status.Status_name + '</td>';
                    html += '<td>' + moment(val.Date).format("MMM Do YY") + '</td>';
                    html += '<td>' + val.Message + '</td>';
                    html += '<td>  <a class="btn btn-outline-success btn-sm" href="'+val.File_url+'" target="_blank" ><i class="os-icon os-icon-ui-51"></i><span>Download</span></a>';

                    html += '</tr>';
                    i++;
                });

            $('#ttickets').html(html);
        }
    });
}

