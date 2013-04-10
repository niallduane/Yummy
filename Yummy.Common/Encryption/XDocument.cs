using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.Xml.Linq;
using System.Security.Cryptography;

namespace Yummy.Common.Encryption
{
    public class XDocument : System.Xml.Linq.XDocument
    {
        const string _elementName = "encryption",
             _idkey = "id",
             _hashkey = "hash";

        public XDocument() : base() { }
        public XDocument(params object[] content) : base(content) { }
        public XDocument(System.Xml.Linq.XDocument other) : base(other) { }
        public XDocument(XDeclaration declaration, params object[] content) : base(declaration, content) { }

        public void Encrypt(string userId, string secret)
        {
            this.AddEncryptionNode(userId, this.HashBeforeEncryption(secret).Encrypt(""));
        }

        public bool IsAuthentic(string secret)
        {
            String hash1 = this.HashBeforeEncryption(secret).Encrypt("");

            if (EncrytionNode.Element(_hashkey).Value.Equals(hash1))
            {
                return true;
            }

            return false;
        }

        private void AddEncryptionNode(string id, string hash)
        {
            if (EncrytionNode == null)
            {
                XElement element = new XElement(_elementName);
                element.Add(new XElement(_idkey, id));
                element.Add(new XElement(_hashkey, hash));

                this.Descendants().FirstOrDefault<XElement>().Add(element);
            }
            else
            {
                var element = this.EncrytionNode;
                element.Element(_idkey).Value = id;
                element.Element(_hashkey).Value = hash;
            }
        }

        public XElement EncrytionNode
        {
            get
            {
                return (from x in this.Descendants(_elementName)
                        select x).FirstOrDefault<XElement>();
            }
        }

        private string HashBeforeEncryption(string secret)
        {
            String hashdata = "";
            foreach (XElement e in this.Descendants().Where(x => x.Name != _elementName))
            {
                if (!e.HasElements)
                {
                    if (String.IsNullOrEmpty(hashdata))
                    {
                        hashdata = e.Value;
                    }
                    else
                    {
                        hashdata = hashdata + "." + e.Value;
                    }
                }
            }
            hashdata = hashdata + "." + secret;

            return hashdata;
        }
    }
}
