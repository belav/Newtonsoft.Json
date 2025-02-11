﻿#region License
// Copyright (c) 2007 James Newton-King
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

using Newtonsoft.Json.Linq;
#if DNXCORE50
using Xunit;
using Test = Xunit.FactAttribute;
using Assert = Newtonsoft.Json.Tests.XUnitAssert;
#else
using NUnit.Framework;
#endif
using System;
using System.Collections.Generic;
#if NET20
using Newtonsoft.Json.Utilities.LinqBridge;
#else
using System.Linq;
#endif
using System.Text;

namespace Newtonsoft.Json.Tests.Documentation.Samples.JsonPath
{
    [TestFixture]
    public class QueryJsonSelectTokenJsonPath : TestFixtureBase
    {
        [Test]
        public void Example()
        {
            #region Usage
            JObject o = JObject.Parse(
                @"{
              'Stores': [
                'Lambton Quay',
                'Willis Street'
              ],
              'Manufacturers': [
                {
                  'Name': 'Acme Co',
                  'Products': [
                    {
                      'Name': 'Anvil',
                      'Price': 50
                    }
                  ]
                },
                {
                  'Name': 'Contoso',
                  'Products': [
                    {
                      'Name': 'Elbow Grease',
                      'Price': 99.95
                    },
                    {
                      'Name': 'Headlight Fluid',
                      'Price': 4
                    }
                  ]
                }
              ]
            }"
            );

            // manufacturer with the name 'Acme Co'
            JToken acme = o.SelectToken("$.Manufacturers[?(@.Name == 'Acme Co')]");

            Console.WriteLine(acme);
            // { "Name": "Acme Co", Products: [{ "Name": "Anvil", "Price": 50 }] }

            // name of all products priced 50 and above
            IEnumerable<JToken> pricyProducts = o.SelectTokens(
                "$..Products[?(@.Price >= 50)].Name"
            );

            foreach (JToken item in pricyProducts)
            {
                Console.WriteLine(item);
            }
            // Anvil
            // Elbow Grease
            #endregion

            StringAssert.AreEqual(
                @"{
  ""Name"": ""Acme Co"",
  ""Products"": [
    {
      ""Name"": ""Anvil"",
      ""Price"": 50
    }
  ]
}",
                acme.ToString()
            );

            Assert.AreEqual("Anvil", (string)pricyProducts.ElementAt(0));
            Assert.AreEqual("Elbow Grease", (string)pricyProducts.ElementAt(1));
        }
    }
}
