using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_C_Sharp
{
    class HelpInfo
    {
        public static void Help()
        {
            Console.WriteLine("Для получения сведений об определенной команде наберите HELP <имя команды>");
            Console.WriteLine("[Имя_файла]    Открывает файл соответствующим приложением");
            Console.WriteLine("ATTRIB         Отображает или изменяет атрибуты файлов.");
            Console.WriteLine("CD             Вывод имени либо смена текущей папки.");
            Console.WriteLine("CLS            Очистка экрана.");
            Console.WriteLine("COPY           Копирование одного или нескольких файлов в другое место.");
            Console.WriteLine("CREATE	       Создание нового файла.");
            Console.WriteLine("DEL            Удаление одного или нескольких файлов.");
            Console.WriteLine("DIR            Вывод списка файлов и подпапок из указанной папки.");
            Console.WriteLine("EXIT           Завершает работу программы MyCMD.EXE.");
            Console.WriteLine("HELP           Выводит справочную информацию о командах Windows.");
            Console.WriteLine("HISTORY	       Выводит историю запросов.");
            Console.WriteLine("MKDIR          Создает каталог.");
            Console.WriteLine("MOVE           Перемещает один или несколько файлов из одного каталога");
            Console.WriteLine("RENAME         Переименовывает файлы.");
            Console.WriteLine("TITLE          Назначает заголовок окна для сеанса CMD.EXE.");
            Console.WriteLine("TYPE           Отображает содержимое текстовых файлов.");
            Console.WriteLine();
            Console.WriteLine("Дополнительные сведения о средствах см. в описании программ командной строки в справке.");
        }
        public static void HelpAttrib()
        {
            Console.WriteLine("Отображает или изменяет атрибуты файлов.");
            Console.WriteLine();
            Console.WriteLine("ATTRIB [диск:][путь][имя_файла]");
            Console.WriteLine();
            Console.WriteLine("  [диск:][путь][имя_файла]");
            Console.WriteLine("      Указывает файл по которому отобразить информацию.");
        }
        public static void HelpCd()
        {
            Console.WriteLine("Выводит имя или изменяет текущий каталог.");
            Console.WriteLine();
            Console.WriteLine("CD [/D] [диск:][путь]");
            Console.WriteLine("CD [..]");
            Console.WriteLine();
            Console.WriteLine("  ..  обозначает переход в родительский каталог.");
            Console.WriteLine();
            Console.WriteLine("Команда CD диск: отображает имя текущего каталога указанного диска.");
            Console.WriteLine("Команда CD без параметров отображает имена текущего диска и каталога.");
        }
        public static void HelpCls()
        {
            Console.WriteLine("Очищает содержимое экрана.");
            Console.WriteLine();
            Console.WriteLine("CLS");
        }
        public static void HelpCopy()
        {
            Console.WriteLine("Копирование одного или нескольких файлов в другое место.");
            Console.WriteLine();
            Console.WriteLine("COPY источник[|источник[|...]] [результат]");
            Console.WriteLine();
            Console.WriteLine("  источник     Имена одного или нескольких копируемых файлов.");
            Console.WriteLine("  результат    Каталог и/или имя для конечных файлов.");
            Console.WriteLine("Чтобы объединить файлы, укажите один конечный и несколько исходных файлов,");
            Console.WriteLine("используя формат \"файл1|файл2|файл3|...\".");
        }
        public static void HelpCreate()
        {
            Console.WriteLine("Создание нового файла.");
            Console.WriteLine();
            Console.WriteLine("CREATE[текст] > [название_файла]");
            Console.WriteLine();
            Console.WriteLine("  Текст			Определяет текст который будет записан в файл.");
            Console.WriteLine("  Название_файла	Определяет новое имя файла.");
        }
        public static void HelpDel()
        {
            Console.WriteLine("Удаление одного или нескольких файлов.");
            Console.WriteLine();
            Console.WriteLine("DEL names");
            Console.WriteLine();
            Console.WriteLine("  names         Список из одного или нескольких файлов или каталогов.");
            Console.WriteLine("                Для удаления группы файлов можно использовать подстановочные знаки. Если");
            Console.WriteLine("                указан каталог, будут удалены все файлы в этом");
            Console.WriteLine("                каталоге.");
            Console.WriteLine();
            Console.WriteLine("Чтобы объединить файлы, укажите несколько файлов,");
            Console.WriteLine("используя символ \"|\". Пример формата \"файл1|файл2|файл3|...\".");
        }
        public static void HelpDir()
        {
            Console.WriteLine("Вывод списка файлов и подкаталогов в указанном каталоге.");
            Console.WriteLine();
            Console.WriteLine("DIR [drive:][path][filename]");
            Console.WriteLine();
            Console.WriteLine("  [drive:][path][filename]");
            Console.WriteLine("              Диск, каталог или имена файлов для включения в список.");
            Console.WriteLine();
            Console.WriteLine("DIR [name] [/S]");
            Console.WriteLine("  [name]      Название файла.");
            Console.WriteLine("  /S          Отображение файлов начиная с текущей директории, ");
            Console.WriteLine("              названия которых содержат [name].");
        }
        public static void HelpExit()
        {
            Console.WriteLine("Завершает программу MyCMD.EXE.");
            Console.WriteLine();
            Console.WriteLine("EXIT");
        }
        public static void HelpHelp()
        {
            Console.WriteLine("Вывод справочных сведений о командах Windows.");
            Console.WriteLine();
            Console.WriteLine("HELP [<команда>]");
            Console.WriteLine();
            Console.WriteLine("    <команда> - команда, интересующая пользователя.");
        }
        public static void HelpHistory()
        {
            Console.WriteLine("Выводит историю запросов.");
            Console.WriteLine();
            Console.WriteLine("HISTORY");
        }
        public static void HelpMkdir()
        {
            Console.WriteLine("Создание каталога.");
            Console.WriteLine();
            Console.WriteLine("MKDIR [диск:]путь");
            Console.WriteLine();
            Console.WriteLine("Команда MKDIR создает при необходимости все промежуточные каталоги в пути.");
            Console.WriteLine(@"Например, если \a не существует, то:");
            Console.WriteLine();
            Console.WriteLine(@"    mkdir \a\b\c\d");
        }
        public static void HelpMove()
        {
            Console.WriteLine("Перемещение одного или более файлов:");
            Console.WriteLine("MOVE [диск:][путь]имя_файла1[,...] назначение");
            Console.WriteLine();
            Console.WriteLine("Переименование папки/файла:");
            Console.WriteLine("MOVE [диск:][путь]имя [диск:][путь]новое_имя");
            Console.WriteLine();
            Console.WriteLine("  [диск:][путь]имя	  Определяет местоположение файла или файлов, которые");
            Console.WriteLine("                          необходимо переместить.");
            Console.WriteLine("  Назначение              Определяет новое местоположение файла. Назначение");
            Console.WriteLine("                          может состоять из буквы диска (с последующим");
            Console.WriteLine("                          двоеточием), имени папки или их комбинации. При");
            Console.WriteLine("                          перемещении только одного файла, можно указать и его");
            Console.WriteLine("                          новое имя, если хотите выполнить его одновременное");
            Console.WriteLine("                          переименование при перемещении.");
            Console.WriteLine("  [диск:][путь]имя  Определяет папку, которую необходимо переименовать.");
            Console.WriteLine("  новое_имя         Определяет новое имя папки.");
        }
        public static void HelpRename()
        {
            Console.WriteLine("Переименование одного или нескольких файлов.");
            Console.WriteLine();
            Console.WriteLine("RENAME [диск:][путь]имя_файла1 имя_файла2.");
            Console.WriteLine();
            Console.WriteLine("Чтобы объединить переименование файлов, укажите несколько файлов,");
            Console.WriteLine("используя символ \"|\". Пример формата \"файл1|файл2|файл3|...\".");
            Console.WriteLine();
            Console.WriteLine("Обратите внимание, что для конечного файла невозможно указать другой диск или путь.");

        }
        public static void HelpTitle()
        {
            Console.WriteLine("Изменение заголовка окна командной строки.");
            Console.WriteLine();
            Console.WriteLine("TITLE [строка]");
            Console.WriteLine();
            Console.WriteLine("  строка       Будущий заголовок окна командной строки.");
        }
        public static void HelpType()
        {
            Console.WriteLine("Вывод содержимого одного или нескольких текстовых файлов.");
            Console.WriteLine();
            Console.WriteLine("TYPE [диск:][путь]имя_файла");
            Console.WriteLine();
            Console.WriteLine("Чтобы объединить вывод файлов, укажите несколько файлов,");
            Console.WriteLine("используя символ \"|\". Пример формата \"файл1|файл2|файл3|...\".");
        }
    }
}
