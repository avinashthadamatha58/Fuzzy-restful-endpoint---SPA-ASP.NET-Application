var n = -1;

$(function () {
    $('#tblCustomers').footable();
});

var set = null;

function openModal(eventid, imageid) {
    var imgid = "id_" + eventid + "_" + imageid;
    var ename = "EName_" + eventid;
    ename = document.getElementById(ename).innerHTML;
    var comments = "EComments_" + eventid;
    comments = document.getElementById(comments).innerHTML;
    var date = "EDate_" + eventid;
    date = document.getElementById(date).innerHTML;
    if (set != null) {
        clearInterval(set);
    }
    set = setInterval(function () { execute(imgid) }, 300);

    $('.form-eventid').text(eventid);
    $('.form-control').text(ename);
    $('.form-eventComments').html(comments);
    $('.form-eventDate').text(date);
    $('#addPage').modal('show');
}

function execute(imgid) {
    var ig = document.getElementById(imgid).src;
    if (ig != "images/load.gif") {
        $('.image').attr('src', ig);
        clearInterval(set);
    }
    if (ig == "data:image/png;base64,") {
        $('.image').attr('src', "images/load.gif");
        clearInterval(set);
    }
}

function saveData(eventID) {
    var checkBoxID = this.event.target.id;
    document.getElementById(checkBoxID).disabled = true;
    var checkbox_status = this.event.target.checked;
    $.ajax
        ({
            type: "PUT",
            url: "http://localhost:49318/HelloApi",
            async: true,
            data: JSON.stringify({ "eventid": eventID, "checkboxid": checkBoxID, "checkboxstatus": checkbox_status }),
            contentType: "application/json",

            success: function (result) {
                document.getElementById(result).disabled = false;

            }
        });
}

var executeAjax = function (eID, iID) {
    n = n + 1;
    var p = "rptCustomers_CheckBox1_" + n;
    $(p).data("id", n);
    $.ajax
        ({
            type: "GET",
            url: "http://localhost:49318/HelloApi?paramOne=" + eID + "&paramTwo=" + iID,
            async: true,

            success: function (result) {
                var resultarray = result.split(",");

                var id = "id_" + iID;

                var final = "data:image/png;base64," + resultarray[0];

                document.getElementById('id_' + eID + '_' + iID).setAttribute('src', final);

                document.getElementById('id1_' + eID + '_' + iID)
                    .setAttribute(
                    'src', final
                    );
            }
        });

    if (isPostBack) {

    }
    else {
        var url = "&foo2=" + p + "&foo3=dummy"
        $.ajax
            ({
                type: "GET",
                url: 'http://localhost:49318/HelloApi?foo=' + eID + url,
                async: true,

                success: function (result) {
                    var resultarray = result.split(",");
                    var checkID = resultarray[0];
                    var boolret = resultarray[1];
                    var finalbool = result;
                    if (boolret === "True") {
                        document.getElementById(checkID).checked = true;
                        document.getElementById(checkID).disabled = false;
                    }
                    else {
                        document.getElementById(checkID).checked = false;
                        document.getElementById(checkID).disabled = false;
                    }
                }

            });
    }
}