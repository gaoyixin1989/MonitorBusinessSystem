﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WF/CCFlowDesigner/Site.Master"
    AutoEventWireup="true" CodeBehind="Truck.aspx.cs" Inherits="CCFlow.WF.CCFlowDesigner.Truck" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="server">
    <link href="../Comm/Style/Table0.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Raphael/raphael.js" type="text/javascript"></script>
    <link href="../Scripts/easyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/easyUI/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <style type="text/css">
        #holder
        {
            width: 1000px;
            height: 600px;
            margin: auto;
            border: 1px dashed #c0c0c0;
        }
    </style>
    <script type="text/javascript">
        var jdata;
        var rFlow;
        var r;
        var arrs = [];
        var dragNode;
        var dragBorderPointStart;
        var dragTextPointStart;
        var dragTrackTextPointStart;

        //定义样式变量，可在此统一修改流程样式
        var STYLE_NODE_WIDTH = 50;
        var STYLE_NODE_HEIGHT = 50;
        var STYLE_NODE_BORDER_RADIUS = 5;
        //var STYLE_NODE_DEFAULT_ICON_PATH = '/ccflow5/ClientBin/NodeIcon/';
        var STYLE_NODE_DEFAULT_ICON_PATH = '/ClientBin/NodeIcon/';
        var STYLE_NODE_DEFAULT_ICON = 'Default.jpg';
        var STYLE_FIRST_NODE_BORDER_COLOR = 'green';
        var STYLE_END_NODE_BORDER_COLOR = 'red';
        var STYLE_NODE_BORDER_COLOR = 'black';
        var STYLE_NODE_BORDER_HOVER_COLOR = 'blue';
        var STYLE_NODE_BORDER_NORMAL_WIDTH = 1;
        var STYLE_NODE_BORDER_HOVER_WIDTH = 2;
        var STYLE_NODE_FONT_SIZE = 14;
        var STYLE_NODE_FORE_COLOR = 'none';
        var STYLE_NODE_HOVER_FORE_COLOR = 'blue';
        var STYLE_LABEL_FONT_SIZE = 12;
        var STYLE_LABEL_FORE_COLOR = 'none';
        var STYLE_LINE_COLOR = 'green';
        var STYLE_LINE_HOVER_COLOR = 'blue';
        var STYLE_LINE_WIDTH = 2;
        var STYLE_LINE_TRACK_COLOR = 'red';
        var STYLE_NODE_TRACK_FONT_SIZE = 14;
        var STYLE_NODE_TRACK_FORE_COLOR = 'none';
        //var DATA_USER_ICON_PATH = '/ccflow5/DataUser/UserIcon/';
        var DATA_USER_ICON_PATH = '/DataUser/UserIcon/';
        var DATA_USER_ICON_DEFAULT = 'Default.png';

        $(function () {
            $.ajax({
                type: "Post",
                contentType: "application/json;utf-8",
                url: "../Admin/XAP/WebService.asmx/GetFlowTrackJsonData",
                dataType: "json",
                data: "{fk_flow:'<%=this.FK_Flow %>',workid:'<%=this.WorkID %>'}",
                success: function (re) {
                    jdata = $.parseJSON(re.d);
                    if (!jdata.success) {
                        alert(jdata.msg);
                    }
                    else {
                        drawStart(jdata.datas);
                    }
                },
                error: function (re) {
                    alert(re.responseText);
                }
            });
        });

        function drawStart(datas) {
            /// <summary>加载时绘制流程图/轨迹</summary>
            /// <param name="datas" Type="Object">绘制的数据，包含flow/nodes/labels/dirs/tracks，均为Array</param>

            r = new Raphael('holder', 1000, 600);
            var rNode, rDir, rBorder, rImage, rText, rLabel, rPath;

            rFlow = new RFlow('<%=this.FK_Flow %>');

            //绘制节点
            var nodeBorderColor = STYLE_NODE_BORDER_COLOR;
            var startNodePosType = 0;
            var endNodePosType = getMaxInArray(datas.nodes, 'NodePosType');
            var nodeTracks;
            var nodeTrack;
            var isExist;
            $.each(datas.nodes, function () {
                //确定边框颜色
                switch (this.NodePosType) {
                    case startNodePosType:
                        nodeBorderColor = STYLE_FIRST_NODE_BORDER_COLOR;
                        break;
                    case endNodePosType:
                        nodeBorderColor = STYLE_END_NODE_BORDER_COLOR;
                        break;
                    default:
                        nodeBorderColor = STYLE_NODE_BORDER_COLOR;
                        break;
                }

                //检测轨迹数据，如果有，则将该节点图标换成处理人头像，人名绘制于节点图标的右侧
                nodeTracks = getTracksByFromNode(datas.tracks, this.NodeID);
                if (nodeTracks.length > 0) {
                    nodeTrack = getTrackForCurrNode(nodeTracks);
                }
                else {
                    nodeTrack = null;
                }

                //确定节点图标
                if (nodeTrack != null) {
                    this.Icon = DATA_USER_ICON_PATH + nodeTrack.EmpFrom + '.png';

                    isExist = checkUrl(this.Icon);
                    if (isExist == false) {
                        this.Icon = DATA_USER_ICON_PATH + DATA_USER_ICON_DEFAULT;
                    }
                }
                else {
                    if (this.Icon == null || this.Icon.length == 0) {
                        this.Icon = STYLE_NODE_DEFAULT_ICON_PATH + STYLE_NODE_DEFAULT_ICON;
                    }
                    else {
                        if (this.Icon.indexOf('.') == -1) {
                            this.Icon = STYLE_NODE_DEFAULT_ICON_PATH + this.Icon + '.png';
                        }
                        else {
                            this.Icon = STYLE_NODE_DEFAULT_ICON_PATH + this.Icon.substr(this.Icon.lastIndexOf('/') + 1);
                        }

                        isExist = checkUrl(this.Icon);
                        if (isExist == false) {
                            this.Icon = STYLE_NODE_DEFAULT_ICON_PATH + STYLE_NODE_DEFAULT_ICON;
                        }
                    }
                }

                //开始绘制，并把绘制后的对象存储起来以便下次使用
                rNode = new RNode(this.NodeID, this.Name, this.X - STYLE_NODE_WIDTH / 2, this.Y - STYLE_NODE_HEIGHT / 2);
                rNode.rBorder = r.rect(rNode.x, rNode.y, STYLE_NODE_WIDTH, STYLE_NODE_HEIGHT, STYLE_NODE_BORDER_RADIUS);
                rNode.rIcon = r.image(this.Icon, rNode.x + 1, rNode.y + 1, STYLE_NODE_WIDTH - 2, STYLE_NODE_HEIGHT - 2);
                rNode.rText = r.text(rNode.x + STYLE_NODE_WIDTH / 2, rNode.y + STYLE_NODE_HEIGHT + 10, this.Name);
                rNode.rBorderColor = nodeBorderColor;
                rNode.isTrackNode = nodeTrack != null;
                rNode.tracks = nodeTracks;
                rNode.currTrack = nodeTrack;

                //在节点右侧绘制流程轨迹中当前节点的处理人/处理时间
                if (rNode.isTrackNode) {
                    rNode.rTrackText = r.text(rNode.x + STYLE_NODE_WIDTH + 5, rNode.y + 25, rNode.currTrack.EmpFromT + '\n' + rNode.currTrack.RDT.substr(5, 11));
                    rNode.rTrackText.attr({ "stroke": STYLE_NODE_TRACK_FORE_COLOR, "font-size": STYLE_NODE_TRACK_FONT_SIZE, "text-anchor": "start" });
                    rNode.rTrackText.toFront();
                }

                rNode.rBorder.attr({ "stroke": nodeBorderColor, "stroke-width": STYLE_NODE_BORDER_NORMAL_WIDTH });
                rNode.rText.attr({ "stroke": STYLE_NODE_FORE_COLOR, "font-size": STYLE_NODE_FONT_SIZE });
                
                //增加节点图片的鼠标滑过/移开效果
                rNode.rIcon.hover(function () {
                    var node = rFlow.getNodeByRIconId(this.id);
                    if (node != null) {
                        node.rBorder.attr({ "stroke": STYLE_NODE_BORDER_HOVER_COLOR, "stroke-width": STYLE_NODE_BORDER_HOVER_WIDTH });
                        node.rText.attr({ "stroke": STYLE_NODE_HOVER_FORE_COLOR });

                        if (node.isTrackNode) {
                            node.rTrackText.attr({ "stroke": STYLE_NODE_HOVER_FORE_COLOR });

                            //组织节点的轨迹信息
                            var table = $('#tracktable');
                            table.empty();
                            table.append(
                                    '<tr>' +
                                    '   <td colspan="4" class="GroupTitle">轨迹信息</td>' +
                                    '</tr>');

                            $.each(node.tracks, function () {
                                table.append(
                                    '<tr>' +
                                    '   <td style="width:120px">' + this.RDT + '</td>' +
                                    '   <td style="width:80px">' + this.ActionTypeText + '</td>' +
                                    '   <td style="width:50px">' + this.EmpToT + '</td>' +
                                    '   <td>' + this.Msg + '</td>' +
                                    '</tr>');
                            });

                            var p = $(this.node).offset();
                            $('#trackinfo').offset({ top: p.top + this.attr('height') + 2, left: p.left });
                            $('#trackinfo').show();
                        }
                    }
                }, function () {
                    var node = rFlow.getNodeByRIconId(this.id);
                    if (node != null) {
                        node.rBorder.attr({ "stroke": node.rBorderColor, "stroke-width": STYLE_NODE_BORDER_NORMAL_WIDTH });
                        node.rText.attr({ "stroke": STYLE_NODE_FORE_COLOR });

                        if (node.isTrackNode) {
                            node.rTrackText.attr({ "stroke": STYLE_NODE_TRACK_FORE_COLOR });

                            $('#trackinfo').offset({ top: 0, left: 0 });
                            $('#trackinfo').hide();
                        }
                    }
                });

                rNode.rIcon.drag(iconMove, iconDrag, iconUp);

                rFlow.nodes.push(rNode);
            });

            //绘制节点的连线
            var fromNode, toNode;
            $.each(datas.dirs, function () {
                rDir = new RDirection(this.Node, this.ToNode, this.DirType, this.IsCanBack);
                rDir.FromNode = rFlow.getNode(this.Node);
                rDir.ToNode = rFlow.getNode(this.ToNode);

                //计算连线的起始点
                rDir.rPath = r.drawArr({
                    rStart: rDir.FromNode.rBorder,
                    rEnd: rDir.ToNode.rBorder,
                    pathColor: rDir.FromNode.isTrackNode && rDir.ToNode.isTrackNode ? STYLE_LINE_TRACK_COLOR : STYLE_LINE_COLOR
                });

                arrs.push(rDir.rPath);

                rFlow.dirs.push(rDir);
            });

            //绘制标签
            $.each(datas.labels, function () {
                rLabel = new RLabel(this.MyPK, this.Name, this.X, this.Y);
                rLabel.rText = r.text(this.X, this.Y, this.Name);
                rLabel.rText.attr({ "stroke": STYLE_LABEL_FORE_COLOR, "font-size": STYLE_LABEL_FONT_SIZE, "text-anchor": "start" });
                rLabel.rText.toFront();
            });
        }

        function iconDrag() {
            this.ox = this.attr("x");
            this.oy = this.attr("y");
            this.animate({ "fill-opacity": 0.5 }, 500);

            //记录与ICON绑定的其他对象的原始坐标
            dragNode = rFlow.getNodeByRIconId(this.id);
            dragBorderPointStart = { x: dragNode.rBorder.attr("x"), y: dragNode.rBorder.attr("y") };
            dragTextPointStart = { x: dragNode.rText.attr("x"), y: dragNode.rText.attr("y") };

            if (dragNode.isTrackNode) {
                dragTrackTextPointStart = { x: dragNode.rTrackText.attr("x"), y: dragNode.rTrackText.attr("y") };
            }
        }

        function iconMove(dx, dy) {
            var att = { x: this.ox + dx, y: this.oy + dy };
            this.attr(att);

            //动态修改与ICON绑定的其他对象的坐标
            dragNode.rBorder.attr({ x: dragBorderPointStart.x + dx, y: dragBorderPointStart.y + dy });
            dragNode.rText.attr({ x: dragTextPointStart.x + dx, y: dragTextPointStart.y + dy });

            if (dragNode.isTrackNode) {
                dragNode.rTrackText.attr({ x: dragTrackTextPointStart.x + dx, y: dragTrackTextPointStart.y + dy });
            }

            //重绘与该节点相连的连接线
            for (var i = arrs.length; i--; ) {
                if (arrs[i].rStart.id == dragNode.rBorder.id || arrs[i].rEnd.id == dragNode.rBorder.id) {
                    r.drawArr(arrs[i]);
                }
            }
        }

        function iconUp() {
            this.animate({ "fill-opacity": 1 }, 500);

            if (dragNode.isTrackNode) {
                var p = $(dragNode.rIcon.node).offset();
                $('#trackinfo').offset({ top: p.top + dragNode.rIcon.attr('height') + 2, left: p.left });
                dragNode = null;
            }
        }

        function RFlow(sFlowNo) {
            /// <summary>流程</summary>
            /// <param name="sFlowNo" Type="String">流程编号</param>
            this.no = sFlowNo;
            this.nodes = new Array();
            this.labels = new Array();
            this.dirs = new Array();

            if (typeof RFlow._initialized == "undefined") {
                RFlow.prototype.getNode = function (nodeid) {
                    /// <summary>根据指定节点ID获取该结点使用Raphael绘制的对象</summary>
                    /// <param name="nodeid" Type="Int">流程编号</param>
                    for (i in this.nodes) {
                        if (this.nodes[i].id == nodeid) {
                            return this.nodes[i];
                        }
                    }

                    return null;
                }

                RFlow.prototype.getNodeByRIconId = function (raphaelid) {
                    /// <summary>根据绘制的节点中的ICON对象的id获取该结点使用Raphael绘制的对象</summary>
                    /// <param name="raphaelid" Type="Int">流程编号</param>
                    for (i in this.nodes) {
                        if (this.nodes[i].rIcon.id == raphaelid) {
                            return this.nodes[i];
                        }
                    }

                    return null;
                }
            }
        }

        function RNode(iNodeID, sNodeName, iX, iY) {
            /// <summary>节点</summary>
            /// <param name="iNodeID" Type="Int">节点ID</param>
            /// <param name="sNodeName" Type="String">节点名称</param>
            /// <param name="iX" Type="Int">节点中心点X坐标</param>
            /// <param name="iY" Type="Int">节点中心点Y坐标</param>
            this.id = iNodeID;
            this.name = sNodeName;
            this.x = iX;
            this.y = iY;
            this.icon = '';
            this.nodePosType = 0;
            this.hisToNDs = '';
            this.rBorder = null;
            this.rIcon = null;
            this.rText = null;
            this.rTrackText = null;
            this.rBorderColor = 'black';
            this.isTrackNode = false;
            this.tracks = null;
            this.currTrack = null;
        }

        function RLabel(sPk, sLabelName, iX, iY) {
            /// <summary>标签</summary>
            /// <param name="sPk" Type="String">MyPk</param>
            /// <param name="sLabelName" Type="String">标签文本</param>
            /// <param name="iX" Type="Int">标签左上角X坐标</param>
            /// <param name="iY" Type="Int">标签左上角Y坐标</param>
            this.mypk = sPk;
            this.name = sLabelName;
            this.x = iX;
            this.y = iY;
            this.rText = null;
        }

        function RDirection(iFromNodeID, iToNodeID, iDirType, iIsCanBack) {
            /// <summary>结点连接线</summary>
            /// <param name="iFromNodeID" Type="Int">开始节点ID</param>
            /// <param name="iToNodeID" Type="Int">结束节点ID</param>
            /// <param name="iDirType" Type="Int">节点类型 0-前进 1-返回</param>
            /// <param name="iIsCanBack" Type="Int">是否可以原路返回</param>
            this.fromNodeID = iFromNodeID;
            this.toNodeID = iToNodeID;
            this.dirType = iDirType;
            this.isCanBack = iIsCanBack;
            this.rPath = null;
            this.rPathColor = STYLE_LINE_COLOR;
            this.FromNode = null;
            this.ToNode = null;
            this.LinkText = null;
        }

        function getTracksByFromNode(tracks, fromNodeId) {
            /// <summary>从轨迹集合中获取指定节点的轨迹集合</summary>
            /// <param name="tracks" Type="Array">所有轨迹数组</param>
            /// <param name="fromNodeId" Type="Int">指定结点的ID</param>
            var ts = new Array();

            $.each(tracks, function () {
                if (this.NDFrom == fromNodeId) {
                    ts.push(this);
                }
            });

            return ts;
        }

        function getTrackForCurrNode(tracks) {
            /// <summary>获取此节点轨迹集合中的用于绘制到此节点的轨迹</summary>
            /// <param name="tracks" Type="Array">轨迹数组</param>
            for (var i = tracks.length - 1; i >= 0; i--) {
                if (tracks[i].ActionType == 1 ||
                tracks[i].ActionType == 6 ||
                tracks[i].ActionType == 7 ||
                tracks[i].ActionType == 8 ||
                tracks[i].ActionType == 11 ||
                tracks[i].ActionType == 26 ||
                tracks[i].ActionType == 27 ||
                tracks[i].ActionType == 28) {
                    return tracks[i];
                }
            }

            return tracks[tracks.length - 1];
        }

        function getMaxInArray(arr, propName) {
            /// <summary>获取指定对象数组中指定属性的最大值</summary>
            /// <param name="arr" Type="Array">对象数组</param>
            /// <param name="propName" Type="String">属性名称</param>
            var max = 0;

            $.each(arr, function () {
                for (prop in this) {
                    if (prop == propName && !isNaN(this[prop])) {
                        max = Math.max(max, this[prop]);
                    }
                }
            });

            return max;
        }

        //获取组成箭头的三条线段的路径
        function getArr(x1, y1, x2, y2, size) {
            var angle = Raphael.angle(x1, y1, x2, y2); //得到两点之间的角度
            var a45 = Raphael.rad(angle - 45); //角度转换成弧度
            var a45m = Raphael.rad(angle + 45);
            var x2a = x2 + Math.cos(a45) * size;
            var y2a = y2 + Math.sin(a45) * size;
            var x2b = x2 + Math.cos(a45m) * size;
            var y2b = y2 + Math.sin(a45m) * size;
            var result = ["M", x1, y1, "L", x2, y2, "L", x2a, y2a, "M", x2, y2, "L", x2b, y2b];
            //var result = ["M", x1, y1, "L", x2, y2];
            return result;
        }

        function getStartEnd(obj1, obj2) {
            var bb1 = obj1.getBBox(),
                bb2 = obj2.getBBox();
            var p = [
                    { x: bb1.x + bb1.width / 2, y: bb1.y - 1 },
                    { x: bb1.x + bb1.width / 2, y: bb1.y + bb1.height + 1 },
                    { x: bb1.x - 1, y: bb1.y + bb1.height / 2 },
                    { x: bb1.x + bb1.width + 1, y: bb1.y + bb1.height / 2 },
                    { x: bb2.x + bb2.width / 2, y: bb2.y - 1 },
                    { x: bb2.x + bb2.width / 2, y: bb2.y + bb2.height + 1 },
                    { x: bb2.x - 1, y: bb2.y + bb2.height / 2 },
                    { x: bb2.x + bb2.width + 1, y: bb2.y + bb2.height / 2 }
                ];
            var d = {}, dis = [];
            for (var i = 0; i < 4; i++) {
                for (var j = 4; j < 8; j++) {
                    var dx = Math.abs(p[i].x - p[j].x),
                        dy = Math.abs(p[i].y - p[j].y);
                    if (
                         (i == j - 4) ||
                         (((i != 3 && j != 6) || p[i].x < p[j].x) &&
                         ((i != 2 && j != 7) || p[i].x > p[j].x) &&
                         ((i != 0 && j != 5) || p[i].y > p[j].y) &&
                         ((i != 1 && j != 4) || p[i].y < p[j].y))
                       ) {
                        dis.push(dx + dy);
                        d[dis[dis.length - 1]] = [i, j];
                    }
                }
            }
            if (dis.length == 0) {
                var res = [0, 4];
            } else {
                res = d[Math.min.apply(Math, dis)];
            }
            var result = {};
            result.start = {};
            result.end = {};
            result.start.x = p[res[0]].x;
            result.start.y = p[res[0]].y;
            result.end.x = p[res[1]].x;
            result.end.y = p[res[1]].y;
            return result;
        }
        
        Raphael.fn.drawArr = function (raphaelObj) {
            /// <summary>绘制带箭头的连接线</summary>
            /// <param name="raphaelObj" Type="Raphael Element">要绘制的连接的信息对象，包括rStart[开始对象]/rEnd[结束对象]/pathColor[连接线颜色]</param>
            var point = getStartEnd(raphaelObj.rStart, raphaelObj.rEnd);
            var path1 = getArr(point.start.x, point.start.y, point.end.x, point.end.y, 8);
            var pathColor;

            if (raphaelObj.arrPath) {
                raphaelObj.arrPath.attr({ path: path1 });
            }
            else {
                if (raphaelObj.pathColor) {
                    pathColor = raphaelObj.pathColor;
                }
                else {
                    pathColor = STYLE_LINE_COLOR;
                }

                raphaelObj.arrPath = this.path(path1);
                raphaelObj.arrPath.attr({ "stroke": pathColor, "stroke-width": STYLE_LINE_WIDTH }); //设置"arrow-end": "classic-wide-long"有问题

                raphaelObj.arrPath.hover(function () {
                    this.attr("stroke", STYLE_LINE_HOVER_COLOR);
                }, function () {
                    this.attr("stroke", pathColor);
                });
            }

            return raphaelObj;
        };

        function checkUrl(url) {
            /// <summary>判断远程路径是否可以连接成功</summary>
            /// <param name="url" Type="String">远程路径url</param>
            var isSuccess;

            $.ajax({
                type: 'GET',
                cache: false,   //不下载远程url
                async: false,   //同步
                url: url,
                data: '',
                success: function () {
                    isSuccess = true;
                },
                error: function () {
                    isSuccess = false;
                }
            });

            return isSuccess;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="holder">
    </div>
    <div id="msg">
    </div>
    <div id="trackinfo" style="display: none; position: absolute; width: 480px; height: auto;
        background-color: #fff">
        <table id="tracktable" class="Table" cellpadding="0" cellspacing="0" border="0" style="width: 100%">
        </table>
    </div>
</asp:Content>
