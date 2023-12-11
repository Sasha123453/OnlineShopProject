var decreaseButtons = document.querySelectorAll("#decreaseamount");
var trackButtons = document.querySelectorAll("#tracking");
var orderButton = document.getElementById("showorder");
orderButton.addEventListener("click", GetReq);
trackButtons.forEach(function (button) {
    button.addEventListener("click", ToggleChecked);
});
function GetReq() {
    debugger;
    var url = "/cart/showorder?"
    ids.forEach(function (id) {
        url += "ids=" + id + "&";
    });
    window.location.href = url;
}
decreaseButtons.forEach(function (button) {
    button.addEventListener("click", function () {
        var productid = $(this).attr("productid");
        var success = get("/cart/decreaseamount?id=" + productid);
        if (success) {
            var closest = $(this).closest("tr");
            closest.find("#amount").html(parseInt(closest.find("#amount").html()) - 1);
            closest.find("#cost").html(parseInt(closest.find("#cost").html()) - parseInt(closest.find("#price").html()));
        }
    });
});
var increaseButtons = document.querySelectorAll("#increasemount");
increaseButtons.forEach(function (button) {
    button.addEventListener("click", function () {

        var id = $(this).attr("productid");
        var amountadd = parseInt($(this).attr("amount"));
        debugger;
        var success = get("/cart/addtocart?id=" + id);
        if (success) {
            var closest = $(this).closest("tr");
            closest.find("#amount").html(parseInt(closest.find("#amount").html()) + 1);
            closest.find("#cost").html(parseInt(closest.find("#cost").html()) + parseInt(closest.find("#price").html()));
            debugger;
        }
    });
});
function get(url) {
    return fetch(url, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json;charset=utf-8'
        }
    }).then(response => response.ok);
}
const ids = [];
function ToggleChecked(button) {
    var productId = $(this).attr("productid");
    debugger;
    this.classList.toggle("visible");
    if (this.classList.contains("checked")) {
        ids.pop(productId);
    } else {
        ids.push(productId);
    }
}