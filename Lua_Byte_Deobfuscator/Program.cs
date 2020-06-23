using System;
using System.IO;
using System.Threading;
using MoonSharp.Interpreter;

namespace Lua_Byte_Deobfuscator
{
    class Program
    {
        static void Main()
        {
            Console.Title = "Lua Bytecode Deobfuscator by goldblack";
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Credits to goldblack [https://github.com/goldblackk]");
			Console.WriteLine("Thanks to yunglean_ for his awesome work with the obfuscator [https://github.com/yungleanio]\n");
			Console.Write(".lua file: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string file = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine();
            ReadFile(file);
        }

		static void ReadFile(string file)
		{
			string text = File.ReadAllText(file);
			text = text.Replace("load", "print");
            text = text.Replace("()", "");
            File.WriteAllText(file, text);

			Deobfuscate(file);
		}

        static void Deobfuscate(string filewithprint)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Script script = new Script();
			var deobfuscated = script.DoFile(filewithprint);
            Console.WriteLine(deobfuscated);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ClearCurrentConsoleLine();
            string text = File.ReadAllText(filewithprint);
            text = text.Replace("print", "load");
            text = text.Replace(")", ")()");
            File.WriteAllText(filewithprint, text);
            Console.ReadKey(true);
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
