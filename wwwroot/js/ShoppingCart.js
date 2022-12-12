var newStringJson = "";
var couponValue = 0;
var total = 0;
var defaultShipOptCost = 0;
var sippingdataObj = "";
var defaultshippingdata = ""
  
function intValue(stringJson)
{
     newStringJson = stringJson;

    console.log('stringJson' + newStringJson);
}
 
function OpenFileBrowser(imgId, urlId,imgTagId) {
    imgId = "#" + imgId;
    urlId = "#" + urlId;
    imgTagId = "#" + imgTagId;

    $(imgId).trigger('click');

    $(imgId).change(function (e) {
        var fileName = e.target.files[0].name;
        console.info("fileName url..:" + fileName);
        $(urlId).val("/images/" + fileName);
        $(imgTagId).removeAttr("src");
       $(imgTagId).attr("src",  $(urlId).val() );
        console.log("urlId:" + $(urlId).val());
    //console.log("imgId:" + imgId + " urlId:" + urlId + " :" + $(imgId).files + " :" + $(imgId).attr("value"));
        //console.log("imge:" + $(imgTagId).css("background-image"));
        console.log("imge:" + $(imgTagId).attr("src"));
    });
}
function NavImg(obj,id) {
    $(".color-filter2Img").css("background-color", "grey");
    $(obj).css("background-color", "teal");////toggleClass('color-filter2Img');
    var imgId = "#img_" + id
    var picUrl = $(imgId).attr("src");
    var tempicUrl = picUrl.substring(picUrl.lastIndexOf(".") - 1);
    tempicUrl = tempicUrl.substring(0, 1);
    var tempic = "";
    //alert(tempicUrl);
    if (tempicUrl.toLowerCase() == "a") {//img..a.jpg
          tempic = picUrl.substring(0, picUrl.lastIndexOf(".") - 1) + "b" + picUrl.substring(picUrl.lastIndexOf("."));
       // alert(tempic+" tempic")
    }
    else { //img..b.jpg
          tempic = picUrl.substring(0, picUrl.lastIndexOf(".") - 1) + "a" + picUrl.substring(picUrl.lastIndexOf("."));
       // alert(tempic + " tempic")

    }
    $(imgId).attr("src", tempic);
}
function LoadSideMenu() {
   // alert('ddsa');
   
    $.ajax({
        type: "GET",
        url: "Product?handler=ConsturctSideNav",
        //data: {  },
         contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        dataType: "html",
        success: function (response) {
            $(".nestedsidemenuA").html(response);
            TopNavMenu();
            console.info(response);
        },
        failure: function (response) {
            // alert("failure:"+response.responseText);
        },
        error: function (response) {
            // alert("error:::"+ response.responseText);
        }
    });
}

function AddtoCart(pid) {
 

 //alert('pid:'+pid) ;
 
  		$.ajax({
                    type: "GET",
                    url: "Product?handler=AddtoCart",
                    data: {productId: pid },   
                    contentType: "application/json; charset=utf-8",
                    dataType: "html",
                     success: function (response) { 
					 $('#idheaderCartTemp').html('');
                     $('#idheaderCartTemp').append(response);   
                    $("#idheaderCartABC").html('');
                    
                         $("#idheaderCartABC").append($("#idheaderCartPHolder").html());
                         $('#idheaderCartTemp').html('');
                         var cartItems = $(".header-icons-noticount").val();
                         var prdNameId = "#prdName" + pid;
                         if (pid != "0") { prodCount(cartItems, prdNameId); }
                        $(".header-icons-noti").html(cartItems);
                    },
                    failure: function (response) {
                       // alert("failure:"+response.responseText);
                    },
                    error: function (response) {
                       // alert("error:::"+ response.responseText);
                    }
                });
}


function prodCount(cartItems, prdNameId) {
    var nameProduct = $(prdNameId).val(); // $(".block2-btn-addcart").parent().parent().parent().find('.block2-name').html();
   // alert(prdNameId);
    swal(nameProduct + " :" + cartItems, "is added to cart !", "success");
  
}

function LoadDataCartPage(shippingdata) {

    defaultshippingdata = shippingdata;

    $("#divCouponDisplay").hide();
    console.info("shippingdata:" + shippingdata); 
    intValue($("#hidJsonInput1").val()); 
    var shippingOptions = shippingdata;
    shippingdata = shippingOptions.replaceAll(/&quot;/g, "\"");
    console.info(shippingdata);
    sippingdataObj = JSON.parse(shippingdata);
    
    total = $("#spnTotal").html();
    changeCost();
    ChangeTotal(defaultShipOptCost);

   
}

function UpdateCart(jsonInput1) {

    //alert('jsonInput1:' + jsonInput1) ;
    //var jsonInput1 = "[{\"Id\": \"2\",\"Quantity\": \"1\"},{\"Id\": \"1\", \"Quantity\": \"4\"},{\"Id\":\"3\", \"Quantity\": \"6\"}]";
    //var jsonInput = " { Id: 2, Quantity: \"1\"} ";

    console.info('Update:' + IncItem);

    $.ajax({
        type: "GET",
        url: "Product?handler=UpdateCart",
        // data: { productId: pid },
    
        data: { jsonInput: jsonInput1 },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {

            $('#idDivCartItems').html('');
            $('#idDivCartItems').append(response);
            $('#idheaderCartABC').show();
            //Repopulate Cart
            AddtoCart("0");
 
        },
        failure: function (response) {
           //alert("failure:"+response.responseText);
        },
        error: function (response) {
           // alert(response.responseText);
            $('#idDivCartItems').html('');
            $('#idDivCartItems').append(response.responseText);
        }
    });
}

//function DecItem(prdId, stringJson,obj,price,objTotal) {
//    var itmCount = $(obj).val();
//    itmCount = parseInt(itmCount)-1;
//    if (itmCount <= 0) {
//        itmCount = 0;
//    }
//    $(obj).val(itmCount);
//    $(objTotal).html(itmCount*price);

//   console.log('objTotal:' + $(objTotal).attr("class") + " " + itmCount * price);
//}

function IncItem(strOp, prdId, stringJson, obj, price, objTotal) {
    console.info("newStringJson:" + newStringJson);
    console.info("stringJson:" + stringJson);
    var itmCount = $(obj).val();
    if (strOp == '+') { itmCount = parseInt(itmCount) + 1; }
    else {
        itmCount = parseInt(itmCount) - 1;
    }
    if (itmCount <= 0) {
        itmCount = 0;
    }
    $(obj).val(itmCount)
    $(objTotal).html(itmCount * price);
 
    console.log('objTotal:' + $(objTotal).attr("class") + " " + itmCount * price);
    
    var obj = JSON.parse(newStringJson); 
    var jsonInput1 = "[";
    obj.forEach(function (o, index) {

        jsonInput1 += "{\"Id\": \"";

        jsonInput1 += o.Id;

        jsonInput1 += "\",\"Quantity\":\"";       

        console.log("o.Id:" + o.Id);

        if (o.Id == prdId) {
           // alert("prdId:" + prdId);
            jsonInput1 += itmCount;
        }
        else
        {
            jsonInput1 += o.Quantity;
        }
        jsonInput1 += "\"},";
     
        //$("#btnUpdateCart").bind("click", function () {
        //    // UpdateCart("'"+jsonInput1+"'");
        //    alert("UpdateCart");
        //});
    }); jsonInput1 += "]";

    jsonInput1 = jsonInput1.replace("},]", "}]");

    $('#btnUpdateCart').removeAttr('onclick');
    $('#btnUpdateCart').attr('onclick', 'UpdateCart(\'' + jsonInput1 + '\')');


    console.log("Modified:" + jsonInput1); 
  
}




	function changeCost()
    {
        var selectFlg = 0;
				var countryOptionId = $('#selectCountry option:selected').attr("id") ;
	var countryOption = $('#selectCountry option:selected').val() ;
	$("#divShippingOpt").html('');

	var shippingOption = countryOptionId.replace("country_","");
        var shippingOptionCost = "";
        console.info("sippingdataObj:" + sippingdataObj);
        console.info("sippingdataObj.length:" + sippingdataObj.length);
				if(sippingdataObj.length > 0)
	{
					for(var i =0;i<sippingdataObj.length; i++)
                    {
                       
                        shippingOptionCost = sippingdataObj[i].ShippingCost;
                        console.info("shippingOptionCost:" + shippingOptionCost);

	if(countryOption == sippingdataObj[i].ShippingDestinationCountry)
	{
							
        var tem = "";
        if (selectFlg == 0) {
            tem = "<div ><input type='radio' onchange='ChangeTotal(" + shippingOptionCost + ")'   checked='true' name='shippingOpt' /> <label id='lblshippingOpt_'>" + sippingdataObj[i].ShippingOption + "($" + shippingOptionCost + ")</label></div>";
            defaultShipOptCost = shippingOptionCost;
        }
        else {
            tem = "<div ><input type='radio' onchange='ChangeTotal(" + shippingOptionCost + ")'   name='shippingOpt' /> <label id='lblshippingOpt_'>" + sippingdataObj[i].ShippingOption + "($" + shippingOptionCost + ")</label></div>";
        }
        $("#divShippingOpt").append(tem);
        selectFlg = 1;
	//var shippingOptionId = "#lblshippingOpt_"+shippingOption;
 //      /* console.info("#lblshippingOpt_:" + $(shippingOptionId).attr("id"));*/
        console.info("shippingOptionCost:" + shippingOptionCost);
        console.info("spnTotal:" + $("#spnTotal").html());
        console.info("lblCouponValue:" + $("#lblCouponValue").html());

						}
					}
				}  
        ChangeTotal(defaultShipOptCost);
			}

function ChangeTotal(shpOptcost) {

    defaultShipOptCost = shpOptcost;

    var subtotal = total;

    subtotal = subtotal.replace("$", "");
    subtotal= subtotal.toString().trim();
    couponValue = $("#lblCouponValue").html();
    couponValue = couponValue.toString().trim().substr(0, couponValue.indexOf('(')).replace("$","");
    console.info("couponValue_:"+couponValue);
    shpOptcost = shpOptcost.toString().trim();

    if (subtotal == "") { subtotal = 0; }
    if (shpOptcost == "") { shpOptcost = 0; }
    if (couponValue == "") { couponValue = 0; }

    var totl = parseFloat(subtotal) + parseFloat(shpOptcost) - parseFloat(couponValue);

    //alert(totl);
    console.info("subtotal:" + subtotal);
    console.info("couponValue:" + couponValue);
    console.info("shpOptcost:" + shpOptcost);
    console.info("totl:" + totl);
    $("#spnTotal").html(totl.toFixed(2));
}

	function ApplyCouponToCartJS(cartId, couponId) {
       // LoadData(defaultshippingdata);
	var  couponName = $(couponId).val();
	if(couponName=="")
	{
		$('#spnCouponCode').css("display", "");
	$('#spnCouponCode').html("Enter the coupon value");
	return;
				 }
	//alert('couponId'+couponId);   
	$.ajax({
		type: "Get",
	url: "Product?handler=ApplyCouponToCart",
	dataType: 'json',
	data: {cartId: cartId, couponName: couponName },
	contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
	success: function(response) {
	// alert(response) 	
	var shippingOptions = response;
	if(shippingOptions.indexOf('error')>-1)
	{
		$('#spnCouponCode').css("display", "");
	$('#spnCouponCode').html("Coupon doesnot exists!");
	//$("#divCouponDisplay").hide();
        $("#divCouponDisplay").show();
    $("#lblCouponValue").html("$0");
       ChangeTotal(defaultShipOptCost);
	return;
		}
	else
	{$("#divCouponDisplay").show();
	$('#spnCouponCode').css("display","none");
							}

	var  shippingdata = "["+shippingOptions.replaceAll(/&quot;/g,"\"")+"]";
	console.info(shippingdata);

	var sippingdataObj1 = JSON.parse(shippingdata);
        var couponData = sippingdataObj1[0].CouponValue + "(" + sippingdataObj1[0].CouponName +")"   	;
        couponValue = sippingdataObj1[0].CouponValue;
	console.info(couponData);

	$("#lblCouponValue").html("$"+couponData);  
        ChangeTotal(defaultShipOptCost);
			                 },
	failure: function (response) {
	//	alert('failure' + response);
			                 },
	error: function (response) {
		//alert('error'+ response);
	}			         }); 

       


}

	function ClearCoupon()
    {

	if($('#coupon').val()=="")
	{
		$('#spnCouponCode').css("display", "none");
	}
}


function ShowCheckOut() {
    $("#divCheckOutModal").show();
}

//alert('oo');

var tempSubNavMainhtml = "";

function TopNavMenu() { 
   // alert('ret' + $("#globalTopNsvDiv").html());
    $("#globalTopNsvDiv").html($(".nestedsidemenu").html());
    $("#globalTopNsvDiv").find("ul").each(function(){

        var tClass = $(this).attr("class");
        console.info("ul:" + tClass);
        if (tClass == "classCls") {

            $(this).attr("class","main_menu");
        }
        else {
            $(this).attr("class", "sub_menu");
        }
        //alert('ssds');
          tClass = $(this).attr("class");
        console.info("ul...:" + tClass);
    });
   
}