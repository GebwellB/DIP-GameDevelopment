using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LambdaTests
{
    // A Test behaves as an ordinary method
    [TestCase(true, "Steve", "Steve")]
    [TestCase(true, "Geoff", "Geoff")]
    [TestCase(true, "Michael", "Michael")]
    [TestCase(true, "Tom", null)]
    [TestCase(false, "Steve", "Steve")]
    [TestCase(false, "Geoff", "Geoff")]
    [TestCase(false, "Michael", "Michael")]
    [TestCase(false, "Tom", null)]
    public void ListFind(bool useFunctionVersion, string searchName, string expectedResult)
    {
        List<string> names = new List<string>();

        names.Add("Geoff");
        names.Add("Michael");
        names.Add("Steve");

        string foundName = useFunctionVersion ?
            FindNameInList(names, searchName)
            : names.Find((name) => name == searchName);

        Assert.AreEqual(expectedResult, foundName);
    }

    string FindNameInList(List<string> list, string searchName)
    {
        return list.Find((name) => name == searchName);
    }
}
