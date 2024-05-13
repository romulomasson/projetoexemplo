using OfficeOpenXml;
using System.Xml.Serialization;

namespace UXCarbon.Domain.Helpers;

public static class XmlHelper
{
    public static T Deserialize<T>(string xmlString)
    {
        if (xmlString == null) return default;
        var serializer = new XmlSerializer(typeof(T));
        using (var reader = new StringReader(xmlString))
        {
            return (T)serializer.Deserialize(reader);
        }
    }

    #region EPPlus

    public static bool TryFill(this ExcelRange cellRange, Action<bool> a, ListImportacaoExcelMsg ret, bool? vazio = true, bool erro = true, string fileName = null) 
    {
        if (!cellRange.TryFill(a, vazio))
        {
            if (erro)
                ret.Add($"Erro na c�lula: {cellRange?.FullAddress} n�o foi possivel interpreta-lo com booleano {(string.IsNullOrEmpty(fileName) ? "" : $"arquivo:{fileName}")}", false,cellRange);
            return false;
        }
        return true;
    }

    public static bool TryFill(this ExcelRange cellRange, Action<byte> a, ListImportacaoExcelMsg ret, byte? vazio = default(byte), bool erro = true, string fileName = null)
    {
        if (!byte.TryParse(cellRange?.Text?.Trim(), out var v))
        {
            if (erro)
                ret.Add($"Erro na c�lula: {cellRange?.FullAddress} n�o foi possivel interpreta-lo como inteiro {(string.IsNullOrEmpty(fileName) ? "" : $"arquivo:{fileName}")}", false,cellRange);
            return false;
        }
        a.Invoke(v);
        return true;
    }

    public static void Fill(this ExcelRange cellRange, Action<int?> a)
    {
        if (int.TryParse(cellRange?.Text?.Trim(), out var v))
            a.Invoke(v);
    }

    public static bool TryFill(this ExcelRange cellRange, Action<int> a, ListImportacaoExcelMsg ret, int? vazio = default(int), bool erro = true, string fileName = null)
    {
        if (!int.TryParse(cellRange?.Text?.Trim(), out var v))
        {
            if (erro)
                ret.Add($"Erro na c�lula: {cellRange?.FullAddress} n�o foi possivel interpreta-lo como inteiro {(string.IsNullOrEmpty(fileName) ? "" : $"arquivo:{fileName}")}", false, cellRange);
            return false;
        }
        a.Invoke(v);
        return true;
    }

    public static bool TryFill(this ExcelRange cellRange, Action<decimal> a, ListImportacaoExcelMsg ret, decimal? vazio = default(decimal), bool erro = true, string fileName = null)
    {
        if (!decimal.TryParse(cellRange?.Text?.Trim(), out var v))
        {
            if (erro)
                ret.Add($"Erro na c�lula: {cellRange?.FullAddress} n�o foi possivel interpreta-lo como num�rico {(string.IsNullOrEmpty(fileName) ? "" : $"arquivo:{fileName}")}", false, cellRange);
            return false;
        }
        a.Invoke(v);
        return true;
    }

    public static bool TryFill(this
        ExcelRange cellRange,
        Action<string> a,
        ListImportacaoExcelMsg ret,
        string[] remove = null,
        int? minLen = null,
        int? maxLen = null,
        bool obrigatoria = true,
        string fileName = null) 
    {
        var v = cellRange?.Text?.Trim();
        if (remove != null)
            foreach (var re in remove)
                v = v?.Replace(re, "");
        if (obrigatoria && string.IsNullOrEmpty(v))
        {
            ret.Add($"Erro na c�lula: obrigat�ria {cellRange?.FullAddress} esta vazia. {(string.IsNullOrEmpty(fileName) ? "" : $"arquivo:{fileName}")}", false, cellRange);
            return false;
        }
        if (minLen.HasValue && v.Length < minLen.Value)
        {
            ret.Add($"Erro na c�lula: {cellRange?.FullAddress} comprimento menor que o m�nomo de {minLen}. {(string.IsNullOrEmpty(fileName) ? "" : $"arquivo:{fileName}")}", false, cellRange);
            return false;
        }
        if (maxLen.HasValue && v.Length > maxLen.Value)
        {
            ret.Add($"Erro na c�lula: {cellRange?.FullAddress} comprimento maior que o m�ximo de {maxLen}. {(string.IsNullOrEmpty(fileName) ? "" : $"arquivo:{fileName}")}", false, cellRange);
            return false;
        }
        a.Invoke(v);
        return true;
    }


    private static bool TryFill(this ExcelRange cellRange, Action<bool> a, bool? vazio = true)
    {
        var t = cellRange?.Text?.ToLower()?.Trim();
        if (bool.TryParse(t, out var ret))
        {
            a.Invoke(ret);
            return true;// True, False, true, false
        }
        if (string.IsNullOrEmpty(t) && vazio.HasValue)//Vazio � verdadeiro
        {
            a.Invoke(vazio.Value);
            return true;
        }
        if (t.StartsWith("s") || t.StartsWith("v"))
        {
            a.Invoke(true);
            return true;
        }
        if (t.StartsWith("n") || t.StartsWith("f"))
        {
            a.Invoke(false);
            return true;
        }
        return false;
    }

    #endregion

    #region EPPlus 2
    public static bool TryFill(this ExcelRange cellRange, Action<decimal> a, ListImportacaoExcelMsg ret, bool erro = true)
    {
        if (!decimal.TryParse(cellRange?.Text?.Trim(), out var v))
        {
            if (erro)
                ret.Add($"Erro na c�lula: n�o foi possivel interpreta-lo com num�rico", true, cellRange);
            return false;
        }
        a.Invoke(v);
        return true;
    }
    public static bool TryFill2(this ExcelRange cellRange, Action<bool> a, ListImportacaoExcelMsg ret, bool? vazio = true, bool erro = true)
    {
        if (!cellRange.TryFill2(a, vazio))
        {
            if (erro)
                ret.Add($"Erro na c�lula: n�o foi possivel interpreta-lo com booleano", true, cellRange);
            return false;
        }
        return true;
    }

    public static bool TryFill2(this ExcelRange cellRange, Action<byte> a, ListImportacaoExcelMsg ret, byte? vazio = default(byte), bool erro = true)
    {
        if (!byte.TryParse(cellRange?.Text?.Trim(), out var v))
        {
            if (erro)
                ret.Add($"Erro na c�lula: n�o foi possivel interpreta-lo como inteiro", true, cellRange);
            return false;
        }
        a.Invoke(v);
        return true;
    }

    public static void Fill2(this ExcelRange cellRange, Action<int?> a)
    {
        if (int.TryParse(cellRange?.Text?.Trim(), out var v))
            a.Invoke(v);
    }

    public static bool TryFill2(this ExcelRange cellRange, Action<int> a, ListImportacaoExcelMsg ret, int? vazio = default(int), bool erro = true)
    {
        if (!int.TryParse(cellRange?.Text?.Trim(), out var v))
        {
            if (erro)
                ret.Add($"Erro na c�lula: n�o foi possivel interpreta-lo como inteir", true, cellRange);
            return false;
        }
        a.Invoke(v);
        return true;
    }

    public static bool TryFill2(this ExcelRange cellRange, Action<decimal> a, ListImportacaoExcelMsg ret, decimal? vazio = default(decimal), bool erro = true)
    {
        if (!decimal.TryParse(cellRange?.Text?.Trim(), out var v))
        {
            if (erro)
                ret.Add($"Erro na c�lula: n�o foi possivel interpreta-lo como num�rico", true, cellRange);
            return false;
        }
        a.Invoke(v);
        return true;
    }

    public static bool TryFillDecimal(this ExcelRange cellRange, Action<decimal> a, ListImportacaoExcelMsg ret, decimal? vazio = default(decimal), bool erro = true)
    {
        if (!decimal.TryParse(cellRange?.Text?.Trim().Replace(".",","), out var v))
        {
            if (erro)
                ret.Add($"Erro na c�lula: n�o foi possivel interpreta-lo como num�rico", true, cellRange);
            return false;
        }
        a.Invoke(v);
        return true;
    }

    public static bool TryFill2(this
        ExcelRange cellRange,
        Action<string> a,
        ListImportacaoExcelMsg ret,
        string[] remove = null,
        int? minLen = null,
        int? maxLen = null,
        bool obrigatoria = true,
        string fileName = null)
    {
        var v = cellRange?.Text?.Trim();
        if (remove != null)
            foreach (var re in remove)
                v = v?.Replace(re, "");
        if (obrigatoria && string.IsNullOrEmpty(v))
        {
            ret.Add($"Erro na c�lula: obrigat�ria esta vazia.", true, cellRange);
            return false;
        }
        if (minLen.HasValue && v.Length < minLen.Value)
        {
            ret.Add($"Erro na c�lula: comprimento menor que o m�nomo de {minLen}.", true, cellRange);
            return false;
        }
        if (maxLen.HasValue && v.Length > maxLen.Value)
        {
            ret.Add($"Erro na c�lula: comprimento maior que o m�ximo de {maxLen}.", true, cellRange);
            return false;
        }
        a.Invoke(v);
        return true;
    }


    private static bool TryFill2(this ExcelRange cellRange, Action<bool> a, bool? vazio = true)
    {
        var t = cellRange?.Text?.ToLower()?.Trim();
        if (bool.TryParse(t, out var ret))
        {
            a.Invoke(ret);
            return true;// True, False, true, false
        }
        if (string.IsNullOrEmpty(t) && vazio.HasValue)//Vazio � verdadeiro
        {
            a.Invoke(vazio.Value);
            return true;
        }
        if (t.StartsWith("s") || t.StartsWith("v"))
        {
            a.Invoke(true);
            return true;
        }
        if (t.StartsWith("n") || t.StartsWith("f"))
        {
            a.Invoke(false);
            return true;
        }
        return false;
    }

    #endregion

    public class ImportacaoExcelMsg
    {
        public string Msg { get; set; }
        public string Cel { get; set; }
        public string Vlr { get; set; }
        public bool Error { get; set; }
    }

    public class ListImportacaoExcelMsg : List<ImportacaoExcelMsg>
    {
        public void Add(string msg, bool error, ExcelRange cellRange)
        {
            var vlr = string.Empty;
            if (cellRange?.Value is object[,] a)
            {
                for (int i = 0; i < a.GetLength(0); i++)
                {
                    for (int j = 0; j < a.GetLength(1); j++)
                    {
                        vlr += a[i, j] + " | ";
                    }
                }
                vlr = vlr.Substring(0, vlr.Length - 3);
            }
            else
                vlr = cellRange?.Text;


            Add(new ImportacaoExcelMsg { Msg = msg, Error = error, Cel = cellRange?.Address, Vlr = vlr });
        }

        public void Add(string msg, bool error, string line, string vlr)
        {
            Add(new ImportacaoExcelMsg { Msg = msg, Error = error, Cel = line, Vlr = vlr });
        }
    }
}


