using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Textq
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceFile = "";
            string targetFile = "";
            string encodigStr = "-UTF8";
            
            if (args.Length < 2)
            {
                Console.WriteLine("You must specify the source and destination file paths. \nPress enter key ...");
                Console.ReadLine();
                return;
            }

            if (args.Length > 0)
            {
                sourceFile = args[0];
                targetFile = args[1];
            }

            if (args.Length > 2)
                encodigStr = args[2];

            ConverToQDos(sourceFile, targetFile, encodigStr);

            Console.WriteLine("End conversion. \nPress enter key ...");
            Console.ReadLine();
        }


        public static void ConverToQDos(string sourceFile, string targetFile, string encodingStr = "-UTF8")
        {
            var table = LoadQDosTable();
            List<byte> outQDos = new List<byte>();
            var enc = Encoding.UTF8;
            
            
            // TODO: test all encoding 
            if (encodingStr == "-Unicode")
                enc = Encoding.Unicode;
            else if (encodingStr == "-ASCII")
                enc = Encoding.ASCII;
            else if (encodingStr == "-UTF32")
                enc = Encoding.UTF32;

            char c;
            using (FileStream fs = File.OpenRead(sourceFile))
            {                
                using (StreamReader streamReader = new StreamReader(fs, enc))
                {
                    while (!streamReader.EndOfStream)
                    {
                        c = GetNextCharacter(streamReader);

                        if (c == '\r')
                        {
                            var c2 = GetNextCharacter(streamReader);
                            if (c2 == '\n')
                                AddByteToQDosList(table, outQDos, c2);
                            else
                            {
                                AddByteToQDosList(table, outQDos, '\n');
                                AddByteToQDosList(table, outQDos, c2);
                            }
                        }
                        else 
                            AddByteToQDosList(table, outQDos, c);                        
                    }
                }
                File.WriteAllBytes(targetFile, outQDos.ToArray());
            }
        }

        public static void AddByteToQDosList(Dictionary<char, byte> table, List<byte> outQDos, char c)
        {
            byte charN;

            if (table.ContainsKey(c))
            {
                outQDos.Add(table[c]);
            }
            else
            {
                charN = (byte)c;
                if (charN > 191)
                    charN = 1;
                outQDos.Add((byte)c);
            }
        }

        static Dictionary<char, byte> LoadQDosTable()
        {
            Dictionary<char, byte> table = new Dictionary<char, byte>();

            table.Add(' ', 32);
            table.Add('!', 33);
            table.Add('"', 34);
            table.Add('#', 35);
            table.Add('$', 36);
            table.Add('%', 37);
            table.Add('&', 38);
            table.Add('\'', 39);
            table.Add('(', 40);
            table.Add(')', 41);
            table.Add('*', 42);
            table.Add('+', 43);
            table.Add(',', 44);
            table.Add('-', 45);
            table.Add('.', 46);
            table.Add('/', 47);
            table.Add('0', 48);
            table.Add('1', 49);
            table.Add('2', 50);
            table.Add('3', 51);
            table.Add('4', 52);
            table.Add('5', 53);
            table.Add('6', 54);
            table.Add('7', 55);
            table.Add('8', 56);
            table.Add('9', 57);
            table.Add(':', 58);
            table.Add(';', 59);
            table.Add('<', 60);
            table.Add('=', 61);
            table.Add('>', 62);
            table.Add('?', 63);
            table.Add('@', 64);
            table.Add('A', 65);
            table.Add('B', 66);
            table.Add('C', 67);
            table.Add('D', 68);
            table.Add('E', 69);
            table.Add('F', 70);
            table.Add('G', 71);
            table.Add('H', 72);
            table.Add('I', 73);
            table.Add('J', 74);
            table.Add('K', 75);
            table.Add('L', 76);
            table.Add('M', 77);
            table.Add('N', 78);
            table.Add('O', 79);
            table.Add('P', 80);
            table.Add('Q', 81);
            table.Add('R', 82);
            table.Add('S', 83);
            table.Add('T', 84);
            table.Add('U', 85);
            table.Add('V', 86);
            table.Add('W', 87);
            table.Add('X', 88);
            table.Add('Y', 89);
            table.Add('Z', 90);
            table.Add('[', 91);
            table.Add('\\', 92);
            table.Add(']', 93);
            table.Add('^', 94);
            table.Add('_', 95);
            table.Add('£', 96);
            table.Add('a', 97);
            table.Add('b', 98);
            table.Add('c', 99);
            table.Add('d', 100);
            table.Add('e', 101);
            table.Add('f', 102);
            table.Add('g', 103);
            table.Add('h', 104);
            table.Add('i', 105);
            table.Add('j', 106);
            table.Add('k', 107);
            table.Add('l', 108);
            table.Add('m', 109);
            table.Add('n', 110);
            table.Add('o', 111);
            table.Add('p', 112);
            table.Add('q', 113);
            table.Add('r', 114);
            table.Add('s', 115);
            table.Add('t', 116);
            table.Add('u', 117);
            table.Add('v', 118);
            table.Add('w', 119);
            table.Add('x', 120);
            table.Add('y', 121);
            table.Add('z', 122);
            table.Add('{', 123);
            table.Add('|', 124);
            table.Add('}', 125);
            table.Add('~', 126);
            table.Add('©', 127);
            table.Add('ä', 128);
            table.Add('ã', 129);
            table.Add('å', 130);
            table.Add('é', 131);
            table.Add('ö', 132);
            table.Add('õ', 133);
            table.Add('ø', 134);
            table.Add('ü', 135);
            table.Add('ç', 136);
            table.Add('ñ', 137);
            table.Add('æ', 138);
            table.Add('œ', 139);
            table.Add('á', 140);
            table.Add('à', 141);
            table.Add('â', 142);
            table.Add('ë', 143);
            table.Add('è', 144);
            table.Add('ê', 145);
            table.Add('ï', 146);
            table.Add('í', 147);
            table.Add('ì', 148);
            table.Add('î', 149);
            table.Add('ó', 150);
            table.Add('ò', 151);
            table.Add('ô', 152);
            table.Add('ú', 153);
            table.Add('ù', 154);
            table.Add('û', 155);
            table.Add('ß', 156);
            table.Add('¢', 157);
            table.Add('¥', 158);
            table.Add('`', 159);
            table.Add('Ä', 160);
            table.Add('Ã', 161);
            table.Add('Å', 162);
            table.Add('É', 163);
            table.Add('Ö', 164);
            table.Add('Õ', 165);
            table.Add('Ø', 166);
            table.Add('Ü', 167);
            table.Add('Ç', 168);
            table.Add('Ñ', 169);
            table.Add('Æ', 170);
            table.Add('Œ', 171);
            table.Add('α', 172);
            table.Add('δ', 173);
            table.Add('θ', 174);
            table.Add('λ', 175);
            table.Add('μ', 176);
            table.Add('π', 177);
            table.Add('Φ', 178);
            table.Add('¡', 179);
            table.Add('¿', 180);
            table.Add('§', 182);
            table.Add('¤', 183);
            table.Add('«', 184);
            table.Add('»', 185);
            table.Add('°', 186);
            table.Add('÷', 187);
            table.Add('←', 188);
            table.Add('→', 189);
            table.Add('↑', 190);
            table.Add('↓', 191);
            return table;
        }
        
        static char GetNextCharacter(StreamReader streamReader) => (char)streamReader.Read();

    }
}
