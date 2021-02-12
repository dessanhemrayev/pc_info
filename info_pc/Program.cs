using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;

namespace info_pc
{
    class Program
    {
    static public double Byte_to_gb(long size_d){
                 return Math.Round(size_d / Math.Pow(1024, 3),2);
               }
    static public void get_info_hdd()
    {
        Console.WriteLine();
        Console.WriteLine("=================================================================");
        Console.WriteLine("Структура файловой системы (тип, имена и объёмы дисков)");
        Console.WriteLine("=================================================================");
        foreach (var drive in DriveInfo.GetDrives())
        {
            try
            {
                Console.WriteLine("Имя диска: " + drive.Name);
                Console.WriteLine("Файловая система: " + drive.DriveFormat);
                Console.WriteLine("Тип диска: " + drive.DriveType);
                Console.WriteLine("Объем доступного свободного места : " + Byte_to_gb(drive.AvailableFreeSpace) + " GB");
                Console.WriteLine("Готов ли диск: " + drive.IsReady);
                Console.WriteLine("Корневой каталог диска: " + drive.RootDirectory);
                Console.WriteLine("Общий объем свободного места, доступного на диске : " + Byte_to_gb(drive.TotalFreeSpace) + " GB");
                Console.WriteLine("Размер диска : " + Byte_to_gb(drive.TotalSize) + " GB");
                Console.WriteLine("Метка тома диска: " + drive.VolumeLabel);
            }
            catch {}

            Console.WriteLine();
        }
    }

        static public void get_info_motherboard() {
            Console.WriteLine();
            Console.WriteLine("=================================================================");
            Console.WriteLine("Производитель и название материнской платы");
            Console.WriteLine("=================================================================");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Manufacturer,Product, SerialNumber,Version FROM Win32_BaseBoard");
           
            ManagementObjectCollection information = searcher.Get();
            foreach (ManagementObject obj in information)
            {
                
                foreach (PropertyData data in obj.Properties)
                Console.WriteLine("{0} = {1}", data.Name, data.Value);
              
            }
         
        }
        static public void get_info_jmd()
        {
            Console.WriteLine();
            Console.WriteLine("=================================================================");
            Console.WriteLine("Идентификатор ЖМД");
            Console.WriteLine("=================================================================");
            ManagementObjectSearcher mosDisks11 = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
            
            List<string> str_info = new List<string>();
            foreach (ManagementObject moDisk1 in mosDisks11.Get())

            {

                str_info.Add(moDisk1["Model"].ToString());

            }
            foreach (string item in str_info)
            {

                ManagementObjectSearcher mosDisks = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive WHERE Model = '" + item + "'");

                foreach (ManagementObject moDisk in mosDisks.Get())

                {

                    Console.WriteLine("Тип: " + moDisk["MediaType"].ToString());
                    Console.WriteLine("Модель: " + moDisk["Model"].ToString());
                    Console.WriteLine("Сериа номер: " + moDisk["SerialNumber"].ToString());
                    Console.WriteLine("Интерфейс: " + moDisk["InterfaceType"].ToString());
                    Console.WriteLine("Обьем: " + moDisk["Size"].ToString() + " bytes (" + Math.Round(((((double)Convert.ToDouble(moDisk["Size"]) / 1024) / 1024) / 1024), 2) + " GB))");
                    Console.WriteLine("Разделы: " + moDisk["Partitions"].ToString());
                    //Console.WriteLine("Signature: " + moDisk["Signature"].ToString());
                    Console.WriteLine("Версия ПО: " + moDisk["FirmwareRevision"].ToString());
                    Console.WriteLine("Блоки: " + moDisk["TotalCylinders"].ToString());
                    Console.WriteLine("Сектора: " + moDisk["TotalSectors"].ToString());
                    Console.WriteLine("Головок: " + moDisk["TotalHeads"].ToString());
                    Console.WriteLine("Треки: " + moDisk["TotalTracks"].ToString());
                    Console.WriteLine("Байт на сектор: " + moDisk["BytesPerSector"].ToString());
                    Console.WriteLine("Секторов на треке: " + moDisk["SectorsPerTrack"].ToString());
                    Console.WriteLine("Треков на блоке: " + moDisk["TracksPerCylinder"].ToString());
                    
                    Console.WriteLine("#################################################################");
                }
            }
        }
        static void Main(string[] args)
        {
          while (true)
            {
                Console.WriteLine();
                Console.WriteLine("=================================================================");
                Console.WriteLine("1.Производитель и название материнской платы");
                Console.WriteLine("2.Идентификатор ЖМД");
                Console.WriteLine("3.Структура файловой системы (тип, имена и объёмы дисков)");
                Console.Write("Ввведите номер задании:");
               string zadanie = Console.ReadLine();
                switch (zadanie)
                {
                    case "1": { get_info_motherboard(); Console.WriteLine(); break; }
                    case "2": {get_info_jmd(); Console.WriteLine(); break; }
                    case "3": { get_info_hdd(); Console.WriteLine(); break; }
                    default:
                        {
                            Console.WriteLine();
                            Console.WriteLine("Ошибка при вводе номера задании!!!");
                            break;
                        }
                }
            }
            
        }

        
    }
}
