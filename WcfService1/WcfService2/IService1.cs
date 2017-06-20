using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService2
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        Int32 add(Int32 value, Int32 value2);

        [OperationContract]
        List<string> getTrip(string start, string stop);

        [OperationContract]
        List<string> getTripWithTime(string start, string stop,string timeS,DateTime date, DateTime dateEnd);
        

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
    }

    public class Trip
    {
        string startPoint;
        string startTime;
        string endPoint;
        string endTime;
        public Trip(string TownA, string TownB, string TimeFrom, string TimeTo)
        {
            this.startPoint = TownA;
            this.endPoint = TownB;
            this.startTime = TimeFrom;
            this.endTime = TimeTo;
        }
        public string StartPoint
        {
            get
            {
                return startPoint;
            }
            set
            {
                startPoint = value;
            }
        }

        public string StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                startTime = value;
            }
        }

        public string EndPoint
        {
            get
            {
                return endPoint;
            }
            set
            {
                endPoint = value;
            }
        }

        public string EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                endTime = value;
            }
        }

    }
    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }

    [DataContract]
    public class Trips
    {
        List<Trip> allTrips = new List<Trip>();
        [DataMember]
        public List<Trip> AllTrips
        {
            get { return allTrips; }
            set { allTrips = value; }
        }

        public List<Trip> ParseCVS()
        {
            string[] csvLines = File.ReadAllLines(@"C:\Users\kriscool\Desktop\trains.csv");


            foreach (string line in csvLines.Skip(1))
            {
                var splitedLine = line.Split(',');
                allTrips.Add(
                    new Trip(
                        splitedLine[0],
                        splitedLine[2],
                        splitedLine[1],
                        splitedLine[3]
                        ));
            }
            return allTrips;
        }


    }
}
