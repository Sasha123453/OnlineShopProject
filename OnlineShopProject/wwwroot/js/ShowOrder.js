const ids = [];
$(document).ready(function () {
    debugger;
    const params = new URLSearchParams(window.location.search);
    for (var value of params.getAll("ids")) {
        debugger;
        ids.push(value);
    }
});
var decreaseButtons = document.querySelectorAll("#decreaseamount");
var orderButton = document.getElementById("showorder");
orderButton.addEventListener("click", GetReq);
function GetReq() {
    debugger;
    var url = "/cart/createorder?"
    ids.forEach(function (id) {
        url += "ids=" + id + "&";
    });
    window.location.href = url;
}
decreaseButtons.forEach(function (button) {
    ids.push($(this).attr("productid"));
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
async function ShowPage() {
    try {
        debugger;
        let response = await fetch("/cart/createorder", {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(ids)
        });
        document.body.innerHTML = await response.text();
    } catch (err) {
        console.log('Fetch error:' + err);
    }
}
