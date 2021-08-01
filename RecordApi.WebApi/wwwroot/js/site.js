

$(document).ready(function () {

    fetch("api/records/name")
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error("Unable to get records.", error));

   
    $("#ddlSort").change(function () {
        fetch(`api/records/${this.value}`)
            .then(response => response.json())
            .then(data => _displayItems(data))
            .catch(error => console.error("Unable to get records.", error));
       
    });

    $("#btnAdd").click(function () {$("#modAdd").modal("show");});

    $("#btnComma").click(function () { _postItem("%2C"); });

    $("#btnPipe").click(function () {_postItem("%7C");});

    $("#btnSpace").click(function () {_postItem("%2A");});

    
});

function _displayItems(data) {
    
    const tBody = document.getElementById("tblRecordBody");
    tBody.innerHTML = "";

    

    data.forEach(item => {
        const tr = tBody.insertRow();

        const td1 = tr.insertCell(0);
        const textNode1 = document.createTextNode(item.lastName);
        td1.appendChild(textNode1);

        const td2 = tr.insertCell(1);
        const textNode2 = document.createTextNode(item.firstName);
        td2.appendChild(textNode2);

        const td3 = tr.insertCell(2);
        const textNode3 = document.createTextNode(item.email);
        td3.appendChild(textNode3);

        const td4 = tr.insertCell(3);
        const textNode4 = document.createTextNode(item.favoriteColor);
        td4.appendChild(textNode4);

        const td5 = tr.insertCell(4);
        const textNode5 = document.createTextNode(item.dateOfBirth);
        td5.appendChild(textNode5);

  
    });
}

function _postItem(delim) {
    

    let d = Date.parse($("#birthdate").val());

    
    
    const record = {
        lastName: $("#lastname").val(),
        firstName: $("#firstname").val(),
        email: $("#email").val(),
        favoriteColor: $("#favcolor").val(),
        dateOfBirth: $.format.date(d, "d-M-yyyy")
    }
     const body = JSON.stringify(record);

  
    
    $.ajax({
        type: "POST",
        url: `api/records?delimiter=${delim}`,
        
        data: JSON.stringify(record),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) { console.log(data); },
        error: function (errMsg) {
            console.log(errMsg);
        }
    });

    
    $("#lastname").val("");
    $("#firstname").val("");
    $("#email").val("");
    $("#favcolor").val("");
    $("#birthdate").val("");

    location.reload();


   
}
