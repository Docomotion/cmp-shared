using System;
using System.Collections.Generic;
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace Docomotion.Shared.Utils
{
    public partial class FLib
    {
        public static byte[] SharpZLibExtract(byte[] toDecompress)
        {
            byte[] result = null;
            try
            {
                using (MemoryStream outputStream = new MemoryStream())
                {
                    using (MemoryStream stream = new MemoryStream(toDecompress))
                    {
                        ZipInputStream zipInputStream = new ZipInputStream(stream);
                        ZipEntry zipEntry = zipInputStream.GetNextEntry();
                        while (zipEntry != null)
                        {
                            byte[] buffer = new byte[4096];     // 4K is optimum
                            StreamUtils.Copy(zipInputStream, outputStream, buffer);
                            zipEntry = zipInputStream.GetNextEntry();
                        }
                    }

                    outputStream.Position = 0;
                    result = new byte[outputStream.Length];
                    StreamUtils.ReadFully(outputStream, result);
                }
            }
            catch (Exception ex)
            {
            }

            return result;
        }

        public static void SharpZLibExtractEntries(byte[] toDecompress, Dictionary<string, byte[]> entries)
        {
            try
            {
                using (MemoryStream stream = new MemoryStream(toDecompress))
                {
                    ZipInputStream zipInputStream = new ZipInputStream(stream);
                    ZipEntry zipEntry = zipInputStream.GetNextEntry();
                    while (zipEntry != null)
                    {
                        using (MemoryStream outputStream = new MemoryStream())
                        {
                            byte[] buffer = new byte[4096];     // 4K is optimum
                            StreamUtils.Copy(zipInputStream, outputStream, buffer);

                            outputStream.Position = 0;
                            byte[] entry = new byte[outputStream.Length];
                            StreamUtils.ReadFully(outputStream, entry);
                            entries.Add(zipEntry.Name, entry);

                            zipEntry = zipInputStream.GetNextEntry();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static byte[] SharpZLibCompress(byte[] toCompress)
        {
            byte[] result = null;

            try
            {
                ZipEntryFactory factory = new ZipEntryFactory();
                ZipEntry entry = factory.MakeFileEntry("FFI", false);

                MemoryStream msInput = new MemoryStream(toCompress);
                using (MemoryStream ms = new MemoryStream())
                {
                    ZipOutputStream zipOutputStream = new ZipOutputStream(ms);
                    zipOutputStream.SetLevel(9);
                    zipOutputStream.UseZip64 = UseZip64.Dynamic;
                    zipOutputStream.PutNextEntry(entry);
                    StreamUtils.Copy(msInput, zipOutputStream, new byte[4096]);
                    zipOutputStream.CloseEntry();
                    zipOutputStream.IsStreamOwner = false;
                    zipOutputStream.Close();
                    result = ms.ToArray(); //SharpCompressor.SerializeStream(ms);
                }
            }
            catch (Exception ex)
            {
            }

            return result;
        }
    }

    //public class SharpCompressor
    //{
    //    public static byte[] Compress(byte[] toCompress)
    //    {
    //        byte[] result = new byte[0];
    //        try
    //        {
    //            ZipEntryFactory factory = new ZipEntryFactory();
    //            ZipEntry entry = factory.MakeFileEntry("zipEntry", false);

    //            MemoryStream msInput = new MemoryStream(toCompress);
    //            using (MemoryStream ms = new MemoryStream())
    //            {
    //                ZipOutputStream zipOutputStream = new ZipOutputStream(ms);
    //                zipOutputStream.SetLevel(9);
    //                zipOutputStream.UseZip64 = UseZip64.Dynamic;
    //                zipOutputStream.PutNextEntry(entry);
    //                StreamUtils.Copy(msInput, zipOutputStream, new byte[4096]);
    //                zipOutputStream.CloseEntry();
    //                zipOutputStream.IsStreamOwner = false;
    //                zipOutputStream.Close();
    //                result = SharpCompressor.SerializeStream(ms);
    //            }
    //        }
    //        catch (Exception ex)
    //        { }
    //        return result;
    //    }

    //    public static byte[] Extract(byte[] toDecompress)
    //    {
    //        byte[] result = new byte[0];
    //        try
    //        {
    //            using (MemoryStream outputStream = new MemoryStream())
    //            {
    //                using (MemoryStream stream = new MemoryStream(toDecompress))
    //                {
    //                    ZipInputStream zipInputStream = new ZipInputStream(stream);
    //                    ZipEntry zipEntry = zipInputStream.GetNextEntry();
    //                    while (zipEntry != null)
    //                    {
    //                        byte[] buffer = new byte[4096];     // 4K is optimum
    //                        StreamUtils.Copy(zipInputStream, outputStream, buffer);
    //                        zipEntry = zipInputStream.GetNextEntry();
    //                    }
    //                }
    //                result = SerializeStream(outputStream);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //        }
    //        return result;
    //    }

    //    public static byte[] SerializeStream(Stream inputStream)
    //    {
    //        inputStream.Position = 0;
    //        byte[] result = new byte[inputStream.Length];
    //        StreamUtils.ReadFully(inputStream, result);
    //        return result;
    //    }
    //}
}

