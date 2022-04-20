using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SevenZip;
using SevenZip.Compression;


namespace AutoFont.SevenZ
{
    public class Af7Z
    {
        public void Compression7Z(byte[] pInput, ref byte[] pOutput)
        {

            Int32 dictionary = 8388608;
            Int32 posStateBits = 2;
            Int32 litContextBits = 3; // for normal files
            Int32 litPosBits = 0;
            Int32 algorithm = 2;
            Int32 numFastBytes = 32;//128;//normal compession 
            string mf = "bt4";
            bool eos = false;

            MemoryStream l_oInMemoryStream = new MemoryStream(pInput);
            MemoryStream l_oOutMemoryStream = new MemoryStream();



            CoderPropID[] propIDs = 
				{
					CoderPropID.DictionarySize,
					CoderPropID.PosStateBits,
					CoderPropID.LitContextBits,
					CoderPropID.LitPosBits,
					CoderPropID.Algorithm,
					CoderPropID.NumFastBytes,
					CoderPropID.MatchFinder,
					CoderPropID.EndMarker
				};


            object[] properties = 
				{
					(Int32)(dictionary),
					(Int32)(posStateBits),
					(Int32)(litContextBits),
					(Int32)(litPosBits),
					(Int32)(algorithm),
					(Int32)(numFastBytes),
					mf,
					eos
				};


            SevenZip.Compression.LZMA.Encoder l_oEncoder = new SevenZip.Compression.LZMA.Encoder();

            l_oEncoder.SetCoderProperties(propIDs, properties);

            l_oEncoder.WriteCoderProperties(l_oOutMemoryStream);

            Int64 fileSize;

            fileSize = pInput.Length;

            for (int i = 0; i < 8; i++)
            {
                l_oOutMemoryStream.WriteByte((Byte)(fileSize >> (8 * i)));
            }
            l_oEncoder.Code(l_oInMemoryStream, l_oOutMemoryStream, -1, -1, null);


            pOutput = l_oOutMemoryStream.ToArray();


        }

        public void UnCompression7Z(byte[] pInput, ref byte[] pOutput)
        {

            MemoryStream l_oInMemoryStream = new MemoryStream(pInput);
            MemoryStream l_oOutMemoryStream = new MemoryStream();

            byte[] properties = new byte[5];
            if (l_oInMemoryStream.Read(properties, 0, 5) != 5)
                throw (new Exception("input .lzma is too short"));

            SevenZip.Compression.LZMA.Decoder l_oDecoder = new SevenZip.Compression.LZMA.Decoder();

            l_oDecoder.SetDecoderProperties(properties);

            long outSize = 0;

            for (int i = 0; i < 8; i++)
            {
                int v = l_oInMemoryStream.ReadByte();
                if (v < 0)
                    throw (new Exception("Can't Read 1"));
                outSize |= ((long)(byte)v) << (8 * i);
            }

            long compressedSize = l_oInMemoryStream.Length - l_oInMemoryStream.Position;

            l_oDecoder.Code(l_oInMemoryStream, l_oOutMemoryStream, compressedSize, outSize, null);

            pOutput = l_oOutMemoryStream.ToArray();

        }
    }
}
