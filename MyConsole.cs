using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Exam_C_Sharp
{
    class MyConsole
    {
        private static List<string> history = new List<string>();
        public static void StartCmd()  //Вывод информации первого запуска
        {
            Title("Командная строка by Ovkaaa:)");

            Console.WriteLine($"Microsoft Windows[Version 10.0.19042.928]\n" +
                $"(c) Корпорация Майкрософт(Microsoft Corporation).Все права защищены.");
            Console.WriteLine();

            Request();
        }

        private static void Request()   //Вывод строки для запроса
        {
            while (true)
            {
                Console.Write($"{Directory.GetCurrentDirectory()}>");
                string buf = Console.ReadLine();
                if (buf != "")
                {
                    try
                    {
                        ProcessingRequest(buf);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    Console.WriteLine();
                    history.Add(buf);
                }
            }
        }
        private static void BreakdownOfRequests(string fullRequest, out string primaryRequest, out string secondaryRequest)
        {
            //Разбитие на составляющие
            try
            {
                primaryRequest = fullRequest.Substring(0, fullRequest.IndexOf(' '));
                secondaryRequest = fullRequest.Substring(fullRequest.IndexOf(' ') + 1);
            }
            catch (Exception)
            {
                primaryRequest = fullRequest;
                secondaryRequest = null;
            }
        }
        private static void ProcessingRequest(string fullRequest)   //Обработка запроса
        {
            string primaryRequest;
            string secondaryRequest;

            BreakdownOfRequests(fullRequest, out primaryRequest, out secondaryRequest);

            primaryRequest = primaryRequest.ToLower();  //Преобразование к нижнему регистру для отработки свича

            if (File.Exists(primaryRequest))
            {
                OpenFile(primaryRequest);
                return;
            }
            switch (primaryRequest)
            {
                case "attrib":
                    Attrib(secondaryRequest);
                    break;

                case "cd":
                    Cd(secondaryRequest);
                    break;

                case "cls":
                    Cls();
                    break;

                case "copy":
                    Copy(secondaryRequest);
                    break;

                case "create":
                    Create(secondaryRequest);
                    break;

                case "del":
                    Del(secondaryRequest);
                    break;

                case "dir":
                    Dir(secondaryRequest);
                    break;

                case "exit":
                    Exit();
                    break;

                case "help":
                    Help(secondaryRequest);
                    break;

                case "history":
                    HistoryInfo();
                    break;

                case "mkdir":
                    Mkdir(secondaryRequest);
                    break;

                case "move":
                    Move(secondaryRequest);
                    break;

                case "rename":
                    Rename(secondaryRequest);
                    break;

                case "title":
                    Title(secondaryRequest);
                    break;

                case "type":
                    Type(secondaryRequest);
                    break;

                default:
                    Console.WriteLine($"\"{primaryRequest}\" не является внутренней или внешней\n" +
                        $"командой, исполняемой программой или пакетным файлом.");
                    break;
            }
        }
        private static void OpenFile(string fileName)
        {
            var file = new FileInfo(fileName);
            System.Diagnostics.Process.Start(file.FullName);
        }
        private static void Attrib(string fileName) //Вывод информации об аттрибутах файла
        {
            var file = new FileInfo(fileName);
            
            if (!file.Exists && !Directory.Exists(fileName))
            {
                Console.WriteLine($"Не найден файл: {file.Name}");
                return;
            }

            var attributes = File.GetAttributes(file.FullName);

            if (Directory.Exists(fileName))
            {
                Console.Write($"{"",-21}");
            }
            else
            {
                Console.Write($"{attributes,-21}");
            }

            Console.WriteLine($"{file.FullName}");
        }
        private static void Cd(string dir)  //Смена текущей папки
        {
            if (dir == null)
                //Выводит текущего имени каталога
                Console.WriteLine($"{Directory.GetCurrentDirectory()}");
            else
            {
                //Изменение текущего каталога
                try
                {
                    Directory.SetCurrentDirectory(dir);
                }
                catch (Exception)
                {
                    Console.WriteLine("Системе не удается найти указанный путь.");
                }
            }
        }
        private static void Cls()   //Очищает содержимое экрана
        {
            Console.Clear();
        }
        
        private static void Copy(string request)     //Копирование файлов
        {
            string fileNames;
            string destinationDir;

            BreakdownOfRequests(request, out fileNames, out destinationDir);

            //Если запрос без конечной директории назначается текущая директория как конечная
            if (destinationDir == null)
            {
                destinationDir = Directory.GetCurrentDirectory();
            }

            var arrName = fileNames.Split('|');
            int countCopy = 0;


            for (int i = 0; i < arrName.Length; i++)
            {
                if (Directory.Exists(arrName[i]))   //Копирование папок
                {
                    var dir = new DirectoryInfo(arrName[i]);
                    
                    if (dir.FullName.ToLower() == destinationDir.ToLower())  //Директория копируемой папки и конечная директория совпадают
                    {
                        Console.WriteLine($"Невозможно выполнить циклическое копирование.");
                        break;
                    }

                    //Конечная директория для файлов внутри папки
                    string newdDestDir = Path.Combine(destinationDir, dir.Name);

                    //Создание директории если ее не существует в конечной директории
                    if (!Directory.Exists(newdDestDir))
                    {
                        Mkdir(newdDestDir);
                    }

                    //Рекурсивное копирование файлов в папке
                    var arrFiles = dir.GetFiles();
                    
                    if (arrFiles.Length > 0)
                    {
                        string tmpRequest = null;
                        for (int j = 0; j < arrFiles.Length; j++)
                        {
                            tmpRequest += $"{(j == 0 ? "" : "|")}{arrFiles[j].FullName}";
                        }
                       
                        Copy($"{tmpRequest} {newdDestDir}");
                    }

                    //Рекурсивное копирование папок
                    var dirNames = dir.GetDirectories();
                    
                    for (int j = 0; j < dirNames.Length; j++)
                    {
                        Copy($"{dirNames[j].FullName} {newdDestDir}");
                    }
                }
                else if (File.Exists(arrName[i]))   //Копирование файлов
                {
                    //Временная переменная, что бы достать имя файла и его директорию
                    var tmpFile = new FileInfo(arrName[i]);

                    if (tmpFile.DirectoryName.ToLower() == destinationDir.ToLower())  //Директория файла и конечная директория совпадают
                    {
                        Console.WriteLine($"Невозможно скопировать файл поверх самого себя.");
                        break;
                    }
                    else
                    {
                        try
                        {
                            File.Copy(tmpFile.FullName, Path.Combine(destinationDir, tmpFile.Name));

                            Console.WriteLine("+ " + tmpFile.Name);   //Вывод при удачном копировании
                            countCopy++;
                        }
                        catch (IOException)       //Предложение заменить если файл уже существует
                        {
                            ConsoleKeyInfo key = default;
                            //Ожидание нажатия клавиш Y или N
                            do
                            {
                                Console.Write($"Заменить {Path.Combine(destinationDir, tmpFile.Name)} [Yes /No]? ");
                                key = Console.ReadKey();
                                Console.WriteLine();
                            } while (key.Key != ConsoleKey.Y && key.Key != ConsoleKey.N);

                            if (key.Key == ConsoleKey.Y)
                            {
                                Del(Path.Combine(destinationDir, tmpFile.Name));
                                File.Copy(tmpFile.FullName, Path.Combine(destinationDir, tmpFile.Name));

                                Console.WriteLine("+ " + tmpFile.Name);
                                countCopy++;
                            }
                            else
                                Console.WriteLine("- " + tmpFile.Name);
                        }
                        catch (Exception)   //Вывод при неудачном копировании
                        {
                            Console.WriteLine("- " + tmpFile.Name);
                        }
                    }
                }
            }
            if (countCopy > 0)
                Console.WriteLine($"Скопировано файлов:{countCopy,10}.");
        }
        private static void Create(string request)
        {
            string text;
            string fileName = null;
            try
            {
                text = request.Substring(0, request.IndexOf('>'));
                fileName = request.Substring(request.IndexOf('>') + 1).TrimStart();
            }
            catch (Exception)
            {
                try
                {
                    text = request;
                }
                catch (Exception)
                {
                    Console.WriteLine("Ошибка в синтаксисе команды.");
                    return;
                }
            }

            try
            {
                using (var sw = new StreamWriter(fileName))
                {
                    sw.WriteLine(text);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка в синтаксисе команды.");
            }
        }
        private static void Del(string request)      //Удаление файлов и папок
        {
            string fileName;
            string leftoverFiles;

            BreakdownOfRequests(request, out fileName, out leftoverFiles);

            if (fileName == null)
            {
                Console.WriteLine("Ошибка в синтаксисе команды.");
                return;
            }

            var dir = new DirectoryInfo(fileName);

            if (Directory.Exists(fileName)) //Удаление папок
            {
                var arrFiles = dir.GetFiles();
                
                for (int i = 0; i < arrFiles.Length; i++)
                {
                    File.Delete(arrFiles[i].FullName);
                }

                //Рекурсивное удаление папок
                var arrDirs = dir.GetDirectories();
                
                for (int i = 0; i < arrDirs.Length; i++)
                {
                    Del(arrDirs[i].FullName);
                }

                //Удаление пустой папки
                try
                {
                    dir.Delete();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (File.Exists(fileName)) //Удаление файла
            {
                File.Delete(fileName);
            }
            else
                Console.WriteLine($"Не удается найти {dir.FullName}");

            //Рекурсия если еще остались файлы из запроса
            if (leftoverFiles != null)
                Del(leftoverFiles);
        }
        private static void Dir(string fullRequest)
        {
            //Информация о диске
            Console.WriteLine($" Том в устройстве {new DriveInfo(Directory.GetCurrentDirectory()).Name} имеет метку {new DriveInfo(Directory.GetCurrentDirectory()).VolumeLabel}");

            //Счетчики для сбора инфы для статистики
            int countFiles = 0;
            int countDirs = 0;
            long sizeFiles = 0;

            MainDir(fullRequest);


            if (countFiles == 0 && countDirs == 0)
            {
                Console.WriteLine("Файл не найден");
                return;
            }
            Console.WriteLine($"\n" +
                        $"     Всего файлов:");
            PrintFileInfo(countFiles, sizeFiles);
            PrintDirInfo(countDirs);

            //Основная логика функции
            void MainDir(string request)
            {
                string dirName;
                string leftoverDirs;
                
                BreakdownOfRequests(request, out dirName, out leftoverDirs);

                var dir = new DirectoryInfo(dirName != null ? dirName : Directory.GetCurrentDirectory());

                //Если в запросе содержится аттрибут поиска файлов
                if (leftoverDirs?.ToLower() == "/s")
                {
                    Search(dirName, new DirectoryInfo(Directory.GetCurrentDirectory()));
                    return;
                }

                DirName(dir.FullName);

                try
                {
                    if (dir.Exists)
                    {
                        //Цикл вывода информации для файлов
                        foreach (var item in dir.GetFiles())
                        {
                            FileInfo(item, ref countFiles, ref sizeFiles);
                        }

                        //Цикл вывода информации для папок
                        foreach (var item in dir.GetDirectories())
                        {
                            DirInfo(item, ref countDirs);
                        }
                    }
                    else
                    {
                        //Вывод информации для отдельного файла
                        FileInfo(new FileInfo(dir.FullName), ref countFiles, ref sizeFiles);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"Файл не найден");
                }

                if (leftoverDirs != null)
                {
                    //Вызов рекурсии для множественного запроса
                    MainDir(leftoverDirs);
                }

                /* ВНУТРЕННИЕ ФУНКЦИИ МЕТОДА DIR */

                //Поиск файлов
                void Search(string name, DirectoryInfo currentDir)
                {
                    int tmpCountFiles = 0;
                    int tmpCountDirs = 0;
                    long tmpSizeFiles = 0;

                    //Сравнивает имена файлов и папок в текущей директории
                    foreach (var item in currentDir.GetDirectories())
                    {
                        if (item.Name.Contains(name))
                        {
                            if (tmpCountFiles == 0 && tmpCountDirs == 0)
                            {
                                DirName(item.FullName);
                            }
                            DirInfo(item, ref tmpCountDirs);
                        }
                    }
                    foreach (var item in currentDir.GetFiles())
                    {

                        if (item.Name.Contains(name))
                        {
                            if (tmpCountFiles == 0 && tmpCountDirs == 0)
                            {
                                DirName(item.FullName);
                            }
                            FileInfo(item, ref tmpCountFiles, ref tmpSizeFiles);
                        }
                    }

                    //Вывод статистики папки
                    if (tmpCountFiles > 0 || tmpCountDirs > 0)
                    {
                        PrintFileInfo(tmpCountFiles, tmpSizeFiles);
                    }

                    //Обнововляет общие показатели статистики
                    countFiles += tmpCountFiles;
                    countDirs += tmpCountDirs;
                    sizeFiles += tmpSizeFiles;

                    //Переход во внутренний папки для поиска
                    foreach (var item in currentDir.GetDirectories())
                    {
                        Search(name, item);
                    }
                }

                //Вывод названия папки с которой рабтает
                void DirName(string fullDir)
                {
                    Console.WriteLine();
                    if (File.Exists(fullDir))
                    {
                        var fileInfo = new FileInfo(fullDir);
                        Console.WriteLine($" Содержимое папки {fileInfo.DirectoryName}");
                    }
                    else
                    {
                        var dirInfo = new DirectoryInfo(fullDir);
                        Console.WriteLine($" Содержимое папки {dirInfo.FullName}");
                    }
                    Console.WriteLine();
                }

                //Вывод информации о файле
                void FileInfo(FileInfo file, ref int countF, ref long sizeF)
                {
                    Console.WriteLine($"{$"{file.LastWriteTime.Day:00}.{file.LastWriteTime.Month:00}.{file.LastWriteTime.Year}",-12}" +
                                               $"{$"{file.LastWriteTime.Hour:00}:{file.LastWriteTime.Minute:00}",-9}" +
                                               $"{file.Length,14:### ### ##0} " +
                                               $"{file.Name}");
                    countF++;
                    sizeF += file.Length;
                }

                //Вывод информации о папке
                void DirInfo(DirectoryInfo directory, ref int countD)
                {
                    Console.WriteLine($"{$"{directory.LastWriteTime.Day:00}.{directory.LastWriteTime.Month:00}.{directory.LastWriteTime.Year}",-12}" +
                                 $"{$"{directory.LastWriteTime.Hour:00}:{directory.LastWriteTime.Minute:00}",-9}" +
                                 $"{"<DIR>",-15}" +
                                 $"{directory.Name}");
                    countD++;
                }
            }

            //Вывод информации по собраной статистике
            void PrintFileInfo(int countF, long sizeF)
            {
                //Вывод собраной статистики по файлам
                Console.Write($"{countF,16} ");
                if (countF % 10 == 1 &&
                    countF % 100 < 10 &&
                    countF % 100 > 20)
                    Console.Write("файл");
                else if (countF % 10 > 1 &&
                    countF % 10 < 5 &&
                    countF % 100 < 10 &&
                    countF % 100 > 20)
                    Console.Write("файла");
                else
                    Console.Write("файлов");
                Console.WriteLine($" {sizeF,16:### ### ### ##0} байт");
            }
            void PrintDirInfo(int countD)
            {
                //Вывод собраной статистики по папкам
                Console.Write($"{countD,16} ");
                if (countD % 10 == 1 &&
                    countD % 100 < 10 &&
                    countD % 100 > 20)
                    Console.Write("папка");
                else if (countD % 10 > 1 &&
                    countD % 10 < 5 &&
                    countD % 100 < 10 &&
                    countD % 100 > 20)
                    Console.Write("папки");
                else
                    Console.Write("папок");
                Console.WriteLine($" {new DriveInfo(Directory.GetCurrentDirectory()).TotalFreeSpace,16:### ### ### ##0} байт свободно");
            }
        }
        private static void Exit()   //Выход из консоли
        {
            Environment.Exit(1);
        }
        private static void Help(string additationalParametr)     //Вывод информации о командах
        {
            additationalParametr = additationalParametr?.ToLower();

            switch (additationalParametr)
            {
                case null:
                    HelpInfo.Help();
                    break;
                case "attrib":
                    HelpInfo.HelpAttrib();
                    break;
                case "cd":
                    HelpInfo.HelpCd();
                    break;
                case "cls":
                    HelpInfo.HelpCls();
                    break;
                case "copy":
                    HelpInfo.HelpCopy();
                    break;
                case "create":
                    HelpInfo.HelpCreate();
                    break;
                case "del":
                    HelpInfo.HelpDel();
                    break;
                case "dir":
                    HelpInfo.HelpDir();
                    break;
                case "exit":
                    HelpInfo.HelpExit();
                    break;
                case "help":
                    HelpInfo.HelpHelp();
                    break;
                case "history":
                    HelpInfo.HelpHistory();
                    break;
                case "mkdir":
                    HelpInfo.HelpMkdir();
                    break;
                case "move":
                    HelpInfo.HelpMove();
                    break;
                case "title":
                    HelpInfo.HelpTitle();
                    break;
                case "type":
                    HelpInfo.HelpType();
                    break;
                default:
                    Console.WriteLine("Данная команда не поддерживается.");
                    break;
            }
        }
        private static void HistoryInfo()
        {
            int i = 0;
            foreach (var item in history)
            {
                Console.WriteLine($"{++i}. {item}");
            }
        }
        public static void Mkdir(string dir)
        {
            if (dir != null)
            {
                Directory.CreateDirectory(dir);
                return;
            }
            Console.WriteLine("Ошибка в синтаксисе команды.");
        }
        private static void Move(string request)
        {
            string fileNames;
            string destinationDir;

            BreakdownOfRequests(request, out fileNames, out destinationDir);

            var dirDest = new DirectoryInfo(destinationDir);

            var arrFiles = fileNames.Split('|');
            int countMove = 0;

            for (int i = 0; i < arrFiles.Length; i++)
            {
                try
                {
                    var dirStart = new DirectoryInfo(arrFiles[i]);
                    if (dirDest.Parent.FullName == dirStart.Parent.FullName && !dirDest.Exists)
                    {
                        //Переименование файла/папки
                        Directory.Move(dirStart.FullName, dirDest.FullName);
                    }
                    else
                    {
                        //Перемещение файла/папки
                        Directory.Move(dirStart.FullName, Path.Combine(dirDest.FullName, dirStart.Name));
                    }
                    countMove++;
                }
                catch (Exception)
                {
                    Console.WriteLine("Не удается найти указанный файл.");
                }
            }

            if (countMove > 0)
                Console.WriteLine($"Перемещено файлов:{countMove,10}.");
        }
        private static void Rename(string request)
        {
            string fileNames;
            string finalFilename;

            BreakdownOfRequests(request, out fileNames, out finalFilename);

            var arrFiles = fileNames.Split('|');

            for (int i = 0; i < arrFiles.Length; i++)
            {
                string dir;

                //Проверяем пришел файл или папка (если не существующий - выброс исключения)
                try
                {
                    if (File.Exists(arrFiles[i]))
                    {
                        var tmp = new FileInfo(arrFiles[i]);
                        dir = tmp.Directory.FullName;
                    }
                    else if (Directory.Exists(arrFiles[i]))
                    {
                        var tmp = new DirectoryInfo(arrFiles[i]);
                        dir = tmp.Parent.FullName;
                    }
                    else
                    {
                        throw new Exception("Указаный файл не существует.");
                    }
                    NameForRenaming(arrFiles[i], dir);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            //Нумерация мен файлов для переименования
            void NameForRenaming(string nameFile, string dir)
            {
                int countFiles = 0;

                //Цикл будет работать пока не создаться файл
                do
                {
                    //Составление имени конечного имени файла
                    string newName = $"{Path.Combine(dir, finalFilename)}" +
                    $"{(countFiles == 0 ? "" : " (" + countFiles.ToString() + ")")}" +
                    $"{Path.GetExtension(nameFile)}";

                    //Вызов переименования файла если конечного имени не существует
                    if (!Directory.Exists(newName) && !File.Exists(newName))
                    {
                        Move($"{nameFile} {newName}");
                        break;
                    }
                    countFiles++;
                } while (true);
            }
        }
        private static void Title(string newTitle)   //Замена названия окна
        {
            Console.Title = newTitle;
        }
        private static void Type(string request)    //Вывод содержимого текстового файла
        {
            var arrFileNames = request.Split('|');

            for (int i = 0; i < arrFileNames.Length; i++)
            {
                if (arrFileNames.Length > 1)
                {
                    Console.WriteLine();
                    Console.WriteLine(Path.GetFileName(arrFileNames[i]));
                    Console.WriteLine();
                }

                using (var sr = new StreamReader(arrFileNames[i]))
                {
                    while (!sr.EndOfStream)
                    {
                        Console.Write((char)sr.Read());
                    }
                }
            }

        }
    }
}
