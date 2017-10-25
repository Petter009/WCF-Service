using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WebServiceWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LogReaderService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select LogReaderService.svc or LogReaderService.svc.cs at the Solution Explorer and start debugging.
    public class LogReaderService : ILogReaderService
    {
        List<Incident> incidents;
        string path = @"c:\testDir\logfil.txt";
        FileSystemWatcher watcher;
        List<string> stringarray;


        public List<Incident> ReadLog()
        {
            watcher = new FileSystemWatcher();
            watcher.Path = @"c:\testDir\";
            watcher.Changed += OnChanged;
            watcher.EnableRaisingEvents = true;
            stringarray = new List<string>();

            incidents = new List<Incident>();
            var strm = File.OpenRead(path);
            StreamReader sr = new StreamReader(strm);
            for (int i = 0; i < 50; i++)
            {
                string str = sr.ReadLine();
                stringarray.Add(str);
            }

            foreach (var item in stringarray)
            {
                    string[] tempIncident = item.Split('\t');
                    Incident incident = new Incident();
                    incident.Time = DateTime.Parse(tempIncident[0]);
                    incident.ID = tempIncident[1];
                    incident.Alarm = tempIncident[2];
                    incident.Name = tempIncident[3];
                    incident.Department = tempIncident[4];
                    incident.House = tempIncident[5];
                    incident.TimeRegistered = DateTime.Parse(tempIncident[7]);
                    incident.RegisteredTo = tempIncident[8];

                    incidents.Add(incident);
                
            }
            return incidents;
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            StreamReader reader = new StreamReader(path);
            AddItemsTolist(incidents.Last<Incident>().Time.ToString(), reader);
            reader.Close();
        }

        public void AddItemsTolist(string lastItemAdded, StreamReader reader)
        {

            IEnumerable<string> list = reader.ReadUntil(lastItemAdded);



            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }

        }

        public List<Incident> SortListByAlarm(string alarm)
        {
            ReadLog();
            return incidents.FindAll(x => x.Alarm == alarm);
        }

        public List<Incident> SortListByName(string name)
        {
            ReadLog();
            try
            {
                List<Incident> list = incidents.FindAll(x => x.Name == name);
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Incident> SortListByNameAndAlarm(string name, string alarm)
        {
            ReadLog();
            return incidents.FindAll(x => x.Name == name && x.Alarm == alarm);
        }
    }
}
