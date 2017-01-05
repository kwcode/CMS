<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFlow_Charts.aspx.cs" Inherits="WEB.SEO.WebFlow_Charts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>图表统计</title>
    <script src="../js/jquery-1.8.3.min.js"></script>
    <%--<script src="../js/charts/echarts.js"></script>--%>
    <script src="../js/charts/echarts.min.js"></script>
    <script src="../js/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <fieldset>
            <legend>时间区间
            </legend>
            <div style="background-color: #FFF; padding: 10px;">
                <input id="txt_begin" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" value="<%=beginTime %>" type="text" style="padding: 4px 7px;" />
                至
                <input id="txt_end" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" value="<%=endTime %>" type="text" style="padding: 4px 7px;" />
                <input class="inpbbut3" type="button" id="btn_search" value="确认" />
            </div>
        </fieldset>
        <!-- 为 ECharts 准备一个具备大小（宽高）的 DOM -->
        <fieldset>
            <legend style="font-weight: bold;">流量统计  </legend>
            <div id="main" style="width: 100%; height: 500px"></div>
        </fieldset>
        <script type="text/javascript">
            $(function () {
                $("#btn_search").click(function () {
                    var begin = $("#txt_begin").val();
                    var end = $("#txt_end").val();
                    window.location.href = "WebFlow_Charts.aspx?begin=" + begin + "&end=" + end;

                });

                // 基于准备好的dom，初始化echarts实例
                var myChart = echarts.init(document.getElementById('main'));

                // 指定图表的配置项和数据
                option = {
                    title: {
                        // text: '流量统计'
                    },
                    tooltip: {
                        trigger: 'axis'
                    },
                    legend: {
                        data: ['IP数', '访问次数']
                    },
                    grid: {
                        left: '3%',
                        right: '4%',
                        bottom: '3%',
                        containLabel: true
                    },
                    toolbox: {
                        feature: {
                            saveAsImage: {}
                        }
                    },
                    xAxis: {
                        type: 'category',
                        boundaryGap: false,
                        //data: ['周一', '周二', '周三', '周四', '周五', '周六', '周日']
                        data: [<%=DateList%>]
                    },
                    yAxis: {
                        type: 'value'
                    },
                    series: [
                        {
                            name: 'IP数',
                            type: 'line',
                            stack: '总量',
                            data: [<%=IPCountList%>]
                        },
                        {
                            name: '访问次数',
                            type: 'line',
                            stack: '总量',
                            data: [<%=PVCountList%>]
                        }
                    ]
                };
                // 使用刚指定的配置项和数据显示图表。
                myChart.setOption(option);
            })
        </script>
    </form>
</body>
</html>
