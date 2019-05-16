layui.define(["index", "form", "element", "upload"], function (exports) {
    var $ = layui.$
    var element = layui.element
    var form = layui.form;
    var admin = layui.admin;
    var setter = layui.setter;
    var upload = layui.upload;
    $$ = parent.layui.jquery,

    $("#worker").val($$("#worker").find("option:selected").val());
    $("#username").val(layui.data(setter.tableName)["username"]);

    upload.render({ //允许上传的文件后缀
        elem: '#btnUpload'
        , url: '/App/UploadAppIcon'
        , accept: 'file' //普通文件
        , size: 1024
        , exts: 'jpg|png|jpeg|bmp|gif|ico|PNG|JPG|JPEG|BMP|GIF|ICO' //文件类型
        , before: function (obj) {
            obj.preview(function (index, file, result) {
                $('#imgUpload').attr('src', result); //图片链接（base64）
                $("#txtAppImage").val(result);
            });
        }
    });

    exports("appform", {});

});