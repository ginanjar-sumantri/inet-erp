$(document).ready(function() 
{
    var left_height = $("#bodyChooseTable .container .content .left .table .table-inner-left").height();
    var right_height = $("#bodyChooseTable .container .content .left .table .table-inner-right").height();
    if (left_height > right_height) {
        $("#bodyChooseTable .container .content .left .table .table-inner-right").height(left_height);
    }
    else {
        $("#bodyChooseTable .container .content .left .table .table-inner-left").height(right_height);
    }
    $("#bodyChooseTable .container .content .left .table .table-inner-right .number").corner("10px");
    $(".container .content .right .productGroupBox, .container .content .right .productSubGroupBox, .container .content .right .productBox").corner();
    $("#bodyLogin .container .content .left-content, #bodyLogin .container .content .right-content").corner();
    $("#bodyInternet .container .content .right").corner();
    $("#bodyChooseTable .container .content .left .total .send").corner("bottom");
    $("#bodySettlement .content .right .middle").corner();
    $("#bodyPosRetail .containerBox .totalItemBox").corner();
    $("#bodyPosRetail .containerBox .transactionRefBox").corner();
    $("#bodyPosRetail .containerBox .customerBox").corner();
    $("#bodyPosRetail .containerBox .totalBox").corner();
    $("#bodyPosRetail .container .productImageBox").corner();
    $("#bodyPosRetail .container .SendtoCashier").corner();
    $("#NewMemberButton").corner();
    $("#BackButton,#SearchButton,#HoldButton,#OpenHoldButton,#CheckStatusButton,#CancelAllButton,#JoinJobOrderButton,#SendToCashierButton").corner();
    $(".btnInputNumber").corner();
    $("#bodyPosCheckStatus .containerBox .totalItemBox, #bodyPosJoinJobOrder .containerBox .totalItemBox").corner();
    $("#bodyPosCheckStatus .containerBox .transactionRefBox, #bodyPosJoinJobOrder .containerBox .transactionRefBox").corner();
    $("#bodyPosRetail .container .content .top .top-left, #bodyPosRetail .container .content .top .top-center, #bodyPosRetail .container .content .top .top-right, #bodyPosRetail .container .content .bottom .bottom-center").corner();
    var position = $(".table-container-scroll").scrollTop();
	var height = $(".table-container-scroll table").height();
	var up;
	var down;
	$(".table-navigation-scroll .up").mousedown(function () 
	{
		up = setInterval(
							 function()
							 {
								$(".table-container-scroll").scrollTop(position-20);
								if(position <= 0)
								{
									position = 0;
								}
								else
								{
									position = position - 20;
								}
							},10); 	
	    $(".table-navigation-scroll .up").mouseout(function () 
	    {
	        clearInterval(up);
	    });
	    $(".table-navigation-scroll .up").mousemove(function () 
	    {
	        clearInterval(up);
	    });
	}).mouseup(function() 
	{
		clearInterval(up);
	});
	$(".table-navigation-scroll .down").mousedown(function () 
	{
		down = setInterval(
							 function()
							 {
								$(".table-container-scroll").scrollTop(position+20);
								if(position >= (height))
								{
									position = height;
								}
								else
								{
									position = position + 20;
								}
							},10);
	    $(".table-navigation-scroll .down").mouseout(function () 
	    {
	        clearInterval(down);
	    });
	    $(".table-navigation-scroll .down").mousemove(function () 
	    {
	        clearInterval(down);
	    });    
	}).mouseup(function() 
	{
		clearInterval(down);
	});
	var position2 = $(".table-container-scroll2").scrollTop();
	var height2 = $(".table-container-scroll2 table").height();
	var up2;
	var down2;
	$(".table-navigation-scroll2 .up").mousedown(function () 
	{
		up2 = setInterval(
							 function()
							 {
								$(".table-container-scroll2").scrollTop(position2-20);
								if(position2 <= 0)
								{
									position2 = 0;
								}
								else
								{
									position2 = position2 - 20;
								}
							},10); 	
	    $(".table-navigation-scroll2 .up").mouseout(function () 
	    {
	        clearInterval(up2);
	    });
	    $(".table-navigation-scroll2 .up").mousemove(function () 
	    {
	        clearInterval(up2);
	    });
	}).mouseup(function() 
	{
		clearInterval(up2);
	});
	$(".table-navigation-scroll2 .down").mousedown(function () 
	{
		down2 = setInterval(
							 function()
							 {
								$(".table-container-scroll2").scrollTop(position2+20);
								if(position2 >= (height2))
								{
									position2 = height2;
								}
								else
								{
									position2 = position2 + 20;
								}
							},10);
	    $(".table-navigation-scroll2 .down").mouseout(function () 
	    {
	        clearInterval(down2);
	    });
	    $(".table-navigation-scroll2 .down").mousemove(function () 
	    {
	        clearInterval(down2);
	    });    
	}).mouseup(function() 
	{
		clearInterval(down2);
	});
	var position3 = $(".table-container-scroll3").scrollTop();
	var height3 = $(".table-container-scroll3 table").height();
	var up3;
	var down3;
	$(".table-navigation-scroll3 .up").mousedown(function () 
	{
		up3 = setInterval(
							 function()
							 {
								$(".table-container-scroll3").scrollTop(position3-20);
								if(position3 <= 0)
								{
									position3 = 0;
								}
								else
								{
									position3 = position3 - 20;
								}
							},10); 	
	    $(".table-navigation-scroll3 .up").mouseout(function () 
	    {
	        clearInterval(up3);
	    });
	    $(".table-navigation-scroll3 .up").mousemove(function () 
	    {
	        clearInterval(up3);
	    });
	}).mouseup(function() 
	{
		clearInterval(up3);
	});
	$(".table-navigation-scroll3 .down").mousedown(function () 
	{
		down3 = setInterval(
							 function()
							 {
								$(".table-container-scroll3").scrollTop(position3+20);
								if(position3 >= (height3))
								{
									position3 = height3;
								}
								else
								{
									position3 = position3 + 20;
								}
							},10);
	    $(".table-navigation-scroll3 .down").mouseout(function () 
	    {
	        clearInterval(down3);
	    });
	    $(".table-navigation-scroll3 .down").mousemove(function () 
	    {
	        clearInterval(down3);
	    });    
	}).mouseup(function() 
	{
		clearInterval(down3);
	});
	var position4 = $(".table-container-scroll4").scrollTop();
	var height4 = $(".table-container-scroll4 table").height();
	var up4;
	var down4;
	$(".table-navigation-scroll4 .up").mousedown(function() {
	    up4 = setInterval(
							 function() {
							     $(".table-container-scroll4").scrollTop(position4 - 40);
							     if (position4 <= 0) {
							         position4 = 0;
							     }
							     else {
							         position4 = position4 - 20;
							     }
							 }, 10);
	    $(".table-navigation-scroll4 .up").mouseout(function() {
	        clearInterval(up4);
	    });
	    $(".table-navigation-scroll4 .up").mousemove(function() {
	        clearInterval(up4);
	    });
	}).mouseup(function() {
	    clearInterval(up4);
	});
	$(".table-navigation-scroll4 .down").mousedown(function() {
	    down4 = setInterval(
							 function() {
							     $(".table-container-scroll4").scrollTop(position4 + 20);
							     if (position4 >= (height4)) {
							         position4 = height4;
							     }
							     else {
							         position4 = position4 + 20;
							     }
							 }, 10);
	    $(".table-navigation-scroll4 .down").mouseout(function() {
	        clearInterval(down4);
	    });
	    $(".table-navigation-scroll4 .down").mousemove(function() {
	        clearInterval(down4);
	    });
	}).mouseup(function() {
	    clearInterval(down4);
	});
});

