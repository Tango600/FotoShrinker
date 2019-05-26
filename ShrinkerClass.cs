using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Drawing.Imaging;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FotoShrinker
{
    public class ShrinkerClass
    {
        private List<string> FilesList = new List<string>();
        private readonly int MaxTagLength = 900;

        private void DirSearch(string sDir)
        {
            string searchMask = "*.jp*g";
            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d, searchMask))
                    {
                        FilesList.Add(f);
                    }
                    DirSearch(d);
                    FilesList.AddRange(Directory.GetFiles(sDir, searchMask));
                }
                FilesList.AddRange(Directory.GetFiles(sDir, searchMask));
            }
            catch (Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }

        public bool ShrinkFolder(string InFolder, string OutFolder, int maxWidth, int Quality, bool WriteTAG, IEnumerable<string> tagData, IEnumerable<string> tagResized, int ThreadsCount, bool RenameToNum)
        {
            if (Directory.Exists(InFolder))
            {
                if (!Directory.Exists(OutFolder))
                    Directory.CreateDirectory(OutFolder);
                if (OutFolder.Last() != Path.DirectorySeparatorChar)
                    OutFolder += Path.DirectorySeparatorChar;

                DirSearch(InFolder);

                int fileNum = 1;
                var tasks = new ConcurrentQueue<Task>();
                foreach (var file in FilesList)
                {
                    if (ThreadsCount > 1)
                        while (tasks.Count >= ThreadsCount)
                        {
                            Task.WaitAny(tasks.ToArray());
                        }

                    Task t = new Task(() =>
                    {
                        string filename = Path.GetFullPath(file).Replace(InFolder, OutFolder);
                        if (!IsTag(file))
                        {
                            var snip = Resize(file, InFolder, OutFolder, maxWidth, Quality, RenameToNum ? fileNum++ : -1);
                            if (snip.Resized)
                            {
                                if (WriteTAG)
                                {
                                    WriteTag(filename, tagData.ToArray().Concat(tagResized.ToArray()).Select(f => Snippets.FormatWith(f, new object[1] { snip })).ToArray());
                                }
                            }
                            else
                            {
                                if (WriteTAG)
                                    WriteTag(filename, tagData);
                            }
                        }
                        else
                        {
                            if (InFolder.ToLower() != OutFolder.ToLower())
                            {
                                if (!File.Exists(filename))
                                    File.Copy(file, filename, true);
                            }
                        }
                        tasks.TryDequeue(out t);
                    });
                    tasks.Enqueue(t);
                    t.Start();
                }
            }
            return true;
        }

        public Snippets.Snippet Resize(string FileName, string InFolder, string OutFilePath, int MaxWidth, int Quality, int FileNum)
        {
            if (!Directory.Exists(OutFilePath))
                Directory.CreateDirectory(OutFilePath);

            long size = 0;
            string filename = "";
            if (FileNum == -1)
            {
                filename = OutFilePath + Path.GetFileName(FileName);
            }
            else
            {
                filename = OutFilePath + "Image#" + FileNum + Path.GetExtension(FileName);
            }
            string sdir = "";
            foreach (var dir in Path.GetDirectoryName(filename).Split(Path.DirectorySeparatorChar))
            {
                sdir += dir + Path.DirectorySeparatorChar;
                if (!Directory.Exists(sdir))
                    Directory.CreateDirectory(sdir);
            }
            DateTime fileDate = File.GetLastWriteTime(FileName).AddSeconds(1);

            Bitmap image;
            using (FileStream fsSource = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                size = fsSource.Length;
                image = new Bitmap(fsSource);
                fsSource.Close();
            }
            int originalWidth = image.Width;
            int originalHeight = image.Height;

            var snip = new Snippets.Snippet
            {
                Path = FileName,
                Height = originalHeight,
                Width = originalWidth,
                Size = size,
                Resized = false
            };

            if (Math.Max(originalWidth, originalHeight) > MaxWidth + 10)
            {
                float ratio = (float)MaxWidth / (float)Math.Max(originalWidth, originalHeight);

                int newWidth = (int)(originalWidth * ratio);
                int newHeight = (int)(originalHeight * ratio);

                using (Bitmap newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb))
                {
                    using (Graphics graphics = Graphics.FromImage(newImage))
                    {
                        graphics.CompositingQuality = CompositingQuality.HighQuality;
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.SmoothingMode = SmoothingMode.HighQuality;
                        graphics.DrawImage(image, 0, 0, newWidth, newHeight);
                    }

                    ImageCodecInfo imageCodecInfo = ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);

                    Encoder encoder = Encoder.Quality;
                    EncoderParameters encoderParameters = new EncoderParameters(1);
                    EncoderParameter encoderParameter = new EncoderParameter(encoder, Quality);
                    encoderParameters.Param[0] = encoderParameter;
                    try
                    {
                        if (File.Exists(filename))
                            File.Delete(filename);

                        using (FileStream fsImage = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Write))
                        {
                            newImage.Save(fsImage, imageCodecInfo, encoderParameters);
                        }
                    }
                    finally
                    {

                    }
                }
                snip.Resized = true;
                File.SetCreationTime(filename, fileDate);
                File.SetLastWriteTime(filename, fileDate);
            }
            else
            {
                if (InFolder.ToLower() != OutFilePath.ToLower())
                {
                    if (!File.Exists(filename))
                        File.Copy(FileName, filename, true);
                }
            }
            image.Dispose();

            return snip;
        }

        public bool IsTag(string InFileName)
        {
            int buffLength = MaxTagLength;
            byte[] tagData;
            using (FileStream fs = new FileStream(InFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                fs.Seek(fs.Length - buffLength, SeekOrigin.Begin);

                tagData = new byte[buffLength];
                fs.Read(tagData, 0, buffLength);
                fs.Close();
            }

            bool tagExists = false;
            for (int i = buffLength - 1; i > 0; i--)
            {
                if (tagData[i] == 84)
                {
                    if (tagData[i + 1] == 65)
                    {
                        if (tagData[i + 2] == 71)
                        {
                            if (tagData[i + 3] == 42)
                            {
                                tagExists = true;
                                break;
                            }
                        }
                    }
                }
            }
            return tagExists;
        }

        public IEnumerable<string> ReadTag(string InFileName)
        {
            var tag = new List<string>();
            int buffLength = MaxTagLength;
            byte[] tagData;
            using (FileStream fs = new FileStream(InFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                fs.Seek(fs.Length - buffLength, SeekOrigin.Begin);
                tagData = new byte[buffLength];
                fs.Read(tagData, 0, buffLength);
                fs.Close();
            }

            int pos = -1;
            for (int i = buffLength - 1; i > 0; i--)
            {
                if (tagData[i] == 84)
                {
                    if (tagData[i + 1] == 65)
                    {
                        if (tagData[i + 2] == 71)
                        {
                            if (tagData[i + 3] == 42)
                            {
                                pos = i;
                                break;
                            }
                        }
                    }
                }
            }

            if (pos != -1)
            {
                byte[] tagDataS = new byte[buffLength - pos];

                for (int i = pos; i < buffLength; i++)
                {
                    tagDataS[i - pos] = tagData[i];
                }

                string stag = string.Join("", tagDataS.Select(f => (char)f).Select(f => f.ToString()));
                tag = stag.Split('*').Select(f => f.Replace("/&astrx", "*")).Skip(1).Where(f => !string.IsNullOrEmpty(f)).ToList();
            }
            return tag;
        }

        public void WriteTag(string InFileName, IEnumerable<string> Params)
        {
            if (File.Exists(InFileName))
            {
                var TAG = ReadTag(InFileName).ToArray();
                if (!TAG.Any(f => Params.Any(x => x.ToLower() == f.ToLower())))
                {
                    File.SetAttributes(InFileName, FileAttributes.Normal);

                    int buffLength = MaxTagLength;
                    DateTime fileDate = File.GetLastWriteTime(InFileName);

                    using (FileStream fs = new FileStream(InFileName, FileMode.Append, FileAccess.Write, FileShare.Write))
                    {
                        string tagS = "";
                        if (TAG.Count() == 0)
                            tagS = "TAG*";
                        tagS += string.Join("*", TAG.Concat(Params).Distinct().Select(f => f.Replace("*", "/&astrx"))) + "*";

                        byte[] tagData = tagS.ToArray().Select(f => (byte)f).ToArray();

                        if (tagData.Length < MaxTagLength + 1)
                            fs.Write(tagData, 0, tagData.Length);
                        fs.Close();
                    }

                    File.SetCreationTime(InFileName, fileDate.AddSeconds(1));
                    File.SetLastWriteTime(InFileName, fileDate.AddSeconds(1));
                }
            }
        }
    }
}
