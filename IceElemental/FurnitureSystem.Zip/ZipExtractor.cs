﻿namespace FurnitureSystem.Zip
{
    using System;
    using System.IO;
    using System.IO.Compression;

    public static class ZipExtractor
    {
        public static void Extract(string zipPath, string extractPath)
        {
            using (var archive = ZipFile.OpenRead(zipPath))
            {
                foreach (var entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
                    {
                        entry.ExtractToFile(Path.Combine(extractPath, entry.FullName), true);
                    }
                }
            } 
        }
    }
}
