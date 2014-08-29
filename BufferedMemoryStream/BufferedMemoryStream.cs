namespace BufferedMemoryStream
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class BufferedMemoryStream : Stream
    {
        private List<byte[]> allocations = new List<byte[]>();

        /// <summary>
        /// Returns the contents on the stream as a byte array
        /// </summary>
        /// <returns></returns>
        public byte[] ToArray()
        {
            byte[] rtn = new byte[this.Length];

            int offset = 0;
            foreach (byte[] buffer in allocations)
            {
                Buffer.BlockCopy(buffer, 0, rtn, offset, buffer.Length);
                offset += buffer.Length;
            }

            return rtn;
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void Flush()
        {
        }

        public override long Length
        {
            get
            {
                int offset = 0;
                foreach (byte[] buffer in allocations)
                {
                    offset += buffer.Length;
                }
                return offset;
            }
        }

        public override long Position
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            byte[] internalBuffer = new byte[count];

            Buffer.BlockCopy(buffer, offset, internalBuffer, 0, count);

            this.allocations.Add(internalBuffer);
        }

        public override void Close()
        {
            this.allocations.Clear();

            GC.Collect();

            base.Close();
        }
    }

}