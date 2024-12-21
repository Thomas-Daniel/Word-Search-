using PROJET_ALGO;
using System;
using System.IO;

namespace ProjetAlgo
{
    class Program
    {


        /// MÉTHODES


        /// Affiche l'accueil et récupère des données utiles au jeu (langue, vitesse, nombre de joueurs, mode de jeu)
        public static Tuple<string, string, int, int> Accueil()
        {
            string langue, modeJeu;
            int nbJoueurs, vitesse;
            /// Récupère le dossier dans lequel on se situe Directory.GetCurrentDirectory() va chercher le dossier 
            /// d'exécution qui est C:\dossierduProjet\bin\debug mais
            /// on veut juste C:\dossierduProjet. Pour cela on utilise deux fois l'attribut .Parent et la méthode 
            /// Directory.GetParent() qui récupèrent le dossier parent (à un niveau supérieur) pour remonter deux fois l'arborescence. 
            /// Puis on utilise .FullName pour obtenir le chemin "C:\dossierduProjet" sous la forme d'une string.
            string fullName = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\welcome.txt";
            /// Affiche le message de bienvenue contenu dans le fichier "welcome.txt"
            Console.WriteLine(File.ReadAllText(fullName));

            /// Affiche un message et demande à l'utilisateur une valeur avec une saisie sécurisée, pour la langue du jeu.
            string sLangue = "\nVeuillez entrer une langue de jeu\nFrançais : FR\nAnglais : EN\n";
            do
            {
                Console.WriteLine(sLangue);
                langue = Console.ReadLine().ToUpper();
            } while ((langue != "FR") && (langue != "EN"));

            if (langue.ToUpper() == "FR")
            {
                /// Affiche un message et demande à l'utilisateur une valeur avec une saisie sécurisée, pour la vitesse du jeu, le nombre de joueurs
                /// et le mode de jeu.
                string sVitesse = "\nVeuillez entrer une vitesse de jeu\n" +
                    "1 : Partie lente\n" +
                    "2 : Partie normale\n" +
                    "3 : Partie rapide\n";
                do
                {
                    Console.WriteLine(sVitesse);
                    vitesse = int.Parse(Console.ReadLine());
                } while ((vitesse != 1) && (vitesse != 2) && (vitesse != 3));

                string sJoueurs = "\nVeuillez entrer le nombre de joueurs\n";
                do
                {
                    Console.WriteLine(sJoueurs);
                    nbJoueurs = int.Parse(Console.ReadLine());
                } while (nbJoueurs <= 0);

                string sModeJeu = "\nSouhaitez-vous commencer une nouvelle partie " +
                    "ou continuer à partir d'une sauvegarde ? " +
                    "(Entrez respectivement \"N\" ou \"S\")\n";
                do
                {
                    Console.WriteLine(sModeJeu);
                    modeJeu = Console.ReadLine().ToUpper();
                } while ((modeJeu.ToUpper() != "N") && (modeJeu.ToUpper() != "S"));
            }
            else //La langue est forcément l'Anglais dans ce cas
            {
                /// Même chose mais en Anglais
                string sVitesse = "\nPlease enter a game speed\n" +
                    "1 : Slow game\n" +
                    "2 : Normal game\n" +
                    "3 : Fast game\n";
                do
                {
                    Console.WriteLine(sVitesse);
                    vitesse = int.Parse(Console.ReadLine());
                } while ((vitesse != 1) && (vitesse != 2) && (vitesse != 3));

                string sJoueurs = "\nPlease enter the number of players\n";
                do
                {
                    Console.WriteLine(sJoueurs);
                    nbJoueurs = int.Parse(Console.ReadLine());
                } while (nbJoueurs <= 0);

                string sModeJeu = "\nWould you like to start a new game " +
                    "or continue from a backup ? " +
                    "(Enter respectively \"N\" for New Game or \"B\" for Backup)\n";
                do
                {
                    Console.WriteLine(sModeJeu);
                    modeJeu = Console.ReadLine().ToUpper();
                } while ((modeJeu.ToUpper() != "N") && (modeJeu.ToUpper() != "B"));
                /// Si le mode de jeu est B pour Backup en Anglais alors il devient N
                /// car cela simplifiera la gestion quand il sera passé en paramètres
                /// de la classe Jeu
                if (modeJeu.ToUpper() == "B")
                {
                    modeJeu = "S";
                }
            }
            /// On utilise un tuple car la longueur et les types des données contenues sont définies à l'avance
            Tuple<string, string, int, int> tuple = new Tuple<string, string, int, int>(langue, modeJeu, nbJoueurs, vitesse);
            return tuple;
        }


        /// MAIN

        
        static void Main(string[] args)
        {
            Tuple<string, string, int, int> tuple = Accueil();            
            string langue = tuple.Item1;
            string modeJeu = tuple.Item2;
            int nbJoueurs = tuple.Item3;
            int vitesse = tuple.Item4;
            
            /// Lance le jeu
            Jeu jeu = new Jeu(langue, modeJeu, nbJoueurs, vitesse);

            
            Console.ReadKey();

        }
    }
}