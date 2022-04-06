using AbstractClasses.Abstract;
using Spire.Doc;
using Spire.Doc.Documents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractClasses.Concretes
{
    // FreeSpireDoc eklentisi kullanıldı.
    internal class WordDocument: AbstractClasses.Abstract.Document
    {
        public WordDocument(string name) : base(name)
        {
            FileExtention = ".docx";
            DocumentType = DocumentType.Word;
        }

        /// <summary>
        /// Path verilen word dosyasını Byte[] dönüştürür
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public override byte[] DownloadFileAsByte(string filePath)
        {
            string path = $"{Path.Combine(BasePath, filePath, $"{Name}{FileExtention}")}";

            Spire.Doc.Document document = new Spire.Doc.Document();
            //Documanı path'e göre okuduk
            document.LoadFromFile(path);
            byte[] toArray = null;

            // MemoryStream ile dokumanı ramde tampon bölgeye buffer ettik
            using (MemoryStream ms1 = new MemoryStream())
            {
                // Stream file Docx formatında yazdık
                document.SaveToStream(ms1, FileFormat.Docx);
                //save to byte array
                // rame çevrilen datayı byte[] dönüştürdük.
                toArray = ms1.ToArray();
            }

            return toArray;
        }

        /// <summary>
        /// Dosyayı base64 string formatına çeviren method.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public override string DownloadFileBase64(string filePath)
        {
            byte[] bytes = DownloadFileAsByte(filePath);
            return Convert.ToBase64String(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Gelen Datayı Docx olarak path yazdırdık
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="filePath"></param>
        public override void WriteFile<T>(T data, string filePath)
        {
            string path = $"{Path.Combine(BasePath, filePath, $"{Name}{FileExtention}")}";

            if (data is string)
            {
                // Create a Document instance
                Spire.Doc.Document doc = new Spire.Doc.Document();
                //Add a section
                Section section = doc.AddSection();

                /* Deneme yaptık
                section.AddColumn(100, 150);
                
                Table t = section.AddTable(showBorder: true);
                t.AddCaption("Tablo", CaptionNumberingFormat.Roman, CaptionPosition.AboveItem);
                TableRow tr = t.AddRow();
                TableCell tc = tr.AddCell();
                tc.Width = 100;
                Paragraph p = tc.AddParagraph();
                p.AppendText("Cell1");
                */


                //Add a paragraph
                Paragraph para = section.AddParagraph();
                //Append text to the paragraph
                para.AppendText(data as string);
                //Save the result document
                doc.SaveToFile(path, FileFormat.Docx);
            }
            else
            {
                throw new Exception("Text dosyasına sadece metinsel ifade yazdırabilirsiniz");
            }
        }
    }
}
