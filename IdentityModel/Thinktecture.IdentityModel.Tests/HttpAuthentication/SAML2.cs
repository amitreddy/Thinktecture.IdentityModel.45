﻿using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class SAML2
    {
        [TestMethod]
        public void ValidSaml2Token()
        {
            var token = Factory.CreateSaml2Token("test");
            
            var client = new HttpClient(Factory.GetDefaultServer());
            var request = Factory.GetDefaultRequest();
            request.Headers.Authorization = new AuthenticationHeaderValue("Saml2", token);

            var response = client.SendAsync(request).Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);  
        }

        [TestMethod]
        public void EmptyCredentials()
        {
            var client = new HttpClient(Factory.GetDefaultServer());
            var request = Factory.GetDefaultRequest();
            request.Headers.Authorization = new AuthenticationHeaderValue("Saml2");

            var response = client.SendAsync(request).Result;
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [TestMethod]
        public void MalformedCredentials()
        {
            var client = new HttpClient(Factory.GetDefaultServer());
            var request = Factory.GetDefaultRequest();
            request.Headers.Authorization = new AuthenticationHeaderValue("Saml2", "invalid");

            var response = client.SendAsync(request).Result;
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
