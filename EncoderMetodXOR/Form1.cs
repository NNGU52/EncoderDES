using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
using System.IO;

namespace EncoderMetodXOR
{
    public delegate void CreatingKey(ref string key);
    public partial class Form1 : Form
    {
        public CreatingKey key_create; // создаем переменную делегата
        int byteSize; //количество битов на одну букву для шифра XOR
        int byteSizeDES; //количество битов на одну букву для шифра DES
        int Blocksize; //количество символов в блоке
        int Key_Lenght; //длина ключа для шифра DES
        int[] ip; //массив прямой битовой перестановки
        int[] ip_reverse; //массив обратной битовой перестановки
        int[] expansion; //массив для функции расширения
        int[] key_form; //массив для формирования блоков C и D
        int[] shiftkey; //массив для окончательного формирования ключа длиной 48 бит
        int[] p_set; //перестановка блока
        int[] cycle_move; //для циклической перестановки
        byte[, ,] s_block; //матрица для кодирования блоков S сообщения

        public String Encode_Text; //текст

        public String Decode_Key; //ключ
        public String[] Message_Block { get; set; } //блоки строки
        public String[,] Key_Array { get; set; }
        public String[] Block_S { get; set; } //блоки сообщения


        public Form1()
        {
            InitializeComponent();
            //инициализация массивов
            ip = new int[64] { 58, 50, 42, 34, 26, 18, 10, 2,
                               60, 52, 44, 36, 28, 20, 12, 4,
                               62, 54, 46, 38, 30, 22, 14, 6,
                               64, 56, 48, 40, 32, 24, 16, 8,
                               57, 49, 41, 33, 25, 17,  9, 1,
                               59, 51, 43, 35, 27, 19, 11, 3,
                               61, 53, 45, 37, 29, 21, 13, 5,
                               63, 55, 47, 39, 31, 23, 15, 7 };

            ip_reverse = new int[64] { 40, 8, 48, 16, 56, 24, 64, 32,
                                       39, 7, 47, 15, 55, 23, 63, 31,
                                       38, 6, 46, 14, 54, 22, 62, 30,
                                       37, 5, 45, 13, 53, 21, 61, 29,
                                       36, 4, 44, 12, 52, 20, 60, 28,
                                       35, 3, 43, 11, 51, 19, 59, 27,
                                       34, 2, 42, 10, 50, 18, 58, 26,
                                       33, 1, 41,  9, 49, 17, 57, 25 };

            expansion = new int[48] { 32, 1, 2, 3, 4, 5, 4, 5,
                                       6, 7, 8, 9, 8, 9, 10, 11,
                                      12, 13, 12, 13, 14, 15, 16, 17,
                                      16, 17, 18, 19, 20, 21, 20, 21,
                                      22, 23, 24, 25, 24, 25, 26, 27,
                                      28, 29, 28, 29, 30, 31, 32, 1 };

            key_form = new int[56] { 57, 49, 41, 33, 25, 17, 9,
                                      1, 58, 50, 42, 34, 26, 18,
                                     10,  2, 59, 51, 43, 35, 27,
                                     19, 11,  3, 60, 52, 44, 36,
                                     63, 55, 47, 39, 31, 23, 15, 
                                      7, 62, 54, 46, 38, 30, 22,
                                     14,  6, 61, 53, 45, 37, 29,
                                     21, 13,  5, 28, 20, 12,  4 };

            s_block = new byte[8, 4, 16]{
            {
                {14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7},
                {0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8},
                {4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0},
                {15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13}},
            {
                {15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10},
                {3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5},
                {0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15},
                {13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9}},
            {
                {10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8},
                {13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1},
                {13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7},
                {1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12}},
            {
                {7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15},
                {13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9},
                {10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4},
                {3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14}},
            {
                {2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9},
                {14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6},
                {4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14},
                {11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3}},
            {
                {12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11},
                {10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8},
                {9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6},
                {4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13}},
            {
                {4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1},
                {13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6},
                {1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2},
                {6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12}},
            {
                {13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7},
                {1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2},
                {7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8},
                {2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11}},            
            };

            p_set = new int[32] {   16,  7, 20, 21, 29, 12, 28, 17,
                                     1, 15, 23, 26,  5, 18, 31, 10,
                                     2,  8, 24, 14, 32, 27,  3,  9,
                                    19, 13, 30,  6, 22, 11,  4, 25 };

            cycle_move = new int[16] { 1, 1, 2, 2,
                                       2, 2, 2, 2,
                                       1, 2, 2, 2,
                                       2, 2, 2, 1 };

            shiftkey = new int[48] { 14, 17, 11, 24, 1, 5,
                                      3, 28, 15, 6, 21, 10,
                                     23, 19, 12, 4, 26, 8,
                                     16, 7, 27, 20, 13, 2,
                                     41,52, 31, 37, 47, 55,
                                     30, 40, 51, 45, 33,48,
                                     44, 49, 39,56, 34, 53,
                                     46, 42, 50, 36, 29, 32 };

            // инициализация переменных
            byteSize = 8; // размер одного символа в битах (на 1 символ 1 байт)
            byteSizeDES = 64; // размер блока в битах
            Blocksize = byteSizeDES / byteSize; // кол-во символов в блоке
            Key_Lenght = 48; // размер ключа
            Key_Array = new string[1000, 16];

            //инициализация функций, создающих ключ
            key_create = new CreatingKey(KeyNormalLenght);
            key_create += Create_Key;
            key_create += CD_Form;
        }

        // DES

        //приведение строки к нужной длине
        private string StringNormalLenght(string in_string)
        {
            while (in_string.Length % Blocksize != 0)
            {
                in_string += "$";
            }
            return in_string;
        }

        //перевод текста в двоичное представление
        private string FormatSourceText(string in_string)
        {
            byte[] byteString = Encoding.Default.GetBytes(in_string);

            string out_string = "";

            for (int i = 0; i < in_string.Length; i++)
            {
                string bin_char = Convert.ToString(byteString[i], 2);
                while (bin_char.Length < byteSize)
                    bin_char = "0" + bin_char;
                out_string += bin_char;
            }
            return out_string;
        }

        //разбитие строки на блоки и перевод их в двоичный формат
        private void CutString(string in_string)
        {
            Message_Block = new string[in_string.Length / Blocksize];
            for (int i = 0; i < Message_Block.Length; i++)
            {
                Message_Block[i] = in_string.Substring(i * Blocksize, Blocksize);
                Message_Block[i] = FormatSourceText(Message_Block[i]);
            }
        }

        //приведение ключа к нужной длине и перевод в двоичный формат
        private void KeyNormalLenght(ref string key)
        {
            while (key.Length < Blocksize)
            {
                key = "D" + key;
            }
            //если длина ключа нам не подходит, то обрезаем его
            if (key.Length > Blocksize) key = key.Substring(0, Blocksize);
            key = FormatSourceText(key);
        }

        //реализация "исключающего или" для двух строчек
        private string XOR(string source, string key)
        {
            string final = "";
            string local_key = key;
            while (source.Length > key.Length)
            {
                key = key + local_key;
            }

            for (int i = 0; i < source.Length; i++)
            {
                bool text = Convert.ToBoolean(Convert.ToInt32(source[i].ToString()));
                bool logic_key = Convert.ToBoolean(Convert.ToInt32(key[i].ToString()));

                if (text ^ logic_key)
                    final += "1";
                else
                    final += "0";
            }

            return final;
        }

        //перевод текста из двоичной формы в символьную
        private string FormatEncodedText(string in_string)
        {
            byte[] byteString = new byte[Blocksize];
            int index = 0;

            string out_string = "";

            while (in_string.Length > 0)
            {
                string bin_char = in_string.Substring(0, byteSize);
                in_string = in_string.Remove(0, byteSize);
                byteString[index] = Convert.ToByte(bin_char, 2);
                index++;
            }
            out_string = Encoding.Default.GetString(byteString);
            return out_string;
        }

        //ДАЛЕЕ ФУНКЦИИ МЕТОДА DES

        //битовая перестановка
        private string IP(string in_string, bool direction)
        {
            StringBuilder output = new StringBuilder(in_string); //создаем строку, в которую положим итог

            if (direction == true) //в зависимости от второго параметра выбираем либо прямую либо обратную перестановку
            {
                //прямая перестановка
                for (int i = 0; i < byteSizeDES; i++)
                {
                    output[i] = in_string[ip[i] - 1]; //осуществляем перестановку
                }
            }
            else
            {
                //обратная перестановка
                for (int i = 0; i < byteSizeDES; i++)
                {
                    output[i] = in_string[ip_reverse[i] - 1];
                }
            }

            string out_string = output.ToString(); //конвертируем результат обратно в строку для удобства
            return out_string;
        }

        //функция расширения
        private string Expansion_Func(string in_string)
        {
            //создаем строку в два раза больше из-за разницы в размерах
            StringBuilder output = new StringBuilder(in_string + in_string);
            for (int i = 0; i < Key_Lenght; i++)
            {
                output[i] = in_string[expansion[i] - 1];
            }
            string out_string = output.ToString();
            return out_string.Substring(0, Key_Lenght);
        }

        //функции для ключа

        //добавление контрольных битов в ключ
        private void Create_Key(ref string key_input)
        {
            FormatSourceText(key_input);
            int counter = 0, index = 7, i = 0;
            StringBuilder key_string = new StringBuilder(key_input);
            while (i < byteSizeDES)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (key_string[i + j] == 1) counter++;
                }
                if (counter % 2 == 0) key_string[index] = '1';
                else key_string[index] = '0';
                i += 8; index += 8;
            }
            string out_string = key_string.ToString();
            //return out_string;
        }

        //формирование блоков C и D
        private void CD_Form(ref string key_input)
        {
            StringBuilder change_key = new StringBuilder(key_input);
            for (int i = 0; i < byteSizeDES - 8; i++)
            {
                change_key[i] = key_input[key_form[i] - 1];
            }
            string out_key = change_key.ToString();
        }

        //левый циклический сдвиг
        private string Cyclic(string key_input, int counter)
        {
            string C_key = key_input.Substring(0, key_input.Length / 2);
            string D_key = key_input.Remove(0, key_input.Length / 2);

            for (int i = 0; i < cycle_move[counter]; i++)
            {
                C_key = C_key + C_key[0];
                C_key = C_key.Remove(0, 1);
                D_key = D_key + D_key[0];
                D_key = D_key.Remove(0, 1);
            }

            return C_key + D_key;
        }

        //формирование следующего ключа
        private string NextKey(string key_input)
        {
            StringBuilder out_key = new StringBuilder(key_input);
            for (int i = 0; i < 48; i++)
            {
                out_key[i] = key_input[shiftkey[i] - 1];
            }
            string out_string = out_key.ToString();
            return out_string;
        }

        //работа с блоками сообщения

        //разбивка сообщения длиной 64 бит на блоки по 6 бит
        private void CutToBlocks(string in_string)
        {
            Block_S = new string[in_string.Length / 6]; 
            for (int i = 0; i < Block_S.Length; i++)
            {
                Block_S[i] = in_string.Substring(i * 6, 6);
            }
        }
        //кодирование одного блока S
        private string Block_Code(string in_string, int number)
        {
            string out_string = in_string[0].ToString() + in_string[5].ToString();
            int row = Convert.ToInt32(out_string, 2);
            int column = Convert.ToInt32(in_string.Substring(1, 4), 2);

            row = s_block[number, row, column];
            out_string = Convert.ToString(row, 2);

            //на случай, если блок получился меньше 4 бит
            while (out_string.Length < 4)
            {
                out_string = "0" + out_string;
            }

            return out_string;
        }

        //преобразование блоков S после кодирования
        private string p_shifting(string in_string)
        {
            StringBuilder out_string = new StringBuilder(in_string);

            for (int i = 0; i < byteSizeDES / 2; i++)
            {
                out_string[i] = in_string[p_set[i] - 1];
            }
            string final_string = out_string.ToString();
            return final_string;
        }

        //функция шифрования сообщения
        private string Cipher(string in_string, string key)
        {
            string out_string;
            in_string = Expansion_Func(in_string);
            out_string = XOR(in_string, key);  
            CutToBlocks(out_string);  // Полученный результат складывается побитно по модулю 2 (побитовое исключающее ИЛИ) с текущим значением ключа i k и затем представляется в виде восьми последовательных 6-битовых блоков 
            out_string = "";

            for (int i = 0; i < 8; i++)
            {
                Block_S[i] = Block_Code(Block_S[i], i);
                out_string += Block_S[i];
            }
            out_string = p_shifting(out_string);
            return out_string;
        }

        //одна итерация кодирования DES
        private string DES_OneIter_Forward(string in_string, string key_string)
        {
            string Left = in_string.Substring(0, in_string.Length / 2);
            string Right = in_string.Remove(0, in_string.Length / 2);
            string out_string = Right + XOR(Left, Cipher(Right, key_string));
            return out_string;
        }

        //одна итерация декодирования DES
        private string DES_OneIter_Backward(string in_string, string key_string)
        {
            string Left = in_string.Substring(0, in_string.Length / 2);
            string Right = in_string.Remove(0, in_string.Length / 2);
            string out_string = XOR(Cipher(Left, key_string), Right) + Left;
            return out_string;
        }

        // кнопка загрузки в файл закрытого текста
        private void Close_Of_Fail_Click(object sender, EventArgs e)
        {
            OpenFileDialog read = new OpenFileDialog();
            if (read.ShowDialog() == DialogResult.OK)
            {
                string nameCiphertext = read.FileName;
                richTextBox_Cipher_Text.Clear();
                richTextBox_Cipher_Text.Text = File.ReadAllText(nameCiphertext);
            }
        }

        // кнопка выгрузки из файла закрытого текста
        private void Close_In_Fail_Click(object sender, EventArgs e)
        {
            OpenFileDialog write = new OpenFileDialog();
            if (write.ShowDialog() == DialogResult.OK)
            {
                string nameCiphertext = write.FileName;
                File.WriteAllText(nameCiphertext, richTextBox_Cipher_Text.Text);
            }
        }

        // кнопка загрузки открытого текста из файла
        private void Open_Of_File_Click(object sender, EventArgs e)
        {
            OpenFileDialog read = new OpenFileDialog();
            if (read.ShowDialog() == DialogResult.OK)
            {
                string nameOpentext = read.FileName;
                richTextBox_Plain_Text.Clear();
                richTextBox_Plain_Text.Text = File.ReadAllText(nameOpentext);
            }
        }

        // кнопка загрузки открытого текста в файл
        private void Open_In_File_Click(object sender, EventArgs e)
        {
            OpenFileDialog write = new OpenFileDialog();
            if (write.ShowDialog() == DialogResult.OK)
            {
                string nameOpentext = write.FileName;
                File.WriteAllText(nameOpentext, richTextBox_Plain_Text.Text);
            }
        }

        // кнопка геенрации ключа
        private void Generating_key_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            byte[] rndKey = new byte[8];
            for (int i = 0; i < rndKey.Length; i++)
            {
                rndKey[i] = (byte)rand.Next(65, 90);
            }
            textBox_key.Text = Encoding.ASCII.GetString(rndKey);
        }

        // кнопка зашифровать
        private void Encode_Click(object sender, EventArgs e)
        {
            Encode_Text = richTextBox_Plain_Text.Text;
            Decode_Key = textBox_key.Text;

            Encode_Text = StringNormalLenght(Encode_Text);
            CutString(Encode_Text);
            key_create(ref Decode_Key);

            Encode_Text = "";

            for (int i = 0; i < Message_Block.Length; i++)
            {
                Message_Block[i] = IP(Message_Block[i], true);
                for (int j = 0; j < 16; j++)
                {
                    Decode_Key = NextKey(Decode_Key);
                    Key_Array[i, j] = Decode_Key;
                    Message_Block[i] = DES_OneIter_Forward(Message_Block[i], Decode_Key);
                    Decode_Key = Cyclic(Decode_Key, j);
                }
                Message_Block[i] = IP(Message_Block[i], false);

                Encode_Text += FormatEncodedText(Message_Block[i]);
            }
            byte[] Encode_Text_byte = Encoding.Default.GetBytes(Encode_Text);
            richTextBox_Cipher_Text.Text = Convert.ToBase64String(Encode_Text_byte);
        }

        // кнопка расшифровать
        private void Decode_Click(object sender, EventArgs e)
        {
            Encode_Text = richTextBox_Cipher_Text.Text;

            byte[] Encode_Text_inbyte = Convert.FromBase64String(Encode_Text);
            String Encode_Text_frombyte = Encoding.Default.GetString(Encode_Text_inbyte);

            CutString(Encode_Text_frombyte);
            Encode_Text_frombyte = "";

            for (int i = 0; i < Message_Block.Length; i++)
            {
                Message_Block[i] = IP(Message_Block[i], true);

                for (int j = 0; j < 16; j++)
                {
                    Message_Block[i] = DES_OneIter_Backward(Message_Block[i], Key_Array[i, 15 - j]);
                    Decode_Key = Cyclic(Decode_Key, j);
                }
                Message_Block[i] = IP(Message_Block[i], false);
                Encode_Text_frombyte += FormatEncodedText(Message_Block[i]);
            }

            for (int i = 0; i < Encode_Text_frombyte.Length; i++)
            {
                if (Encode_Text_frombyte[i] == '$')
                {
                    Encode_Text_frombyte = Encode_Text_frombyte.Remove(i);
                }
            }

            richTextBox_Cipher_Text.Text = Encode_Text_frombyte;
        }
    }
}
