using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public delegate double MathOperation(double x, double y);

class Program
{
    static void Main()
    {
        // Задача 1
        List<int> numbers = new List<int> { 10, 5, 20, 15, 25 };
        int secondMaxValue = numbers.OrderByDescending(x => x).Skip(1).First();
        int secondMaxIndex = numbers.IndexOf(secondMaxValue);

        Console.WriteLine($"Task 1:");
        Console.WriteLine($"Second max value: {secondMaxValue}, Index: {secondMaxIndex}");

        numbers.RemoveAll(x => x % 2 != 0);

        Console.WriteLine("List after removing odd numbers:");
        foreach (var num in numbers)
        {
            Console.Write($"{num} ");
        }
        Console.WriteLine();

        // Задача 2
        List<double> doubles = new List<double> { 10.5, 5.7, 20.3, 15.8, 25.2 };
        double average = doubles.Average();

        Console.WriteLine($"\nTask 2:");
        Console.WriteLine($"Elements greater than average ({average}):");
        foreach (var num in doubles.Where(x => x > average))
        {
            Console.Write($"{num} ");
        }
        Console.WriteLine();

        // Задача 3
        string inputFileName = "input.txt";
        string outputFileName = "output.txt";
        string[] numbersInFile = File.ReadAllLines(inputFileName);
        Array.Reverse(numbersInFile);
        File.WriteAllLines(outputFileName, numbersInFile);

        Console.WriteLine($"\nTask 3:");
        Console.WriteLine("Numbers in reversed order have been written to 'output.txt'");

        // Задача 4
        string employeesFileName = "employees.txt";
        string outputEmployeesFileName = "output_employees.txt";
        string[] employeeLines = File.ReadAllLines(employeesFileName);

        var below10000 = employeeLines.Where(line => int.Parse(line.Split(',')[5]) < 10000);
        var above10000 = employeeLines.Where(line => int.Parse(line.Split(',')[5]) >= 10000);

        File.WriteAllLines(outputEmployeesFileName, below10000.Concat(above10000));

        Console.WriteLine($"\nTask 4:");
        Console.WriteLine($"Filtered employee data has been written to '{outputEmployeesFileName}'");

        // Задача 5
        MusicCatalog catalog = new MusicCatalog();

        catalog.AddDisk("Disk1");
        catalog.AddDisk("Disk2");

        catalog.AddSong("Disk1", "Song1", "Artist1");
        catalog.AddSong("Disk1", "Song2", "Artist2");
        catalog.AddSong("Disk2", "Song3", "Artist1");

        Console.WriteLine($"\nTask 5:");
        Console.WriteLine("Initial Music Catalog:");
        catalog.DisplayCatalog();

        catalog.RemoveSong("Disk1", "Song1");
        catalog.RemoveDisk("Disk2");

        Console.WriteLine("\nCatalog after removing a song and a disk:");
        catalog.DisplayCatalog();

        Console.WriteLine("\nSongs by Artist1:");
        catalog.SearchByArtist("Artist1");
    }
}

class MusicCatalog
{
    private Hashtable disks = new Hashtable();

    public void AddDisk(string diskName)
    {
        disks.Add(diskName, new List<string>());
    }

    public void RemoveDisk(string diskName)
    {
        disks.Remove(diskName);
    }

    public void AddSong(string diskName, string songName, string artist)
    {
        if (disks.ContainsKey(diskName))
        {
            var songs = (List<string>)disks[diskName];
            songs.Add($"{songName} by {artist}");
        }
        else
        {
            Console.WriteLine($"Disk '{diskName}' not found.");
        }
    }

    public void RemoveSong(string diskName, string songName)
    {
        if (disks.ContainsKey(diskName))
        {
            var songs = (List<string>)disks[diskName];
            songs.Remove($"{songName}");
        }
        else
        {
            Console.WriteLine($"Disk '{diskName}' not found.");
        }
    }

    public void DisplayCatalog()
    {
        foreach (DictionaryEntry entry in disks)
        {
            Console.WriteLine($"Disk: {entry.Key}");

            var songs = (List<string>)entry.Value;
            foreach (var song in songs)
            {
                Console.WriteLine($"  {song}");
            }
        }
    }

    public void SearchByArtist(string artist)
    {
        foreach (DictionaryEntry entry in disks)
        {
            var songs = (List<string>)entry.Value;
            var artistSongs = songs.Where(s => s.Contains($"by {artist}"));
            foreach (var song in artistSongs)
            {
                Console.WriteLine($"  {song}");
            }
        }
    }
}
