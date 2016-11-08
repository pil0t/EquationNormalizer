using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace EquationNormalizer
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new Parser();
            if (args.Length > 0)
            {
                var result = new List<string>();
                var errors = new List<string>();
                var fileName = args[0];

                if (!File.Exists(fileName))
                {
                    Console.WriteLine($"Файл '{fileName}' не найден");
                }

                Console.WriteLine($"Чтение файла '{fileName}'");
                var lines = File.ReadAllLines(fileName);

                var sw = Stopwatch.StartNew();
                foreach (var line in lines)
                {
                    try
                    {
                        var equation = parser.Parse(line);
                        result.Add(equation.Normalize().ToString());
                    }
                    catch (Exception e)
                    {
                        errors.Add($"Ошибка обработки '{line}': {e.Message}");
                    }
                }

                sw.Stop();

                File.WriteAllLines(fileName + ".out", result);

                if (errors.Count > 0)
                {
                    File.WriteAllLines(fileName + ".err", errors);
                }

                Console.WriteLine($"Обработано строк: {lines.Length} за {sw.Elapsed}, Ошибок: {errors.Count}");
            }
            else
            
            while (true)
            {
                var line = Console.ReadLine();
                try
                {
                    var equation = parser.Parse(line);
                    var normalized = equation.Normalize();
                    Console.WriteLine(normalized);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine("Ошибка: " + e.Message);
                }
            }
        }
    }
}
