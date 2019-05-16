/**

 @Name：书籍DEMO
 @Author：kakake
 @Date：2019-03-12
    
 */
layui.define(['layer', 'form', 'element', 'table', 'index'], function (exports) {
    var layer = layui.layer;
    var form = layui.form;
    var element = layui.element;
    var table = layui.table;
    var $ = layui.$;
    var admin = layui.admin;
    var setter = layui.setter;

    //表格绑定
    booktable = table.render({
        elem: '#bookTable',
        where: {
            //token: layui.data(setter.tableName)['token'],
            txtname: $("#txtName option:selected").text()  //获取文本值
        },
        height: 'full-130',
        cellMinWidth: 80,
        size: 'sm',
        toolbar: '#toolbar',
        url: '/book/getbooklist',
        page: true,
        even: true,
        cols: [[
            { width: "70", field: 'Id', title: 'ID', fixed: 'left', hide: true },
            { width: "150", field: 'BookName', title: '书籍名称' },
            { width: "200", field: 'BuyPrice', title: '价格' },
            { width: "90", field: 'Flag', title: '状态标识', align: 'center', templet: '#flagTpl' },
            { width: "235", title: '操作', fixed: 'right', align: 'center', toolbar: '#option' }   //操作script的id                  
        ]]
    });

    //查找reload,通过条件，查询网格的table重载数据显示到界面
    form.on("submit(LAY-app-front-search)", function (data) {
        AppDt.reload({
            where: {
                //token: layui.data(setter.tableName)['token'],
                txtname: data.field.txtName
            },
            page: {
                curr: 1
            }
        });
    });

    //添加按钮的点击事件
    table.on('toolbar(bookTable)', function (obj) {
        switch (obj.event) {
            case 'add':
                layer.open({
                    type: 2
                    , title: '新增书籍'
                    , content: '/book/bookform'
                    , maxmin: true
                    , area: ['680px', '480px']  //宽，高
                    , btn: ['保存', '取消']
                    , btn2: function (index, layero) {
                        $("#LAY-app-form-reset").trigger('click');
                    }
                    , cancel: function () {
                        $("#LAY-app-form-reset").trigger('click');
                    }
                    , yes: function (index, layero) {
                        var iframeWindow = window['layui-layer-iframe' + index],
                            submitID = 'LAY-app-front-submit',
                            submit = layero.find('iframe').contents().find('#' + submitID);
                        //监听提交
                        iframeWindow.layui.form.on('submit(' + submitID + ')', function (data) {
                            var field = data.field; //获取提交的字段
                            var load = layer.msg('正在处理，请稍候', { icon: 16, time: 0, shade: [0.3, '#393D49'] });
                            console.log(field);
                            admin.req({
                                type: 'post',
                                url: '/App/SaveAppData',
                                data: field,
                                done: function (obj) {
                                    layer.msg("保存成功");
                                    //提交 Ajax 成功后，静态更新表格中的数据
                                    AppDt.reload(); //数据刷新
                                    layer.close(index); //关闭弹层
                                }
                            });
                        });
                        submit.trigger('click');
                    }
                });
                break;
        };
    });

    //监听行工具事件
    table.on('tool(bookTable)', function (obj) {  //tool是工具条事件名，SSOAppTable是table原始容器的属性
        var data = obj.data;//当前行数据
        var layEvent = obj.event;//获得lay-event的值
        if (obj.event === "edit") {
            layer.open({
                type: 2
                , title: '编辑应用信息'
                , content: '/book/bookform'
                , area: ['680px', '480px']  //宽，高
                , btn: ['保存', '取消']
                , success: function (layero, index) {
                    setTimeout(function () {
                        //执行完毕，layer的动画一般是执行500毫秒
                        var iframeWin = window['layui-layer-iframe' + index];
                        var othis = layero.find('iframe').contents().find("#formApp");
                        othis.find('input[name="txtAppID"]').val(data.AppId);
                        othis.find('input[name="txtAppKey"]').val(data.AppKey);
                        othis.find('input[name="txtAppName"]').val(data.AppName);
                        othis.find('select[name="selAppType"]').val(data.AppType);
                        othis.find('input[name="txtAppUrl"]').val(data.AppUrl);
                        othis.find('input[name="txtAppImage"]').val(data.AppImage);
                        othis.find('input[name="txtSortNo"]').val(data.SortNo);
                        othis.find("#txtAppKey").attr("disabled", true);

                        if (data.AppImage == "")
                            othis.find("#imgUpload").attr("src", "resource/service.jpg");
                        else
                            othis.find("#imgUpload").attr("src", data.AppImage);

                        iframeWin.layui.form.render("select");
                    }, 500);
                }
                , btn2: function (index, layero) {
                    $("#LAY-app-form-reset").trigger('click');
                }
                , cancel: function () {
                    $("#LAY-app-form-reset").trigger('click');
                }
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'LAY-app-front-submit'  //子界面的保存，取消按钮的lay-filter
                        , submit = layero.find('iframe').contents().find('#' + submitID);

                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (data) {
                        var field = data.field; //获取提交的字段

                        var load = layer.msg('正在处理，请稍候', { icon: 16, time: 0, shade: [0.3, '#393D49'] });
                        admin.req({
                            type: 'post'
                            , url: '/App/SaveAppData'
                            , data: field
                            , done: function (res) {
                                layer.msg("保存成功");
                                //提交 Ajax 成功后，静态更新表格中的数据
                                AppDt.reload(); //数据刷新
                                layer.close(index); //关闭弹层
                            }
                        });

                    });
                    submit.trigger('click');
                }
            });
        }

        if (obj.event === "disable" || obj.event === "enable") {
            // 启用停用机构
            var confirm_msg = "";
            var status = 0;
            if (data.DelFlag == 0) {
                confirm_msg = "确认停用么?";
                status = 1;
            }
            else {
                confirm_msg = "确认启用么?";
                status = 0;
            }

            layer.confirm(confirm_msg, { icon: 3, title: "提示" },
                function (index) {
                    admin.req({
                        type: "post",
                        url: "/App/FlagApp",
                        data: {
                            token: layui.data(setter.tableName)["token"],
                            txtAppId: data.AppId,
                            txtDelFlag: status
                        },
                        done: function (res) {
                            AppDt.reload();// 数据刷新
                            layer.close(index); //关闭弹层
                        }
                    });
                });
        }
    });

    exports('book', {})
});
