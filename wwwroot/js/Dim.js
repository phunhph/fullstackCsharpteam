// sự kiện time in\
var date = new Date();
var lastMinute = -1;
document.getElementById("in").onclick = timeIn;
//let a = @HttpContext.Request.Cookies["id_nv"]
//console.log( a );
function timeIn() {
    date = new Date();
    // Kiểm tra ngày
    if (date.getDate() === lastMinute) {
        return; // Không thêm thông tin vào bảng
    }
    // tạo vị trí và đối tượng spon thông tin
    let tbody = document.getElementById("tbody");
    let tr = document.createElement("tr");
    let ID = document.createElement("th");
    let textID = document.createElement("input");
    let ID_NV = document.createElement("th");
    let textID_NV = document.createElement("input");
    let NAME = document.createElement("th");
    let textNAME = document.createElement("input");
    let TIMEIN = document.createElement("th");
    let DateIn = document.createElement("input");
    let TIMEOUT = document.createElement("th");
    var DateOut = document.createElement("input");
    // đưa thông tin
    textID.value = date.getFullYear() + "/" + date.getMonth() + "/" + date.getDate();
    textID_NV.value = "NV1";
    textNAME.value = "name1";
    DateIn.value =
        date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
    // đưa dữ liệu ra ngoài
    tbody.append(tr);
    tr.append(ID);
    ID.append(textID);
    tr.append(ID_NV);
    ID_NV.append(textID_NV);
    tr.append(NAME);
    NAME.append(textNAME);
    tr.append(TIMEIN);
    TIMEIN.append(DateIn);
    tr.append(TIMEOUT);
    TIMEOUT.append(DateOut);
    // chèn id
    tr.id = date.getDate();
    ID.id = "ID" + date.getDate();
    ID_NV.id = "ID_NV" + date.getDate();
    NAME.id = "NAME" + date.getDate();
    TIMEIN.id = "TIMEIN" + date.getDate();
    DateOut.id = "TIMEOUT" + date.getDate();
    lastMinute = date.getDate(); // Lưu trữ ngày điểm danh
    // tạo name
    textID.name = "id"
    textID_NV.name = "id_nv"
    textNAME.name = "name"
    DateIn.name = "timein"
    DateOut.name="timeout"
}
// sự kiên time out
document.getElementById("out").onclick = timeout;
function timeout() {
    date = new Date();
    let tbody = document.getElementById("tbody");
    let tr = document.createElement("tr");
    let ID = document.createElement("th");
    let textID = document.createElement("input");
    let ID_NV = document.createElement("th");
    let textID_NV = document.createElement("input");
    let NAME = document.createElement("th");
    let textNAME = document.createElement("input");
    let TIMEIN = document.createElement("th");
    let DateIn = document.createElement("input");
    let TIMEOUT = document.createElement("th");
    var DateOut = document.createElement("input");
    // đưa thông tin
    textID.value = date.getFullYear() + "/" + date.getMonth() + "/" + date.getDate();
    textID_NV.value = "NV1";
    textNAME.value = "name1";
    DateIn.value =
        date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
    DateOut.value =
        date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
    console.log(date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds());
    // đưa dữ liệu ra ngoài
    tbody.append(tr);
    tr.append(ID);
    ID.append(textID);
    tr.append(ID_NV);
    ID_NV.append(textID_NV);
    tr.append(NAME);
    NAME.append(textNAME);
    tr.append(TIMEIN);
    TIMEIN.append(DateIn);
    tr.append(TIMEOUT);
    TIMEOUT.append(DateOut);
    // tạo name
    textID.name = "id"
    textID_NV.name = "id_nv"
    textNAME.name = "name"
    DateIn.name = "timein"
    DateOut.name = "timeout"
}
