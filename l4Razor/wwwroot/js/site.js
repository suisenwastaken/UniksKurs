function showdata(){
    if(document.getElementById('TypeSelect').value == 1){
        document.getElementById('DataSelect').style.display = "flex";
    }

    if(document.getElementById('TypeSelect').value == 0){
        document.getElementById('DataSelect').style.display = "none";
    }
}

const imgArray = new Array();

imgArray[0] = new Image();
imgArray[0].src = 'css/images/Schedule/Айрин.jpg';

imgArray[1] = new Image();
imgArray[1].src = 'css/images/Schedule/Архив.jpg';

imgArray[2] = new Image();
imgArray[2].src = 'css/images/Schedule/Ода.jpg';

imgArray[3] = new Image();
imgArray[3].src = 'css/images/Schedule/ТанцКласс.jpg';

imgArray[4] = new Image();
imgArray[4].src = 'css/images/Schedule/Театр.jpg';

let NameOfPlace;

function showimage(sel){

    id = document.getElementById("SelectImage").value;
    const img = imgArray[id]
    img.width = 900;
    img.height = 480;
    document.getElementById("ImageBlock").textContent = "";
    document.getElementById("ImageBlock").appendChild(img);

    let NameOfPlace = sel.options[id - (-1)].text;
    console.log(NameOfPlace);

    let HeadText = document.getElementById("HeadText");
    HeadText.innerHTML = "Расписание залов - " + NameOfPlace;
}

function ChangeIcon(){

    let seed = String((Math.random() + 1).toString(36).substring(7));
    let PictureString = 'https://api.dicebear.com/6.x/big-smile/svg?seed=' + seed + '&scale=110&radius=50&backgroundColor=b6e3f4';
    document.getElementById("ProfileImage").src = PictureString;
    console.log(seed)
    document.getElementById("PictureUrl").value = PictureString;
}