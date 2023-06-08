
//======================================== tạo array lấy dữ liệu chưa làm ======================================
let IdNV;
let NameNV;
let gender;
let Address;
let User;
let Password;
let objDS = {
    IdNV: IdNV,
    NameNV: NameNV,
    gender: gender,
    Address: Address,
    User: User,
    Password: Password

}
//======================================== Edit cũ dùng lấy id =============================================


//function confEdit(id) {

//    console.log(event.target);
//    console.log(id);
//    document.getElementById("edit1").value = id;
//    let willEdit = confirm("Bạn có chắc chắn muốn xóa nhân viên" + results + "không?");
//    if (willEdit) {
//        let url = '@Html.Raw(Url.Action("Edit", new { Id = "_Id" }))';
//        url = url.replace("_Id", results)
//        window.location.href = url;
//    }
//}

//================================================ Delete ================================================

function confDeleteModal() {

    $("#Deleted").submit();

}

function confDelete() {

    let galleryModal = new bootstrap.Modal(document.getElementById('exampleModal'), {
        keyboard: false
    });
    //===================== lấy data(id) từ check box ======================
    let checkbox = document.getElementsByName('delete');
    let result = [];
    for (var i = 0; i < checkbox.length; i++) {
        if (checkbox[i].checked === true) {
            result.push(checkbox[i].value);
        }
    }
    document.getElementById('deleteList').value = result.join();
    let t = result.join();
    let deleteModalBody = document.getElementById('deleteModalBody');
    deleteModalBody.innerHTML = t;
    
    galleryModal.show();
}
//=========================== button setting================================
function confSetting() {
    $(".deleteEdit").show();
    $("#btnCreateDelete").show();
    $('#btnSetting').attr('class', 'd-none');


    //let th = document.createElement("th");
    //let td = $("<td>");
    //let input = $("<input>");
    ////=================== thẻ input =========================================
    //$(input).attr('name', 'deleted');
    //$(input).attr('id', 'delete');
    //$(input).attr('type', 'checkbox');

    //$(input).attr('value', '@staff.Id_nv');
    //$(td).append(input);

    ////=================================================================================
    //td.attr('id', 'checkBoxDelete');

    //$('#popertyTableTh').append(th);
    //$('.popertyTableTd').append(td);


    //$(th).attr('id', 'propertyTh');

    //th.innerHTML = "delete";
   
    
    
    //document.getElementById("btnCreateDelete").style.display = "block";
   // document.getElementByID("btnSetting").style.display = "none" ;
   


}

