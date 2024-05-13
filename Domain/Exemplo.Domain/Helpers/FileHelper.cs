using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Exemplo.Domain.Helpers;

namespace Exemplo.Domain.Helpers;

public static class FileHelper
{
    public static byte[] ConvertFromBase64String(string input)
    {
        Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
        var teste1 = regex.Replace(input, string.Empty);
        return Convert.FromBase64String(teste1);
    }

    public static byte[] StreamToBytes(System.IO.Stream stream)
    {
        long originalPosition = 0;

        if (stream.CanSeek)
        {
            originalPosition = stream.Position;
            stream.Position = 0;
        }

        try
        {
            byte[] readBuffer = new byte[4096];

            int totalBytesRead = 0;
            int bytesRead;

            while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
            {
                totalBytesRead += bytesRead;

                if (totalBytesRead == readBuffer.Length)
                {
                    int nextByte = stream.ReadByte();
                    if (nextByte != -1)
                    {
                        byte[] temp = new byte[readBuffer.Length * 2];
                        Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                        Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                        readBuffer = temp;
                        totalBytesRead++;
                    }
                }
            }

            byte[] buffer = readBuffer;
            if (readBuffer.Length != totalBytesRead)
            {
                buffer = new byte[totalBytesRead];
                Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
            }
            return buffer;
        }
        finally
        {
            if (stream.CanSeek)
            {
                stream.Position = originalPosition;
            }
        }
    }

    public static byte[] ConvertToXlsx(this List<object> list, string sheetName = "Sheet-1", bool haveHeader = true)
    {
        byte[] bytes = null;

        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add(sheetName);
            var currentRow = 1;

            if (haveHeader)
            {
                var headers = ObterHeaderOuDados(list, true);
                var regex = new Regex("^[0-9]+$");
                int t = 1;
                foreach (var header in headers)
                {
                    worksheet.Cell(currentRow, t).Value = header;

                    //if (regex.IsMatch(propertyValuesHeader[property].ToString()))
                    //    worksheet.Column(t).Style.NumberFormat.Format = "@";
                    t++;
                }
            }

            var dados = ObterHeaderOuDados(list, false);
            foreach (var item in dados)
            {
                currentRow++;

                int i = 1;
                foreach (var property in item)
                {
                    worksheet.Cell(currentRow, i).Value = property;
                    i++;
                }
            }

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                bytes = stream.ToArray();
            }
        }

        return bytes;
    }

    private static List<dynamic> ObterHeaderOuDados(List<dynamic> list, bool header = false)
    {
        var listHeader = new List<dynamic>();
        var listDados = new List<dynamic>();

        if (header)
        {
            foreach (PropertyInfo l in list.FirstOrDefault().GetType().GetProperties())
                listHeader.Add(l.Name);
            return listHeader;
        }
        else
        {
            foreach (var l in list)
            {
                var item = new List<dynamic>();
                foreach (PropertyInfo lp in l.GetType().GetProperties())
                    item.Add(lp.GetValue(l));

                listDados.Add(item);
            }
            return listDados;
        }
    }
    public static string FormataTemplatePosicional(this string layout, object data)
    {
        string rowFormat = string.Empty;

        if (!string.IsNullOrEmpty(layout))
        {
            string[] layoutArray = layout.Split(',');

            foreach (var layoutItem in layoutArray)
            {
                //{ TipoDeRegistro | Number | 1 | 9} Exemplo
                var arrayItem = layoutItem.Replace("{", "").Replace("}", "").Split('|');
                string propName = arrayItem[0].ToLower();
                string typeValue = arrayItem[1].ToLower();
                string lenghtProp = arrayItem[2].ToString();
                string defaultValue = arrayItem[3].ToLower();


                switch (defaultValue)
                {
                    case ("column"):
                        string propNamePascalCase = data.GetType().GetProperties().ToList().Where(t => t.Name.ToLower() == propName).Select(t => t.Name).FirstOrDefault().ToString();
                        string valueData = data.GetType().GetProperty(propNamePascalCase).GetValue(data, null).ToString();

                        Regex regex = new Regex(@"[^a-zA-Z0-9 ]");
                        valueData = regex.Replace(valueData, "").ToUpper();

                        rowFormat = String.Concat(rowFormat, FormataLinha(typeValue, lenghtProp, valueData.ToString()).Substring(0, lenghtProp.ToInt32()));
                        break;
                    case ("empty"):
                        rowFormat = String.Concat(rowFormat, ' '.Repeat(lenghtProp.ToInt32()));
                        break;
                    default:
                        rowFormat = String.Concat(rowFormat, FormataLinha(typeValue, lenghtProp, defaultValue));
                        break;
                }
            }
        }

        return rowFormat.ToUpperInvariant();
    }

    public static string FormataTemplateDelimitado(this string layout, object data, string delimitador)
    {
        string rowFormat = string.Empty;

        if (!string.IsNullOrEmpty(layout))
        {
            string[] layoutArray = layout.Split(',');

            foreach (var layoutItem in layoutArray)
            {
                var arrayItem = layoutItem.Replace("{", "").Replace("}", "").Split('|');
                string propName = arrayItem[0].ToLower();
                string defaultValue = arrayItem[1].ToLower();

                switch (defaultValue)
                {
                    case ("column"):
                        string propNamePascalCase = data.GetType().GetProperties().ToList().Where(t => t.Name.ToLower() == propName).Select(t => t.Name).FirstOrDefault().ToString();
                        string valueData = data.GetType().GetProperty(propNamePascalCase).GetValue(data, null).ToString();

                        Regex regex = new Regex(@"[^a-zA-Z0-9 /]");
                        valueData = regex.Replace(valueData, "").ToUpper();

                        rowFormat = String.Concat(rowFormat, valueData, delimitador);
                        break;
                    case ("empty"):
                        rowFormat = String.Concat(rowFormat, " ", delimitador);
                        break;
                    default:
                        rowFormat = String.Concat(rowFormat, defaultValue, delimitador);
                        break;
                }
            }
        }

        return rowFormat.ToUpperInvariant();
    }

    private static string FormataLinha(string typeValue, string lenghtProp, string valueData)
    {
        string valorFormatado = string.Empty;
        switch (typeValue)
        {
            case ("number"):
                valorFormatado = valueData.PadRight(lenghtProp.ToInt32(), '0');
                break;
            case ("string"):
                valorFormatado = valueData.PadRight(lenghtProp.ToInt32(), ' ');
                break;
            case ("decimal"):
                valorFormatado = valueData.PadRight(lenghtProp.ToInt32(), '0');
                break;
            default:
                break;
        }

        return valorFormatado;
    }
    private static string Repeat(this char charToRepeat, int repeat)
    {

        return new string(charToRepeat, repeat);
    }
    private static string Repeat(this string stringToRepeat, int repeat)
    {
        var builder = new StringBuilder(repeat * stringToRepeat.Length);
        for (int i = 0; i < repeat; i++)
        {
            builder.Append(stringToRepeat);
        }
        return builder.ToString();
    }
}



