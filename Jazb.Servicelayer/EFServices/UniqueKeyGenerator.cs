using System;
using System.Data.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Jazb.DomainClasses.Entities;
using System.IO;

namespace Jazb.Servicelayer.EFServices
{
  
        public class GenerateUniqCode
        {
            public int LEN { get; set; }
            public string GenerateCode(int lenght)
            {
                LEN = lenght;
                string[] keys = new string[LEN];
                string code = "";
                for (int loop = 0; loop < LEN; loop++)
                {
                    keys[loop] = UsingGuid();
                }

                Hashtable e = Frequency(keys);
                foreach (string key in keys)
                {
                    code = key;
                }

                return code;
            }

        public string GetPaymentUniqCode(int Len)
        {

            string Code = "";
            int l = Len == 0 ? 1000000 : Len;
            Code = RNGCharacterMask(l, false);
            return Code;
        }

        public string SanjeshCheckUniqCodeSanjesh(int Len)
        {

            Jazb.Datalayer.Context.JazbDbContext db = new Datalayer.Context.JazbDbContext();
            bool bln = true;
            string Code = "";
            int l = Len == 0 ? 1000000 : Len;

            while (bln)
            {
                Code = GenerateCode(l);
                var q = (from p in db.AttachmentImageSanjeshs where p.TrakingCode == Code select p.TrakingCode).ToList();
                var qcount = q.Count();

                if (qcount == 0)
                    bln = false;
            }

            return Code;
        }

        public string CheckUniqCode(int Len)
            {

                Jazb.Datalayer.Context.JazbDbContext db = new Datalayer.Context.JazbDbContext();
                bool bln = true;
                string Code = "";
                int l = Len == 0 ? 1000000 : Len;

                while (bln)
                {
                    Code = GenerateCode(l);
                    var q = (from p in db.Valentears where p.TrackingCode == Code select p.TrackingCode).ToList();
                    var qcount = q.Count();

                    if (qcount == 0)
                        bln = false;
                }

                return Code;
            }

            public string CheckUniqCode_FAQ(int Len, IEnumerable<Valentear> valentears)
            {
                bool bln = true;
                string Code = "";
                int l = Len == 0 ? 1000000 : Len;

                while (bln)
                {

                    Code = GenerateCode(l);
                    var q = from p in valentears
                            where p.TrackingCode.Equals(Code)
                            select p;
                    if (q.Count() == 0)
                        bln = false;
                }

                return Code;
            }


            private string UsingGuid()
            {
                string result = Guid.NewGuid().ToString().GetHashCode().ToString("x");
                return result;
            }


            private string UsingTicks()
            {
                string val = DateTime.Now.Ticks.ToString("x");
                return val;
            }


        private string RNGCharacterMask(int len, bool HasWord)
        {
            int maxSize = len;
            int minSize = 5;
            char[] chars;
            string a;
            if (HasWord)
            {
                chars = new char[62];
                a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            }
            else
            {
                chars = new char[10];
                a = "1234567890";
            }
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            { result.Append(chars[b % (chars.Length - 1)]); }
            return result.ToString();
        }


        private string UsingDateTime()
            {
                return DateTime.Now.ToString().GetHashCode().ToString("x");
            }


            private Hashtable Frequency(string[] keys)
            {
                Hashtable freq = new Hashtable(LEN);

                foreach (string key in keys)
                {
                    if (freq[key] == null)
                    {
                        freq.Add(key, 0);
                    }
                    else
                    {
                        freq[key] = (int)freq[key] + 1;
                    }
                }
                return freq;
            }

        }
   
}
