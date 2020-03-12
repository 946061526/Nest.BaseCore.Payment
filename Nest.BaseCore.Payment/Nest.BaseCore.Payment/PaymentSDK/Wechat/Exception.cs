using System;
using System.Collections.Generic;
using System.Web;

namespace Nest.BaseCore.Payment.PaymentSDK.Wechat
{
    public class WxPayException : Exception 
    {
        public WxPayException(string msg) : base(msg) 
        {

        }

     }
    public class WxPayFallException : Exception
    {
        public WxPayFallException(string msg) : base(msg)
        {

        }

    }
}