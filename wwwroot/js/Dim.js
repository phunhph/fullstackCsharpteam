
var date = new Date();

const textID = document.createElement("input");;
let DateIn = document.createElement("input");
var DateOut = document.createElement("input");
//nhận dữ liệu
textID.value = date.getFullYear() + "/" + date.getMonth() + "/" + date.getDate();
DateIn.value =
date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
DateOut.value =
date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
textID.name = "id"
DateIn.name = "timein"
DateOut.name = "timeout"
//ẩn
textID.style.display = "none";
DateIn.style.display = "none";
DateOut.style.display = "none";
// suất
console.log(DateIn)
document.getElementsByTagName("form")[0].append(textID);
document.getElementsByTagName("form")[0].append(DateIn);
document.getElementsByTagName("form")[0].append(DateOut);

