
var errorMap = new Map(); 
var allForms = [];
var i = 0;
$(document).ready(function () {

    $.validator.setDefaults({
        onkeyup: false
    })
    $(".text-danger").hide();
    $("#divErrorMsg.text-danger").show();

    $("[type='submit']").each(function () {
        //alert($(this.form).attr("method"));
       // allForms[i++] = $(this.form);
    });

    $("[class='row']").each(function () {
        i++;
        $(this).before("<div id=\"divErrorMsg\" class=\"text-danger\"></div>");
      
        return false;
    });

    $("[type='submit']").bind("click", function () { 
     
        parseErrorOnSubmit($(this.form));
       
    });   

    $("input").blur(function () {

   // alert($(this.form).valid());

        if (!$(this.form).valid()) {
        //    addDelError();
         }
            var ctrlName = $(this).attr("name");
            parseError(ctrlName);
     
    });
  
});

 

function formValid(objForm) {

    return $(objForm).valid();
}

 

function parseErrorOnSubmit(frm) { 
   //alert('frm' + $(frm).valid());
 if (!$(frm).valid())
    {
        addDelError();
    }
   // showHide("");    
 
    //$("span[data-valmsg-for]").each(function () {

    //    var ctrlId = $(this).attr("data-valmsg-for");
    //    var errorMsg = $(this).text() + ""; 
           
    //    addDelError(ctrlId, errorMsg);
    //    showHide(ctrlId, errorMsg);
    //});
    
}

function parseError(ctrlIdParam)
{
 
    var errorSpanObj = $("[data-valmsg-for*='" + ctrlIdParam + "']");

    var ctrlId = $(errorSpanObj).attr("data-valmsg-for");

     var errorMsg = $(errorSpanObj).text() + "";  
 
    showHide(ctrlId, errorMsg);    
}


function addDelError() {
    var ctrlId = "";
    var errorMsg1 = "";
   // $("span[data-valmsg-for]").each(function () {
    $("[class *='-validation-']").each(function () {
 
         ctrlId = $(this).attr("data-valmsg-for");
        var errorMsg = $(this).text() + ""; 

        errorMsg1 += errorMsg + "  ";
    if(errorMsg!="") {
        if (errorMap.has(ctrlId)) {
            errorMap.delete(ctrlId);
            errorMap.set(ctrlId, errorMsg);
           // alert('updt');
           
        } else {
            errorMap.set(ctrlId, errorMsg);
          //  alert('set');
        }
    }
    else
    {        
       // alert(ctrlId+' del: ' + errorMap.get(ctrlId));
        errorMap.delete(ctrlId);
    }
         
    });
 
    errorMap.forEach(function (v, k, m) { 
        $("#" + k).removeClass("validInput");
        $("#" + k).addClass("errorInput");
        $("#" + k).focus();
        $("#divErrorMsg").text(v);
    });
    
        //
   
    //alert(errorMap.size + " " + errorMsg1) ;
   // alert(errorMap.get(ctrlId));
}

function showHide(ctrlId, errorMsg) {
 
    var tem = "";
    if (ctrlId != "") {
        var ctrlId1 = ctrlId.replace(".", "_");

        if (errorMsg != "") {
            $("#divErrorMsg").text(errorMsg);
            $("#" + ctrlId1).removeClass("validInput");
            $("#" + ctrlId1).addClass("errorInput");
            $("#" + ctrlId1).focus();
        }
        else {
            $("#" + ctrlId1).removeClass("errorInput");
            $("#" + ctrlId1).addClass("validInput");
        }

    }
}
  
 