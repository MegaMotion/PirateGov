//if (!jQuery) { throw new Error("This page requires jQuery"); }
//NOPE, NO JQUERY HERE ... (?)

//Chart.defaults.global.hover.mode = 'nearest';
//Chart.defaults.global.scales.xAxes.ticks.beginAtZero = true;//NOPE! Not sure how this works, but not like this.
Chart.defaults.global.elements.rectangle.borderWidth = 2;//AHA, okay this one worked.

function makeChart() {
    console.log("CanvasID: " + canvasID);
    var i;
    for (i = 0; i < answerLabels.length; i++)
    {
        answerLabels[i] = answerLabels[i].replace("&#x27;", "'");
        console.log("Answer " + i.toString() + ": " + answerLabels[i]);
    }
    var canvasObj = document.getElementById(canvasID.toString());
    var ctx = canvasObj.getContext('2d');
    ctx.canvas.height = answerLabels.length * 20;
    console.log("Setting canvas height: " + ctx.canvas.height);
    var myChart = new Chart(ctx, {
        type: 'horizontalBar',
        data: {
            labels: answerLabels,
            datasets: [{
                label: '# of Votes',
                data: answerCounts,
                backgroundColor: answerBGColors,
                borderColor: answerBGColors
            }]
        },
        options: {
            scales: {
                xAxes: [{
                    ticks: {
                        beginAtZero: true,
                        stepSize: 1
                    }
                }]
            }
        }
    });
}

