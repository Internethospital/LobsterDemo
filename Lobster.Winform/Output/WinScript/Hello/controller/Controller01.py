# -*- coding: UTF-8 -*-
__author__ = '${author}'
global trace
#日志显示开始加载脚本
trace.printlog('begin load Controller01.py',True)

import sys
sys.path.append("WinScript/Hello")
#将trace设置为跨文件的全局变量
import model.globaltrace as glo
glo.set_trace(trace)

#导入自己的模块
from model.Model01 import Model01
from view.View01 import View01

#导入常用的模块
import json
from datetime import datetime



#入口函数
def main(_controller):
  try:
    trace.printlog('call main')

    view01 = View01(_controller)  # 有几个界面类就要实例化几个
    controller01 = Controller01(_controller,view01)  # 先实列化控制器类，然后在把对象传给界面类，这样界面就可以直接调用控制器中的方法

    return 0;
  except Exception,ex:
    return ex;


#控制器类
##############################################################
class Controller01(object):
  def __init__(self, _controller,_view01):
    self.controller=_controller
    self.view01=_view01
    self.rview01=_controller.RenderList['View01']
    #注册事件
    self.rview01.initload(self.viewload)
    self.rview01.bind('btnHello', 'click', self.Hello)
    self.rview01.bind('btnHello2', 'click', self.Hello2)
    self.rview01.bind('btnHello3', 'click', self.Hello3)
    self.rview01.bind('btnHello4', 'click', self.Hello4)
    self.rview01.bind('btnHello5', 'click', self.Hello5)
  #界面初始化加载
  def viewload(self):
    pass
    
  def Hello(self,sender,e):
    self.rview01.msgbox('Hello World!','提示','ok','info')
    trace.printlog('hello world')

  def Hello2(self,sender,e):
    trace.printlog('Hello2')
    retdata = self.controller.request("Books.Service#BookDemoService2#service001","kakake")
    val = self.controller.getjson(retdata, 0)
    self.rview01.msgbox(val,'提示','ok','info')
    trace.printlog(val)

  def Hello3(self,sender,e):
    trace.printlog('Hello3')
    retdata = self.controller.request("Books.Service#BookDemoService2#GetBooks","")
    val = self.controller.getjson(retdata, 0)
    booklist= json.loads(val)
    self.rview01.msgbox(val,'提示','ok','info')
    trace.printlog(val)
    trace.printlog(booklist)

  def Hello4(self,sender,e):
    trace.printlog('Hello4')
    book={'Id':0,'BookName':'kakake','BuyPrice':20.00,'BuyDate':'2018-02-04','Flag':0}
    jsonpara=json.dumps(book)
    retdata = self.controller.request("Books.Service#BookDemoService2#SaveBook",jsonpara)
    val = self.controller.getjson(retdata, 0)
    self.rview01.msgbox(val,'提示','ok','info')
    trace.printlog(val)

  def Hello5(self,sender,e):
    trace.printlog('Hello5')
    jsonpara='1'
    retdata = self.controller.request("Books.Service#BookDemoService2#DeleteBook",jsonpara)
    val = self.controller.getjson(retdata, 0)
    self.rview01.msgbox(val,'提示','ok','info')
    trace.printlog(val)
##############################################################



#日志显示脚本加载完成
trace.printlog('end load Controller01.py')
