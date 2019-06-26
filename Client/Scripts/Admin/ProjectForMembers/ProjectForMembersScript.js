$(document).ready(function () {
    LoadIndexProjectForMember();
    $('#tableProjects').DataTable({
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
                    html += '</tr>';
                    i++;
                });

            $('.tbody').html(html);
        }
    });
}


