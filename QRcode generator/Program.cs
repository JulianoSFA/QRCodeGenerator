using System;
using System.Drawing;
using System.IO;

namespace QRcode_generator
{
    class Program
    {
        int size;
        int qrCodeCounter;
        string quantity;
        string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/QRcode";

        static void Main()
        {
            Program programa = new Program();
            programa.Run();
        }

        void Run()
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/QRcode"))
            {
                Console.WriteLine("Criando Diretório...");
                CreateDirectory();
            } 
            else
                Console.WriteLine("Diretório existente");
            

            Console.WriteLine("Coloque o tamanho da imagem");
            size = int.Parse(Console.ReadLine());
            string[] text = System.IO.File.ReadAllLines(path + "/Links.txt"); //Lê as linhas do txt e coloca cada uma em um array

            for (int i = 0; i < text.Length; i++) 
            {
                Bitmap createdQRCode = CreateQRCode(size, text[i]);
                createdQRCode.Save("C:/Users/Juliano/Desktop/QRcode/Saida/" + i + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                qrCodeCounter++;
                int item = i+1;
                Console.WriteLine(string.Format("Item {0} concluido", item));
            } //para cada item do array, gera um QRcode

            if (text.Length > 1)
            {
                quantity = " itens";
            }
            else
            {
                quantity = " item";
            }
            
            //Feedback da quantidade de itens concluidos
            Console.WriteLine("Operação concluida para " + qrCodeCounter + quantity);
            Console.ReadLine();

        }

        Bitmap CreateQRCode(int size, string text)
        {
            var qrCode = new ZXing.BarcodeWriter();
            var encodeOptions = new ZXing.Common.EncodingOptions() {Width = size, Height = size, Margin = 0 };
            qrCode.Options = encodeOptions;
            qrCode.Format = ZXing.BarcodeFormat.QR_CODE;
            var result = new Bitmap(qrCode.Write(text));
            return result;
        } //Gera o QRcode da string com base no tamanho (Configurações fixas) (.bmp)

        void CreateDirectory()
        {
            Directory.CreateDirectory(path);
            Directory.CreateDirectory(path + "/Saida");
            File.Create(path + "/Links.txt");
        }
    }
}
