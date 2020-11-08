using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SynoConnect.Translatte
{
    public class Translattor
    {
        private XDocument _textXML;
        public Translattor()
        {
            SwitchLng(CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
        }
        public string GetTranslatte(string path)
        {
            // For that you will need to add reference to System.Web.Helpers
            byte[] bytes = Encoding.Default.GetBytes(ElementAtPath(_textXML.Root, path.Replace(" ", "")).Value);
            return Encoding.UTF8.GetString(bytes);
        }
        private XElement ElementAtPath(XElement root, string path)
        {
            if (root == null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Invalid path.");
            }

            return root.XPathSelectElement(path);
        }
        public void SwitchLng(string lng)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "SynoConnect.Translatte.Ressource." + lng + ".string.json";
            if ( assembly.GetManifestResourceStream(resourceName) == null )
            {
                resourceName = "SynoConnect.Translatte.Ressource.fr.string.json";
            }
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                _textXML = JsonConvert.DeserializeXNode(reader.ReadToEnd());
            }
        }
    }
}
