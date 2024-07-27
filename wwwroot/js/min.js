
function AddNewItems() {

    $("#newproducts").modal("show");
}

function EditItems() {

    updateProduct();

    $("#editproducts").modal("show");
}

function updateProduct() {
    var select = document.getElementById('product');
    var selectedOption = select.options[select.selectedIndex];
    var name = selectedOption.getAttribute('data-name');
    var description = selectedOption.getAttribute('data-description');

    document.getElementById('edititem').value = name;
    document.getElementById('editdescription').value = description;
}

function ShowDelMessage(id) {

    gid = id;
    $("#confirm").modal('show');
}

function CnfirmDel() {

    $.ajax({
        type: "POST",
        url: "/Home/DeleteItem",
        data: { record_no: gid },
        success: function (result) {
            window.location.href = "/Home/AddNewItems"
        }
    });
}
}