# -*- coding: UTF-8 -*-
__author__ = 'kakake'
global trace
#日志显示开始加载脚本
trace.printlog('begin load Controller01.py',True)

import sys
sys.path.append("WinScript/Hello")
#将trace设置为跨文件的全局变量
import cloudsoft as soft
soft.set_trace(trace)

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
    
  #界面初始化加载
  def viewload(self):
    pass
    
  def Hello(self,sender,e):
    self.rview01.msgbox('Hello World!','提示','ok','info')
    trace.printlog('hello world')

##############################################################



#日志显示脚本加载完成
trace.printlog('end load Controller01.py')
