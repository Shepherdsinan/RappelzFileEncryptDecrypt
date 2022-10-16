using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //public string RC4(string input, string key)
        //{
        //    StringBuilder result = new StringBuilder();
        //    int x, y, j = 0;
        //    int[] box = new int[256];
        //    for (int i = 0; i < 256; i++)
        //        box[i] = i;
        //    for (int i = 0; i < 256; i++)
        //    {
        //        j = (key[i % key.Length] + box[i] + j) % 256;
        //        x = box[i];
        //        box[i] = box[j];
        //        box[j] = x;
        //    }
        //    for (int i = 0; i < input.Length; i++)
        //    {
        //        y = i % 256;
        //        j = (box[y] + j) % 256;
        //        x = box[y];
        //        box[y] = box[j];
        //        box[j] = x;
        //        result.Append((char)(input[i] ^ box[(box[y] + box[j]) % 256]));
        //    }
        //    return result.ToString();
        //}


            public static string Encrypt(string key, string data)
            {
                Encoding unicode = Encoding.Unicode;

                return Convert.ToBase64String(Encrypt(unicode.GetBytes(key), unicode.GetBytes(data)));
            }

            public static string Decrypt(string key, string data)
            {
                Encoding unicode = Encoding.Unicode;

                return unicode.GetString(Encrypt(unicode.GetBytes(key), Convert.FromBase64String(data)));
            }

            public static byte[] Encrypt(byte[] key, byte[] data)
            {
                return EncryptOutput(key, data).ToArray();
            }

            public static byte[] Decrypt(byte[] key, byte[] data)
            {
                return EncryptOutput(key, data).ToArray();
            }

            private static byte[] EncryptInitalize(byte[] key)
            {
                byte[] s = Enumerable.Range(0, 256)
                  .Select(i => (byte)i)
                  .ToArray();

                for (int i = 0, j = 0; i < 256; i++)
                {
                    j = (j + key[i % key.Length] + s[i]) & 255;

                    Swap(s, i, j);
                }

                return s;
            }

            private static IEnumerable<byte> EncryptOutput(byte[] key, IEnumerable<byte> data)
            {
                byte[] s = EncryptInitalize(key);

                int i = 0;
                int j = 0;

                return data.Select((b) =>
                {
                    i = (i + 1) & 255;
                    j = (j + s[i]) & 255;

                    Swap(s, i, j);

                    return (byte)(b ^ s[(s[i] + s[j]) & 255]);
                });
            }

            private static void Swap(byte[] s, int i, int j)
            {
                byte c = s[i];

                s[i] = s[j];
                s[j] = c;
            }
       

        private void button1_Click(object sender, EventArgs e)
        {
            txtEncrypt.Text = Encrypt("HongFlashMall", txtInput.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtDecrypt.Text = Decrypt("HongFlashMall", txtEncrypt.Text);
        }
    }

}
