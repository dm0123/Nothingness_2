using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Nothingness_2
{
    public class XMLWorker
    {
        private static XMLWorker inst;
        private XDocument doc;
        private string fileName = "scores.xml";
        private XMLWorker()
        {
            try
            {
                doc = XDocument.Load(fileName);
            }
            catch (Exception e)
            {
                doc = new XDocument(new XElement("Scorici"));
            }
        }

        public static XMLWorker Instance
        {
            get
            {
                if (inst == null)
                {
                    inst = new XMLWorker();
                }
                return inst;

            }
        }

        public void Save(string _name, string _score)
        {
            IEnumerable<XElement> name;
            try
            {
                name = from t in doc.Root.Elements("Score")
                           where t.Element("Name").Value == _name
                           select t;
                if (name.Count() > 0)
                {
                    foreach (var val in name)
                    {
                        val.Element("Value").Value = _score;
                    }
                }
            }
            catch(System.NullReferenceException)
            {
                //doc.Root.Add(new XElement("Score", new XElement("Name", _name), new XElement("Value", _score)));
            }
            doc.Root.Add(new XElement("Score", new XElement("Name", _name), new XElement("Value", _score)));
            doc.Save(fileName);
        }

        public Dictionary<string, string>/*IEnumerable<XElement>*/ Load()
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            var provisionalRes = from t in doc.Root.Elements("Score")
                                 orderby Int32.Parse(t.Element("Value").Value) descending
                                 select t;
            foreach (var el in provisionalRes)
            {
                res[el.Element("Name").Value] = el.Element("Value").Value;
            }
            //return provisionalRes;
            return res;
        }


    }
}
