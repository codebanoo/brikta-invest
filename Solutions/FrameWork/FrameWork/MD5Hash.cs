﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrameWork
{
    public static class MD5Hash
    {
        public static string GetMD5Hash(string input = "")
        {
            try
            {
                System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
                bs = x.ComputeHash(bs);
                System.Text.StringBuilder s = new System.Text.StringBuilder();
                foreach (byte b in bs)
                {
                    s.Append(b.ToString("x2").ToLower());
                }
                return s.ToString();
            }
            catch
            {
                return "error";
            }
        }
    }
}