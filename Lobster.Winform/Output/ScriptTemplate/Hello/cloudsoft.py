# -*- coding: UTF-8 -*-
__author__ = 'kakake'

global trace
def set_trace(t):
  global trace
  trace=t

def get_trace():
  return trace

#数据库连接字符串
#global Database
Database={
'connectionString':'Data Source=.;Initial Catalog=cloudhis;User ID=sa;pwd=1;',
'providerName':'SqlServer'
}