using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kf
{
    public class MemBlock
    {
        private byte[] data;
        private int index;

        public MemBlock(byte[] d)
        {
            data = d;
            index = 0;
        }
        public MemBlock(int size)
        {
            data = new byte[size];
            index = 0;
        }
        public void Seek(int pos)
        {
            if(pos<0)
            {
                pos = Math.Max(data.Length + pos, 0);
            }
            index = pos;
            if (index > data.Length)
                index = data.Length;
        }

        public byte[] Data
        {
            get { return data; }
            set { data = value; index = 0; }
        }

        public void Skip(int offset)
        {
            index = index + offset;
            if (index > data.Length)
                index = data.Length;
            if (index < 0)
                index = 0;
        }

        public int Current
        {
            get { return index; }
            set { Seek(value); }
        }

        public int Size
        {
            get { return data.Length; }
            set { data = new byte[value]; index = 0;}
        }

        public override string ToString() 
        {
            int columns = 16;
            string s = "";
            for (int start = 0; start < data.Length; start += columns)
            {
                if(data.Length>65535)
                    s += start.ToString("x8") + "   ";
                else
                    s += start.ToString("x4") + "   ";
                string hex = "";
                for(int i = start;i<Math.Min(start+ columns, data.Length);++i)
                {
                    hex += data[i].ToString("x2") + " ";
                }
                s += hex + new string(' ',(3*columns)-hex.Length);

                s += "   ";
                for (int i = start; i < Math.Min(start + columns, data.Length); ++i)
                {
                    byte[] b = new byte[1];
                    b[0] = data[i];
                    if (b[0] < 32)
                        b[0] = 128;
                    string c = System.Text.Encoding.UTF8.GetString(b);
                    s += c;
                }
                s += "\n";
            }
            return s;
        }


        public byte GetU8()
        {
            if (index <= data.Length - 1)
                return data[index++];
            return 0;
        }
        public ushort GetU16()
        {
            if (index <= data.Length - 2)
            {
                ushort value = BitConverter.ToUInt16(data, index);
                index += 2;
                return value;
            }
            return 0;
        }
        public uint GetU32()
        {
            if (index <= data.Length - 4)
            {
                uint value = BitConverter.ToUInt32(data, index);
                index += 4;
                return value;
            }
            return 0;
        }
        public sbyte GetS8()
        {
            if (index <= data.Length - 1)
                return (sbyte)data[index++];
            return 0;
        }
        public short GetS16()
        {
            if (index <= data.Length - 2)
            {
                short value = BitConverter.ToInt16(data, index);
                index += 2;
                return value;
            }
            return 0;
        }
        public int GetS32()
        {
            if (index <= data.Length - 4)
            {
                int value = BitConverter.ToInt32(data, index);
                index += 4;
                return value;
            }
            return 0;
        }
        public float GetFloat()
        {
            if (index <= data.Length - 4)
            {
                float value = BitConverter.ToSingle(data, index);
                index += 4;
                return value;
            }
            return 0;
        }
        public double GetDouble()
        {
            if (index <= data.Length - 8)
            {
                double value = BitConverter.ToDouble(data, index);
                index += 8;
                return value;
            }
            return 0;
        }

        public string GetUTF8(int fieldWidth = 0, bool zeroTerminated = true)
        {
            int fw = fieldWidth;
            string s = "";
            if (fieldWidth == 0)
                fieldWidth = data.Length - index;
            int end = Math.Min(fieldWidth + index, data.Length);
            bool foundZero = false;
            if (zeroTerminated)
            {
                for (int i = index; i < end; ++i)
                {
                    if (data[i] == 0)
                    {
                        end = i;
                        foundZero = true;
                        break;
                    }
                }
            }
            if (index < end)
            {

                s = System.Text.Encoding.UTF8.GetString(data, index, end - index);
                if (fw == 0)
                {
                    index = end;
                    if (foundZero)
                    {
                        index++;
                    }
                }
                else
                {
                    index += fw;
                }
            }

            return s;
        }


        public void SetU8(byte value)
        {
            if (index <= data.Length - 1)
            {
                data[index++] = value;
            }
        }
        public void SetU16(ushort value)
        {
            if (index <= data.Length - 2)
            {
                BitConverter.GetBytes(value).CopyTo(data, index);
                index += 2;
            }
        }
        public void SetU32(uint value)
        {
            if (index <= data.Length - 3)
            {
                BitConverter.GetBytes(value).CopyTo(data, index);
                index += 4;
            }
        }
        public void SetS8(sbyte value)
        {
            if (index <= data.Length - 1)
            {
                data[index++] = (byte)value;
            }
        }
        public void SetS16(short value)
        {
            if (index <= data.Length - 2)
            {
                BitConverter.GetBytes(value).CopyTo(data, index);
                index += 2;
            }
        }
        public void SetS32(int value)
        {
            if (index <= data.Length - 4)
            {
                BitConverter.GetBytes(value).CopyTo(data, index);
                index += 4;
            }
        }
        public void SetFloat(float value)
        {
            if (index <= data.Length - 4)
            {
                BitConverter.GetBytes(value).CopyTo(data, index);
                index += 4;
            }
        }
        public void SetDouble(double value)
        {
            if (index <= data.Length - 8)
            {
                BitConverter.GetBytes(value).CopyTo(data, index);
                index += 8;
            }
        }

        public void SetUTF8(string value, int fieldWidth = 0, bool zeroTerminated = true)
        {
            byte[] b = System.Text.Encoding.UTF8.GetBytes(value);
            int available = data.Length - index;

            if (fieldWidth==0)
            {
                int size = Math.Min(b.Length, zeroTerminated?available-1:available);
                for(int i=0;i<size;++i)
                {
                    data[index++] = b[i];
                }
                if (zeroTerminated)
                {
                    SetU8(0);
                }
            }
            else
            {
                int size = Math.Min(fieldWidth, available) - (zeroTerminated ? 1 : 0);

                for (int i = 0; i < size; ++i)
                {
                    if (i < b.Length)
                    {
                        data[index++] = b[i];
                    }
                    else
                    {
                        data[index++] = 0;
                    }
                }
                if (zeroTerminated)
                {
                    SetU8(0);
                }
            }
        }

        public byte[] GetArray(int size)
        {
            byte[] b = new byte[size];
            int copyAmount = Math.Min(data.Length - index, size);
            Array.Copy(data, index, b, 0, copyAmount);
            index += copyAmount;
            return b;
        }

        public void SetArray(byte[] value)
        {
            int size = Math.Min(value.Length, data.Length - index);
            if(size>0)
            {
                Array.Copy(value, 0, data, index, size);
                index += size;
            }
        }

    }
}
