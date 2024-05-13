using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.IO.Compression;
using System.Text;

namespace Exemplo.Extensions;

public static class BusExtensions
{
    public static T As<T>(this ServiceBusMessage message) where T : class
    {
        return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(message.Body));
    }

    public static ServiceBusMessage AsMessage(this object obj, bool compress = false)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));

        if (compress)
            bytes = bytes.CompressByteZip();

        return new ServiceBusMessage(bytes);
    }

    public static bool Any(this IList<ServiceBusMessage> collection)
    {
        return collection != null && collection.Count > 0;
    }

    public static byte[] CompressByteZip(this byte[] bytes)
    {
        using var mso = new MemoryStream();
        using (var gs = new GZipStream(mso, CompressionMode.Compress))
            gs.Write(bytes, 0, bytes.Length);

        return mso.ToArray();
    }

    public static bool IsByteZipCompressedData(this byte[] bytes) => BitConverter.ToUInt16(bytes, 0) == 0x8b1f;
    public static byte[] DecompressByteZip(this byte[] bytes)
    {
        byte[] lengthBuffer = new byte[4];
        Array.Copy(bytes, bytes.Length - 4, lengthBuffer, 0, 4);
        int uncompressedSize = BitConverter.ToInt32(lengthBuffer, 0);

        var buffer = new byte[uncompressedSize];
        using (var ms = new MemoryStream(bytes))
        {
            using var gzip = new GZipStream(ms, CompressionMode.Decompress);
            gzip.Read(buffer, 0, uncompressedSize);
        }

        return buffer;
    }

}




