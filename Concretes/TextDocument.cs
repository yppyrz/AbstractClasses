using AbstractClasses.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractClasses.Concretes
{
    /// <summary>
    /// Concrete sınıflarda abstract sınıftan gelen tüm abstract üyeler implemente edilmelidir. abstract olarak işaretlenmiş tüm üyeleri ovverride etmek zorundayız
    /// </summary>
    internal class TextDocument : Document
    {
        public TextDocument(string name) : base(name)
        {
            DocumentType = DocumentType.Text;
            FileExtention = ".txt";
        }

        /// <summary>
        /// Bir dizin altındaki dosyamızı Byte[] çeviren method
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public override byte[] DownloadFileAsByte(string filePath)
        {
            string path = Path.Combine(BasePath, filePath, $"{Name}{FileExtention}");

            if (File.Exists(path))
            {
                using (System.IO.TextReader readFile = new StreamReader(path))
                {
                    string data = readFile.ReadToEnd();
                    List<byte> bytes = new List<byte>();

                    foreach (var item in data)
                    {
                        bytes.Add(Convert.ToByte(item));
                    }

                    readFile.Close();

                    return bytes.ToArray();

                }
            }
            else
            {
                throw new Exception("Böyle bir dosya bulunamadı!");
            }


        }

        /// <summary>
        /// byte[] c# da base64 formatına dönüştürülebilir. Bu sebep ile data önce byte[] çevirmek için DownloadFileAsByte methodumuzu kullandık. Convert.ToBase64String methodu ile base64 formatına çevirdik.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public override string DownloadFileBase64(string filePath)
        {
            byte[] bytes = DownloadFileAsByte(filePath);
            return Convert.ToBase64String(bytes);
        }

        public override void WriteFile<T>(T data, string filePath)
        {
            // Path.Combine ile dizin oluşturma işlemi yapar
            string path = $"{Path.Combine(BasePath, filePath, $"{Name}{FileExtention}")}";

            if (data is string)
            {
                if (System.IO.File.Exists(path))
                {
                    throw new Exception("Daha önce aynı isimde bir dosya oluşturmuşsunuz");
                }
                else
                {
                    try
                    {
                        // sistem kaynaklarını kullanan sınıflar arasında iletişim yaparken using keyword içerisinde instance alınır. sebebi ise c# bu özel sınıfların ramden temizlenmesini tek başına yönetemez. biz bu tarz sınıflara unmanagement resource diyoruz. database bağlantıları, uzak sunucu bağlantıları, dosya okuma yazma işlemleri bunlara örnek teşkil eder.
                        using (System.IO.TextWriter writeFile = new StreamWriter(path))
                        {
                            writeFile.Write(data);
                            writeFile.Flush();
                            writeFile.Close();
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }


                }

                // Dosya kaydetme işlemi
            }
            else
            {
                throw new Exception("Text dosyasına sadece metinsel ifade yazdırabilirsiniz");
            }
        }
    }
}
