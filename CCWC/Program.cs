using System;
using System.IO;
using System.Text;

class Program
{
     static void Main(string[] args)
     {
          if (args.Length < 1)
          {
               Console.WriteLine("Usage: ccwc [-c|-l|-w|-m] <file> OR use piped input");
               return;
          }

          string option = string.Empty;
          string filePath = string.Empty;
          string content = string.Empty;

          bool isInputRedirected = Console.IsInputRedirected;

          if (isInputRedirected)
          {
               using var reader = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding);
               content = reader.ReadToEnd();
               filePath = ""; // no filename for piped input
          }
          else if (args.Length == 1 && File.Exists(args[0]))
          {
               option = "";
               filePath = args[0];
               content = File.ReadAllText(filePath);
          }
          else if (args.Length == 2)
          {
               option = args[0];
               filePath = args[1];
               content = File.ReadAllText(filePath);
          }
    
          byte[] bytes = Encoding.UTF8.GetBytes(content);

          int totalBytes = bytes.Length;
          int totalLines = content.Split("\n").Length;
          int totalWords =  content.Split(new char[] { ' ', '\r','\n', '\t' },StringSplitOptions.RemoveEmptyEntries).Length;
          int totalChar = content.Length;

          switch (option)
          {
               case "-c":
                    Console.WriteLine(totalBytes);
                    break;
               case "-l":
                    Console.WriteLine(totalLines);
                    break;
               case "-w":
                    Console.WriteLine(totalWords);
                    break;
               case "-m":
                    Console.WriteLine(totalChar);
                    break;
               default:
                    string defaultResult = $"{totalBytes} {totalLines} {totalWords}";
                    Console.WriteLine(defaultResult);
                    break;

          }         
    }    
}