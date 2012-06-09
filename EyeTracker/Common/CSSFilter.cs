using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;

namespace EyeTracker.Common
{
    public class CSSFilter : Stream
    {
        private Stream stream;
        private long position;

        public CSSFilter(Stream stream)
        {
            this.stream = stream;
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void Flush()
        {
            this.stream.Flush();
        }

        public override long Length
        {
            get { return 0; }
        }

        public override long Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.stream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            this.stream.Close();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            string html = System.Text.Encoding.Default.GetString(buffer);
            List<string> files = new List<string>();
            List<string> tags = new List<string>();

            // replace css
            int i = 0;
            while ((i = html.IndexOf("rel=\"stylesheet\"", i)) > -1)
            {
                i++;
                int tagStart = html.Substring(0, i).LastIndexOf('<');
                int tagEnd = html.IndexOf('>', i);
                string tag = html.Substring(tagStart, tagEnd + 1 - tagStart);
                int fileStart = tag.IndexOf("href=\"") + 6;
                int fileEnd = tag.IndexOf("\"", fileStart);
                string href = tag.Substring(fileStart, fileEnd - fileStart).ToLower();
                string file = Path.GetFileNameWithoutExtension(href);

                // Get content folder relative url
                string[] fileParts = Path.GetDirectoryName(href).Split(new char[]{'\\'}, StringSplitOptions.RemoveEmptyEntries);
                string filePath = string.Empty;
                if (fileParts.Length > 0)
                {
                    var fp = fileParts.SkipWhile(p => p == "content");
                    if (fp.Any())
                    {
                        filePath = HttpUtility.UrlEncode(string.Join("_", fp.ToArray()) + "_");
                    }
                }
                filePath += file;

                files.Add(filePath);
                tags.Add(tag);
            }

            if (tags.Any())
            {
                html = html.Replace(tags.First(), string.Format("<link href=\"/content/css/{0}/{1}.css\" rel=\"stylesheet\" type=\"text/css\" />", string.Join("/", files.Take(files.Count - 1)), files.Last()));
                foreach (var t in tags)
                {
                    html = html.Replace(t, string.Empty);
                }
            }

            byte[] outdata = System.Text.Encoding.Default.GetBytes(html);

            //write bytes to stream
            this.stream.Write(outdata, 0, outdata.GetLength(0));
        }
    }
}