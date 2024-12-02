﻿$(document).ready(function (e) {
    GetAll();
    $(".select2").select2();
    $("#formSupplier").select2({
        dropdownParent: $("#staticBackdrop")
    });
});

var pageNumber = 1;
var totalPage = 0;
var modelID = 0;
var htmlMaterial = "";
var _attachFiles = [];
var lstFileDeleted = [];
$('#request').keydown(function (e) {
    if (e.keyCode == 13) {
        pageNumber = 1;
        GetAll();
    }
})
function previousPage() {
    if (pageNumber > 1) {
        pageNumber -= 1;
        GetAll();
    }
}
function nextPage() {
    if (pageNumber < totalPage) {
        pageNumber += 1;
        GetAll();
    }
}
$('#pageSize').change(function () {
    pageNumber = 1;
    GetAll();
});

$('#groupId').change(function () {
    pageNumber = 1;
    GetAll();
});

function Pagination() {
    var html = `<li class="page-item"> <a onclick="previousPage()" class="page-link cusor" aria - label="Previous" > <span aria-hidden="true">&laquo;</span> </a > 
                </li > `;
    if (totalPage == 1) {
        html += `<li class="page-item"><a class="page-link" style="background-color: aliceblue;">${pageNumber}</a></li>`
    }
    else if (pageNumber == 1) {
        html += `<li class="page-item"><a class="page-link" style="background-color: aliceblue;">${pageNumber}</a></li>
            <li class="page-item cusor"><a onclick="nextPage()" class="page-link">${pageNumber + 1}</a></li>`;
    }
    else if (pageNumber == totalPage) {
        html += `<li class="page-item cusor"><a onclick="previousPage()"  class="page-link">${pageNumber - 1}</a></li>
            <li class="page-item"><a style="background-color: aliceblue;" class="page-link">${pageNumber}</a></li>`;
    }
    else {
        html += `<li class="page-item cusor"><a onclick="previousPage()" class="page-link">${pageNumber - 1}</a></li>
            <li class="page-item"><a class="page-link" style="background-color: aliceblue;" >${pageNumber}</a></li>
            <li class="page-item cusor"><a onclick="nextPage()" class="page-link">${pageNumber + 1}</a></li>`;
    }
    html += `<li class="page-item cusor">
            <a onclick="nextPage()" class="page-link" aria-label="Next">
                <span aria-hidden="true">&raquo;</span>
            </a>
         </li>`;

    $('#pagination').html(html);
}

$('#btn_search').click(function () {
    pageNumber = 1;
    GetAll();
})


function showModal() {
    $('#staticBackdrop').modal('show');
    if (modelID == 0) {
        $('#btn_deleteModal').hide();
    }
}

function CloseModal() {
    $("#formUnit").val(0);
    modelID = 0;
    document.getElementById('form').reset();
    $('#staticBackdrop').modal('hide');
}

$('#add_new').click(function () {
    $('#staticBackdropLabel').text("Thêm Phiếu nhập");
    showModal();
})
$('#btn_deleteModal').click(function () {
    DeleteById(modelID);
});

function GetAll() {

    let dataRequest = {
        Request: $('#request').val(),
        PageNumber: parseInt(pageNumber),
        AccountID: parseInt($('#accountID').val()) ,
        DateStart: $('#dateStart').val(),
        DateEnd: $('#dateEnd').val(),
    }
    $.ajax({
        url: "/Admin/GoodsReceipt/GetAll",
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify(dataRequest),
        success: function (data) {
            console.log(data);
            var html = '';
            //$.each(data.data, function (index, item) {
            //    html += `<tr class="align-middle">
            //                <td scope="col" class="align-center text-center" style="white-space: nowrap">
            //                    <button class="btn btn-sm btn-primary" onclick="GetById(${item.Id})" ><i class="bi bi-pencil-square"></i> Sửa</button>
            //                    <button class="btn btn-sm btn-danger" onclick="DeleteById(${item.Id})"><i class="bi bi-trash3"></i> Xóa</button>
            //                </td>
            //                <td class="" class="text-center"> <a style="color: blue;  cursor: pointer;" onclick="GetById(${item.Id})">${item.SupplierCode} </a></td>
            //                <td class="">${item.SupplierName}</td>
            //                <td class="">${item.PhoneNumber}</td>
            //                <td class="">${item.Decription}</td>
            //                <td class="text-center">${moment(item.CreatedDate).format("DD/MM/YYYY")}</td>
            //                <td class="">${item.CreatedBy}</td>
            //            </tr>`;
            //})
            //let total = Math.ceil(data.totalCount[0].TotalCount / 10);
            //totalPage = total > 0 ? total : 1;
            //$('#tbody').html(html);
            //$('#page_details').text(`Trang ${pageNumber} / ${totalPage}`);
            //$('#pageNumber').val(pageNumber);
            //Pagination();
        },

        error: function (err) {
            alert(err.responseText);
        }
    });
}


function GetById(id) {
    $('#btn_deleteModal').show();
    $('#staticBackdropLabel').text("Cập nhật Phiếu nhập");
    modelID = id;
    let _url = "/Admin/Supplier/GetById";
    $.ajax({
        url: _url,
        type: 'GET',
        dataType: 'json',
        data: {
            Id: id
        },
        contentType: 'application/json',
        success: function (data) {
            $('#formCode').val(data.supplierCode);
            $('#formName').val(data.supplierName);
            $("#formPhoneNumber").val(data.phoneNumber);
            $("#formDecription").val(data.decription);
        },
        error: function (err) {
            MessageError(err.responseText);
        }
    });
    showModal();
}
function CreateOrUpdate() {
    let isValid = true;
    var obj = {
        Id: parseInt(modelID),
        UnitId: parseInt($("#formUnit").val()),
        MaterialCode: $("#formCode").val(),
        MaterialName: $("#formName").val(),
        MinQuantity: parseInt($("#formMinQuantity").val()),
        Decription: $("#formDecription").val()
    };

    if (obj.MaterialCode == "") {
        alert("Vui lòng nhập Mã nguyên liệu!");
        isValid = false;
    }
    else if (obj.MaterialName == "") {
        alert("Vui lòng nhập Tên nguyên liệu!");
        isValid = false;
    }
    else if (obj.UnitId <= 0) {
        alert("Vui lòng chọn Đơn vị!");
        isValid = false;
    }
    else if (obj.MinQuantity == "") {
        alert("Vui lòng nhập Số lượng tối thiểu!");
        isValid = false;
    }

    if (isValid) {
        let _url = "/Admin/Material/CreateOrUpdate";
        $.ajax({
            type: 'POST',
            url: _url,
            contentType: 'application/json;charset=utf-8',
            data: JSON.stringify(obj),
            success: function (result) {
                if (result.status == 0) {
                    alert(result.statusText)
                }
                else {
                    CloseModal();
                    GetAll();
                }
            },
            error: function (err) {
                alert(err.responseText);
            }
        });
    }
}
function DeleteById(id) {
    if (confirm("Bạn có chắc chắn muốn thực hiện thao tác này?") == true) {
        let _url = "/Admin/Supplier/Delete";
        $.ajax({
            type: 'GET',
            url: _url,
            contentType: 'application/json;charset=utf-8',
            data: {
                Id: id,
            },
            success: function (result) {
                if (result.status == 1) {
                    CloseModal();
                    pageNumber = 1;
                    GetAll();
                }
                else alert(result.message);

            },
            error: function (err) {
                alert(err.responseText);
            }
        });
    }
}



//==================================================================================================================
function GetAllMaterial() {
    $.ajax({
        type: 'GET',
        url: "/Admin/ProductSize/GetAllNoPage",
        contentType: 'application/json;charset=utf-8',
        data: {},
        success: function (result) {
            htmlSize = `<option value="0" disabled selected hidden>Chọn nguyên vật liệu</option>`;
            result.forEach(e => {
                htmlSize += `<option value="${e.id}">${e.sizeName}</option>`;
            })
        }, error: function (err) {
            MessageError(err.responseText);
        }
    });
}

function addRow() {
    let html = `<tr class="sortable product_details_item">
                <th scope="col">
                       <select class="form-select productSizeId select2-material">
                                   ${htmlMaterial}    
                       </select>
                </th>
                <th scope="col">
                        <input type="text" class="form-control productPrice" placeholder="Giá tiền" oninput="formatMoney(event)">
                </th>
                <td class="text-center" scope="col">VNĐ</th>
                <th scope="col" style="text-align:center;vertical-align: middle;"><button  onclick="deleteRow(event)" class="btn btn-danger"><i class="bi bi-trash"></i></th>
            </tr>`;
    $('#tbodySteps').append(html);


    $(".select2-material").select2({
        dropdownParent: $("#staticBackdrop")
    });
}
function deleteRow(event) {
    if (confirm("Bạn có chắc muốn xóa dòng này không?")) {
        let targetElement = $(event.target);
        targetElement.closest("tr").remove();
    }
}
function formatMoney(event) {
    let value = $(event.target).val();
    value = value.replace(/[^0-9]/g, '');
    let lastValue = value.replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    $(event.target).val(lastValue);
}
function UploadFile(productId) {
    try {
        var filedata = new FormData();
        if (_attachFiles.length > 0) {
            $.each(_attachFiles, function (key, item) {
                filedata.append(key, item);
            })

            $.ajax({
                url: '/Admin/Product/UploadFile?Id=' + productId,
                type: 'POST',
                dataType: 'json',
                data: filedata,
                processData: false,
                contentType: false,
                success: function (result) {
                    if (parseInt(result.status) == 1) {
                        _attachFiles = [];
                        $("#AttachFiles").html('');
                    } else {
                        alert(result.message);
                    }
                },

                error: function (err) {
                    alert(err.responseText);
                }
            });
        }

    } catch (e) {
        alert(e);
    }
}

async function onSelectedFile(event) {

    var html = '';
    var fileSelecteds = $('input[name="AttachFiles"]').get(0).files;
    $.each(fileSelecteds, function (i, file) {
        _attachFiles.push(file);
    })

    $.each(_attachFiles, function (key, item) {
        html += `<p class="m-0 px-1 text-nowrap text-dark a-product-details">${item.name}<span class="text-danger icon-x-span" onclick="return onRemoveFile(${key},0)"><i class="bi bi-x"></i></span></p>`;

    })
    $('#AttachFiles').html(html);
}
function onRemoveFile(index, fileID) {
    if (confirm("Bạn có chắc muốn xóa ảnh này không?")) {
        _attachFiles.splice(index, 1);
        var html = '';
        $.each(_attachFiles, function (key, item) {
            html += `<p class="m-0 px-1 text-nowrap text-dark a-product-details">${item.name}<span class="text-danger icon-x-span" onclick="return onRemoveFile(${key},${item.id})"><i class="bi bi-x"></i></span></p>`;
        });
        $('#AttachFiles').html(html);
    }
    let idFile = parseInt(fileID);
    if (idFile > 0) lstFileDeleted.push(idFile);

}