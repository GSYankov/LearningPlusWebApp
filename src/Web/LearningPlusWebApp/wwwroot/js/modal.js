document.querySelectorAll(".gallery li").forEach(function (li, idx) {
    li.addEventListener("click", onclickPhoto);
})
document.querySelectorAll(".modal")[0].addEventListener("click", onclickModal);

function onclickPhoto(e) {
    let modalLink = e.srcElement.currentSrc.replace("w=350&q=60", "w=2350&q=60");
    document.querySelectorAll(".modal .inner span img")[0].src = modalLink;
    document.querySelector(".modal").style.display = "flex";
}

function onclickModal() {
    document.querySelector(".modal").style.display = "none"
}


