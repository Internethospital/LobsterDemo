# -*- coding: UTF-8 -*-
__author__ = 'kakake'
global trace
trace.printlog('begin load BookDemoControllerEx.py',True)

import sys
sys.path.append("WinScript/BooksDemo1.0")
#将trace设置为跨文件的全局变量
import cloudsoft as soft
soft.set_trace(trace)

from model.book import Book
from view.frmBooks import frmBooks


import json
from datetime import datetime


#入口函数
def main(_controller):
  try:
    trace.printlog('call main')

    bookview = frmBooks(_controller)  # 有几个界面类就要实例化几个
    bookcontroller = bookController(_controller,bookview)  # 先实列化控制器类，然后在把对象传给界面类，这样界面就可以直接调用控制器中的方法

    return 0;
  except Exception,ex:
    return ex;


#控制器类
##############################################################
class bookController(object):
  def __init__(self, _controller,_ibookview):
    self.controller=_controller
    self.ibookview=_ibookview
    self.rbookview=_controller.RenderList['frmBooks']
    #注册事件
    self.rbookview.bind('btnadd', 'click', self.AddBook)#新增书籍
    self.rbookview.bind('btnalter', 'click', self.AlterBook)#修改书籍
    self.rbookview.bind('btndelete', 'click', self.DeleteBook)#删除数据
    self.rbookview.bind('btnclose', 'click', self.CloseBook)#关闭操作
    self.rbookview.bind('btnCancel', 'click', self.CancelBook)#取消操作
    self.rbookview.bind('btnSave', 'click', self.SaveBook)#保存书籍
    self.rbookview.bind('gridbooks', 'currentcellchanged', self.SelectedBook)#选择书籍
    self.rbookview.initload(self.GetBooks)#获取书籍
    
    
    
  #获取书籍
  def GetBooks(self):
    self.ibookview.setviewstate(1)  # 进入默认状态
    sql="select * from BOOKS"
    #trace.printlog(soft.Database)
    bookdata= self.controller.dbquery(soft.Database['connectionString'],soft.Database['providerName'],sql)
    dt=self.controller.json2dt(bookdata)
    self.ibookview.setbooklist(dt)
  #新增书籍
  def AddBook(self,sender,e):
    self.ibookview.setviewstate(2)#进入编辑状态
    book=Book(0,'',0.0,datetime.now().strftime("%Y-%m-%d %H:%M:%S"),0)
    self.ibookview.setcurrbook(book)
  #修改书籍
  def AlterBook(self,sender,e):
    self.ibookview.setviewstate(2)  # 进入编辑状态
  #删除数据
  def DeleteBook(self,sender,e):
    cell = self.ibookview.getCurrentCell()
    if cell is False:#判断是否选中
      return
    val= self.rbookview.msgbox('确定删除?','提示','yesno','question')
    if val==6:#选择Yes
      sql="delete from books where Id=%s" % (self.ibookview.currbook.Id)
      self.controller.dbnonquery(soft.Database['connectionString'],soft.Database['providerName'],sql)
      self.GetBooks()
  #保存书籍
  def SaveBook(self,sender,e):
    book=self.ibookview.getcurrbook()
    sql=''
    if int(book.Id)==0:
      sql_insert="insert into books(BookName,BuyPrice,BuyDate,Flag) values('%s',%s,'%s',%s)"
      sql=sql_insert % (book.BookName,book.BuyPrice,book.BuyDate,book.Flag)
    else:
      sql_update="update books set BookName='%s',BuyPrice=%s,BuyDate='%s',Flag=%s where Id=%s"
      sql=sql_update % (book.BookName,book.BuyPrice,book.BuyDate,book.Flag,book.Id)
    #trace.printlog(sql)
    self.controller.dbnonquery(soft.Database['connectionString'],soft.Database['providerName'],sql)
    self.rbookview.msgboxsimple('保存成功')
    self.GetBooks()
  #取消操作
  def CancelBook(self,sender,e):
    self.ibookview.setviewstate(1)  # 进入默认状态
  #关闭操作
  def CloseBook(self,sender,e):
    pass
  #选择书籍
  def SelectedBook(self,sender,e):
    self.ibookview.gridbooksCurrentCellChanged()


##############################################################



#日志显示脚本加载完成
trace.printlog('end load BookDemoControllerEx.py')