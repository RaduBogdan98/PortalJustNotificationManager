using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PortalJustNotificationManager.Persistence
{
   class PersistanceManager<T>
   {
      internal void Serialize(string filePath, T serializableObject)
      {
         if(File.Exists(filePath))
         {
            File.Delete(filePath);
         }

         using (MemoryStream stream = new MemoryStream())
         {
            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            try
            {
               serializer.Serialize(stream, serializableObject);
               File.WriteAllBytes(filePath, this.Encrypt(stream.ToArray()));
               File.SetAttributes(filePath, FileAttributes.Hidden);
            }
            catch (SerializationException)
            {
               Console.WriteLine("Serializarea a esuat.");
            }
         }
      }

      internal T Deserialize(string xmlFilePath)
      {
         if (File.Exists(xmlFilePath))
         {
            using (MemoryStream stream = new MemoryStream(Decrypt(File.ReadAllBytes(xmlFilePath))))
            {
               // Construct a BinaryFormatter and use it to serialize the data to the stream.
               XmlSerializer serializer = new XmlSerializer(typeof(T));
               try
               {
                  return (T)serializer.Deserialize(stream);
               }
               catch (Exception)
               {
                  Console.WriteLine("Serializarea a esuat.");
                  return default;
               }
            }
         }

         return default;
      }

      private byte[] Encrypt(byte[] bytesToEncrypt)
      {
         int keyIndex = 0;
         for (int i = 0; i < bytesToEncrypt.Length; i++)
         {
            int byteValue = bytesToEncrypt[i];
            bytesToEncrypt[i] = (byte)(byteValue + key[keyIndex]);
            keyIndex++;

            if (keyIndex == 4) keyIndex = 0;
         }

         return bytesToEncrypt;
      }

      private byte[] Decrypt(byte[] bytesToDecrypt)
      {
         int keyIndex = 0;
         for (int i = 0; i < bytesToDecrypt.Length; i++)
         {
            int byteValue = bytesToDecrypt[i];
            bytesToDecrypt[i] = (byte)(byteValue - key[keyIndex]);
            keyIndex++;

            if (keyIndex == 4) keyIndex = 0;
         }

         return bytesToDecrypt;
      }

      private int[] key = { 82, 65, 68, 85 };
   }
}
