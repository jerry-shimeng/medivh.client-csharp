using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Medivh.Common
{
    public class HttpHelper
    {
        public static bool Post(string postData, string url, out string response)
        {
            return Post(postData, url, out response, null);
        }

        //Post
        public static bool Post(string postData, string url, out string response, Dictionary<string, string> headers, string contentType = "application/json")
        {
            return BeginRequest("POST", postData, url, out response, headers, contentType);
        }

        //Delete
        public static bool Delete(string data, string url, out string response, Dictionary<string, string> headers,
            string contentType = "application/json")
        {
            return BeginRequest("DELETE", data, url, out response, headers, contentType);
        }

        //Get
        public static bool Get(string data, string url, out string response, Dictionary<string, string> headers,
           string contentType = "application/json")
        {
            return BeginRequest("Get", data, url, out response, headers, contentType);
        }

        public static bool BeginRequest(string method, string requestData, string url, out string response, Dictionary<string, string> headers, string contentType = "application/json")
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("url");

            //init request;
            WebRequest webRequest = WebRequest.Create(url);
            HttpWebRequest httpRequest = webRequest as HttpWebRequest;

            httpRequest.ContentType = contentType;
            httpRequest.Method = method;
            httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:38.0) Gecko/20100101 Firefox/38.0";
            if (headers != null)
            {
                foreach (var kv in headers)
                {
                    httpRequest.Headers.Add(kv.Key, kv.Value);
                }
            }

            if (!string.IsNullOrWhiteSpace(requestData))
            {
                byte[] data;

                data = UTF8Encoding.UTF8.GetBytes(requestData); ;
                httpRequest.ContentLength = data.Length;

                using (Stream requestStream = httpRequest.GetRequestStream())
                {
                    requestStream.Write(data, 0, data.Length);
                    requestStream.Close();
                }
            }

            Stream responseStream;
            string stringResponse = string.Empty;

            try
            {
                var context = httpRequest.GetResponse();
                using (responseStream = context.GetResponseStream())
                {
                    using (StreamReader responseReader =
                        new StreamReader(responseStream, Encoding.GetEncoding("utf-8")))
                    {
                        stringResponse = responseReader.ReadToEnd();
                    }
                    responseStream.Close();
                }

                response = stringResponse;
                return true;
            }
            catch (WebException e)
            {
                var errorResponse = e.Response as WebResponse;
                if (errorResponse == null)
                {
                    response = e.Message;
                    //LogHelper.Error("HttpHelper", e);
                    return false;
                }

                using (responseStream = errorResponse.GetResponseStream())
                {
                    using (StreamReader responseReader =
                        new StreamReader(responseStream, Encoding.GetEncoding("utf-8")))
                    {
                        stringResponse = responseReader.ReadToEnd();
                    }
                    responseStream.Close();
                }
                response = stringResponse;
                return false;
            }
            catch (Exception e)
            {
                response = e.Message;
                //LogHelper.Error("HttpHelper", e);
                return false;
            }
        }

        public class ContentType
        {
            public const string Xml = "xml/text";
            public const string UrlEncode = "application/x-www-form-urlencoded";
        }
    }
}