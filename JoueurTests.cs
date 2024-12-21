using Microsoft.VisualStudio.TestTools.UnitTesting;
using PROJET_ALGO;
using System.Diagnostics;

namespace GameTests
{
    [TestClass]
    public class JoueurTests
    {
        [TestMethod]
        public void PlayerName()
        {
            Joueur test = new Joueur("test");
            Assert.AreEqual("test", test.Name, "Name is incorrect");

            test.Name = "testupdated";
            Assert.AreEqual("testupdated", test.Name, "Name is incorrect");
        }

        [TestMethod]
        public void PlayerAddScore()
        {
            Joueur test = new Joueur("test");
            test.Add_Score(15);
            Assert.AreEqual(15, test.Score, "Score is incorrect");
            test.Score = 40;
            Assert.AreEqual(40, test.Score, "Score is incorrect");

        }

        [TestMethod]
        public void PlayerFoundWords()
        {
            Joueur test = new Joueur("test");
            test.Add_Mot("PLAYER");
            test.Add_Mot("THIS_IS_A_TEST");
            Assert.AreEqual("PLAYER", test.MotsTrouves[0], "The Word found is incorrect");
            Assert.AreEqual("THIS_IS_A_TEST", test.MotsTrouves[1], "The Word found is incorrect");

        }

        [TestMethod]
        public void PlayerToString()
        {
            Joueur test = new Joueur("test");
            test.Add_Mot("PLAYER");
            test.Add_Mot("THIS_IS_A_TEST");
            test.Score = 70;
            string expectedOutput = "Joueur : test\nScore : 70\nListe des mots trouvés : PLAYER THIS_IS_A_TEST ";
            Assert.AreEqual(expectedOutput, test.ToString(), "The method ToString is incorrect");

        }

    }
}