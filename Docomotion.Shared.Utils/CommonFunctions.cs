using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Docomotion.Shared.Utils
{
    public partial class FLib
    {
        public static byte[] String2ByteArray(string sValue, int codePage)
        {
            byte[] byteArray = null;

            Encoding encoding = null;

            try
            {
                encoding = Encoding.GetEncoding(codePage);
            }
            catch
            {
                encoding = Encoding.UTF8;
            }

            byteArray = encoding.GetBytes(sValue);

            return byteArray;
        }

        public static string ByteArray2UTF8(byte[] source, int codePage, out bool isSupported)
        {
            string UTF8Str = null;
            Encoding encoding = null;

            isSupported = true;

            try
            {
                encoding = Encoding.GetEncoding(codePage);
            }
            catch (NotSupportedException)
            {
                isSupported = false;
                return null;
            }
            catch
            {
                encoding = Encoding.UTF8;
            }

            if (!encoding.Equals(Encoding.UTF8))
            {
                Encoding encodingUtf8 = Encoding.UTF8;
                byte[] bytesUtf8 = Encoding.Convert(encoding, encodingUtf8, source);
                UTF8Str = encodingUtf8.GetString(bytesUtf8);
            }
            else
            {
                UTF8Str = encoding.GetString(source);
            }

            return UTF8Str;
        }

        public static byte[] EncodeByteArray2Unicode(byte[] source, int codePage)
        {
            byte[] unicodeArray = null;
            Encoding encodingSource = null;

            try
            {
                encodingSource = Encoding.GetEncoding(codePage);

                unicodeArray = Encoding.Convert(encodingSource, Encoding.Unicode, source);
            }
            catch (NotSupportedException)
            {
                unicodeArray = source;
            }
            catch
            {
                unicodeArray = source;
            }

            return unicodeArray;
        }

        public static int FindXMLTagByName(byte[] source, int startIndex, byte[] sequence)
        {
            int index = -1;
            int sourceLength = source.Count();

            if (startIndex == (-1)) startIndex = 0;

            if (startIndex < sourceLength)
            {
                for (int c = startIndex; c < sourceLength; c++)
                {
                    byte b = source[c];
                    if (b == '<')
                    {
                        bool found = true;
                        int seqLen = sequence.Count();
                        for (int i = 0; i < seqLen; i++)
                        {
                            if (source[c + i + 1] != sequence[i])
                            {
                                found = false;
                                c += (i + 1);
                                break;
                            }
                        }

                        if (found)
                        {
                            index = c;
                            c += seqLen;
                            break;
                        }
                    }

                }
            }

            return index;
        }

        public static int FindXMLTagByNameCaseInsensitive(byte[] source, int startIndex, byte[] sequenceLowCase, byte[] sequenceUperCase)
        {
            int index = -1;
            int sourceLength = source.Count();

            if (startIndex < sourceLength)
            {
                for (int c = startIndex; c < sourceLength; c++)
                {
                    byte b = source[c];
                    if (b == '<')
                    {
                        bool found = true;
                        int seqLen = sequenceLowCase.Count();

                        if ((c + seqLen) <= sourceLength)
                        {
                            for (int i = 0; i < seqLen; i++)
                            {
                                if (source[c + i + 1] != sequenceLowCase[i] && source[c + i + 1] != sequenceUperCase[i])
                                {
                                    found = false;
                                    c += (i + 1);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            index = -1;
                            break;
                        }

                        if (found)
                        {
                            index = c;
                            c += seqLen;
                            break;
                        }
                    }

                }
            }

            return index;
        }

        public static bool IsWhiteSpace(byte b)
        {
            bool res = false;

            if (b == 0x09 || b == 0x0A || b == 0x0D || b == 0x20)
            {
                res = true;
            }

            return res;
        }

        public static bool IsValidFilePath(string filePath)
        {
            char[] invalidChars = System.IO.Path.GetInvalidPathChars();

            int ind = filePath.IndexOfAny(invalidChars);

            bool res = (ind == (-1));

            if (res)
            {
                System.Text.RegularExpressions.Regex driveCheck = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z]:\\$|^\\|/*");
                if (!driveCheck.IsMatch(filePath.Substring(0, 3))) res = false;
            }

            return res;
        }

         public static string GetComputerName()
        {
            return System.Environment.MachineName;
        }

        public static string GetDateAsString(string format)
        {
            DateTime theDate = DateTime.Now;

            string strDate = null;
            try
            {
                strDate = theDate.ToString(format);
            }
            catch
            {
                strDate = theDate.ToString();
            }

            return strDate;
        }

        public static string GetGuid(bool brackets = false, bool numeric = false, bool hexadecimal = false)
        {
            string guidString = string.Empty;
            if (brackets) guidString = Guid.NewGuid().ToString("B");
            else if (numeric) guidString = Guid.NewGuid().ToString("N");
            else if (hexadecimal) guidString = Guid.NewGuid().ToString("X");
            else guidString = Guid.NewGuid().ToString();

            return guidString;
        }

        public static string GetUserName()
        {
            return Environment.UserName;
        }

        public static string ConvertToBase64(byte[] buffer)
        {
            return Convert.ToBase64String(buffer);
        }

        public static int FindSequence(byte[] source, int startIndex, byte[] sequence)
        {
            int index = -1;
            int sourceLength = source.Count();

            if (startIndex < sourceLength)
            {
                for (int c = startIndex; c < sourceLength; c++)
                {
                    byte b = source[c];
                    if (b == sequence[0])
                    {
                        bool found = true;
                        int seqLen = sequence.Count();

                        if (c + seqLen <= sourceLength)
                        {
                            for (int i = 1; i < seqLen; i++)
                            {
                                if (source[c + i] != sequence[i])
                                {
                                    found = false;
                                    c += i;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            found = false;
                            break;
                        }

                        if (found)
                        {
                            index = c;
                            c += seqLen;
                            break;
                        }
                    }

                }
            }

            return index;
        }

        public static string EncodeByBitsReverse(string closedPassword)
        {
            string openPassword = null;

            if (!string.IsNullOrWhiteSpace(closedPassword))
            {
                byte[] passwordBytes = Enumerable.Range(0, closedPassword.Length)
                         .Where(x => x % 2 == 0)
                         .Select(x => Convert.ToByte(closedPassword.Substring(x, 2), 16))
                         .ToArray();

                for (int i = 0; i < passwordBytes.Length; i++)
                {
                    passwordBytes[i] = (byte)~passwordBytes[i];
                }

                openPassword = Encoding.UTF8.GetString(passwordBytes);
            }

            return openPassword;
        }

        public static string DecodeByBitsReverse(string openPassword)
        {
            string closedPassword = null;
            //coding the password : >>>>>>>>>>>>>
            //1) reverce bits of each byte
            //2) convert byte value to hex string X:2
            byte[] passwordBytes = Encoding.UTF8.GetBytes(openPassword);

            for (int i = 0; i < passwordBytes.Length; i++)
            {
                passwordBytes[i] = (byte)~passwordBytes[i];

                closedPassword += String.Format("{0:X2}", passwordBytes[i]);
            }

            return closedPassword;
        }

        public static byte[] ConvertHexString2ByteArray(string hex)
        {
            byte[] bytes = null;

            try
            {
                bytes = Enumerable.Range(0, hex.Length)
                   .Where(x => x % 2 == 0)
                   .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                   .ToArray();
            }
            catch { }

            return bytes;
        }

        public static string ToStringFloatAsInt(float number)
        {
            return number.ToString("F0", System.Globalization.CultureInfo.InvariantCulture);
        }

        public static int Points2Pixel(float points, float resolution)
        {
            return Convert.ToInt32(Math.Ceiling((points / 72) * resolution));
        }

        public static float CalculateSuperSubScriptFontSize(float curFontSize)
        {
            return Convert.ToSingle(System.Math.Round(curFontSize / 1.63));
        }

        public static string CombinePrefixedLength4String(List<string> items)
        {
            string result = string.Empty;

            foreach(string item in items)
            {
                string hexLenght = item.Length.ToString("X4");
                result = $"{result}{hexLenght}{item}";
            }

            return result;
        }

        public static List<string> SeparatePrefixedLength4String(string prefixedString)
        {
            List<string> items = new List<string>();

            try
            {
                if (prefixedString.Length > 4)
                {
                    int curPos = 0;
                    do
                    {
                        int len = 0;
                        int.TryParse(prefixedString.Substring(curPos, 4), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out len);
                        curPos += 4;

                        string item = string.Empty;
                        if (curPos + len < prefixedString.Length)
                            item = prefixedString.Substring(curPos, len);
                        else
                            item = prefixedString.Substring(curPos);

                        curPos += len;

                        items.Add(item);
                    }
                    while (curPos < prefixedString.Length);
                }
            }
            catch { }

            return items;
        }
    }
}
