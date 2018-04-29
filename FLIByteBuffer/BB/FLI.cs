using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace FLI
{
    public class ByteBuffer : IDisposable
    {
        List<byte> Buff;
        byte[] readBuff;
        int readpos;
        bool buffUpdate = false;


        //IDisposable
        private bool disposedVal = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedVal)
            {
                if (disposing)
                {
                    Buff.Clear();
                }
                readpos = 0;
            }
            this.disposedVal = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }


        //Creating a new ByteBuffer
        public ByteBuffer()
        {
            Buff = new List<byte>();
            readpos = 0;
        }

        public int GetReadPos()
        {
            return readpos;
        }

        public byte[] toArray()
        {
            return Buff.ToArray();
        }
        public int Count()
        {
            return Buff.Count;
        }
        //If String a is 30 chars, the next count will start at 31
        public int Lenght()
        {
            return Count() - readpos;
        }
        //Clear the buffer
        public void Clear()
        {
            Buff.Clear();
            readpos = 0;
        }



        //Whatever is being passed Through account for that
        public void WriteByte(byte inputs)
        {
            Buff.Add(inputs);
            buffUpdate = true;
        }

        public byte ReadByte(bool peak = true)
        {
            if (Buff.Count > readpos)
            {
                if (buffUpdate)
                {
                    //Reading Data into new intance
                    readBuff = Buff.ToArray();
                    buffUpdate = false;
                }

                byte x = readBuff[readpos];

                if (peak & Buff.Count > readpos)
                {
                    //Dont need to read it again
                    readpos += 4;
                }
                return x;
            }
            else
            {
                throw new Exception("ByteBuffer is Over the limit!");
            }
        }

        public void WriteBytes(byte[] input)
        {
            Buff.AddRange(input);
            buffUpdate = true;
        }

        public byte[] ReadBytes(int l, bool peak = true)
        {
            if (buffUpdate)
            {
                readBuff = Buff.ToArray();
                buffUpdate = false;
            }

            byte[] x = Buff.GetRange(readpos, l).ToArray();

            if (peak)
            {
                readpos += l;
            }
            return x;
        }


        public void WriteShort(short input)
        {
            Buff.AddRange(BitConverter.GetBytes(input));
            buffUpdate = true;
        }

        public void WriteInt(int input)
        {
            Buff.AddRange(BitConverter.GetBytes(input));
            buffUpdate = true;
        }

        public int ReadInt(bool peak = true)
        {
            if (Buff.Count > readpos)
            {
                if (buffUpdate)
                {
                    //Reading Data into new intance
                    readBuff = Buff.ToArray();
                    buffUpdate = false;
                }

                int x = BitConverter.ToInt32(readBuff, readpos);

                if (peak & Buff.Count > readpos)
                {
                    //Dont need to read it again
                    readpos += 4;
                }
                return x;
            }
            else
            {
                throw new Exception("ByteBuffer is Over the limit!");
            }
        }

        public void WriteFloat(float input)
        {
            Buff.AddRange(BitConverter.GetBytes(input));
            buffUpdate = true;
        }

        public float ReadFloat(bool peak = true)
        {
            if (Buff.Count > readpos)
            {
                if (buffUpdate)
                {
                    //Reading Data into new intance
                    readBuff = Buff.ToArray();
                    buffUpdate = false;
                }

                //Single is a Float
                float x = BitConverter.ToSingle(readBuff, readpos);

                if (peak & Buff.Count > readpos)
                {
                    //Dont need to read it again
                    readpos += 4;
                }
                return x;
            }
            else
            {
                throw new Exception("ByteBuffer is Over the limit!");
            }
        }



        //We want the lenght of the string
        public void WriteString(string input)
        {
            Buff.AddRange(BitConverter.GetBytes(input.Length));
            Buff.AddRange(Encoding.ASCII.GetBytes(input));
            buffUpdate = true;
        }

        public string ReadString(bool peak = true)
        {
            int lenght = ReadInt(true);

            if (buffUpdate = true)
            {
                readBuff = Buff.ToArray();
                buffUpdate = false;
            }

            string x = Encoding.ASCII.GetString(readBuff, readpos, lenght);
            if (peak & Buff.Count > readpos)
            {
                if (x.Length > 0)
                {
                    readpos += lenght;
                }

            }
            return x;
        }


    }

}