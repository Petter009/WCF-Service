using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WebServiceWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILogReaderService" in both code and config file together.
    [ServiceContract]
    public interface ILogReaderService
    {

        [OperationContract]
        List<Incident> ReadLog();

        [OperationContract]
        List<Incident> SortListByName(string name);

        [OperationContract]
        List<Incident> SortListByAlarm(string alarm);

        [OperationContract]
        List<Incident> SortListByNameAndAlarm(string name, string alarm);
    }
    // [OperationContract]
    //IEnumerable<string> GetIncidentsInTheLastFiveSeconds();

    // TODO: Add your service operations here


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "PWebServices.ContractType".
    [DataContract]
    public class Incident
    {
        [DataMember]
        public DateTime Time { get; set; }

        [DataMember]
        public string ID { get; set; }

        [DataMember]
        public string Alarm { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Department { get; set; }

        [DataMember]
        public string House { get; set; }

        [DataMember]
        public DateTime TimeRegistered { get; set; }

        [DataMember]
        public string RegisteredTo { get; set; }

        public Incident(DateTime time, string id, string alarm, string name, string department, string house, DateTime timeregistered, string registeredto)
        {
            this.Time = time;
            this.ID = id;
            this.Alarm = alarm;
            this.Name = name;
            this.Department = department;
            this.House = house;
            this.TimeRegistered = timeregistered;
            this.RegisteredTo = registeredto;
        }
        public Incident()
        {

        }
    }
}
