const askUser = function () {
    if (confirm("Do You Want To Go To Your Shopping Cart?")) {
          $('.goToCart').val(true);
        //$('.AddToCartBtnIndex').replaceWith(' <a asp-controller="Home" asp-action="AddToCart" asp-route-Id="@item.Id" asp-route-goToCart="@true"   class="AddToCartBtnIndex btn bg - cart"> < i class="fa fa-cart-plus mr-2" ></i > Add to cart</a >');
        //$('.AddToCartBtnIndex').sumbit();
    }
}

$('#AddToCartBtn').click(askUser);

$('.AddToCartBtnIndex').click(askUser);


