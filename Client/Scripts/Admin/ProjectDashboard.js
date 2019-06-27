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
                        '<a class="btn btn-outline-success btn-sm" onclick="return GoToProject(' + val.Id + ')" ><i class="os-icon os-icon-window-content"></i><span>Detail</span></a>';

                    html += '</tr>';
                    i++;
                });

            $('.tbody').html(html);
        }
    });
}

function GoToProject(Id){
    var url= "/ProjectDetail/Detail/"+Id;
    window.location = url;
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
            $('#project_status').val(result.Status_Id);
            console.log(result.Status_Id);
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
    $('#project_status').val('');
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
    else if ($('#project_end').val() <= ($('#project_start').val()) ) {
        swal("Oops", "Minimum project duration is one day. ", "error" )
    }

    else if ($('#project_description').val() == "" || $('#project_description').val() == " ") {
        swal("Oops", "Please add project description", "error")
    }

    else if ($('#project_status option:selected').val() == "" || $('#project_status option:selected').val() == " ") {
        swal("Oops", "Empty project status", "error")
    }

    else if ($('#id').val() == "" || $('#id').val() == " ") {
        Save();
    } else {
        Edit();
    }
}

function ValidateEdit() {
    if ($('#project_Name').val() == "" || $('#project_Name').val() == " ") {
        swal("Oops", "Please Insert Project Name", "error")
    }
    else if ($('#project_start').val() == "" || $('#project_start').val() == " ") {
        swal("Oops", "Please add project start", "error")
    }
    else if ($('#project_status').val() == "" || $('#project_status').val() == " ") {
        swal("Oops", "Please select project status", "error")
    }
    else if ($('#project_start').val() == "" || $('#project_start').val() ) {
        swal("Oops", "Start date cannot be null, today is "+moment().format("MM/DD/YYYY"), "error")
    }
    else if ($('#project_end').val() <= ($('#project_start').val()) ) {
        swal("Oops", "Minimum project duration is one day. ", "error" )
    }

    else if ($('#project_description').val() == 0 || $('#project_description').val() == "") {
        swal("Oops", "Please add project description", "error")
    }

    else if ($('#project_status').val() == "" || $('#project_status').val() == " ") {
        swal("Oops", "Empty Project Status", "error")
    }

    else if ($('#id').val() == "" || $('#id').val() == " ") {
        Save();
    } else {
        Edit();
    }
}