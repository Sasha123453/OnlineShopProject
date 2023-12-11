function addToCart(button) {
    productid = $(button).attr("productid");
    debugger;
    if (get("/cart/addtocart?id=" + productid)) {
        button.classList.add("incart");
    }
}
function addToComparsion(button) {
    productid = $(button).attr("productid");
    debugger;
    if (get("/comparsion/addtocomparsion?id=" + productid)) {
        button.classList.add("incomparsion");
    }
}
function addToFavorite(button) {
    productid = $(button).attr("productid");
    debugger;
    if (get("/favorite/addtofavorite?id=" + productid)) {
        button.classList.add("incomparsion");
    }
}
function get(url) {
    return fetch(url, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json;charset=utf-8'
        }
    }).then(response => response.ok);
}