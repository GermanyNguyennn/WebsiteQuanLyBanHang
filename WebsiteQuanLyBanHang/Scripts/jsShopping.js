$(document).ready(function () {
    ShowCount();
    $('body').on('click', '.btnAddToCart', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quantity = 1;
        var total_quantity = $('#quantity_value').text();
        if (total_quantity !== '') {
            quantity = parseInt(total_quantity);
        }

        $.ajax({
            url: '/ShoppingCart/AddToCart',
            type: 'POST',
            data: { id: id, quantity: quantity },
            success: function (rs) {
                if (rs.Success) {
                    $('#checkout_items').html(rs.Count);
                    alert(rs.Mess);
                } else {
                    alert(rs.Mess);
                }
            },
            error: function () {
                alert("Đã có lỗi xảy ra khi thêm vào giỏ hàng!");
            }
        });
    });

    $('body').on('click', '.btnCheckout', function (e) {
        e.preventDefault();

        $.ajax({
            url: '/ShoppingCart/Checkout',
            type: 'GET',
            dataType: 'html',
            success: function (response) {
                try {
                    var res = JSON.parse(response);
                    if (res && res.Success === false) {
                        alert(res.Mess);
                    } else {
                        document.open();
                        document.write(response);
                        document.close();
                    }
                } catch (e) {
                    document.open();
                    document.write(response);
                    document.close();
                }
            },
            error: function () {
                alert("Đã có lỗi xảy ra khi thực hiện thanh toán!");
            }
        });
    });

    
    $('body').on('click', '.btnUpdate', function (e) {
        e.preventDefault();
        var id = $(this).data("id");
        var quantity = $('#quantity_' + id).val();
        Update(id, quantity);

    });

    $('body').on('click', '.btnDeleteAll', function (e) {
        e.preventDefault();
        var conf = confirm('Bạn có chắc muốn xóa hết sản phẩm trong giỏ hàng?');
        if (conf == true) {
            DeleteAll();
        }

    });

    $('body').on('click', '.btnDelete', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var conf = confirm('Bạn có chắc muốn xóa sản phẩm này khỏi giỏ hàng?');
        if (conf == true) {
            $.ajax({
                url: '/ShoppingCart/Delete',
                type: 'POST',
                data: { id: id },
                success: function (rs) {
                    if (rs.Success) {
                        $('#checkout_items').html(rs.Count);
                        $('#trow_' + id).remove();
                        LoadCart();
                    }
                }
            });
        }
    });
});

function ShowCount() {
    $.ajax({
        url: '/ShoppingCart/ShowCount',
        type: 'GET',
        success: function (rs) {
            $('#checkout_items').html(rs.Count);
        }
    });
}
function DeleteAll() {
    $.ajax({
        url: '/ShoppingCart/DeleteAll',
        type: 'POST',
        success: function (rs) {
            if (rs.Success) {
                LoadCart();
            }
        }
    });
}
function Update(id, quantity) {
    $.ajax({
        url: '/ShoppingCart/Update',
        type: 'POST',
        data: { id: id, quantity: quantity },
        success: function (rs) {
            if (rs.Success) {
                LoadCart();
            }
        }
    });
}

function LoadCart() {
    $.ajax({
        url: '/ShoppingCart/PartialItemCart',
        type: 'GET',
        success: function (rs) {
            $('#load_data').html(rs);
        }
    });
}