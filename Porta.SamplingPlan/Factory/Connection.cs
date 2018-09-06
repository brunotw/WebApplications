using IW.Eims.SamplingPlan.App_Start;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Description;
using System.Web;

namespace Porta.SamplingPlan.Factory
{
    public static class Connection
    {
        private static IOrganizationService service;
        private static string connectionString;

        private static string environment = string.Empty;
        private static string crmUser = string.Empty;
        private static string crmPassword = string.Empty;
        private static string crmEndPoint = string.Empty;


        public static IOrganizationService Service
        {
            get
            {
                if (service == null && AppSetup.LoadMockData == false)
                {
                    Connect();
                }

                return service;
            }

            private set { service = value; }
        }

        public static string ConnectionString
        {
            get { return connectionString; }
            private set { connectionString = value; }
        }



        private static void Connect()
        {
            GetConfiguration();
            ConnectToMSCRM(crmUser, crmPassword, crmEndPoint);
        }

        private static void TestConnection()
        {
            try
            {
                var response = service.Execute(new WhoAmIRequest());
            }
            catch (Exception ex)
            {
                throw new Exception($"Something went wront while trying to connection to Dynamics 365. Error details: {ex.Message}");
            }
        }

        private static void GetConfiguration()
        {
            environment = ConfigurationManager.AppSettings["environment"];
            crmEndPoint = ConfigurationManager.AppSettings[environment];
            crmUser = ConfigurationManager.AppSettings["crmusername"];
            crmPassword = ConfigurationManager.AppSettings["crmpassword"];
        }

        private static void ConnectToMSCRM(string UserName, string Password, string SoapOrgServiceUri)
        {
            ClientCredentials credentials = new ClientCredentials();
            credentials.UserName.UserName = UserName;
            credentials.UserName.Password = Password;
            Uri serviceUri = new Uri(SoapOrgServiceUri);
            OrganizationServiceProxy proxy = new OrganizationServiceProxy(serviceUri, null, credentials, null);
            proxy.EnableProxyTypes();
            service = proxy;
            TestConnection();
        }

        public static IOrganizationService GetImpersonatedService(Guid systemId)
        {
            ClientCredentials credentials = new ClientCredentials();
            credentials.UserName.UserName = crmUser;
            credentials.UserName.Password = crmPassword;
            Uri serviceUri = new Uri(crmEndPoint);
            OrganizationServiceProxy proxy = new OrganizationServiceProxy(serviceUri, null, credentials, null);
            proxy.EnableProxyTypes();
            proxy.CallerId = systemId;
            TestConnection();

            return proxy;
        }
    }
}