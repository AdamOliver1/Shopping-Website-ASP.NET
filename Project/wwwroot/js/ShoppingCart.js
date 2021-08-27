$(document).ready(function () {
    $(".submitShoppingCart").click(function () {
        if (confirm("Are you sure you want to remove this product from your cart?")) {
            $(this).closest('form').submit();
        }
        else {
            $(this).prop('checked', true);           
        }
    });
});

$('#btnCheckOut').click(function () {
    alert("Congratulations! The purchase was made successfully")
});

