let listLink = document.querySelectorAll('.shortLink')
for (let item of listLink) {
    item.onclick = function (e) {
        let t = e.target.getAttribute('longUrl')
        window.open(t);
    }
}