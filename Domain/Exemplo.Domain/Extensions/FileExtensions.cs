
namespace Exemplo.Extensions;

public static class FileExtensions
{
    private const char CR = '\r';
    private const char LF = '\n';
    private const char NULL = (char)0;
 
    public static byte[] ToByteArray(this Stream str)
    {
        using (var memoryStream = new MemoryStream())
        {
            str.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }

    public static int GetStreamLines(this Stream stream)
    {
        int bytesRead, lineCount = 0;

        if (stream == null) return lineCount;

        var byteBuffer = new byte[1024 * 1024];
        var detectedEOL = NULL;
        var currentChar = NULL;

        while ((bytesRead = stream.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
        {
            for (var i = 0; i < bytesRead; i++)
            {
                currentChar = (char)byteBuffer[i];

                if (detectedEOL != NULL)
                {
                    if (currentChar == detectedEOL) lineCount++;
                }
                else if (currentChar == LF || currentChar == CR)
                {
                    detectedEOL = currentChar;
                    lineCount++;
                }
            }
        }

        if (currentChar != LF && currentChar != CR && currentChar != NULL) lineCount++;
        return lineCount;
    }
}




