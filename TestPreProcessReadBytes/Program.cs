using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestPreProcessReadBytes
{
    //#define CHECK_RETURN_VALUES

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    namespace PerfTest2
    {
        class Program
        {
            #region Constants

            const int Iterations = 5;

            const int PacketSize = 22;
            const int PacketDataSize = 18;
            const int PacketStartToken = 255;

            const int OneKbPacketCount = 46;
            const int FiveHundredTwelveKbPacketCount = 23831;
            const int OneMbPacketCount = 47662;
            const int TenMbPacketCount = 476625;
            const int OneHundredMbPacketCount = 4766254;

            const string AssertFormat = "Incorrect number of packets - Expected/Actual: {0}/{1}";

            #endregion

            byte[] Packet = { 255, 255, 255, 255, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };

            static void Main(string[] args)
            {
                Program p = new Program ();

                bool newMethod = false;
                if (args.Length > 0)
                    newMethod = Convert.ToBoolean (args[0]);

                int size = 1;
                if (args.Length > 1)
                    size = Convert.ToInt32 (args[1]);

                switch (size)
                {
                    case 10:
                        p.Test10Mb (newMethod);
                        break;
                    case 100:
                        p.Test100Mb (newMethod);
                        break;
                    default:
                        p.Test1Mb (newMethod);
                        break;
                }
            }

            #region Testing methods

            private void Test1Mb(bool newMethod)
            {
                Stopwatch sw = new Stopwatch ();

                List<List<byte>> oneMbTestData = new List<List<byte>> ();
                for (int i = 0 ; i < Iterations ; i++)
                    oneMbTestData.Add (Build1MbTestData ());

                var testData = new List<byte> (PacketSize * OneMbPacketCount);

                sw.Reset ();
                sw.Start ();

                if (!newMethod)
                {
                    for (int i = 0 ; i < Iterations ; i++)
                    {
                        testData = oneMbTestData[i];
                        PreProcessReadBytes (ref testData);
#if CHECK_RETURN_VALUES
                    int packetCount = testData.Count () / PacketDataSize;
                    if (packetCount != OneMbPacketCount)
                        throw new Exception (string.Format (AssertFormat, OneMbPacketCount, packetCount));
#endif
                    }
                    sw.Stop ();
                    Console.WriteLine (string.Format ("Elapsed time for 1Mb test data {0} iterations: {1}", Iterations, sw.Elapsed.ToString ()));
                }
                else
                {
                    for (int i = 0 ; i < Iterations ; i++)
                    {
                        testData = oneMbTestData[i];
                        PreProcessReadBytesNew (ref testData);
#if CHECK_RETURN_VALUES
                    int packetCount = testData.Count () / PacketDataSize;
                    if (packetCount != OneMbPacketCount)
                        throw new Exception (string.Format (AssertFormat, OneMbPacketCount, packetCount));
#endif
                    }
                    sw.Stop ();
                    Console.WriteLine (string.Format ("(NEW)Elapsed time for 1Mb test data {0} iterations: {1}", Iterations, sw.Elapsed.ToString ()));
                }
            }

            private void Test10Mb(bool newMethod)
            {
                Stopwatch sw = new Stopwatch ();

                List<List<byte>> tenMbTestData = new List<List<byte>> ();
                for (int i = 0 ; i < Iterations ; i++)
                    tenMbTestData.Add (Build10MbTestData ());

                var testData = new List<byte> (PacketSize * TenMbPacketCount);

                sw.Reset ();
                sw.Start ();

                if (!newMethod)
                {
                    for (int i = 0 ; i < Iterations ; i++)
                    {
                        testData = tenMbTestData[i];
                        PreProcessReadBytes (ref testData);
#if CHECK_RETURN_VALUES
                    int packetCount = testData.Count () / PacketDataSize;
                    if (packetCount != OneMbPacketCount)
                        throw new Exception (string.Format (AssertFormat, OneMbPacketCount, packetCount));
#endif
                    }
                    sw.Stop ();
                    Console.WriteLine (string.Format ("Elapsed time for 10Mb test data {0} iterations: {1}", Iterations, sw.Elapsed.ToString ()));
                }
                else
                {
                    for (int i = 0 ; i < Iterations ; i++)
                    {
                        testData = tenMbTestData[i];
                        PreProcessReadBytesNew (ref testData);
#if CHECK_RETURN_VALUES
                    int packetCount = testData.Count () / PacketDataSize;
                    if (packetCount != OneMbPacketCount)
                        throw new Exception (string.Format (AssertFormat, OneMbPacketCount, packetCount));
#endif
                    }
                    sw.Stop ();
                    Console.WriteLine (string.Format ("(NEW)Elapsed time for 10Mb test data {0} iterations: {1}", Iterations, sw.Elapsed.ToString ()));
                }
            }

            private void Test100Mb(bool newMethod)
            {
                Stopwatch sw = new Stopwatch ();

                List<List<byte>> oneHundredMbTestData = new List<List<byte>> ();
                for (int i = 0 ; i < Iterations ; i++)
                    oneHundredMbTestData.Add (Build100MbTestData ());

                var testData = new List<byte> (PacketSize * OneHundredMbPacketCount);

                sw.Reset ();
                sw.Start ();

                if (!newMethod)
                {
                    for (int i = 0 ; i < Iterations ; i++)
                    {
                        testData = oneHundredMbTestData[i];
                        PreProcessReadBytes (ref testData);
#if CHECK_RETURN_VALUES
                    int packetCount = testData.Count () / PacketDataSize;
                    if (packetCount != OneMbPacketCount)
                        throw new Exception (string.Format (AssertFormat, OneMbPacketCount, packetCount));
#endif
                    }
                    sw.Stop ();
                    Console.WriteLine (string.Format ("Elapsed time for 100Mb test data {0} iterations: {1}", Iterations, sw.Elapsed.ToString ()));
                }
                else
                {
                    for (int i = 0 ; i < Iterations ; i++)
                    {
                        testData = oneHundredMbTestData[i];
                        PreProcessReadBytesNew (ref testData);
#if CHECK_RETURN_VALUES
                    int packetCount = testData.Count () / PacketDataSize;
                    if (packetCount != OneMbPacketCount)
                        throw new Exception (string.Format (AssertFormat, OneMbPacketCount, packetCount));
#endif
                    }
                    sw.Stop ();
                    Console.WriteLine (string.Format ("(NEW)Elapsed time for 100Mb test data {0} iterations: {1}", Iterations, sw.Elapsed.ToString ()));
                }
            }

            #endregion

            #region Data construction methods

            private List<byte> BuildEmptyTestData()
            {
                return new List<byte> ();
            }

            private List<byte> Build1KbTestData()
            {
                var oneKbTestData = new List<byte> ();
                for (int i = 0 ; i < OneKbPacketCount ; i++)
                    oneKbTestData.AddRange (Packet);
                return oneKbTestData;
            }

            private List<byte> Build512KbTestData()
            {
                var oneKbTestData = new List<byte> ();
                for (int i = 0 ; i < FiveHundredTwelveKbPacketCount ; i++)
                    oneKbTestData.AddRange (Packet);
                return oneKbTestData;
            }

            private List<byte> Build1MbTestData()
            {
                var oneMbTestData = new List<byte> ();
                for (int i = 0 ; i < OneMbPacketCount ; i++)
                    oneMbTestData.AddRange (Packet);
                return oneMbTestData;
            }

            private List<byte> Build10MbTestData()
            {
                var tenMbTestData = new List<byte> ();
                for (int i = 0 ; i < TenMbPacketCount ; i++)
                    tenMbTestData.AddRange (Packet);
                return tenMbTestData;
            }

            private List<byte> Build100MbTestData()
            {
                var oneHundredMbTestData = new List<byte> ();
                for (int i = 0 ; i < OneHundredMbPacketCount ; i++)
                    oneHundredMbTestData.AddRange (Packet);
                return oneHundredMbTestData;
            }

            #endregion

            #region These are the methods I'm actually performance testing

            private void PreProcessReadBytes(ref List<byte> rawData)
            {
                int last4Index = 0;
                bool readData = false;
                byte[] last4 = new byte[4];

                // list to store individual data packets and the number of packets we've read
                List<byte> packets = new List<byte> ();

                // inspect raw data in 1 byte chunks - look for 4 byte sequences of start tokens (255)
                for (int i = 0 ; i < rawData.Count ; i++)
                {
                    // we are in read data mode so add this byte to our packet list
                    if (readData)
                        packets.Add (rawData[i]);
                    else
                    {
                        if (last4Index < 4)
                        {
                            // record this byte in the last 4 byte state array
                            last4[last4Index] = rawData[i];
                            last4Index++;
                        }
                        // we must have hit an invalid packet because the last 4 index
                        // should never be 3 at this point. when it is 3 it signifies that
                        // the last4 array is already full so re-index array
                        else
                            last4 = new byte[] { last4[1], last4[2], last4[3], rawData[i] };
                    }

                    // the packet has been fully read so add it to the list of processed raw data packets
                    if (packets.Count % PacketDataSize == 0)
                        readData = false;

                    // check to see if the last 4 bytes encountered were start tokens
                    if (last4[0] == PacketStartToken && last4[1] == PacketStartToken &&
                        last4[2] == PacketStartToken && last4[3] == PacketStartToken)
                    {
                        // set the read state variable to true
                        readData = true;

                        // clear out the last 4 byte state array and reset array index
                        last4 = new byte[4];
                        last4Index = 0;
                    }
                }

                // catch case where the last packet was incomplete - if so, throw out the partial last packet
                int remainder = packets.Count % PacketDataSize;
                if (remainder != 0)
                    packets.RemoveRange (packets.Count - remainder, remainder);

                rawData = packets;
            }

            private void PreProcessReadBytesNew(ref List<byte> rawData)
            {
                int last4Index = 0;
                bool readData = false;
                byte[] last4 = new byte[4];

                // list to store individual data packets and the number of packets we've read
                List<byte> packets = new List<byte> ();

                // inspect raw data in 1 byte chunks - look for 4 byte sequences of start tokens (255)
                for (int i = 0 ; i < rawData.Count ; i++)
                {
                    // we are in read data mode so add this byte to our packet list
                    if (readData)
                        packets.Add (rawData[i]);
                    else
                    {
                        if (last4Index < 4)
                        {
                            // record this byte in the last 4 byte state array
                            last4[last4Index] = rawData[i];
                            last4Index++;
                        }
                        // we must have hit an invalid packet because the last 4 index
                        // should never be 3 at this point. when it is 3 it signifies that
                        // the last4 array is already full so re-index array
                        else
                        {
                            last4[0] = last4[1];
                            last4[1] = last4[2];
                            last4[2] = last4[3];
                            last4[3] = rawData[i];
                        }
                    }

                    // the packet has been fully read so add it to the list of processed raw data packets
                    if (packets.Count % PacketDataSize == 0)
                        readData = false;

                    // check to see if the last 4 bytes encountered were start tokens
                    if (last4[0] == PacketStartToken && last4[1] == PacketStartToken &&
                        last4[2] == PacketStartToken && last4[3] == PacketStartToken)
                    {
                        // set the read state variable to true
                        readData = true;

                        // clear out the last 4 byte state array and reset array index
                        last4[0] = 0;
                        last4[1] = 0;
                        last4[2] = 0;
                        last4[3] = 0;
                        last4Index = 0;
                    }
                }

                // catch case where the last packet was incomplete - if so, throw out the partial last packet
                int remainder = packets.Count % PacketDataSize;
                if (remainder != 0)
                    packets.RemoveRange (packets.Count - remainder, remainder);

                rawData = packets;
            }

            #endregion
        }
    }
}
