/*
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
 
namespace Tests
{
    public class JsonReaderTest
    {
        // create instance for test
        JsonReader jsonReader;
 
        //JsonLessonList jsonLessonList;
 
        [SetUp]
        public void Setup()
        {
            jsonReader = new GameObject().AddComponent<JsonReader>();
            jsonReader.jsonFile = Resources.Load("lesson-test") as TextAsset;
        }
 
        [TearDown]
        public void Teardown()
        {
            Object.Destroy(jsonReader);
        }
 
        // Verify class exists
        [Test]
        public void JsonReaderClassExists()
        {
            Assert.IsNotNull(jsonReader);
            Assert.IsNotNull(jsonReader.jsonFile);
        }
 
        [UnityTest]
        public IEnumerator TestStart()
        {
            Assert.Pass("PASS, ignore stack trace");
 
            yield return null;
        }
 
        [Test]
        public void TestFileParsesOkTest()
        {
            JsonLessonList testLessonList = jsonReader.LoadLessonFromFile();
 
            // NullReferenceException here is often caused by an error in the test file itself,
            // check that field names match the structure
            Assert.IsNotNull(testLessonList);
        }
 
    }
}
*/