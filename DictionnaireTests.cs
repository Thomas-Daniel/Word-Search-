using Microsoft.VisualStudio.TestTools.UnitTesting;
using PROJET_ALGO;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameTests
{
    [TestClass]
    public class DictionnaireTests
    {
        [TestMethod]
        public void DictionnaireGetSet()
        {
            Dictionnaire test = new Dictionnaire("FR", 5);
            Assert.AreEqual("FR", test.Langue, "Langue is incorrect");
            Assert.AreEqual(5, test.TailleMot, "TailleMot is incorrect");

        }

        [TestMethod]
        public void DictionnaireWordList()
        {
            Dictionnaire test = new Dictionnaire("FR", 2);
            List<string> wordList = test.wordList("MotsPossiblesFR.txt", 2);
            Assert.AreEqual(75, wordList.Count, "wordList.Count is incorrect");
            Assert.AreEqual("AA", wordList[0], "wordList[0] is different to AA");
            Assert.AreEqual("XI", wordList[74], "wordList[0] is different to XI");

            Assert.AreEqual(75, test.ListeMot.Count, "wordList.Count is incorrect");
            Assert.AreEqual("AA", test.ListeMot[0], "wordList[0] is different to AA");
            Assert.AreEqual("XI", test.ListeMot[74], "wordList[0] is different to XI");

        }

        [TestMethod]
        public void DictionnaireToString()
        {
            Dictionnaire test = new Dictionnaire("FR", 2);
            Assert.AreEqual("Longueur des mots : 2\nLangue : Français", test.ToString(), "ToString string is incorrect");

        }

        [TestMethod]
        public void DictionnaireRechDichoRecursif()
        {
            Dictionnaire test = new Dictionnaire("FR", 5);
            Assert.IsTrue(test.RechDichoRecursif("MATHS", test.ListeMot.Count), "RechDichoRecursif didn't found the specify word");
            Assert.IsTrue(test.RechDichoRecursif("OpErA", test.ListeMot.Count), "RechDichoRecursif didn't found the specify word");
            Assert.IsTrue(test.RechDichoRecursif("halle", test.ListeMot.Count), "RechDichoRecursif didn't found the specify word");
            Assert.IsFalse(test.RechDichoRecursif("AAAAA", test.ListeMot.Count), "RechDichoRecursif didn't found the specify word");
            Assert.IsFalse(test.RechDichoRecursif("HALLES", test.ListeMot.Count), "RechDichoRecursif didn't found the specify word");

        }
    }
}