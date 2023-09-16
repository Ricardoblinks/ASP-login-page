function SomeFunction(ViewBagMessageValue) {

    var divZat = document.createElement("div");
    divZat.id = "zatemnenie";

    var divOkno = document.createElement("div");
    divOkno.id = "okno";
    divOkno.innerText = ViewBagMessageValue;

    divZat.appendChild(divOkno);

    var br = document.createElement("br");

    divOkno.appendChild(br);

    var a = document.createElement("a");
    a.href = "https://localhost:44386/";
    a.className = "close";
    a.innerHTML = "Закрыть окно";
    a.id = "close";

    divOkno.appendChild(a);

    var main = document.getElementsByClassName("form-container")[0];

    main.appendChild(divZat);

    if (divOkno.innerText.indexOf("ResultCode") == -1)
        $('#okno').css('border', '5px solid #00ff00');
    else
        $('#okno').css('border', '5px solid #ff0000');

    $('#zatemnenie').css('display', 'block');
}