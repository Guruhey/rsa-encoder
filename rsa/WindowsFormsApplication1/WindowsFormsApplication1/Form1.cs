using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace WindowsFormsApplication1
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Взятие текста из файла
        /// </summary>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";//Изначальное имя файла, чтобы поле имя файла не называлось openFileDialog
            OpenFileDialog of = openFileDialog1;// Переменной of присваиваем openFileDialog1
            of.Filter = "txt file *.txt |*.txt";// Делаем фильтр, чтобы искать только txt файлы
            of.Title = "OPEN As...";// Титул будет называться "OPEN As..."
            DialogResult dr = of.ShowDialog();//Вызываем окно
            if (dr == System.Windows.Forms.DialogResult.OK)// Если результат диалога = ОК
            {
                textBox1.Text = System.IO.File.ReadAllText(of.FileName);// В textBox1 записывается весь текст из файла
            }
            
        }
        /// <summary>
        /// Сохранение textboxa в файл
        /// </summary>
        private void saveAssToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = saveFileDialog1;//Переменной sfd присваеваем saveFileDialog1
            sfd.Filter = "txt file *.txt |*.txt";// Делаем фильтр, чтобы искать только txt файлы
            sfd.Title = "Save As...";// Титул будет называться "Save As..."

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)// Если результат диалога = ОК
                {
                    if (sfd.FileName != "")//Если имя файла не равно пустоте
                    {
                        StreamWriter streamWriter = new StreamWriter(sfd.FileName);// Задаем переменную streamWriter
                        streamWriter.Write(textBox1.Text);//Записываем из текстбокса в файл
                        streamWriter.Close();//Закрываем
                    }
                }

        }
        /// <summary>
        /// Выход из приложения
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();// Закрываем приложение
        }
        /// <summary>
        /// Зашифровка
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            knpc Xuy = new knpc();//Обращаемся к классу
            textBox1.Text = Xuy.Encrypt(textBox1.Text);// Через переменную шифруем информацию из текстбокса в текстбокс
        }
        /// <summary>
        /// Расшифровка
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            knpc Xuy = new knpc();//Обращаемся к классу
            textBox1.Text = Xuy.Decrypt(textBox1.Text);// Через переменную расшифровываем информацию из текстбокса в текстбокс
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
        /// <summary>
        /// Генерация ключей
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            knpc Xuy = new knpc();//Обращаемся к классу
            Xuy.keys();//Генерируем ключи
        }
        
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


    }
}

/*
            using (RSACryptoServiceProvider ffs = new RSACryptoServiceProvider()) //инициализируем провайдер
            {
                pukey = ffs.ToXmlString(false); //public key
                SaveFileDialog sa = saveFileDialog1;
                sa.Filter = "public file ( *.pke )|*.pke";
                sa.Title = "Save As...";
                DialogResult dr = sa.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    File.WriteAllText(sa.FileName, pukey);
                }

                privkey = ffs.ToXmlString(true); // private key
                SaveFileDialog sav = saveFileDialog1;
                sa.Filter = "private file ( *.kez )|*.kez";
                sa.Title = "Save As...";
                DialogResult pr = sa.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    File.WriteAllText(sa.FileName, privkey);
                }
                ffs.FromXmlString(pukey);  //berem publ 
                byte[] DataToEncrypt; // база для шифрования
                byte[] Encrypted; // baza pod uje zaseyveniye
                UTF8Encoding ByteConverter = new UTF8Encoding();// инициализируем байтконвектор
                string hi = textBox1.Text;// hi = text iz boxa 
                DataToEncrypt = ByteConverter.GetBytes(hi); //beren soobsheniye i shifruem s pomoshu baytconvektora
                Encrypted = ffs.Encrypt(DataToEncrypt, false);
                cont = Convert.ToBase64String(Encrypted); //result
                textBox1.Text = cont;
            }



                using (RSACryptoServiceProvider ffs = new RSACryptoServiceProvider())
            {
                openFileDialog1.FileName = "";
                OpenFileDialog ofd = openFileDialog1;
                ofd.Filter = "private file ( *.kez )|*.kez";
                ofd.Title = "Onen As...";
                DialogResult dr = ofd.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    privkey = File.ReadAllText(ofd.FileName);
                }
                ffs.FromXmlString(privkey);
                byte[] Decrypted;
                byte[] DataToDecrypt = Convert.FromBase64String(cont);
                UTF8Encoding ByteConverter = new UTF8Encoding();
                Decrypted = ffs.Decrypt(DataToDecrypt, false);
                cont = ByteConverter.GetString(Decrypted);
                textBox1.Text = cont;
            }

*/
//Перевод в  string 