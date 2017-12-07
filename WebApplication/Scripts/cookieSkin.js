try {
    var skin = $.cookie("skin");
    $("#switchSkin").attr("href", "/view/CSS/Skins/" + skin + "/search.css");
    $("#switchSkinControl").attr("href", "/view/CSS/Skins/" + skin + "/control.css");
    
} catch (e) { }