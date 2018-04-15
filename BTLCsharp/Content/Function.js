
// Function cho nut close 

function closeBtnClicked() {
    alert("Xin chao ");
    var response = confirm("Are you want leave page and don't save your work !");
    if (response === true) {
        var url = '@Url.Action("Index","Home")';
        window.location.href = url;

    }
    return response;
}