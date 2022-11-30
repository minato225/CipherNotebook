﻿using System.IO;

namespace CipherNoteBook.Server.Service;

public class TextService : ITextService
{
    public string GetText(string fileName)
    {
        string result;
        try
        {
            result = File.ReadAllText($"Texts//{fileName}");
        }
        catch (FileNotFoundException)
        {
            return null;
        }

        return result;
    }
}