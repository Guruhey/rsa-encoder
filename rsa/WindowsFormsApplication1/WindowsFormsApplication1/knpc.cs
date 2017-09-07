using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class knpc
    {
        string pukey = "";//переменная которая отвечает за публичный ключ
        string privkey = "";//премеенная которая будет отвечать за приватный ключ
        string cont = "";// Возвращаевая переменная для зашифровки
        /// <summary>
        /// Это генерация ключей
        /// </summary>
        public void keys()
        {
            using (RSACryptoServiceProvider ffs = new RSACryptoServiceProvider())
            {
                pukey = ffs.ToXmlString(false); //Создаем public key
                SaveFileDialog sa = new SaveFileDialog();//Присваеваем sa = SaveFileDialog()
                sa.Filter = "public file ( *.txt )|*.txt";// Делаем фильтр, чтобы искать только pke файлы
                sa.Title = "Save As...";//Делаем титул
                DialogResult dr = sa.ShowDialog();//Вызываем окно диалога
                if (dr == System.Windows.Forms.DialogResult.OK)// Если результат диалога = ОК
                {

                    File.WriteAllText(sa.FileName, pukey);//Пишем из переменной pukey в файл
                    privkey = ffs.ToXmlString(true); //Создаем private key
                    SaveFileDialog sav = new SaveFileDialog();//Присваеваем sav = SaveFileDialog()
                    sav.Filter = "private file ( *.kez )|*.kez";//Делаем фильтр, чтобы искать только kez файлы
                    sav.Title = "Save As...";//Делаем титул
                    DialogResult pr = sav.ShowDialog();//Вызываем окно диалога
                    if (dr == System.Windows.Forms.DialogResult.OK)// Если результат диалога = ОК
                    {
                        File.WriteAllText(sav.FileName, privkey);//Пишем из переменной privkey в файл
                    }
                }
               
            }
        }
        /// <summary>
        /// Если посмотреть на Encrypt, можно понят что это шифровка.
        /// </summary>
        /// <param name="doc"></param>
        /// <returns>Зашифрованное сообщение</returns>
        public string Encrypt(string doc)
        {
            using (RSACryptoServiceProvider ffs = new RSACryptoServiceProvider())
            {

                OpenFileDialog op = new OpenFileDialog();//Присваеваем op = OpenFileDialog()
                op.FileName = "";//Имя файла равно пустоте
                op.Filter = "public file ( *.txt )|*.txt";//Делаем фильтр, чтобы искать только pke файлы
                op.Title = "Open As...";//Делаем титул
                DialogResult dr = op.ShowDialog();//Вызываем окно диалога
                if (dr == System.Windows.Forms.DialogResult.OK)// Если результат диалога = ОК
                {
                    pukey = File.ReadAllText(op.FileName);//Считывем из файла публ.ключ и пишем в переменную
                    UTF8Encoding ByteConverter = new UTF8Encoding();//Инициализация функции кофертации из кодировок
                    byte[] encryptedData = null;//Переменная для хранения зашифр.инфы в байтах
                    byte[] DataToEncrypt = ByteConverter.GetBytes(doc);//Перевод входящего массива string в другую кодировку
                    ffs.FromXmlString(pukey);//импорт публичного ключа в класс
                    encryptedData = ffs.Encrypt(DataToEncrypt, false);//Запись зашифр.инфы в массив байтов
                    cont = Convert.ToBase64String(encryptedData);//Кофертация из байтов в base64 строку
                    
                }
                return cont;
            }
        }
        /// <summary>
        /// Если посмотреть на Decrypt, можно понят что это расшифровка.
        /// </summary>
        /// <param name="ccont">Переменная с результататом</param>
        /// <returns>Расшифрованное сообщение</returns>
        public string Decrypt(string ccont)
        {
            using (RSACryptoServiceProvider ffs = new RSACryptoServiceProvider())
            {
                OpenFileDialog kk = new OpenFileDialog();//Присваеваем kk = OpenFileDialog()
                kk.FileName = "";//Имя файла равно пустоте
                OpenFileDialog offd = kk;//Присваеваем offd = kk
                offd.Filter = "private file ( *.kez )|*.kez";//Делаем фильтр, чтобы искать только kez файлы
                offd.Title = "Open As...";//Делаем титул
                DialogResult dr = offd.ShowDialog();//Вызываем окно диалога
                if (dr == System.Windows.Forms.DialogResult.OK)// Если результат диалога = ОК
                {
                    privkey = File.ReadAllText(offd.FileName);//Считывем из файла прив.ключ и пишем в переменную
                    UTF8Encoding ByteConverter = new UTF8Encoding();//Инициализация функции кофертации из кодировок
                    byte[] decryptedData = null;//Переменная для хранения расшифр.инфы в байтах
                    byte[] DataToDecrypt = Convert.FromBase64String(ccont);//
                    ffs.FromXmlString(privkey);//импорт приватного ключа в класс
                    decryptedData = ffs.Decrypt(DataToDecrypt, false);//Запись расшифр.инфы в массив байтов
                    string result = ByteConverter.GetString(decryptedData);//Переводим расшифрованные байты в текст
                    return result;
                }
            }
           return ccont;
           
        }
    }
}
