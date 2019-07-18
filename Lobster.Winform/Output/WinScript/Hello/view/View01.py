# -*- coding: UTF-8 -*-
__author__ = '${author}'

import sys
import model.globaltrace as glo
trace=glo.get_trace()
#日志开始加载脚本
trace.printlog('begin load View01.py')

# 界面类
##############################################################
class View01(object):
  def __init__(self, _controller):
    self.view = _controller.RenderList['View01']
    
##############################################################


#日志显示脚本加载完成
trace.printlog('end load View01.py')