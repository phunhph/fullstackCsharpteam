function conf() {

    $("#Deleted").submit();

}
//====================================================================== Edit ======================================



function conf1(id) {

    console.log(event.target);
    console.log(id);
    document.getElementById("edit1").value = id;
    let willEdit = confirm("Bạn có chắc chắn muốn xóa nhân viên" + results + "không?");
    if (willEdit) {
        let url = '@Html.Raw(Url.Action("Edit", new { Id = "_Id" }))';
        url = url.replace("_Id", results)
        window.location.href = url;
    }
}
function conf2() {

    let galleryModal = new bootstrap.Modal(document.getElementById('exampleModal'), {
        keyboard: false
    });

    let checkbox = document.getElementsByName('deleted');
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

function confSetting() {
    document.getElementById("propertyDelete").style.display = "block";
    document.getElementById("checkBoxDelete").style.display = "block";
    
}

