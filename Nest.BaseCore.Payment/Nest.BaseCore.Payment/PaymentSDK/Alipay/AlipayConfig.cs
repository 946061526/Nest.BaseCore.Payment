namespace Nest.BaseCore.Payment.PaymentSDK.Alipay
{

    /// <summary>
    ///  应用2.0签约2017091481377687
    /// </summary>
    public class AliPayConfig
    {

#if DEBUG
        public const string pid = "2088721673112851";
        public const string AppId = "2018122062618396";
        public const string alipublickey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAmrqkMUO648Ro/lt1MNV+xdLtui0rBGjMv5Z+qA/+hL0i0Q4dtkVdyz4bWWDiX4u45DKJBwf+Q10bD/aRpIax1jj6R1FtvBEmuZdOsWZ1Og2+h0UpjDYT6freEmoHBMMXE2L1l5iZNOuypmACj9SpYJazScY2r1ygywl3WPCwFd6Fx/Y68E/3Mr0GsXSbX9CIcOxYZk+aQJgElKifcTANRx6G4liAm3+GcVQlUA9FpEbmQpisV4OZ9MLzhCkCmFRw3MmbiM+Bg3ZR4HK7eMGVb4Z33I+HrFUu5N20l/UmNp4c3VophIULUxiFDNtroPEpXO4JkaZv4KPkZQRqxbxOuQIDAQAB";
        public const string gatewayUrl = "https://openapi.alipay.com/gateway.do";
        public const string AppAuthorizeUrl = "https://openauth.alipay.com/oauth2/publicAppAuthorize.htm";
#else
        ////沙箱
        //public const string pid = "2088721673112851";
        //public const string AppId = "2018122062618396";
        //public const string alipublickey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAmrqkMUO648Ro/lt1MNV+xdLtui0rBGjMv5Z+qA/+hL0i0Q4dtkVdyz4bWWDiX4u45DKJBwf+Q10bD/aRpIax1jj6R1FtvBEmuZdOsWZ1Og2+h0UpjDYT6freEmoHBMMXE2L1l5iZNOuypmACj9SpYJazScY2r1ygywl3WPCwFd6Fx/Y68E/3Mr0GsXSbX9CIcOxYZk+aQJgElKifcTANRx6G4liAm3+GcVQlUA9FpEbmQpisV4OZ9MLzhCkCmFRw3MmbiM+Bg3ZR4HK7eMGVb4Z33I+HrFUu5N20l/UmNp4c3VophIULUxiFDNtroPEpXO4JkaZv4KPkZQRqxbxOuQIDAQAB";
        //public const string gatewayUrl = "https://openapi.alipay.com/gateway.do";
        //public const string AppAuthorizeUrl = "https://openauth.alipay.com/oauth2/publicAppAuthorize.htm";
        public const string pid = "2088721673112851";
        public const string AppId = "2018122062618396";
        public const string alipublickey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAmrqkMUO648Ro/lt1MNV+xdLtui0rBGjMv5Z+qA/+hL0i0Q4dtkVdyz4bWWDiX4u45DKJBwf+Q10bD/aRpIax1jj6R1FtvBEmuZdOsWZ1Og2+h0UpjDYT6freEmoHBMMXE2L1l5iZNOuypmACj9SpYJazScY2r1ygywl3WPCwFd6Fx/Y68E/3Mr0GsXSbX9CIcOxYZk+aQJgElKifcTANRx6G4liAm3+GcVQlUA9FpEbmQpisV4OZ9MLzhCkCmFRw3MmbiM+Bg3ZR4HK7eMGVb4Z33I+HrFUu5N20l/UmNp4c3VophIULUxiFDNtroPEpXO4JkaZv4KPkZQRqxbxOuQIDAQAB";
        public const string gatewayUrl = "https://openapi.alipay.com/gateway.do";
        public const string AppAuthorizeUrl = "https://openauth.alipay.com/oauth2/publicAppAuthorize.htm";
#endif

        public const string apiname = "com.alipay.account.auth";
        public const string method = "alipay.open.auth.sdk.code.get";
        public const string app_name = "mc";
        public const string biz_type = "openservice";
        public const string product_id = "APP_FAST_LOGIN";
        public const string scope = "kuaijie";
        public const string auth_type = "LOGIN";
        public const string sign_type = "RSA2";



        /*******以下不参与签名******/
        public const string charset = "UTF-8";


        public const string return_url = "/scantopay/paySuccess";//支付返回地址
        public const string auth_return_url = "/scanToPay/payment";//授权返回地址

        public const string privatekey = "MIIEpAIBAAKCAQEAqwnAOSYOsvrbSukumplbaycjTq6aE9mman3g5BUaQKs0b3898E4hzvQ6QVaANjBwdPTJRQPjfXUKwZYHD1KmN32PobovgyU5kSh/sdhTOreyQBkQIDO/hZfM0jfMqf9bcy47LhJq09T5YlpYV2BY6PAJrREghLGPaBloF4bZlxn+cANFO+L5K919P7IHDV3j3KbnTae59lpSJyM3qcE3Mxc09ZhzJDDOcVf6V6puY0HG6ugM0iX7HLoFrligHznf/5ugCYHI42aIamaHdOWNAjkfo3wK/F0v13UXBzZvs/nxFJR5eystD/1/8e0Ka4wtsKD8Zgfl7SeGXQ4MIBVyawIDAQABAoIBAQCIy4OyMX4QKBK8F0Pu4jj7upHCnGMe/TTcd1EnGrmkf0mw41Pmnpbrruno2AYzUQqggCd5y2JnNPUlX5jF6JSITSRTdVYKzfr83idDVoE6tTEbkvAS0VCcyIxIldhbHqFFvfQXJSPLyMqsnxWIzMZPh6w1fz1C48COwcM/Ddt5vUFCZBPi1KsokThClZJMngJz/t3AR66S3/MJTuyvfBqhSe/Ai91R0ope/16YxGJW9Lbz+5U29mHD46x08djXSH57bOApROb+37IdmPREntM/EreHGkXDBxKbRthgtso1CWfXbT+1q7ZPezn2O70xUTAnjGU4X5iB57cEg7MXM+4BAoGBAN1k5+5QJWvOhCpuh4yuGSdKNngGQeUIwTPb3a7msj79ZBy5OwAw249M/PjY2gASTx8IS31ultfiTocl2WJBiEl14jrrAGeo7QE7ht0XhSIwyX49E+DJ9l9IUnO4cq13VCq1N/GsDvc4vybd5/acYQUf6c43y3C+FSnmv7ghuhm9AoGBAMXF2FbjGlGtEtVgkia/TlpVyv7BbqrpxrnBaneT+QoyFSkQUCzJ1tP/vIqZNlrNjaAvQi4+TlBEZVG27ENyoTpPD4gFAXTm+/IY8HUVFAg15nSTVLjmsgo3zV1o8m+AsYMCHFbgZwOKG5saUlhbIe4mJNUStLeZWLm02Nck7PtHAoGAQq2TVKj6vD9UetsTJAGDPdwSD5AC5JIAbjf3yidc46+5KRV4eZQ9bClJv0DAV2ksPzJmWf6mm5pjAD0b/YWzIDzKx+fjFVVBHC/rbEcbJ7L93HFUvUzWUNgdTRDuKORZiwtNMBIb58VFfNU0eYebiMmVxy/yq5/0C8ydTT2LKRkCgYBXL2e2rfkdRnoF/MORN0a4RhfuBKHf0J0dqGlh+6aO2xM+/gUKKZR98sBQLUir8O/dTNgVALyPYAd2ZXh0J2VyCgZjxSkmOumYiWv/gWJKWFCW1ZhExtZ38K+k3S36/7XBdj2+AsEG62xODOV+M/aaQNB96fgm9AAMJfBtr8aU4wKBgQCJn9yvYOSvWyHAWVu6HZHmRyMlpqHOlhFKZn1nJiTYGUcv5FST2p1Nj0P5mCzmGI5qRVv7mMFLMZ1xyZ6l4VzArD2d8YTxCl9Wui1mP7ZqN34Jd58vZQABeY+8XgUEkMjUbZWUDK5xmcJ4IdvwVZyVdKujZG3uiBeW60DSfG98IA==";
        public const string publickey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAqwnAOSYOsvrbSukumplbaycjTq6aE9mman3g5BUaQKs0b3898E4hzvQ6QVaANjBwdPTJRQPjfXUKwZYHD1KmN32PobovgyU5kSh/sdhTOreyQBkQIDO/hZfM0jfMqf9bcy47LhJq09T5YlpYV2BY6PAJrREghLGPaBloF4bZlxn+cANFO+L5K919P7IHDV3j3KbnTae59lpSJyM3qcE3Mxc09ZhzJDDOcVf6V6puY0HG6ugM0iX7HLoFrligHznf/5ugCYHI42aIamaHdOWNAjkfo3wK/F0v13UXBzZvs/nxFJR5eystD/1/8e0Ka4wtsKD8Zgfl7SeGXQ4MIBVyawIDAQAB";



        public const string granttype = "authorization_code";


    }
}
