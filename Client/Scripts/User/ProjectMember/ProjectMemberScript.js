$(document).ready(function () {
    LoadIndexProjectMember();
    $('#table').DataTable({
        "ajax": LoadIndexProjectMember()
    })
})

function Save() {
    var projectMember = new Object();
    projectMember.User_Id = $('#User_Id').val();
    $.ajax({
        url: "/ProjectMembers/InsertOrUpdate/",
        data: projectMember,
        success: function (result) {
            swal({
                title: "Saved!",
                text: "That data has been save!",
                type: "success"
            },
            function () {
                window.location.href = '/ProjectMembers.Index/';
            });
            LoadIndexProjectMember();
            $('#applicationModal').modal('hide');
            ClearScreen();
        }
    });
};

function LoadIndexProjectMember() {
    $.ajax({
        type: "GET",
        async: false,
        url: "/ProjectMembers/LoadProjectMember/",
        dataType: "json",
        success: function (data) {
            var html = '';
            var i = 1;
            $.each(data, function (index, val) {
                html += '<tr>';
                html += '<td>' + i + '</td>';
                html += '<td>' + val.User_Id + '</td>';
                html += '<td>' + val.Project.Project_name + '</td>'
                html += '<td>' + val.Rule.Rule_Name + '</td>'
                html += '<td>' + '<a href="#" class="fa fa-pencil" onclick=return GetById(' + val.Id + ')">Edit</a>';
                html += ' | <a href="#" class="fa fa-trash" onclick=return Delete(' + val.Id + ')">Delete</a></td>';
                html += '</tr>';
                i++;
            });
            $('.tbody').html(html);
        }
    });
}

function Edit() {
    var projectMember = new Object();
    projectMember.Id = $('#Id').val();
    projectMember.User_Id = $('#User_Id').val();
    projectMember.Project.Project_name = $('#Project_Name').val();
    projectMember.Rule.Rule_Name = $('#Rule_Name').val();
    $.ajax({
        url: "/ProjectMembers/InsertOrUpdate/",
        data: projectMember,
        success: function (result) {
            swal({
                title: "Saved!",
                text: "That data has been save!",
                type: "success"
            },
            function () {
                window.location.href = '/ProjectMembers/Index/';
            });
            LoadIndexProjectMember();
            $('#applicationModal').modal('hide');
            ClearScreen;
        }
    });
};

function GetById(id) {
    $.ajax({
        url: "/ProjectMembers/GetById/",
        type: "GET",
        dataType: "json",
        data: { Id: id },
        success: function (result) {
            $('#Id').val(result.Id);
            $('#Name').val(result.User_Id);
            $('#Project_Name').val(result.Project.Project_name);
            $('#Rule_Name').val(result.Rule.Rule_Name)
            $('#applicationModal').modal('show');
            $('#Update').show();
            $('#Save').hide();
        }
    });
}

function Delete(id) {
    swal({
        title: "Are you sure?",
        text: "You will not be able to recover this imaginary file!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, delete it!",
        closeOnConfirm: false
    },
    function () {
        $.ajax({
            url: "/ProjectMembers/Delete/",
            data: { Id: id },
            success: function (response) {
                swal({
                    title: "Deleted!",
                    text: "That data has been soft delete!",
                    type: "success"
                },
                function () {
                    window.location.href = '/ProjectMembers/Index/';
                });
            },
            error: function (response) {
                swal("Oops", "We couldn't connect to the server!", "error");
            }
        });
    });
}

function ClearScreen() {
    $('#User_Id').val('');
    $('#Id').val('');
    $('#Update').hide();
    $('#Save').show();
}

function Validate() {
    if ($('#User_Id').val() == "" || $('#User_Id').val() == " ") {
        swal("Oops", "Please User Id", "error")
    } else if ($('#Id').val() == "") {
        save();
    } else {
        Edit();
    }
}