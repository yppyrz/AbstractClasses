using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractClasses.Abstract
{
    public enum DocumentType
    {
        Word,
        Excel,
        PDF,
        Text,
        XML,
        JSON
    }
    // Bir işin teknik detayını bilmeiyorsak fakat bu işin birden fazla şekilde bir teknik ile yapılacağını biliyorsak, bu işi yapacak sınıflara rehberlik eden base bir sınıf ile çalışır. Bu base sınıftan instance alınamaz. Sebebi abstract keyword işaretlenmesidir. Abstract sınıflar veri tabanı nesnesi olarak kullanılamazlar. Sebebi instance (örneğinin) alınamamasıdır. Sadece abstract sınıflar içerisinde abstract üyeler tanımlanabilir. Bu üyeler property veya method olabilir. Abstract tanımlanmış üyeler kalıtım altığımız sınıflarda ovveride edilmek zorundadır. Protected keyword ile kalıtım alınan sınıfın özelliklerine erişim değişiklik uygulanabilir.
    internal abstract class Document
    {
        /// <summary>
        /// Tüm doküman tipleri bu dosya dizini altında kaydolacağı için base sınıfta bu işlemi yaptık
        /// </summary>
        protected string BasePath { get; private set; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);

        /// <summary>
        /// Dokuman tipi, WORD, EXCEL, PDF vb.. Yüklenecek olan dosyanın uzantısından bilebiliriz.
        /// </summary>
        public DocumentType DocumentType { get; protected set; }

        /// <summary>
        /// Kaç Byte yer kapladığı. Dokumanı yüklerken bildiğimiz bir değer son kullanıcıdan bu değer biz doküman yükleme aşamasında gelecek
        /// </summary>
        public long Size { get; protected set; }

        /// <summary>
        /// Dokumanları kaydediğimiz dosya dizini tutarız
        /// </summary>
        public string FilePath { get; protected set; }

        /// <summary>
        /// Doküman ismi 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Yüklenecek olan dosyanın uzantısı (.docx,.xls.pdf vb)
        /// </summary>
        public string FileExtention { get; protected set; }

        /// <summary>
        /// Bir şeyin döküman tipinde tutulabilmesi için adı ve yükleneceği konum yeterlidir.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="filePath"></param>
        public Document(string name)
        {
            Name = name;
        }

        /// <summary>
        ///  Bu method içerisinde nasıl bir algoritma çalışacağını bu sınıf bilmiyor ama birşey döküman ise bu dökümana bir verinin kaydedilmesi gerektiğini biliyor.
        /// </summary>
        /// <typeparam name="T">Herhangi bir tipten veri dışarıdan alıcaz</typeparam>
        /// <param name="data"></param>
        public abstract void WriteFile<T>(T data, string filePath);

        /// <summary>
        /// Ya dosya byte[] şeklinde indirilebilir. yada Base64 formatında indirilebilir.
        /// </summary>
        /// <param name="pathName"></param>
        /// <returns></returns>
        public abstract byte[] DownloadFileAsByte(string filePath);

        /// <summary>
        /// Base64 formatında veri okuruz.
        /// </summary>
        /// <param name="pathName"></param>
        /// <returns></returns>
        public abstract string DownloadFileBase64(string filePath);

    }
}
