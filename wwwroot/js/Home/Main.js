if (!jQuery) { throw new Error("This page requires jQuery"); }

////////////////////////////////////////////////////
//      jquery for the /Home/Main page.      //
////////////////////////////////////////////////////

$(document).ready(function () {

    //Setup Click Events
    //$("body").on("click", "#myButton", myFunction);

});


/*
function myFunction() {

    var f = $("#subscribe-form");

    //get the action attribute
    var action = f.attr("action");

    //get the serialized data
    var serializedForm = f.serialize();

    $.ajax({
        type: "POST",
        cache: false,
        url: action,
        data: serializedForm,
        success: function (data) {
            $("#subscribe").replaceWith(data);
      //      $(".spinner-TimeZone").fadeOut("fast");
      //      $("#submitTimeZone").fadeIn("normal");
        }
    });
}*/