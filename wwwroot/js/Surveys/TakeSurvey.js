if (!jQuery) { throw new Error("This page requires jQuery"); }

////////////////////////////////////////////////////
//      jquery for the /Surveys/TakeSurvey page.      //
////////////////////////////////////////////////////

$(document).ready(function ()
{

    //Setup Click Events
    $("body").on("click", "#takeSurvey", takeSurvey);
    $("body").on("click", "#updateSurvey", updateSurvey);
});

function takeSurvey()
{
    var c;
    var answers = $(".survey-answer-radio:checked").toArray();    
    var answer_ids = [];
    //var userId = $("#UserId").val();

    for (c = 0; c < answers.length; c++)
    {
        console.log(c + ":  " + answers[c].getAttribute("name") + " " + answers[c].getAttribute("value")  );
        //OKAY, here we go! We now have a list of only the checked answers, and we need to extract the answer id
        //from each value (it looks like "Answer_32"). Unless we just remove the "answer_" part and then we just have it.
        //Done. Okay, so ...
        answer_ids[c] = answers[c].getAttribute("value");
        //And now, we just send this array back through the ajax, and make the UserSurveyAnswer entry on the server side.

    }
    $.ajax({
        type: "POST",
        cache: false,
        dataType: "json",
        url: "/Surveys/TakeSurvey",
        data: {
            answers: JSON.stringify(answer_ids)
        },
        success: function (data) {
            console.log("response URL: " + data.responseUrl);
            window.location.href = data.responseUrl;
        },
        error: function () {
            alert("Ajax failed! TakeSurvey.js, takeSurvey()");
        }
    });
}

function updateSurvey() {
    var c;
    var answers = $(".survey-answer-radio:checked").toArray();
    var answer_ids = [];
    //var userId = $("#UserId").val();
    console.log("UPDATE SURVEY!  answers: " + answers.length);

    for (c = 0; c < answers.length; c++) {
        console.log(c + ":  " + answers[c].getAttribute("name") + " " + answers[c].getAttribute("value"));
        //OKAY, here we go! We now have a list of only the checked answers, and we need to extract the answer id
        //from each value (it looks like "Answer_32"). Unless we just remove the "answer_" part and then we just have it.
        //Done. Okay, so ...
        answer_ids[c] = answers[c].getAttribute("value");
        //And now, we just send this array back through the ajax, and make the UserSurveyAnswer entry on the server side.

    }
    $.ajax({
        type: "POST",
        cache: false,
        dataType: "json",
        url: "/Surveys/UpdateSurvey",
        data: {
            answers: JSON.stringify(answer_ids)
        },
        success: function (data) {
            console.log("response URL: " + data.responseUrl);
            window.location.href = data.responseUrl;
        },
        error: function () {
            alert("Ajax failed! TakeSurvey.js, updateSurvey()");
        }
    });
}
