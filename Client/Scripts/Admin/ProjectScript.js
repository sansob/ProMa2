$(document).ready(function () {
    LoadIndexProject();
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
                    html += '<td>' + moment().startOf(val.Project_Deadline).from(val.Project_start) + '</td>';
                    html += '<td>' + val.Status.Status_name + '</td>';
                    html += '<td>' +
                        '<a class="btn btn-outline-dark btn-sm" onclick="return GetById(' + val.Id + ')" ><i class="os-icon os-icon-edit-1"></i><span>Edit</span></a>';
                    html += '  <a class="btn btn-danger btn-sm" onclick="return Delete(' + val.Id + ')" ><i class="os-icon os-icon-cancel-square"></i><span>Delete</span></a>';

                    html += '</tr>';
                    i++;
                });

            $('.tbody').html(html);
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
    project.Status_Id = '1';
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
                    window.location.href = '/Projects/Index/';
                });
            LoadIndexProject();
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
    project.Status_Id = '1';
    
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
                        window.location.href = '/Projects/';
                    });
                LoadIndexProject();
                $('#modelAddNew').modal('hide');
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
                    function () {
                        window.location.href = '/Projects/Index/';
                    });
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
    $('#project_description').val('');
    $('#Update').hide();
}