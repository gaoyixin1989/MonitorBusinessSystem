

var login = document.getElementById("login");
var yhzc = document.getElementById("yhzc");
var h2 = login.getElementsByTagName("h2");
var dl = login.getElementsByTagName("dl");
var ul = login.getElementsByTagName("ul");
var lis = ul[0].getElementsByTagName("li");
var allspan = ul[0].getElementsByTagName("span");
var qlog = h2[0].getElementsByTagName("a");
var divs = login.getElementsByTagName("div");
var oldClickID = "pan";

qlog[0].onclick = function () {
    for (var i = 0; i < divs.length; i++)
    { divs[i].className = null; }
    for (var i = 0; i < allspan.length; i++)
    { allspan[i].id = null; }
    lis[0].className = "none";
    dl[0].className = "qlogin block";
}
for (var x = 1; x < 20; x++)
{ show(); }
function show() {
    var test = "btypepan" + x;
    if (x < 10)
    {x = "0" + x;}
    var btype = document.getElementById("btypepan" + x);
    if (btype) {
        var as = btype.getElementsByTagName("a");
        var bdivs = btype.getElementsByTagName("div");
        var spans = btype.getElementsByTagName("span");

        for (var i = 0; i < spans.length; i++) {
            spans[i].num = i;
            //���¶��ַ�ʽ������ݿͻ���Ҫ��ѡ�����������ƶ��¼����뿼���ں����м���ʱ���룬�������
//            spans[i].onmouseover = type_onmouseover; //�����֮��
//            spans[i].onmouseout = type_onmouseout; //����ƿ�
            spans[i].onmouseup = type_onmouseover;//�����
        }
    }



    function type_onmouseover() {
        for (var i = 0; i < lis.length; i++)
        { lis[i].className = null; }
        for (var i = 0; i < allspan.length; i++)
        { allspan[i].id = null; }
        for (var i = 0; i < divs.length; i++)
        { divs[i].className = null; }

        if (oldClickID == this.num) {
            oldClickID = "pan";
            return;
        }
        else {
            oldClickID = this.num;
        }
        dl[0].className = "qlogin";
        yhzc.id = null;
        spans[this.num].id = spans[this.num].className;
        if (bdivs[this.num]) {
            bdivs[this.num].className = "block"; 
            btype.className = "hoverli";
        }
    }

    function type_onmouseout() {
        for (var i = 0; i < lis.length; i++)
        { lis[i].className = null; }
        for (var i = 0; i < allspan.length; i++)
        { allspan[i].id = null; }
        for (var i = 0; i < divs.length; i++)
        { divs[i].className = null; }
    }

}