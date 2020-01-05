if (!jQuery) { throw new Error("This page requires jQuery"); }

////////////////////////////////////////////////////
//      jquery for the /Surveys/Edit page.      //
////////////////////////////////////////////////////

$(document).ready(function () {

    //Setup Click Events
    $("body").on("click", "#saveSurvey", saveSurvey);//Any advantage to this over below method?
    $("body").on("click", " #addQuestion", addQuestion);
    $("body").on("click", " #removeQuestion", removeQuestion);
    $("body").on("click", " #addAnswer", addAnswer);
    $("body").on("click", " #removeAnswer", removeAnswer);
    $("body").on("click", " #moveAnswerUp", moveAnswerUp);
    $("body").on("click", " #moveAnswerDown", moveAnswerDown);

    //Hmm, why do these not work?
    //$("#saveSurvey").click(saveSurvey);
    //$("#addQuestion").click(addQuestion);
    //$("#removeQuestion").click(removeQuestion);
    //$("#addAnswer").click(addAnswer);
    //$("#removeAnswer").click(removeAnswer);
    //$("#moveAnswerUp").click(moveAnswerUp);
    //$("#moveAnswerDown").click(moveAnswerDown);

    //$("#sort").click(handleSort);
    //$(".sort-input").blur(handleSort);
});

//function myFunction() {
//    console.log("Calling myFunction!!");
//}


function saveSurvey() {

    var f = $("#survey-form");

    //get the action attribute
    var action = f.attr("action");
    console.log("Calling saveSurvey, action = " + action.toString());
    //get the serialized data
    var serializedForm = f.serialize();

    $.ajax({
        type: "POST",
        cache: false,
        url: "/Surveys/saveSurvey",
        data: serializedForm,
        success: function (data) {
            $("#subscribe").replaceWith(data);
        }
    });
}

/*
function addQuestion() {
    console.log("Adding Question????? ID " + $("#Id").toString());
    $.post("/Surveys/addQuestion/",
        {
            survey_id: $("#Id").toString()
        },
        function (data, status) {
            alert("Data: " + data + "\nStatus: " + status);
        },
        "json"
    );
}*/

function addQuestion() {
     //$("#surveyId").val()
    console.log("Adding Question??? Id: " + $("#Id").val());
    $.ajax({
        type: "POST",
        cache: false,
        dataType: "json",
        url: "/Surveys/addQuestion",
        data: {
            survey_id: $("#Id").val(),
            question_id: $(this).attr("data-question-id")
        },
        success: function (data) {
            console.log("response URL: " + data.responseUrl);
            window.location.href = data.responseUrl;
        },
        error: function () {
            alert("ajax failed!");
        }
    });
}

function removeQuestion() {
    console.log("Remove Question! " + $(this).attr("data-question-id"));
    $.ajax({
        type: "POST",
        cache: false,
        dataType: "json",
        url: "/Surveys/removeQuestion",
        data: {
            survey_id: $("#Id").val(),
            question_id: $(this).attr("data-question-id")
        },
        success: function (data) {
            window.location.href = data.responseUrl;
        },
        error: function () {
            alert("ajax fail!");
        }
    });
}

function addAnswer() {
    //console.log("Add Answer!");
    $.ajax({
        type: "POST",
        cache: false,
        dataType: "json",
        url: "/Surveys/addAnswer",
        data: {
            survey_id: $("#Id").val(),
            question_id: $(this).attr("data-question-id"),
            answer_id: $(this).attr("data-answer-id")
        },
        success: function (data) {
            console.log("response URL: " + data.responseUrl);
            window.location.href = data.responseUrl;
        },
        error: function () {
            alert("ajax failed!");
        }
    });
}

function removeAnswer() {
    //console.log("Remove Answer!");
    $.ajax({
        type: "POST",
        cache: false,
        dataType: "json",
        url: "/Surveys/removeAnswer",
        data: {
            survey_id: $("#Id").val(),
            question_id: $(this).attr("data-question-id"),
            answer_id: $(this).attr("data-answer-id")
        },
        success: function (data) {
            console.log("response URL: " + data.responseUrl);
            window.location.href = data.responseUrl;
        },
        error: function () {
            alert("ajax failed!");
        }
    });
}

function moveAnswerUp() {
    console.log("Move answer up! " + $(this).attr("data-answer-id"));
    $.ajax({
        type: "POST",
        cache: false,
        dataType: "json",
        url: "/Surveys/moveAnswerUp",
        data: {
            survey_id: $("#Id").val(),
            question_id: $(this).attr("data-question-id"),
            answer_id: $(this).attr("data-answer-id")
        },
        success: function (data) {
            console.log("response URL: " + data.responseUrl);
            window.location.href = data.responseUrl;
        },
        error: function () {
            alert("ajax failed!");
        }
    });
}

function moveAnswerDown() {
    console.log("Move answer down! " + $(this).attr("data-answer-id"));
    $.ajax({
        type: "POST",
        cache: false,
        dataType: "json",
        url: "/Surveys/moveAnswerDown",
        data: {
            survey_id: $("#Id").val(),
            question_id: $(this).attr("data-question-id"),
            answer_id: $(this).attr("data-answer-id")
        },
        success: function (data) {
            console.log("response URL: " + data.responseUrl);
            window.location.href = data.responseUrl;
        },
        error: function () {
            alert("ajax failed!");
        }
    });
}

function handleSort() {
    //console.log("Handling sort function! ");
    console.log("Handling sort function! " + $(this).attr('id'));

}