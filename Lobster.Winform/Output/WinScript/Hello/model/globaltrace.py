# -*- coding: UTF-8 -*-
__author__ = 'kakake'

global trace
def set_trace(t):
  global trace
  trace=t

def get_trace():
  return trace