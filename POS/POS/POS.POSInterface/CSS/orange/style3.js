$(document).ready(function() {
    var left_height = $("#bodyChooseTable .container .content .left .table .table-inner-left").height();
    var right_height = $("#bodyChooseTable .container .content .left .table .table-inner-right").height();
    if (left_height > right_height) {
        $("#bodyChooseTable .container .content .left .table .table-inner-right").height(left_height);
    }
    else {
        $("#bodyChooseTable .container .content .left .table .table-inner-left").height(right_height);
    }

    //$("#bodyChooseTable .container .content .left .table .table-inner-right .number span").corner("10px");

    $("#bodyPosOpenHold .containerBox .totalItemBox").corner();
    $("#bodyPosOpenHold .containerBox .transactionRefBox").corner();
    $("#bodyPosReservation .containerBox .reservation").corner();
    $("#bodyPosReservation .containerBox .reservationInput").corner();
    $("#bodyPosReservation .containerBox .reservationInput .reservationBox").corner();
    $("#bodySerachCriteria .containerBox .logoBox").corner();
    $("#bodySerachCriteria .containerBox .searchCriteriaBox").corner();
});
