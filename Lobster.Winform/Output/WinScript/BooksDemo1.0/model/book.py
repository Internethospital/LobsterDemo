# -*- coding: UTF-8 -*-
__author__ = 'kakake'
import sys
import cloudsoft as soft
trace=soft.get_trace()
trace.printlog('begin load book.py')
#soft.trace.printlog(soft.Database['connectionString'])
 
#实体类
##############################################################
class Book(object):
  def __init__(self,_id,_bookname,_buyprice,_buydate,_flag):
    self.Id=int(_id)
    self.BookName=_bookname
    self.BuyPrice=_buyprice
    self.BuyDate=str(_buydate)
    self.Flag=_flag

##############################################################

#日志显示脚本加载完成
trace.printlog('end load book.py')