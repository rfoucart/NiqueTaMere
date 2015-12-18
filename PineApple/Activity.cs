using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PineApple
{
    class Activity
    {
        private static int _referenceNumber=0;
        private int _number;
        private string _description;
        private int _indexOfGenericType;
        private int _indexOfType;
        private List<int> _astronautes;
        private int _location;
        private bool _externExperiment;
        private bool _scaph;
        private bool _spaceVehicle;
        private MDate _startDate;
        private MDate _endDate;

        /// <summary>
        /// 
        /// </summary>

        public Activity()
        {
            _number = _referenceNumber;
            _referenceNumber++;
            _description = string.Empty;
            _indexOfGenericType = 0;
            _indexOfType = 0;
            _location = 0;
            _externExperiment = false;
            _spaceVehicle = false;
            _scaph = false;
            _astronautes = new List<int>(0);
            MDate d = new MDate(1, 0, 0);
            _startDate = d;
            _endDate = d;
        }
        public Activity(string description, int genericType ,int type, int location, List<int> astronautes, bool externMission, bool spaceVehicle, bool scaph, MDate startDate, MDate endDate)
        {
            _number = _referenceNumber;
            _referenceNumber++;
            _description = description;
            _indexOfGenericType = genericType;
            _indexOfType = type;
            _location = location;
            _externExperiment = externMission;
            _spaceVehicle = spaceVehicle;
            _scaph = scaph;
            _astronautes = astronautes;
            _startDate = startDate;
            _endDate = endDate;
        }
        public int getNumber()
        {
            return _number;
        }
        public bool getSpaceVehicule()
        {
            return _spaceVehicle;
        }
        public bool getExternBool()
        {
            return _externExperiment;
        }

        public string getDescription()
        {
            return _description;
        }
        public int getLocation()
        {
            return _location;
        }
        public List<int> getAstronautes()
        {
            return _astronautes;
        }
        public int getDay()
        {
            return _startDate.getDay();
        }
        public MDate getStartDate()
        {
            return _startDate;
        }
        public MDate getEndDate()
        {
            return _endDate;
        }
        public int getIndexOfGenericType()
        {
            return _indexOfGenericType;
        }
        public bool getScaph()
        {
            return _scaph;
        }
        public int getIndexOfType()
        {
            return _indexOfType;
        }
        public static void setRefNumber(int refNumber)
        {
            if(_referenceNumber==0)
            {
                _referenceNumber = refNumber;
            }
        }
        public void WriteXML(XmlDocument xmlDoc, XmlNode rootNode)
        {
            XmlNode ReferenceNumber = xmlDoc.CreateElement("ReferenceNumber");
            ReferenceNumber.InnerText = _referenceNumber.ToString();
            rootNode.AppendChild(ReferenceNumber);

            XmlNode activity = xmlDoc.CreateElement("Activity");
            rootNode.AppendChild(activity);
          
            XmlNode Number = xmlDoc.CreateElement("Number");
            Number.InnerText = this._number.ToString();
            activity.AppendChild(Number);

            XmlNode Description = xmlDoc.CreateElement("Description");
            Description.InnerText = this._description.ToString();
            activity.AppendChild(Description);

            XmlNode GenericType = xmlDoc.CreateElement("GenerycType");
            GenericType.InnerText = this._indexOfGenericType.ToString();
            activity.AppendChild(GenericType);

            XmlNode Type = xmlDoc.CreateElement("Type");
            Type.InnerText = this._indexOfType.ToString();
            activity.AppendChild(Type);

            string astro = "";
            int c = 0;
            foreach (int i in this._astronautes)
            {
                if (c < this._astronautes.Count() - 1)
                {
                    astro += i.ToString() + " ";
                    c++;
                }
                else if (c == this._astronautes.Count() - 1)
                {
                    astro += i.ToString();
                }
            }
            XmlNode Astronautes = xmlDoc.CreateElement("Astronautes");
            Astronautes.InnerText = astro;
            activity.AppendChild(Astronautes);

            XmlNode Location = xmlDoc.CreateElement("Location");
            Location.InnerText = this._location.ToString();
            activity.AppendChild(Location);

            XmlNode ExternMission = xmlDoc.CreateElement("ExternMission");
            ExternMission.InnerText = this._externExperiment.ToString();
            activity.AppendChild(ExternMission);

            XmlNode SpaceVehicle = xmlDoc.CreateElement("SpaceVehicle");
            SpaceVehicle.InnerText = this._spaceVehicle.ToString();
            activity.AppendChild(SpaceVehicle);

            XmlNode Scaph = xmlDoc.CreateElement("Scaph");
            Scaph.InnerText = this._scaph.ToString();
            activity.AppendChild(Scaph);


            XmlNode StartDate = xmlDoc.CreateElement("StartDate");
            StartDate.InnerText = this._startDate.ToString();
            activity.AppendChild(StartDate);

            XmlNode EndDate = xmlDoc.CreateElement("EndDate");
            EndDate.InnerText = this._endDate.ToString();
            activity.AppendChild(EndDate);

            //___________________________________________________________
        }
        public void updateActivity(Activity a)
        {
            _description = a._description;
            _indexOfGenericType = a._indexOfGenericType;
            _indexOfType = a._indexOfType;
            _location = a._location;
            _externExperiment = a._externExperiment;
            _spaceVehicle = a._spaceVehicle;
            _scaph = a._scaph;
            _astronautes = a._astronautes;
            _startDate = a._startDate;
            _endDate = a._endDate;
        }
    }
}
