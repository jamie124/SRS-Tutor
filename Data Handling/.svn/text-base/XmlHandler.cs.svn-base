using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Security.Cryptography;

namespace TutorClient
{
    public class XmlHandler
    {
        // Encrypts a byte string using Rijndael
        // Encryption functions copied and modified from http://www.codeproject.com/KB/security/DotNetCrypto.aspx
        private byte[] Encrypt(byte[] prData, byte[] prKey, byte[] prIV)
        {
            // Create a MemoryStream to accept the encrypted bytes 

            MemoryStream iStream = new MemoryStream();

            Rijndael iRijndael = Rijndael.Create();

            iRijndael.Key = prKey;
            iRijndael.IV = prIV;

            CryptoStream cs = new CryptoStream(iStream,
               iRijndael.CreateEncryptor(), CryptoStreamMode.Write);


            cs.Write(prData, 0, prData.Length);
            cs.Close();

            byte[] encryptedData = iStream.ToArray();

            return encryptedData;
        }

        public string Encrypt(string prStringToEncrypt, string prPassword)
        {

            byte[] iStringBytes = System.Text.Encoding.Unicode.GetBytes(prStringToEncrypt);

            PasswordDeriveBytes iPDB = new PasswordDeriveBytes(prPassword,
                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 
            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

            byte[] iEncryptedData = Encrypt(iStringBytes,
                     iPDB.GetBytes(32), iPDB.GetBytes(16));

            return Convert.ToBase64String(iEncryptedData);
        }

        // Save user setting to file
        public void SaveUserSettings(Settings prSettings)
        {
            // Create a new file
            XmlTextWriter iTextWriter = new XmlTextWriter("tutorSettings.xml", null);
            // Opens the document
            iTextWriter.WriteStartDocument();

            // Comments
            iTextWriter.WriteComment("Users Settings for Tutor's SRS Client");

            // Write first element
            iTextWriter.WriteStartElement("Tutor");
            iTextWriter.WriteStartElement("r", "RECORD", "urn:record");

            // Username
            iTextWriter.WriteStartElement("UserName", "");
            iTextWriter.WriteString(prSettings.UserName);
            iTextWriter.WriteEndElement();

            // Server Address
            iTextWriter.WriteStartElement("ServerAddresses", "");
            if (prSettings.ServerIPs != null)
            {
                foreach (string iIPAddress in prSettings.ServerIPs)
                {
                    iTextWriter.WriteStartElement("IPAddress", "");
                    iTextWriter.WriteString(iIPAddress);
                    iTextWriter.WriteEndElement();
                }
            }
            iTextWriter.WriteEndElement();
            
            // Server Port
            iTextWriter.WriteStartElement("ServerPort", "");
            iTextWriter.WriteString(prSettings.ServerPort);
            iTextWriter.WriteEndElement();

            // AutoLogin
            iTextWriter.WriteStartElement("AutoLogin", "");
            iTextWriter.WriteString(prSettings.AutoLogin.ToString());
            iTextWriter.WriteEndElement();

            // Ends the document.
            iTextWriter.WriteEndDocument();

            // Close the writer
            iTextWriter.Close();
        }

        // Attempt to load the users settings
        public bool LoadUserSettings(string prFilename, out Settings prSettings)
        {
            int iCurrIP = 0;

            prSettings = new Settings();
            if (File.Exists(prFilename))
            {
                XmlDocument iXmlDocument = new XmlDocument();

                iXmlDocument.Load(prFilename);

                //Make sure document has been loaded
                if (iXmlDocument != null)
                {
                    XmlNodeList iStudents = iXmlDocument.ChildNodes;
                    XmlNodeList iStudentDetails;
                    foreach (XmlNode iNode in iStudents)
                    {
                        if (iNode.InnerText != "version=\"1.0\"")
                        {
                            iStudentDetails = iNode.ChildNodes;
                            foreach (XmlNode iStudent in iStudentDetails)
                            {
                                prSettings.UserName = iStudent.ChildNodes[0].InnerText;
                                prSettings.ServerIPs = new string[iStudent.ChildNodes[1].ChildNodes.Count];
                                foreach (XmlNode iServerIP in iStudent.ChildNodes[1])
                                {
                                    prSettings.ServerIPs[iCurrIP] = iServerIP.InnerText;
                                    iCurrIP++;
                                }
                                prSettings.ServerPort = iStudent.ChildNodes[2].InnerText;
                                if (iStudent.ChildNodes[3].InnerText == "True")
                                {
                                    prSettings.AutoLogin = true;
                                }
                                else
                                    prSettings.AutoLogin = false;
                            }
                        }
                    }
                }
                return true;
            }
            return false;
        }
    }
}
