using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comp110_worksheet_7
{
    public static class DirectoryUtils
    {
        // Return the size, in bytes, of the given file
        public static long GetFileSize(string filePath)
        {
            return new FileInfo(filePath).Length;
        }

        // Return true if the given path points to a directory, false if it points to a file
        public static bool IsDirectory(string path)
        {
            return File.GetAttributes(path).HasFlag(FileAttributes.Directory);
        }

        // Return the total size, in bytes, of all the files below the given directory
        public static long GetTotalSize(string directory)
        {
            long totalSize = 0;
            if (IsDirectory(directory))
            {
                var files = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
                foreach (var file in files)
                {
                    totalSize += GetFileSize(file);
                }
            }
            return totalSize;
        }

        // Return the number of files (not counting directories) below the given directory
        public static int CountFiles(string directory)
        {
            string[] files;
            if (IsDirectory(directory))
            {
                files = Directory.GetFiles(directory, "*", SearchOption.TopDirectoryOnly);
                return files.Length;
            }
            else
            {
                return 0;
            }
        }

        // Return the nesting depth of the given directory. A directory containing only files (no subdirectories) has a depth of 0.
        public static int GetDepth(string directory)
        {
            var directories = Directory.GetDirectories(directory);
            if (directories.Length != 0)
            {
                foreach (var dir in directories)
                {
                    return GetDepth(dir) + 1;
                }
            }
            return 0;
        }

        // Get the path and size (in bytes) of the smallest file below the given directory
        public static Tuple<string, long> GetSmallestFile(string directory)
        {
            long smallestFile = 0;
            string smallestFilePath = "";
            if (IsDirectory(directory))
            {
                var files = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
                smallestFile = GetFileSize(files[0]);
                foreach (var file in files)
                {
                    if (GetFileSize(file) < smallestFile)
                    {
                        smallestFile = GetFileSize(file);
                        smallestFilePath = file;
                    }
                }

            }
            return new Tuple<string, long>(smallestFilePath, smallestFile);
        }

        // Get the path and size (in bytes) of the largest file below the given directory
        public static Tuple<string, long> GetLargestFile(string directory)
        {
            long largestFile = 0;
            string largestFilePath = "";
            if (IsDirectory(directory))
            {
                var files = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
                largestFile = GetFileSize(files[0]);
                foreach (var file in files)
                {
                    if (GetFileSize(file) > largestFile)
                    {
                        largestFile = GetFileSize(file);
                        largestFilePath = file;
                    }
                }

            }
            return new Tuple<string, long>(largestFilePath, largestFile);
        }

        // Get all files whose size is equal to the given value (in bytes) below the given directory
        public static IEnumerable<string> GetFilesOfSize(string directory, long size)
        {
            List<string> filesOfSize = new List<string>();
            var files = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                if (GetFileSize(file) == size)
                {
                    filesOfSize.Add(file);
                }
            }
            return filesOfSize;
        }
    }
}
