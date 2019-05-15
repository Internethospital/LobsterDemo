var base64 = require("../images/base64");
var config = require('../../config.js');
// pages/demo/books.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    BooksList:["kakake","曾浩"],
    token:""
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    let that = this;

    that.setData({
      icon20: base64.icon20,
      icon60: base64.icon60
    });

    that.gettoken(function(){
      that.getbooks();
    });
  },
/*
获取Token
*/
gettoken:function(callback){
  let that = this;
  wx.request({
    url: config.serverUrl + 'auth/login',
    data: {},
    success: res => {
      console.log(res.data);
      that.setData({ token: res.data.tokenValue });
      if(callback){
            callback();
      }
    }
  });
},
/**
 * 获取书籍
 */
getbooks:function(){
  let that = this;
  wx.request({
    url: config.serverUrl + 'api/books',
    data: {},
    header: { 'Authorization': 'Bearer ' +that.data.token},
    success: res => {
      console.log(res.data);
      that.setData({ BooksList: res.data });
    }
  });
}

})