
var rolesData = [];


function getRoledetails() {
    ChangeValues(false);
}
function ChangeValues(flg) {
           
            var selUser = $("#ddlUsers").find(":selected").val();
           
            if (rolesData.length > 0) {
                var selRoleArr = rolesData.filter(x => x.userId == selUser);

                if ($("#roleUserId") != undefined) $("#roleUserId").val(selUser);

                var selRoleAll = "";
                    if (flg) 
                    {
                        $("#ddlRoles").val([]);
                        $("#ddlRoles").removeAttr("style");
                        $("#ddlRoles").css("background-color", "purple");
                        $('#ddlRoles').find('option').each(function (i, opt) {
                          
                            for (var i = 0; i < selRoleArr.length; i++) {
                          
                                if ($(opt).val() == selRoleArr[i].roleId) {
                                    $(opt).attr('selected', 'true');
                                    selRoleAll += selRoleArr[i].roleId + ",";
                                    $(opt).css("background-color", "purple");
                                    $(opt).css("color", "red");
                                   
                                }
                            }
                        });
                    } 
                if (flg) {
       
                }
                else {
                    selRoleAll = "";
                 
                    $('#ddlRoles').find('option:selected').each(function (i, opt) {    
                       $(opt).css({ "background-color": "#5e42a6", "color": "#cc2020" });  
                        selRoleAll += $(opt).val() + ","

                    });   
                }
                $("#rolRolesId").val(selRoleAll);
               
            }    
        }

$(document).ready(function () {

    var query_string = window.location.href;

    if (query_string.indexOf('?') > -1) {
        var quryStr = query_string.substr(query_string.indexOf('?') + 1);
        var parsedQueryStr = parse_query_string(quryStr);
        $("#ddlUsers").val(parsedQueryStr.Id);
        // alert(quryStr)//
        // alert(parsedQueryStr.Id);
    }
    else {
        $("#btnSubmt").attr("value", "Create");
       // alert($("#btnSubmt").attr("value"))
    }

    $.ajax({
        type: "GET",
        url: "/Admin/GetUserRoles",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        //data:{p1:"test" },
        success: function (response) {
              //alert(response)              ;
            rolesData = response;
            ChangeValues(true);
        },
        failure: function (response) {
            alert("Failure");
        },
        error: function (response) {

            alert("Error");
        }
    });

}); 