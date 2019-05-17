var base64 = require("../images/base64");
var config = require('../../config.js');
var util = require('../../utils/util.js');
// pages/demo/books.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    BooksList:[{"Id":"1","BookName":"小程序开发","BuyPrice":"20.00","Flag":"0","WorkdId":"1"}],
    token:"",
    icon20: base64.icon20,
    icon60: base64.icon60
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    let that = this;
    //获取书籍
    that.getbooks();
  },
/**
 * 获取书籍
 */
getbooks:function(){
  let that = this;
  wx.request({
    url: config.serverUrl + 'demo/v1/book/getbookdata',
    data: { page: 1, limit: 10, bookName:''},
    //header: { 'Authorization': 'Bearer ' +that.data.token},
    success: res => {
      console.log(util.getResponseCode(res));
      that.setData({ BooksList: util.getResponseData(res,'data') });
    }
  });
}

})