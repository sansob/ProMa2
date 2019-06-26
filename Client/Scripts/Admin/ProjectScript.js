$(document).ready(function () {
    LoadIndexProject();
    LoadStatusProject();
    $('#tableProjects').DataTable({
        "ajax": LoadIndexProject(),
        "paging": true,
        "filter": true,
        "autoWidth": false
    })
});

function LoadIndexProject() {
    $.ajax({
        type: "GET",
        async: false,
        url: "/Projects/LoadProject/",
        dataType: "json",
        success: function (data) {
            console.log(data);
            var html = '';
            var i = 1;
            $.each(data,
                function (index, val) {
                    html += '<tr>';
                    html += '<td>' + i + '</td>';
                    html += '<td>' + val.Project_name + '</td>';
                    html += '<td>' + moment(val.Project_Start).format("MM/DD/YYYY") + '</td>';
                    html += '<td>' + moment(val.Project_Deadline).format("MM/DD/YYYY") + '</td>';
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

function LoadStatusProject() {
    $.ajax({
        type: "GET",
        async: false,
        url: "/Status/GetStatusProject/",
        dataType: "json",
        success: function (data) {
            console.log(data);
            var html = '';
            html += '<option value="" selected disabled hidden>Choose here</option>';
            
            $.each(data,
                function (index, val) {
                    html += ' <option value="'+val.Id+'">'+val.Status_name+'</option>';
                });

            $('#project_status').html(html);
        }
    });
}

function Edit() {
    var project = new Object();
    project.Id = $('#id').val();
    project.Project_Name = $('#project_Name').val();
    project.Project_Start = $('#project_start').val();
    project.Project_Deadline = $('#project_end').val();
    project.Project_Detail = $('#project_description').val();
    project.Status_Id =  $('#project_status').val();
    $.ajax({
        url: '/Projects/InsertOrUpdate/',
        data: project,
        success: function (result) {
            swal({
                    title: "Saved!",
                    text: "That data has been save!",
                    type: "success"
                },
            LoadIndexProject());
            $('#modelAddNew').modal('hide');
           
        }
    });
}

function Save() {
    var project = new Object();
    project.Project_Name = $('#project_Name').val();
    project.Project_Start = $('#project_start').val();
    project.Project_Deadline = $('#project_end').val();
    project.Project_Detail = $('#project_description').val();
    project.Status_Id =  $('#project_status').val();
    
        $.ajax({
            url: '/Projects/InsertOrUpdate/',
            data: project,
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
    ClearScreen();
    
    
}

function GetById(Id) {
    $('#Update').show();
    $('#Save').hide();
    $.ajax({
        url: "/Projects/GetById/",
        data: {Id: Id},
        success: function (result) {
            $('#id').val(result.Id);
            $('#project_Name').val(result.Project_name);
            $('#project_start').val(moment(result.Project_Start).format('MM/DD/YYYY'));
            $('#project_end').val(moment(result.Project_Deadline).format('MM/DD/YYYY'));
            $('#project_description').val(result.Project_Detail);
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
            url: '/Projects/Delete/',
            data: {Id: Id},
            success: function (response) {
                swal({
                        title: "Deleted!",
                        text: "That data has been soft delete!",
                        type: "success"
                    }, 
                    LoadIndexProject());
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
    $('#project_start').val('');
    $('#project_end').val('');
    $('#project_status').val('');
    $('#project_description').val('');
    $('#Update').hide();
}

function Validate() {
    if ($('#project_Name').val() == "" || $('#project_Name').val() == " ") {
        swal("Oops", "Please Insert Project Name", "error")
    } 
    else if ($('#project_start').val() == "" || $('#project_start').val() == " ") {
        swal("Oops", "Please add project start", "error")
    }    
    else if ($('#project_status').val() == "" || $('#project_status').val() == " ") {
        swal("Oops", "Please select project status", "error")
    }
    else if ($('#project_start').val() < moment().format("MM/DD/YYYY") ) {
        swal("Oops", "Start date is lower then today, today is "+moment().format("MM/DD/YYYY"), "error")
    }
    else if ($('#project_end').val() <= moment().format("MM/DD/YYYY") ) {
        swal("Oops", "Minimum project duration is one day. ", "error" )
    }
    
    else if ($('#project_description').val() == "" || $('#project_description').val() == " ") {
        swal("Oops", "Please add project description", "error")
    }
    else if ($('#id').val() == "" || $('#id').val() == " ") {
        Save();
    } else {
        Edit();
    }
}