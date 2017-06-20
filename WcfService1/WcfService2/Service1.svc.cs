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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {

        Trips trips = new Trips();
        List<Trip> allTrips = new List<Trip>();
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public Int32 add(Int32 value, Int32 value2)
        {
            return value + value2;
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public List<string> getTrip(string start, string stop)
        {
            if (start == null || stop == null || start == "" || stop == "")
            {
                throw new FaultException("Invalid start or end location");
            }
            List<Trip> list = ParseCVS();
            List<string> outputList = new List<string>();
            List<string> temp = new List<string>();

            foreach (Trip record in list)
            {
                if (record.StartPoint.Equals(start) && record.EndPoint.Equals(stop) && DateTime.Compare(Convert.ToDateTime(record.StartTime), Convert.ToDateTime(record.EndTime)) <= 0)
                {
                    outputList.Add(toStringTrainData(record));
                }
            }
            temp = findIndircet(start, stop);
            foreach (string t in temp)
            {
                outputList.Add(t);
            }
            try
            {
                if (!outputList.Any())
                {
                    outputList.Add("Brak Połączenia");
                    // throw new FaultException("Brak połączenia");
                }
            }
            catch (FaultException e)
            {
                outputList.Add("Brak połączeń");
            }



            return outputList;


        }


        private List<string> findIndircetConnectionsWithTime(string start, string end, string startTime, DateTime date, DateTime dateEnd)
        {

            List<Trip> startTemp = new List<Trip>();
            List<Trip> temp = new List<Trip>();
            foreach (Trip trip in allTrips)
            {
                if (trip.EndPoint == end)
                {
                    temp.Add(trip);
                }
                else if (trip.StartPoint == start)
                {
                    startTemp.Add(trip);
                }
            }
            List<string> indirectRoutes = new List<string>();
            int a = int.Parse(startTime);

            foreach (Trip startT in startTemp)
            {
                foreach (Trip endT in temp)
                {

                    if (startT.EndPoint == endT.StartPoint && DateTime.Compare(Convert.ToDateTime(startT.StartTime), Convert.ToDateTime(endT.EndTime)) <= 0 && DateTime.Compare(Convert.ToDateTime(endT.EndTime), dateEnd) <= 0)
                    {
                        string res = startT.StartPoint + " " + startT.StartTime + " " + startT.EndPoint + " " + startT.EndTime + " " + endT.EndPoint + " " + endT.EndTime;

                        indirectRoutes.Add(res);


                    }


                }
            }
            List<string> indirectConnections = new List<string>();
            int index = 1;
            foreach (String rout in indirectRoutes)
            {
                indirectConnections.Add(index + rout);
                index++;
            }

            return indirectConnections;
        }

        private List<string> findIndircet(string start, string end)
        {
            List<Trip> startTemp = new List<Trip>();
            List<Trip> temp = new List<Trip>();
            foreach (Trip trip in allTrips)
            {
                if (trip.EndPoint == end)
                {
                    temp.Add(trip);
                }
                else if (trip.StartPoint == start)
                {
                    startTemp.Add(trip);
                }
            }
            List<string> indirectRoutes = new List<string>();

            foreach (Trip startT in startTemp)
            {
                foreach (Trip endT in temp)
                {
                    if (startT.EndPoint == endT.StartPoint && DateTime.Compare(Convert.ToDateTime(startT.StartTime), Convert.ToDateTime(endT.EndTime)) <= 0)
                    {
                        string res = startT.StartPoint + " " + startT.StartTime + " " + startT.EndPoint + " " + startT.EndTime + " " + endT.EndPoint + " " + endT.EndTime;

                        indirectRoutes.Add(res);


                    }

                }
            }
            List<string> indirectConnections = new List<string>();
            int index = 1;
            foreach (String rout in indirectRoutes)
            {
                indirectConnections.Add(index + rout);
                index++;
            }

            return indirectConnections;
        }
        public string toStringTrainData(Trip record)
        {
            return record.StartPoint.ToString() + " " + record.StartTime.ToString() + " " +
                            record.EndPoint.ToString() + " " + record.EndTime.ToString();
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




        public List<string> getTripWithTime(string start, string stop, string timeS, DateTime date, DateTime dateEnd)
        {

            List<Trip> list = ParseCVS();
            List<string> outputList = new List<string>();
            List<string> temp = new List<string>();

            foreach (Trip record in list)
            {
                var splitedLine = record.StartTime.Split(' ');
                string time = splitedLine[1];
                var splitedLine2 = time.Split(':');
                string checktime = splitedLine2[0];
                int a = int.Parse(checktime);
                int b = int.Parse(timeS);

                if (DateTime.Compare(Convert.ToDateTime(record.StartTime), date) >= 0 && DateTime.Compare(Convert.ToDateTime(record.EndTime), dateEnd) <= 0)
                {
                    if (record.StartPoint.Equals(start) && record.EndPoint.Equals(stop))
                    {
                        outputList.Add(toStringTrainData(record));
                    }
                }

            }



            temp = findIndircetConnectionsWithTime(start, stop, timeS, date, dateEnd);
            foreach (string t in temp)
            {
                outputList.Add(t);
            }

            if (!outputList.Any())
            {
                outputList.Add("Brak");
            }
            return outputList;
        }



    }
}
