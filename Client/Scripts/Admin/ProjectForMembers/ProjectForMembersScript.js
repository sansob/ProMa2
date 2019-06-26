$(document).ready(function () {
    LoadIndexProjectForMember();
    $('# ').DataTable({
        "ajax": LoadIndexProjectForMember(),
        "paging": true,
        "filter": true,
        "autoWidth": false
    })
});

function LoadIndexProjectForMember() {
    $.ajax({
        type: "GET",
        async: false,
        url: "/ProjectForMembers/LoadProjectForMember/",
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
                    html += '<td>' + moment(val.Project.Project_Start).format("MM/DD/YYYY") + '</td>';
                    html += '<td>' + moment(val.Project.Project_Deadline).format("MM/DD/YYYY") + '</td>';                   
                    html += '<td>' + val.Project.Status.Status_name + '</td>'; 
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


