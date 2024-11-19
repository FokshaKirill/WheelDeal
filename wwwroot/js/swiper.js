const carouselItems = document.querySelectorAll(".carousel_item");
let i = 1;

setInterval(() => {
// Accessing All the carousel Items
    Array.from(carouselItems).forEach((item,index) => {

        if(i < carouselItems.length){
            item.style.transform = `translateX(-${i*153}%)`
        }
    })

    if(i < carouselItems.length-5){
        i++;
    }
    else{
        i=0;
    }
},3000)
