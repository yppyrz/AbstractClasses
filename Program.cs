using AbstractClasses.Concretes;
using System;

namespace AbstractClasses
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // bu gibi sınıflar soyut (abstract) sınıf bundan kalıtım alan ve instance alınabilen sınıflara somut sınıf (concrete)
            //var d = new AbstractClasesAndStaticClasses.Abstracts.Document(name: 'AAA');
            //Console.WriteLine(d.DocumentType.ToString());
            // Sample.txt
            var t = new TextDocument(name: "Sample");
            // t.WriteFile<string>(data:"Selam", filePath: "Files");
            byte[] data = t.DownloadFileAsByte(filePath: "Files");
            string base64 = t.DownloadFileBase64(filePath: "Files");


            var w = new WordDocument("Deneme2");
            w.WriteFile<string>(data: "Hello MS Word!", filePath: "Files");
            var word = w.DownloadFileAsByte(filePath: "Files");
            string base64Word = w.DownloadFileBase64(filePath: "Files");
        }
    }
}
