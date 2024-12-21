using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO.Enumeration;

namespace PROJET_ALGO
{
    public class Dictionnaire
    {


        /// ATTRIBUTS


        private int tailleMot; // Taille des mots
        private string langue; // Langue du jeu
        private List<string> listeMots; // 


        /// CONSTRUCTEUR


        /// <summary>
        /// Constructeur de la classe Dictionnaire
        /// </summary>
        /// <param name="langue"> la langue des mots à récupérer </param>
        /// <param name="tailleMot"> la taille des mots à récupérer </param>
        public Dictionnaire(string langue, int tailleMot)
        {
            this.langue = langue;
            if (tailleMot < 2) 
            {
                tailleMot = 2;
            }
            this.tailleMot = tailleMot;            
            
            string fileName;

            /// Le format de la langue est en deux lettres.
            /// On n'utilise pas de switch car dans le cas où la langue entrée n'existe pas ou
            /// ne respecte pas le format, ou n'est pas supporté par le jeu, on joue en FR.
            if (this.langue.ToUpper() == "EN")
            {
                fileName = "MotsPossiblesEN.txt";
            }
            else
            {
                fileName = "MotsPossiblesFR.txt";
            }

            /// On fait appel à la méthode wordlist qui va chercher tous les mots de taille taillMot
            /// dans le fichier fileName.
            this.listeMots = wordList(fileName, tailleMot);
        }


        /// PROPRIÉTÉS


        public string Langue
        {
            get { return this.langue; }
        }

        public int TailleMot 
        { 
            get { return this.tailleMot; } 
        }

        public List<string> ListeMot
        {
            get { return this.listeMots;}
        }

        public override string ToString()
        {
            return String.Format("Longueur des mots : {0}\nLangue : {1}", this.tailleMot, this.langue == "EN" ? "Anglais" : "Français");
        }


        /// MÉTHODES


        /// <summary> Permet de récupérer la liste des mots de la taille spécifiée par tailleMot </summary>
        /// <param name="fileName"> le nom du fichier dans lequel aller chercher les mots </param>
        /// <param name="tailleMot"> la taille des mots à récupérer </param>
        /// <returns> Liste des mots de la taille choisie </returns>
        public List<string> wordList(string fileName, int tailleMot)
        {
            List<string> list = new List<string>(); // Créer une nouvelle collection qui est une liste de string

            /// Voir explications dans Program.cs
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string fullName = String.Format(@"{0}\{1}", path, fileName);

            string[] lines;
            try // Capture des erreurs même si le nom du fichier est forcément valide puisqu'il n'est pas rentré par le joueur
            {
                lines = File.ReadAllLines(fullName); // Essai d'ouvrir le fichier
            }
            catch (FileNotFoundException f) // S'il n'existe pas on réessaie avec le fichier en Français
            {
                Console.WriteLine("Fichier non trouvé.\nLa langue de jeu sera donc le Français");
                fileName = "MotsPossiblesFR.txt"; // Dans le cas où il est quand même invalide la langue par défaut est FR
                fullName = String.Format(@"{0}\{1}", path, fileName);
                lines = File.ReadAllLines(fullName);
            }

            for (int i = 0; i < lines.Length; i += 2) //On itère sur les lignes du fichier
            {
                /// Si la ligne est un string d'un nombre et que ce nombre est tailleMot. Alors on prend la ligne
                /// suivante que l'on découpe avec Split et un espace comme séparateur, en une liste
                /// car la ligne suivante correspond à tous les mots de taille tailleMot séparés par des espaces.
                /// Attention, on teste bien si la ligne est un string d'un nombre (on transforme tailleMot en un string 
                /// et on le compare avec la ligne) et non pas si la ligne parsée en int est égale à tailleMot car cela 
                /// soulève des erreurs dans le cas où ce que l'on parse n'est pas int. Notre méthode réduit les vecteurs d'erreurs.
                if (lines[i] == tailleMot.ToString())
                {
                    foreach (string s in lines[i + 1].Split(" "))
                    {
                        list.Add(s);
                    }
                }
            }
            list.Sort();
            return list;
        }


        /// <summary>
        /// Méthode récursive terminale pour vérifier si un mot donné est dans le dictionnaire.
        /// Méthode peu sensible à la casse car un mot passé en minuscule (.ToUpper() pour mettre en majusculer)
        /// et/ou avec des espaces avant ou après le mot (.Trim() pour enlever les espaces) (ex : '   bOire ') fonctionnera quand même lors des tests.
        /// </summary>
        /// <param name="mot"> le mot recherché </param>
        /// <param name="fin"> la longueur de la liste de mots </param>
        /// <param name="debut"> l'indice du début soit 0 </param>
        /// <returns> booléen en fonction de si le mot est présent ou non </returns>
        public bool RechDichoRecursif(string mot, int fin, int debut = 0)
        {
            int milieu = (debut + fin)/2;
            if (debut > fin)
            {
                return false;
            }
            else if (mot.ToUpper().Trim() == ListeMot[milieu]) { return true; }
            else if (mot.ToUpper().Trim().CompareTo(ListeMot[milieu]) < 0)
            {
                return RechDichoRecursif(mot, milieu - 1, debut);
            }
            else
            {
                return RechDichoRecursif(mot, fin, milieu + 1);
            }
        }
	}
}
