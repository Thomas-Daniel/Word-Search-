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
    public class Joueur
    {


        /// ATTRIBUTS
        

        string nom; // nom du joueur
        int score; // score du joueur
        List<string> motsTrouves = new List<string> (); // Liste des mots trouvés


        /// CONSTRUCTEUR


        /// <summary>
        /// Constructeur de la classe Joueur
        /// </summary>
        /// <param name="name"></param>
        public Joueur(string name)
        {
            this.score = 0; //Score au départ de 0
            this.nom = name;
        }


        /// MÉTHODES


        /// <summary>
        /// Fonciton obligatoire qui ecrit les attributs de chaque joueur
        /// </summary>
        /// <returns>retourne une string decrivant le joueur</returns>
        public override string ToString()
        {
            string s = String.Format("Joueur : {0}\nScore : {1}\n" +
                "Liste des mots trouvés : "
                , this.nom, this.score);
            foreach(string mot in motsTrouves)
            {
                s += mot + " ";
            }
            return s;
        }

        
        /// <summary>
        /// Ajoute le mot dans les mots trouvés par le joueur 
        /// </summary>
        /// <param name="mot">Envoie le mot en parametre pour pouvoir l'ajouter</param>
        public void Add_Mot(string mot)
        {
            motsTrouves.Add(mot);
        }


        /// <summary>
        /// Ajoute au score du joueur la valeur passé en parametre 
        /// </summary>
        /// <param name="val">val est la valeur que l'on veut ajouter au joueur </param>
        public void Add_Score(int val)
        {
            this.score += val;
        }


        /// PROPRIÉTÉS


        /// Propriétés de lecture et d'écriture du nom
        public string Name
        {
            get { return nom; }
            set { nom = value ; }
        }


        /// Propriétés de lecture et d'écriture du score
        public int Score
        {
            get { return score; }
            set { score = value ; }
        }


        /// Propriétés de lecture des motsTrouves
        public List<string> MotsTrouves
        {
            get { return motsTrouves; }        
        }
    }
}
