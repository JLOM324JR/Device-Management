using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.Controllers
{
    public class ContactModel
    {
        public string description { get; set; }

        //Luk_Application
        public string applicationid { get; set; }
        public string name { get; set; }
        public string version { get; set; }
        
        //Luk_Communication_Media_Type
        public string communication { get; set; }

        //LUK_contact_package
        public string no { get; set; }
        public string contactnumber { get; set; }
        public string packageid { get; set; }

        //LUK_department
        public string departmentid { get; set; }


        //Add new device
        public string InsertIMEI { get; set; }
        public string InsertSerialNumber { get; set; }
        public string InsertFirmware { get; set; }
        public string InsertGateWayID { get; set; }
        public string InsertAppID { get; set; }
        public string CmTypeId { get; set; }
        public string statusDevice { get; set; }
        public string contractnumber { get; set; }

        //View
        public string IMEI { get; set; }
        public string SerialNumber { get; set; }
        public string Manufacturer { get; set; }
        public string Firmware { get; set; }
        public string GateWay { get; set; }
        public string AppPlatID { get; set; }






        public object InsertManufacturer { get; set; }
        public object InsertAppPlatID { get; set; }
        public object InsertStatusID { get; set; }
        public object InsertIoTPlatID { get; set; }
        public object InsertCmTypeID { get; set; }
        public object InsertModelID { get; set; }
        public string Model { get; set; }





        public string IoTPlatID { get; set; }
        public string StatusID { get; set; }
        
        public string ModelID { get; set; }
        
       
        public string NameS { get;  set; }
        public string NameA { get; set; }
        public string NameI { get;  set; }
        public string moName { get;  set; }
        public string moBrand { get; set; }
        public string medName { get; set; }
        
        public object InsertEmail { get; set; }
        public object InsertName { get;  set; }
    }
}