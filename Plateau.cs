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
   
    public class Plateau
    {


        /// ATTRIBUTS 


        char[,] plateau;
        int nivDiff;
        int nbLignes;
        int nbColonnes;
        int nbMotsRecherches;
        string langue;
        List<string> motsRecherches;
        List<string> motsPlaces;
        Random r = new Random(); 


        /// CONSTRUCTEUR


        /// <summary>
        /// Constructeur de la classe plateau
        /// </summary>
        /// <param name="nivDiff"> Niveau de difficulté du plateau </param>
        /// <param name="langue"> Langue des mots </param>
        public Plateau(int nivDiff = 1,string langue = "FR")
        {
            this.motsRecherches = new List<string>();
            this.motsPlaces = new List<string>();
            this.nivDiff = nivDiff; //niveau de difficulté             
            this.langue = langue; //choix de la langue pour la grille
            int nbMotsaplaces = 5; // nombre de mots à placer dans la grille
            int dir = 2; //variable decrivant le nombre maximale de direction à prendre en compte
            if (nivDiff == 1)//en fonction du niveau de difficulté on augmente le nombre de mots à placer 
            {
                this.nbLignes = r.Next(5,10);
                this.nbColonnes = r.Next(5, 10);
                

            }else if(nivDiff == 2)//en fonction du niveau de difficulté on augmente le nombre de mots à placer
            {
                this.nbLignes = r.Next(10, 13);
                this.nbColonnes = r.Next(10, 13);
                nbMotsaplaces = 8;
                dir = 4;

            }
            else if (nivDiff == 3)//en fonction du niveau de difficulté on augmente le nombre de mots à placer
            {
                this.nbLignes = r.Next(13, 16);
                this.nbColonnes = r.Next(13, 16);
                nbMotsaplaces = 12;
                dir = 6;

            }
            else if (nivDiff == 4)//en fonction du niveau de difficulté on augmente le nombre de mots à placer
            {
                this.nbLignes = r.Next(16, 19);
                this.nbColonnes = r.Next(16, 19);
                nbMotsaplaces = 15;
                dir = 8;

            }
            else if (nivDiff == 5)//en fonction du niveau de difficulté on augmente le nombre de mots à placer
            {
                this.nbLignes = r.Next(19, 22);
                this.nbColonnes = r.Next(19, 22);
                nbMotsaplaces = 20;
                dir = 8;

            }
            this.plateau = initPlateau(this.nbLignes, this.nbColonnes);// on initialise une matrice de zeros
            while (this.motsPlaces.Count <= nbMotsaplaces) //tant que le nombre de mots à placer n'est pas atteint , on continue à essayer de placer des mots.
            {
                Verif(this.plateau,dir,langue);//on envoie à la fonction VERIF la direction maximale et une langue pour créer des dictionnaires correctes
            }
            for (int i = 0; i < this.nbLignes; i++)
            {
                for (int j = 0; j < this.nbColonnes; j++)
                {
                    if (this.plateau[i, j] == '0')
                    {
                        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; // une fois le nombre de mots placés on remplace tous les zeros restants par une lettre au hasard à chaque fois
                        int n = r.Next(0, alphabet.Length);
                        char lettre = alphabet[n];
                        this.plateau[i, j] = lettre;
                    }
                }
            }  
        }


        /// PROPRIÉTÉS


        public char[,] Plat
        {
            get { return this.plateau; }
            set { this.plateau = value; }
        }

        public int NivDiff
        { get { return this.nivDiff; } }

        public int NbLignes
        { get { return this.nbLignes; } }

        public int NbColonnes
        { get { return this.nbColonnes; } }

        public int NbMotsRecherches
        { get { return this.nbMotsRecherches; } }

        public List<string> MotsRecherches
        { get { return this.motsRecherches; } }


        /// METHODES


        /// <summary>
        /// Initialisation du plateau avec des char '0'
        /// </summary>
        /// <param name="i"> Nombre de lignes du plateau </param>
        /// <param name="j"> Nombre de colonnes du plateau </param>
        /// <returns> Retourne plateau rempli </returns>
        public char[,] initPlateau(int i, int j)// on initialise juste un grille d'une certaine taille avec des zeros
        {
            char[,] plateau = new char[i, j];
            for (int k = 0; k < i; k++)
            {
                for (int l = 0; l < j; l++)
                {
                    plateau[k, l] = '0';
                }
            }
            return plateau;
        }


        /// <summary>
        /// La fonction pioche un mot aléatoire à chaque fois et vérifie à 50 endroits dans la matrice si il peut le placer. Si cest le cas alors elle place le mot en envoyant de mot à la fonction Remplissage.
        /// </summary>
        /// <param name="plateau">Le plateau initialisé de zeros puis ensuite le plateau rempli petit à petit </param>
        /// <param name="dir"> une borne de direction pour ne pas prendre en compte toutes les directions à chaque fois(borne déterminé en focntion des niveaux de difficulté</param>
        /// <param name="langue">Détermine juste la langue de plateau et va donc cherché le dictionnaire correspondant</param>
        /// <returns>Elle retourne le plateau modifié avec le mot mis dedans ou non si cela a été possible</returns>
        public char[,] Verif(char[,] plateau,int dir,string langue)// Fonction qui verifie si on peut placer un mot au hasard dans la graille en testant plusieurs positions au hasard et plusieurs directions
        {
            
            int nbLignes = plateau.GetLength(0);
            int nbColonnes = plateau.GetLength(1);
            int temp = 0;
            int h = 0; 
            int randomLength = r.Next(2, 16); //Mot de longueur au hasard entre 2 et 15
            Dictionnaire Dico = new Dictionnaire(langue, randomLength); //Creation d'un dictionnaire d'une certaine langue avec la taille du mot demandé
            int randomIndex = r.Next(0, Dico.ListeMot.Count);// on pioche un index au hasard 
            string mot = Dico.ListeMot[randomIndex]; // on prends un mot au hasard dans le dictionnaire
                while (h < 50) // on teste si le mot rentre dans une position (on teste dans 50 endroits pris au hasard)
                {
                    int k = r.Next(0, nbLignes);//pioche ua hasard une position sur une ligne 
                    int l = r.Next(0, nbColonnes);//pioche ua hasard une position sur une colonne 
                    int direction = r.Next(0, dir);//pioche ua hasard une direction 


                switch (direction)
                    {
                        case 0:
                            h++; // incremente h pour tester les 50 positions
                            if (mot.Length <= nbColonnes - l)// verifie de que le mot ne va pas sortir de la colonne 
                            {
                                for (int i = 0; i < mot.Length; i++) // cas vers le E 
                                {
                                    if (((plateau[k, l + i] == '0') || (plateau[k, l + i] == mot[i])))//si la case est un zero ou si la lettre presente sur la case est la meme que celle du mot au meme indice, cela incremente temp
                                    {
                                        temp++;// temp est incrementé, il sera chargé de savoir si on peut remplir la position avec le mot

                                    }
                                    else { return plateau; } 

                                }
                            }
                            else { break; }

                            if ((temp >= mot.Length) && (temp > 1) && (Present(mot)))// si temp est plus grand ou egale à plateau et que le mot n'est pas deja presnet dans la grille alors on remplit le mot dans la grille
                            {
                                Remplissage(plateau, k, l, 0, mot);// on envoie le plateau , la position de depart , la direction et le mot à placer;
                            h = 50;// on donne 50 comme valeur à h comme ca les autres positions n'ont pas besoin d'etre tester
                           

                            }
                           

                            break;

                        case 3:// MEME EXPLICATION QUE POUR LE CAS 0 ( les cas sont dans un autre particulier pour repondre aux directions des difficultés)
                            h++;
                            if ((mot.Length <= l))
                            {
                                for (int i = 0; i < mot.Length; i++)//cas vers le O
                                {
                                    if (((plateau[k, l - i] == '0')|| (plateau[k, l - i] == mot[i])))
                                    {
                                        temp++;

                                    }
                                    else
                                    {
                                       return plateau;

                                }

                                }
                            }
                            else { break; }

                            if ((temp >= mot.Length) && (temp > 1) && (Present(mot)))

                            {
                                Remplissage(plateau, k, l, 1, mot);
                            h = 50;

                        }
                           
                            break;

                        case 2:// MEME EXPLICATION QUE POUR LE CAS 0
                        h++;
                            if (mot.Length <= k)
                            {
                                for (int i = 0; i < mot.Length; i++)//cas vers le N
                                {
                                    if ((plateau[k - i, l] == '0')|| (plateau[k - i, l] == mot[i]))
                                    {
                                        temp++;

                                    }
                                    else
                                    {
                                     return plateau;

                                }
                                }
                            }
                            else { break; }

                            if ((temp >= mot.Length) && (temp > 1) && (Present(mot)))
                            {
                                Remplissage(plateau, k, l, 2, mot);
                            h = 50;
                        }
                            
                            break;

                        case 1:// MEME EXPLICATION QUE POUR LE CAS 0
                        h++;
                            if (mot.Length <= nbLignes - k)
                            {
                                for (int i = 0; i < mot.Length; i++)//cas vers le S
                                {
                                    if (((plateau[k + i, l] == '0')|| (plateau[k + i, l] == mot[i])))
                                    {
                                        temp++;

                                    }
                                    else
                                    {
                                      return plateau;

                                }
                                }
                            }
                            else
                            {
                                break;
                            }

                            if ((temp >= mot.Length) && (temp > 1) && (Present(mot)))
                            {
                                Remplissage(plateau, k, l, 3, mot);
                            h = 50;
                        }
                            
                            break;

                        case 4:// MEME EXPLICATION QUE POUR LE CAS 0
                        h++;
                            if ((mot.Length <= nbColonnes - l) && (mot.Length <= k))
                            {
                                for (int i = 0; i < mot.Length; i++) //cas vers le NE
                                {
                                    if ((plateau[k - i, l + i] == '0') || (plateau[k - i, l + i] == mot[i]))
                                    { temp++; }
                                    else
                                    { return plateau; }
                                }
                            }
                            else
                            {
                                break;
                            }
                            if ((temp >= mot.Length) && (temp > 1) && (Present(mot)))
                            {
                                Remplissage(plateau, k, l, 4, mot);
                            }
                            
                            break;

                        case 5:// MEME EXPLICATION QUE POUR LE CAS 0
                        h++;
                            if ((mot.Length <= l) && (mot.Length <= nbLignes - k))
                            {
                                for (int i = 0; i < mot.Length; i++) //cas vers le SO
                                {
                                    if (((plateau[k + i, l - i] == '0')|| (plateau[k + i, l - i] == mot[i])))
                                    {
                                        temp++;
                                    }
                                    else { return plateau; }
                                }
                            }
                            else { break; }

                            if ((temp >= mot.Length) && (temp > 1) && (Present(mot)))
                            {
                                Remplissage(plateau, k, l, 5, mot);
                            h = 50;
                        }
                           
                            break;

                        case 6:// MEME EXPLICATION QUE POUR LE CAS 0
                        h++;
                            if ((mot.Length <= l) && (mot.Length <= k))
                            {
                                for (int i = 0; i < mot.Length; i++) //cas vers le NO
                                {
                                    if (((plateau[k - i, l - i] == '0')|| (plateau[k - i, l - i] == mot[i])))
                                    {
                                        temp++;
                                    }
                                    else { return plateau; }
                                }
                            }
                            else { break; }

                            if ((temp >= mot.Length) && (temp > 1) && (Present(mot)))
                            {
                                Remplissage(plateau, k, l, 6, mot);
                            h = 50;
                        }
                            
                            break;

                        case 7:// MEME EXPLICATION QUE POUR LE CAS 0
                        h++;
                            if ((mot.Length <= nbColonnes - l) && (mot.Length <= nbLignes - k))
                            {
                                for (int i = 0; i < mot.Length; i++)//cas vers le SE
                                {
                                    if (((plateau[k + i, l + i] == '0')  || (plateau[k + i, l + i] == mot[i])))
                                    {
                                        temp++;
                                    }
                                    else
                                    { return plateau; }
                                }
                            }
                            else { break; }

                            if ((temp >= mot.Length) && (temp > 1) && (Present(mot)))
                            {
                                Remplissage(plateau, k, l, 7, mot);
                            h = 50;
                        }
                            
                            break;
                    }

                }    
                    
            // K pour ligne et l pour colonne 
            return plateau;
        }



        /// <summary>
        /// Verifie si le mot est deja present dans la grille.
        /// </summary>
        /// <param name="mot1">Envoie le mot pour vérifier si il est deja placé dans la grille</param>
        /// <returns>Retourne le booleen en fonction de si le mot est present dans la grille </returns>
        public bool Present(string mot1) // fonction qui renvoie un booleen , si le mot est deja placé dans la grille alors la fonction renvoie faux.
        {
            bool b = true;
            if (this.motsPlaces.Contains(mot1))
            {b=false;}
            return b;
        }



        /// <summary>
        /// Rempli simplement la grille avec le mot, la direciton , et la position à remplir.Puis renvoie la grille modifié
        /// </summary>
        /// <param name="plateau">Envoie le plateau pour pouvoir le remplir</param>
        /// <param name="i">indice de la premiere lettre du mot</param>
        /// <param name="j">indice de la premiere colonne du mot</param>
        /// <param name="dir">donne la direction dans laquelle placé le mot</param>
        /// <param name="mot">Envoie simpliement le mot à placer</param>
        /// <returns>retourne la grille avec le mot ajouté</returns>
        public char[,] Remplissage(char[,] plateau, int i, int j, int dir, string mot)// fonction qui s'occupe de remplir la grille avec le mot demandé et la postion ainsi que la direction 
        {
            this.motsRecherches.Add(mot);// on ajoute le mot aux mots recherchés pour pouvoir les afficher une fois la grille remplie
            this.motsPlaces.Add(mot);// on ajoute le mot aux mots deja placés comme ca on peut verifier la condition dans la fonciton Present 
            switch (dir)
            {
                case 0:
                    for (int k = 0; k < mot.Length; k++) //E , on place juste simplement le mot dans la direction demandé
                    {plateau[i, k + j] = mot[k];}
                    break;
                case 1:
                    for (int k = 0; k < mot.Length; k++) //O, on place juste simplement le mot dans la direction demandé
                    { plateau[i, j - k] = mot[k];}
                    break;
                case 2:
                    for (int k = 0; k < mot.Length; k++) //N, on place juste simplement le mot dans la direction demandé
                    { plateau[i - k, j] = mot[k];}
                    break;
                case 3:
                    for (int k = 0; k < mot.Length; k++) //S, on place juste simplement le mot dans la direction demandé
                    { plateau[i + k, j] = mot[k];}
                    break;
                case 4:
                    for (int k = 0; k < mot.Length; k++) //NE, on place juste simplement le mot dans la direction demandé
                    { plateau[i - k, j + k] = mot[k];}
                    break;
                case 5:
                    for (int k = 0; k < mot.Length; k++) //SO, on place juste simplement le mot dans la direction demandé
                    { plateau[i + k, j - k] = mot[k];}
                    break;
                case 6:
                    for (int k = 0; k < mot.Length; k++) //NO, on place juste simplement le mot dans la direction demandé
                    { plateau[i - k, j - k] = mot[k];}
                    break;
                case 7:
                    for (int k = 0; k < mot.Length; k++) //SE, on place juste simplement le mot dans la direction demandé
                    { plateau[i + k, j + k] = mot[k];}
                    break;
            }
            return plateau;
        }
        

        /// <summary>
        /// Affiche le plateau avec des | comme séparateur pour chaque case et des indices pour plus de visibilité, sur les côtés et en haut de la grille.
        /// </summary>
        /// <returns> Renvoie une string qui est l'affichage du plateau </returns>
        public string afficherPlateau()
        {
            /// Affiche les mots à trouver
            string s = "\nMots à trouver : ";
            for (int i = 0; i < MotsRecherches.Count - 1; i++)
            {
                s += MotsRecherches[i] + " ; ";
            }
            s += MotsRecherches[MotsRecherches.Count - 1] + "\n\n";

            /// Affiche les indices des colonnes pour jouer plus facilement
            for (int index = 1; index < this.Plat.GetLength(1) + 1; index++)
            {
                if (index > 9) // pour empêcher le décalage de l'affichage des indices lors du passage aux dizaines
                {
                    s += "  " + index;
                }
                else
                {
                    s += "   " + index;
                }
            }
            s += "\n";

            /// Affiche des séparateurs entre chaque case du plateau pour plus de lisibilité et ajoute les indices des lignes pour plus de clarté
            for (int i = 0; i < this.Plat.GetLength(0); i++)
            {
                for (int j = 0; j < this.Plat.GetLength(1); j++)
                {
                    s += " | " + this.Plat[i, j];
                }
                s += " |  " + (i + 1) + "\n";
            }
            return s;
        }


        /// <summary>
        /// Formate des informations (difficulté, nombre de lignes et nombre de colonnes) sur le plateau sous forme d'une string
        /// </summary>
        /// <returns> Renvoie la string </returns>
        public override string ToString()// Focnction ToString obligatoire
        {
            string s = String.Format("Ce plateau est de difficulté : {0}\n" +
                "de dimension {1}x{2}", this.nivDiff, this.Plat.GetLength(0), this.Plat.GetLength(1));
            return s;
        }


        /// <summary>
        /// Inscrit le plateau et ses données dans un fichier csv pour l'enregistrer
        /// </summary>
        public void ToFile()
        {
            // Le nom du fichier est de la forme Plateau_<Difficulté>_<nbLignes>_<nbColonnes>_<nbMotsRecherches>
            string fileName = String.Format("Plateau_{0}_{1}_{2}_{3}.csv", this.NivDiff, this.NbLignes, this.NbColonnes, this.NbMotsRecherches);
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string fullName = String.Format(@"{0}\{1}", path, fileName);
            string[] data = new string[nbLignes + 2]; //(+2 car il y a l'entête)
            
            /// Ajout données du plateau
            data[0] = String.Format("{0};{1};{2};{3}", this.NivDiff, this.NbLignes, this.NbColonnes, this.NbMotsRecherches);
            
            /// Ajout mots à rechercher
            foreach (string mot in this.MotsRecherches)
            {
                data[1] += mot + ";";
            }
            
            /// Ajout du plateau
            for (int i = 0; i < this.NbLignes; i++)
            {
                for (int j = 0; j < this.NbColonnes; j++)
                {
                    data[i + 2] += this.Plat[i, j] + ";";
                }
            }
            
            /// Ecriture dans le fichier
            File.WriteAllLines(fullName, data);
        }


        /// <summary>
        /// Lit les données d'un fichier csv et instancie un plateau avec ces données
        /// </summary>
        /// <param name="fileName"> fichier à partir duquel on récupère les données et le plateau </param>
        public void ToRead(string fileName)
        {
            /// Récupère le répertoire actuel
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string fullName = String.Format(@"{0}\{1}", path, fileName);
            string[] lines = new string[] { };
            try
            {
                lines = File.ReadAllLines(fullName);
            }
            catch (FileNotFoundException f)
            {
                Console.WriteLine("Le fichier entré n'existe pas");
                Environment.Exit(0);
            }
            
            /// Extraction données du plateau
            string[] data = lines[0].Split(";");
            this.nivDiff = int.Parse(data[0]);
            this.nbLignes = int.Parse(data[1]);
            this.nbColonnes = int.Parse(data[2]);
            this.Plat = new char[NbLignes, NbColonnes];
            this.nbMotsRecherches = int.Parse(data[3]);
            
            /// Extraction mots à rechercher
            string[] temp = lines[1].Split(";");
            this.MotsRecherches.Clear();
            for (int i = 0; i < nbMotsRecherches; i++)
            {
                this.MotsRecherches.Add(temp[i]);
            }

            /// Extraction du plateau
            for (int i = 2; i < NbLignes + 2; i++)
            {
                string[] col = lines[i].Split(";");
                for (int j = 0; j < NbColonnes; j++)
                {
                    this.Plat[i - 2, j] = Char.Parse(col[j]);
                }
            }
        }


        /// <summary>
        /// Teste si le mot passé en paramètre est bien à la ligne, la colonne, et dans la direction spécifiées
        /// </summary>
        /// <param name="mot"> le mot à tester </param>
        /// <param name="i"> la ligne à tester </param>
        /// <param name="j"> la colonne à tester </param>
        /// <param name="direction"> la direction à tester </param>
        /// <returns></returns>
        public bool testPlateau(string mot, int i, int j, string direction)// Fonction qui verifie la proposition de l'utilisateur (elle peut etre considerer comme la reciproque de la fonction Remplissage
        {
            bool b = false;
            if (this.MotsRecherches.Contains(mot))// Si le mot n'est pas dans les mots recherchés alors on revoie directement false 
            {
                switch (direction)
                {
                    case "E":
                        for (int k = 0; k < mot.Length; k++) //E , dans chaque on verifie jsute indice par indice que cela correspond 
                        {
                            if (this.plateau[i, j + k] == mot[k])
                            {
                                b = true;
                            }
                            else
                            {
                                b = false;
                                break;
                            }
                        }
                        break;

                    case "S":
                        for (int k = 0; k < mot.Length; k++) //O, dans chaque on verifie jsute indice par indice que cela correspond 
                        {
                            if (this.plateau[i + k, j] == mot[k])
                            {
                                b = true;
                            }
                            else
                            {
                                b = false;
                                break;
                            }
                        }
                        break;

                    case "N":
                        for (int k = 0; k < mot.Length; k++) //N, dans chaque on verifie jsute indice par indice que cela correspond 
                        {
                            if (this.plateau[i - k, j] == mot[k])
                            {
                                b = true;
                            }
                            else
                            {
                                b = false;
                                break;
                            }
                        }
                        break;

                    case "O":
                        for (int k = 0; k < mot.Length; k++) //S, dans chaque on verifie jsute indice par indice que cela correspond 
                        {
                            if (this.plateau[i, j - k] == mot[k])
                            {
                                b = true;
                            }
                            else
                            {
                                b = false;
                                break;
                            }
                        }
                        break;

                    case "NE":
                        for (int k = 0; k < mot.Length; k++) //NE, dans chaque on verifie jsute indice par indice que cela correspond 
                        {
                            if (this.plateau[i - k, j + k] == mot[k])
                            {
                                b = true;
                            }
                            else
                            {
                                b = false;
                                break;
                            }
                        }
                        break;

                    case "SO":
                        for (int k = 0; k < mot.Length; k++) //SO, dans chaque on verifie jsute indice par indice que cela correspond 
                        {
                            if (this.plateau[i + k, j - k] == mot[k])
                            {
                                b = true;
                            }
                            else
                            {
                                b = false;
                                break;
                            }
                        }
                        break;

                    case "NO":
                        for (int k = 0; k < mot.Length; k++) //NO, dans chaque on verifie jsute indice par indice que cela correspond 
                        {
                            if (this.plateau[i - k, j - k] == mot[k])
                            {
                                b = true;
                            }
                            else
                            {
                                b = false;
                                break;
                            }

                        }
                        break;

                    case "SE":
                        for (int k = 0; k < mot.Length; k++) //SE, dans chaque on verifie jsute indice par indice que cela correspond 
                        {
                            if (this.plateau[i + k, j + k] == mot[k])
                            {
                                b = true;
                            }
                            else
                            {
                                b = false;
                                break;
                            }
                        }
                        break;
                }
            }
            else
            {
                b = false;
            }
            return b;
        }
    }
}






    
