$(document).ready(function () {
    LoadIndexRule();
    $('#tableProjects').DataTable({
        "ajax": LoadIndexRule(),
        "paging": true,
        "filter": true,
        "autoWidth": false
    })
});

function LoadIndexRule() {
    $.ajax({
        type: "GET",
        async: false,
        url: "/Rules/LoadRule/",
        dataType: "json",
        success: function (data) {
            console.log(data);
            var html = '';
            var i = 1;
            $.each(data,
                function (index, val) {
                    html += '<tr>';
                    html += '<td>' + i + '</td>';
                    html += '<td>' + val.Rule_Name + '</td>';
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

function Edit() {
    var rule = new Object();
    rule.Id = $('#id').val();
    rule.Rule_Name = $('#Rule_Name').val();
    $.ajax({
        url: '/Rules/InsertOrUpdate/',
        data: rule,
        success: function (result) {
            swal({
                title: "Saved!",
                text: "That data has been save!",
                type: "success"
            },
                LoadIndexRule());
            $('#modelAddNew').modal('hide');

        }
    });
}

function Save() {
    var rule = new Object();
    rule.Rule_Name = $('#Rule_Name').val();
    $.ajax({
        url: '/Rules/InsertOrUpdate/',
        data: rule,
        success: function (result) {
            swal({
                title: "Saved!",
                text: "That data has been save!",
                type: "success"
            },
                function () {
                    location.reload();
                });
            LoadIndexRule();
        }
    });
    ClearScreen();


}

function GetById(Id) {
    $('#Update').show();
    $('#Save').hide();
    $.ajax({
        url: "/Rules/GetById/",
        data: { Id: Id },
        success: function (result) {
            $('#id').val(result.Id);
            $('#Rule_Name').val(result.Rule_Name);
            $('#modelAddNew').modal('show');
        }
    })
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
            url: '/Rules/Delete/',
            data: { Id: Id },
            success: function (response) {
                swal({
                    title: "Deleted!",
                    text: "That data has been soft delete!",
                    type: "success"
                },
                    LoadIndexRule());
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
    $('#Rule_Name').val('');
    $('#Update').hide();
}

function Validate() {
    if ($('#Rule_Name').val() == "" || $('#Rule_Name').val() == " ") {
        swal("Oops", "Please Insert Rule Name", "error")
    }
    else if ($('#id').val() == "" || $('#id').val() == " ") {
        Save();
    } else {
        Edit();
    }
}