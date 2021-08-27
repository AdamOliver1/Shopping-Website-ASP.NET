$(document).ready(function () {
    $('#btnAddProduct').click(function () {
        event.preventDefault();
        var isOk = true;      
        $(".inputText").each(function () {
                var input = $(this);
                if (input.val() == null || input.val().trim() == "") isOk = false;
            });
        var priceVal = $('input[type=number]').val();
        if (priceVal == null || !$.isNumeric(priceVal)) isOk = false;
        if (isOk) {
            if (confirm("Are you sure you want to add this product?")) {
                alert("You just added a new product!")
                $(this).closest('form').submit();
            }           
        }
        else { 
            alert("One or more of the fields are not valid!");
            $(this).closest('form').submit();
        }       
    });
});






