$(document).ready(function() {
    LoadIndexRule();
    $('#tableProjects').DataTable({
        "ajax": LoadIndexRule(),
        "paging": true,
        "filter": true
    })
})

function LoadIndexRule() {
    $.ajax({
        type: "GET",
        async: false,
        url: "/Projects/LoadProject/",
        dataType: "json",
        success: function(data) {
            var html = '';
            var i = 1;
            $.each(data,
                function(index, val) {
                    html += '<tr>';
                    html += '<td>' + i + '</td>';
                    html += '<td>' + val.Project_name + '</td>';
                    html += '<td>' + val.Id + '</td>';
                    html += '<td>' + val.Project_Deadline + '</td>';
                    html += '<td>' +
                        '<a href="#" class="fa fa-pencil" onclick=return GetById(' +
                        val.Id +
                        ')">Edit</a>';
                    html += ' | <a href="#" class="fa fa-trash" onclick=return Delete(' + val.Id + ')">Delete</a></td>';
                    
                    html += '</tr>';
                    i++;
                });
            $('.tbody').html(html);
        }
    });
}