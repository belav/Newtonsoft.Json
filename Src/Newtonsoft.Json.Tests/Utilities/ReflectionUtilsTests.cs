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

#if !(PORTABLE || DNXCORE50 || PORTABLE40)
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
#if DNXCORE50
using Xunit;
using Test = Xunit.FactAttribute;
using Assert = Newtonsoft.Json.Tests.XUnitAssert;
#else
using NUnit.Framework;
#endif
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Tests.Utilities
{
    [TestFixture]
    public class ReflectionUtilsTests : TestFixtureBase
    {
        [Test]
        public void GetTypeNameSimpleForGenericTypes()
        {
            string typeName;

            typeName = ReflectionUtils.GetTypeName(
                typeof(IList<Type>),
                TypeNameAssemblyFormatHandling.Simple,
                null
            );
            Assert.AreEqual(
                "System.Collections.Generic.IList`1[[System.Type, mscorlib]], mscorlib",
                typeName
            );

            typeName = ReflectionUtils.GetTypeName(
                typeof(IDictionary<IList<Type>, IList<Type>>),
                TypeNameAssemblyFormatHandling.Simple,
                null
            );
            Assert.AreEqual(
                "System.Collections.Generic.IDictionary`2[[System.Collections.Generic.IList`1[[System.Type, mscorlib]], mscorlib],[System.Collections.Generic.IList`1[[System.Type, mscorlib]], mscorlib]], mscorlib",
                typeName
            );

            typeName = ReflectionUtils.GetTypeName(
                typeof(IList<>),
                TypeNameAssemblyFormatHandling.Simple,
                null
            );
            Assert.AreEqual("System.Collections.Generic.IList`1, mscorlib", typeName);

            typeName = ReflectionUtils.GetTypeName(
                typeof(IDictionary<,>),
                TypeNameAssemblyFormatHandling.Simple,
                null
            );
            Assert.AreEqual("System.Collections.Generic.IDictionary`2, mscorlib", typeName);
        }
    }
}

#endif
