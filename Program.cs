using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using APforZusConsole.ServiceReference1;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using RestSharp;
using System.Xml.Linq;
using System.Net;
using Eco.Wcf.Server;

namespace APforZusConsole
{
    class Program
    {

        static void Main(string[] args)
        {
            //string www=pobierzOswiadczenie().ToString();

            WSHttpBinding myBinding = new WSHttpBinding();
            myBinding.Security.Mode = SecurityMode.Transport;
            myBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            myBinding.Security.Message.NegotiateServiceCredential = false;
            myBinding.Security.Message.EstablishSecurityContext = false;
            ServicePointManager.SecurityProtocol= SecurityProtocolType.Tls12;

            myBinding.MessageEncoding = WSMessageEncoding.Mtom;
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;


            EndpointAddress adress = new EndpointAddress("https://193.105.143.152:8001/ws/zus.channel.gabinetoweV2:zla");


            ServiceReference1.zla_PortTypeClient zla_Port = new zla_PortTypeClient(myBinding, adress);
            zla_Port.ClientCredentials.UserName.UserName = "ezla_ag";
            zla_Port.ClientCredentials.UserName.Password = "ezla_ag";
            var t = zla_Port.pobierzOswiadczenieAsync();
            
            Console.WriteLine(t.Result);
            //docTypeRef_Rezultat xxx = null;
            //zla_Port.zalogujPodpisem(www, MetodaUwierzytelnienia.ePuap, "", out xxx);


            Console.ReadKey();

        }
    }
}
