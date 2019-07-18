# -*- coding: UTF-8 -*-
__author__ = '${author}'
import sys
import model.globaltrace as glo
trace=glo.get_trace()
#日志开始加载脚本
trace.printlog('begin load Model01.py')
 
#实体类
##############################################################
class Model01(object):
  def __init__(self):
    pass

##############################################################

#日志显示脚本加载完成
trace.printlog('end load Model01.py')