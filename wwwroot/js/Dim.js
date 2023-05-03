
var date = new Date();

const textID = document.createElement("input");;
let DateIn = document.createElement("input");
var DateOut = document.createElement("input");
//nhận dữ liệu
textID.value = date.getFullYear() + "/" + (date.getMonth()+1) + "/" + date.getDate();
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
document.getElementsByTagName("form")[0].append(textID);
document.getElementsByTagName("form")[0].append(DateIn);
document.getElementsByTagName("form")[0].append(DateOut);

// lich
/*const date = new Date();*/

const renderCalendar = () => {
    // thiết lập ngày đầu tiên của tháng
    date.setDate(1);
    // lấy DOM để đưa gia trị ra ngoài
    const monthDays = document.querySelector(".days");

    // lấy ngày cuối cùng của tháng hiện tại
    const lastDay = new Date(
        date.getFullYear(),
        date.getMonth() + 1,
        0
    ).getDate();
    // lấy ngày cuối cùng của tháng trước đó
    const prevLastDay = new Date(
        date.getFullYear(),
        date.getMonth(),
        0
    ).getDate();
    // lấy thứ trong tuần của ngày đầu tiên trong tháng hiện tại
    const firstDayIndex = date.getDay();

    // lất thứ trong tuần cho ngày cuối cùng của tháng hiện tại
    const lastDayIndex = new Date(
        date.getFullYear(),
        date.getMonth() + 1,
        0
    ).getDay();

    const nextDays = 7 - lastDayIndex - 1;

    const months = [
        "Tháng 1",
        "Tháng 2",
        "Tháng 3",
        "Tháng 4",
        "Tháng 5",
        "Tháng 6",
        "Tháng 7",
        "Tháng 8",
        "Tháng 9",
        "Tháng 10",
        "Tháng 11",
        "Tháng 12",
    ];
    // hiển thị ngày tháng ở trên lịch
    document.querySelector(".content h1").innerHTML = months[date.getMonth()];
    document.querySelector(".content p").innerHTML = new Date().toDateString();
    let days = "";
    // show ngày tháng trước
    for (let x = firstDayIndex; x > 0; x--) {
        days += `<div id="${x + "a"}" class="previous-day">${prevLastDay - x + 1
            }    </div>`;
    }

    // show ngày trong tháng
    for (let i = 1; i <= lastDay; i++) {
        if (
            i === new Date().getDate() &&
            date.getMonth() === new Date().getMonth()
        ) {
            days += `<div id="${i}" class="today">${i}</div>`;
        } else if (
            i < new Date().getDate() &&
            date.getMonth() === new Date().getMonth()
        ) {
            days += `<div id="${i}">${i}</div>`;
        } else {
            days += `<div id="${i}">${i}</div>`;
        }
        
    }

    // show ngày tháng sau 
    for (let j = 1; j <= nextDays; j++) {
        days += `<div class="next-days">${j}</div>`;
        monthDays.innerHTML = days;
    }
    checkDay();
};

function checkDay() {
    const roll = new Array();
    list.forEach(element => {
        let coll = new Date(element.id);
        console.log(coll.getFullYear() + "/" + coll.getMonth() + "/" + coll.getD());
        roll.push(coll);
    });
   
    // lấy thứ trong tuần của ngày đầu tiên trong tháng hiện tại
    const firstDayIndex = date.getDay();
    // lấy ngày cuối cùng của tháng hiện tại
    const lastDay = new Date(
        date.getFullYear(),
        date.getMonth() + 1,
        0
    ).getDate();
    // show ngày tháng trước đã điểm danh
    for (let x = firstDayIndex; x > 0; x--) {
        roll.forEach(element => {
            if (element.getMonth() < date.getMonth() && element.getDate() == document.getElementById(x + "a").innerText)  {
                document.getElementById(x + "a").style.backgroundColor = "red";
                document.getElementById(x + "a").style.border = "1px solid gray";
            }
        });
    }
    
    // ngày trong tháng đã điểm danh
    for (let i = 1; i <= lastDay; i++) {
        roll.forEach(element => {
            // today
            if (
                i === new Date().getDate() &&
                date.getMonth() === element.getMonth()
            ) {
                document.getElementById(element.getDate()).style.backgroundColor = "red";
            } else if (//yesterday
                i < new Date().getDate() &&
                date.getMonth() === element.getMonth() &&
                i === element.getDay()
            ) { 
                document.getElementById(element.getDate()).style.backgroundColor = "red";
                document.getElementById(element.getDate()).style.border = "1px solid gray";
            } 
        });
    }
}
// sự kiện hiển thị ra tháng trước đó và tháng sau đó
document.querySelector(".prev").addEventListener("click", () => {
    date.setMonth(date.getMonth() - 1);
    renderCalendar();
});

document.querySelector(".next").addEventListener("click", () => {
    date.setMonth(date.getMonth() + 1);
    renderCalendar();
});

renderCalendar();
