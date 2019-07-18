# -*- coding: UTF-8 -*-
__author__ = 'kakake'

import sys
import model.globaltrace as glo
trace=glo.get_trace()
trace.printlog('begin load frmBooks.py')

from model.book import Book

# 界面类
##############################################################
class frmBooks(object):
  def __init__(self, _controller):
    self.view = _controller.RenderList['frmBookManagerEx']
    self.currbook = None
    self.ViewState={'默认':1,'编辑':2}

  # 以下界面接口
  def getCurrentCell(self):
    cell = self.view.getattr('gridbooks', 'CurrentCell')
    if cell is None:
      return False
    else:
      return cell

  def gridbooksCurrentCellChanged(self):
    cell = self.getCurrentCell()
    if cell is False:
      return
    index=cell.RowIndex
    trace.printlog('currentRowIndex:'+str(index))
    dt=self.view.getattr('gridbooks', 'DataSource')
    dr=dt.Rows[index]
    book = Book(dr['Id'], dr['BookName'], dr['BuyPrice'], dr['BuyDate'], dr['Flag'])
    self.setcurrbook(book)

  def setviewstate(self, state):
    if state == 1:
      self.view.setattr('btnadd', 'enabled', 'true')
      self.view.setattr('btnalter', 'enabled', 'true')
      self.view.setattr('btndelete', 'enabled', 'true')
      self.view.setattr('btnclose', 'enabled', 'true')

      self.view.setattr('btnCancel', 'enabled', 'false')
      self.view.setattr('btnSave', 'enabled', 'false')
      self.view.setattr('gridbooks', 'enabled', 'true')

      self.view.setattr('txtBookName', 'enabled', 'false')
      self.view.setattr('txtPrice', 'enabled', 'false')
      self.view.setattr('txtDate', 'enabled', 'false')
      self.view.setattr('ckBack', 'enabled', 'false')
    elif state == 2:
      self.view.setattr('btnadd', 'enabled', 'false')
      self.view.setattr('btnalter', 'enabled', 'false')
      self.view.setattr('btndelete', 'enabled', 'false')
      self.view.setattr('btnclose', 'enabled', 'false')

      self.view.setattr('btnCancel', 'enabled', 'true')
      self.view.setattr('btnSave', 'enabled', 'true')
      self.view.setattr('gridbooks', 'enabled', 'false')

      self.view.setattr('txtBookName', 'enabled', 'true')
      self.view.setattr('txtPrice', 'enabled', 'true')
      self.view.setattr('txtDate', 'enabled', 'true')
      self.view.setattr('ckBack', 'enabled', 'true')
    else:
      pass

  def getcurrbook(self):
    book = self.currbook
    book.BookName = self.view.getattr('txtBookName', 'Text')
    book.BuyPrice = self.view.getattr('txtPrice', 'Text')
    book.BuyDate = str(self.view.getattr('txtDate', 'Value'))
    book.Flag = (self.view.getattr('ckBack', 'Checked') == True and 1 or 0)
    return book

  def setcurrbook(self, val):
    self.view.setattr('txtBookName', 'Text', val.BookName)
    #self.view.setattr('txtBookName', 'visible','false')
    self.view.setattr('txtPrice', 'Text', val.BuyPrice)
    self.view.setattr('txtDate', 'Value', val.BuyDate)
    self.view.setattr('ckBack', 'Checked', (val.Flag == 1 and True))
    self.currbook = val

  def setbooklist(self, val):
    self.view.setattr('gridbooks', 'DataSource', val)
##############################################################


#日志显示脚本加载完成
trace.printlog('end load frmBooks.py')