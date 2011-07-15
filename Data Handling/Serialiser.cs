using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace TutorClient
{
    // Custom Dictionary Serializer
    public class DictionarySerializer<K, V>
    {
        public struct SerializableKeyValuePair<TKey, TValue>
        {
            public TKey Key;
            public TValue Value;
            public SerializableKeyValuePair(KeyValuePair<TKey, TValue> kvp)
            {
                this.Key = kvp.Key;
                this.Value = kvp.Value;
            }
        }

        private XmlSerializer Serializer = new XmlSerializer(typeof(List<SerializableKeyValuePair<K, V>>));

        public void Serialize(Dictionary<K, V> dictionary, XmlWriter serializationStream)
        {
            Serializer.Serialize(serializationStream, BuildItemList(dictionary));
        }
        public void Serialize(Dictionary<K, V> dictionary, TextWriter serializationStream)
        {
            Serializer.Serialize(serializationStream, BuildItemList(dictionary));
        }
        public void Serialize(Dictionary<K, V> dictionary, Stream serializationStream)
        {
            Serializer.Serialize(serializationStream, BuildItemList(dictionary));
        }

        private List<SerializableKeyValuePair<K, V>> BuildItemList(Dictionary<K, V> dictionary)
        {
            List<SerializableKeyValuePair<K, V>> list = new List<SerializableKeyValuePair<K, V>>();
            foreach (KeyValuePair<K, V> nonSerializableKVP in dictionary)
            {
                list.Add(new SerializableKeyValuePair<K, V>(nonSerializableKVP));
            }

            return list;

        }


        public Dictionary<K, V> Deserialize(XmlReader serializationStream)
        {
            List<SerializableKeyValuePair<K, V>> dictionaryItems = Serializer.Deserialize(serializationStream) as List<SerializableKeyValuePair<K, V>>;
            return BuildDictionary(dictionaryItems);
        }
        public Dictionary<K, V> Deserialize(TextReader serializationStream)
        {
            List<SerializableKeyValuePair<K, V>> dictionaryItems = Serializer.Deserialize(serializationStream) as List<SerializableKeyValuePair<K, V>>;
            return BuildDictionary(dictionaryItems);
        }
        public Dictionary<K, V> Deserialize(Stream serializationStream)
        {
            List<SerializableKeyValuePair<K, V>> dictionaryItems = Serializer.Deserialize(serializationStream) as List<SerializableKeyValuePair<K, V>>;
            return BuildDictionary(dictionaryItems);
        }

        private Dictionary<K, V> BuildDictionary(List<SerializableKeyValuePair<K, V>> dictionaryItems)
        {
            Dictionary<K, V> dictionary = new Dictionary<K, V>(dictionaryItems.Count);
            foreach (SerializableKeyValuePair<K, V> item in dictionaryItems)
            {
                dictionary.Add(item.Key, item.Value);
            }
            return dictionary;
        }
    }

    public class DictionarySerialiserMethods
    {
        // Code taken and modified from http://social.msdn.microsoft.com/Forums/en/netfxnetcom/thread/8b78fbe6-ee6a-4a00-85a2-e27318ccedb9
        // Convert the user dictionary to a byte array
        public byte[] ConvertUsersOnlineToByteArray(Dictionary<int, transferrableUserDetails> prUsersDict)
        {
            MemoryStream iMemoryStream = new MemoryStream();
            DictionarySerializer<int, transferrableUserDetails> iSerializer = new DictionarySerializer<int, transferrableUserDetails>();

            try
            {
                iSerializer.Serialize(prUsersDict, iMemoryStream);
                return iMemoryStream.ToArray();
            }
            catch (System.Exception ex)
            {
                return null;
            }
            finally
            {
                iMemoryStream.Close();
            }
        }

        // Convert the question dictionary to a byte array
        public byte[] ConvertQuestionDictToByteArray(Dictionary<int, question> prQuestionDict)
        {
            MemoryStream iMemoryStream = new MemoryStream();
            DictionarySerializer<int, question> iSerializer = new DictionarySerializer<int, question>();

            try
            {
                iSerializer.Serialize(prQuestionDict, iMemoryStream);
                return iMemoryStream.ToArray();
            }
            catch (System.Exception ex)
            {
                return null;
            }
            finally
            {
                iMemoryStream.Close();
            }
        }

        // Convert the question to a byte array
        public byte[] ConvertQuestionToByteArray(question prQuestion)
        {
            MemoryStream iMemoryStream = new MemoryStream();
            XmlTextWriter iXmlWriter = new XmlTextWriter(iMemoryStream, Encoding.UTF8);

            XmlSerializer iSerializer = new XmlSerializer(typeof(question));
            try
            {
                iSerializer.Serialize(iMemoryStream, prQuestion);
                return iMemoryStream.ToArray();
            }
            catch (System.Exception ex)
            {
                return null;
            }
            finally
            {
                iMemoryStream.Close();
            }
        }

        // Convert the ChatMessage to a byte array
        public byte[] ConvertChatMessageToByteArray(ChatMessage prChatMessage)
        {
            MemoryStream iMemoryStream = new MemoryStream();
            XmlTextWriter iXmlWriter = new XmlTextWriter(iMemoryStream, Encoding.UTF8);

            XmlSerializer iSerializer = new XmlSerializer(typeof(ChatMessage));
            try
            {
                iSerializer.Serialize(iMemoryStream, prChatMessage);
                return iMemoryStream.ToArray();
            }
            catch (System.Exception ex)
            {
                return null;
            }
            finally
            {
                iMemoryStream.Close();
            }
        }

        // Convert the transferrableUserDetails to a byte array
        public byte[] ConvertUserDetailsToByteArray(transferrableUserDetails prUserDetails)
        {
            MemoryStream iMemoryStream = new MemoryStream();
            XmlTextWriter iXmlWriter = new XmlTextWriter(iMemoryStream, Encoding.UTF8);

            XmlSerializer iSerializer = new XmlSerializer(typeof(transferrableUserDetails));
            try
            {
                iSerializer.Serialize(iMemoryStream, prUserDetails);
                return iMemoryStream.ToArray();
            }
            catch (System.Exception ex)
            {
                return null;
            }
            finally
            {
                iMemoryStream.Close();
            }
        }

        // Deserialize the user dictionary
        public Dictionary<int, transferrableUserDetails> ConvertByteArrayToUserDict(byte[] prData)
        {
            string iTest = prData.ToString();
            MemoryStream iMemoryStream = new MemoryStream(prData);
            DictionarySerializer<int, transferrableUserDetails> iSerializer = new DictionarySerializer<int, transferrableUserDetails>();

            return (Dictionary<int, transferrableUserDetails>)iSerializer.Deserialize(iMemoryStream);
        }

        // Deserialize the question dictionary
        public Dictionary<int, question> ConvertByteArrayToQuestionDict(byte[] prData)
        {
            MemoryStream iMemoryStream = new MemoryStream(prData);
            DictionarySerializer<int, question> iSerializer = new DictionarySerializer<int, question>();

            return (Dictionary<int, question>)iSerializer.Deserialize(iMemoryStream);
        }

        // Deserialize the answer
        public Answer ConvertStringToAnswer(string prData)
        {
            MemoryStream iMemoryStream = new MemoryStream(StringToUTF8ByteArray(prData));
            XmlSerializer iSerializer = new XmlSerializer(typeof(Answer));
            XmlTextWriter iXmlWriter = new XmlTextWriter(iMemoryStream, Encoding.UTF8);
            return (Answer)iSerializer.Deserialize(iMemoryStream);
        }

        // Deserialize the chat message
        public ChatMessage ConvertStringToChatMessage(string prData)
        {
            MemoryStream iMemoryStream = new MemoryStream(StringToUTF8ByteArray(prData));
            XmlSerializer iSerializer = new XmlSerializer(typeof(ChatMessage));
            XmlTextWriter iXmlWriter = new XmlTextWriter(iMemoryStream, Encoding.UTF8);
            return (ChatMessage)iSerializer.Deserialize(iMemoryStream);
        }

        private Byte[] StringToUTF8ByteArray(String pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        } 
    }
}
