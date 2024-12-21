using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using System.IO.Enumeration;

namespace PROJET_ALGO
{
    public class Jeu
    {


        /// ATTRIBUTS


        Plateau plateau; //Plateau actuel
        List<Plateau> platPrec; //Plateaux précédents

        int nbJoueurs; // Nombre de joueurs
        List<Joueur> joueurs; // Liste des joueurs
        
        int nbRounds; // Nombre de rounds de la partie
        int round; // Numéro du round actuel

        string langue; // Langue de la partie
        string modeJeu; // Nouveau jeu ou à partir d'une sauvegarde
        int difficulte; // Difficulté du round
        int vitesse; // Vitesse du jeu

        /// Pour la sauvegarde récup tous attributs ci-dessus + 


        /// CONSTRUCTEUR         


        /// <summary> Constructeur de la classe Jeu </summary>
        /// <param name="langue"> langue du jeu </param>
        /// <param name="modeJeu"> mode de jeu (Nouveau ou à partir d'une Sauvegarde) </param>
        /// <param name="nbJoueurs"> le nombre de joueurs </param>
        /// <param name="vitesse"> la vitesse du jeu qui rendra le jeu plus ou moins compliqué 
        /// en plus de la difficulté qui s'incrémente </param>
        public Jeu(string langue, string modeJeu, int nbJoueurs = 0, int vitesse = 0)
        {            
            this.modeJeu = modeJeu;
            

            if (modeJeu == "N")
            {
                this.nbJoueurs = nbJoueurs;
                this.vitesse = vitesse;
                this.langue = langue.ToUpper();
                this.platPrec = new List<Plateau>();
                nouveauJeu();
            }
            else // sinon on récupère à partir d'une sauvegarde peu importe l'entrée
            {
                this.langue = langue.ToUpper();
                recupJeu();
            }
            lancerJeu();
        }


        /// PROPRIÉTÉS
        

        public Plateau Plat
        {
            get { return this.plateau; }
        }

        public List<Plateau> PlatPrec
        {
            get { return this.platPrec; }
        }

        public int NbJoueurs
        {
            get { return this.nbJoueurs; }
        }

        public List<Joueur> Joueurs
        {
            get { return this.joueurs; }
        }

        public int NbRounds
        {
            get { return this.nbRounds; }
        }

        public int Round
        {
            get { return this.round; }
        }

        public string Langue
        {
            get { return this.langue; }
        }

        public string ModeJeu
        {
            get { return this.modeJeu; }
        }

        public int Difficulte
        {
            get { return this.difficulte; }
        }

        public int Vitesse
        {
            get { return this.vitesse; }
        }


        /// MÉTHODES


        /// <summary> Instancie un nouveau jeu en demandant la difficulté, le nombre de rounds, le nombre de joueurs.
        /// Lance ensuite la création des joueurs </summary>
        public void nouveauJeu()
        {
            Console.Clear();
            if (langue == "FR")
            {
                Console.WriteLine("Bienvenue dans cette nouvelle partie\n");
                Console.WriteLine("\nCombien voulez-vous jouer de manches ?\n");
                do
                {
                    try
                    {
                        this.nbRounds = int.Parse(Console.ReadLine());
                    }
                    catch(FormatException)
                    {
                        Console.WriteLine("\nLa valeur entrée n'est pas un entier\n");
                    }
                } while (this.nbRounds < 1);

                Console.WriteLine("\nQuelle doit être la difficulté ? (entre 1 et 5)\n");
                do
                {
                    try
                    {
                        this.difficulte = int.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("\nLa valeur entrée n'est pas un entier\n");
                    }
                } while ((this.difficulte < 1) && (this.difficulte > 5));
            }
            else // On peut pas juste afficher en fonction de la langue et demander les valeurs en dehors du if et else
            {
                Console.WriteLine("Welcome to this new game\n");
                Console.WriteLine("\nHow many rounds do you want to play ?\n");
                do
                {
                    try
                    {
                        this.nbRounds = int.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("\nLa valeur entrée n'est pas un entier\n");
                    }
                } while (this.nbRounds < 1);

                Console.WriteLine("\nWhat should be the difficulty ? (between 1 and 5)\n");
                do
                {
                    try
                    {
                        this.difficulte = int.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("\nLa valeur entrée n'est pas un entier\n");
                    }
                } while ((this.difficulte < 1) && (this.difficulte > 5));
            }
            Console.Clear();
            creerJoueurs();
            this.round = 1;
        }


        /// <summary>
        /// Créé les joueurs en demandant à chaque fois le nom du joueur et en l'ajoutant à la liste des joueurs
        /// </summary>
        public void creerJoueurs()
        {
            this.joueurs = new List<Joueur>();
            int count = 1;
            for (int i = 0; i < this.nbJoueurs; i++)
            {
                if (this.langue == "FR")
                {
                    Console.WriteLine("Entrer le nom du joueur " + count + "\n");
                }
                else
                {
                    Console.WriteLine("Enter the name of player " + count + "\n");
                }
                this.joueurs.Add(new Joueur(Console.ReadLine()));
                count++;
            }            
        }


        /// <summary>
        /// Permet de récupérer une partie sauvegardée à partir d'un fichier txt entré par l'utilisateur
        /// en instanciant la langue, la difficulté, la vitesse, le numéro round actuel, 
        /// le nombre de rounds, le nombre de joueurs et la liste des joueurs.
        /// </summary>
        public void recupJeu()
        {
            if (this.langue == "FR")
            {
                Console.WriteLine("\nEntrez le nom du fichier de sauvegarde à charger (en .txt)\n");
            }
            else { Console.WriteLine("\nEnter the name of the backup file to load (it must be a .txt)\n"); }

            string[] fileData = new string[] { };
            bool exist;
            string fileName, fullName;
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            
            /// Teste si le fichier entré par le joueur existe sinon redemande un nom de fichier
            do
            {
                fileName = Console.ReadLine();
                fullName = String.Format(@"{0}\{1}", path, fileName);
                try
                {
                    fileData = File.ReadAllLines(fullName);
                    exist = true;
                }
                catch (FileNotFoundException)
                {
                    if (this.langue == "FR")
                    {
                        Console.WriteLine("\nLe fichier n'existe pas\n");
                    }
                    else { Console.WriteLine("\nThe file does not exist\n"); }
                    exist = false;
                }
            } while (!exist);
            
            /// Séparation de chaque donnée du jeu contenue dans la 1ère ligne
            string[] donneesJeu = fileData[0].Split(";");

            /// Extraction et affectation des données sauvegardées
            this.langue = donneesJeu[0];
            this.difficulte = int.Parse(donneesJeu[1]);
            this.vitesse = int.Parse(donneesJeu[2]);
            this.round = int.Parse(donneesJeu[3]);
            this.nbRounds = int.Parse(donneesJeu[4]);
            this.nbJoueurs = int.Parse(donneesJeu[5]);
            this.joueurs = new List<Joueur>();

            /// Extraction de tous les joueurs (les lignes restantes dans le fichier)
            for (int i = 1; i < fileData.Length; i++)
            {
                string[] donneesJoueur = fileData[i].Split(";");
                Joueur joueur = new Joueur(donneesJoueur[0]);
                joueur.Add_Score(int.Parse(donneesJoueur[1]));
                this.joueurs.Add(joueur);
            }
            Console.Clear();
            if (this.langue == "FR") { Console.WriteLine("Les données ont bien été importées dans le jeu"); }
            else { Console.WriteLine("The data has been recorded in the set"); }
            Thread.Sleep(3000); //Pause de 3 secondes
        }


        /// <summary>
        /// Permet de lancer le jeu, de passer chaque round, de donner la main aux joueurs, puis d'afficher
        /// le score à chaque fin de round et à la fin de la partie.
        /// </summary>
        public void lancerJeu()
        {
            /// this.round = this.round car si l'on récup une partie sauvegardée il ne faut pas commencer à 1 mais au round auqeul on est qui sera stocké dans this.round
            for (this.round = this.round; this.round <= this.nbRounds; this.round++)
            {
                Console.Clear();
                Console.WriteLine("Round " + this.round + "\n");

                string choix;
                if (this.langue == "FR")
                {
                    do
                    {
                        Console.WriteLine("\nSouhaitez-vous sauvegarder la partie ? \"O\" ou \"N\"\n");
                        choix = Console.ReadLine();
                    } while ((choix.ToUpper() != "O") && (choix.ToUpper() != "N"));
                }
                else
                {
                    do
                    {
                        Console.WriteLine("\nWould you like to save the game ? \"Y\" or \"N\"\n");
                        choix = Console.ReadLine();
                    } while ((choix.ToUpper() != "Y") && (choix.ToUpper() != "N"));
                    if (choix.ToUpper() == "Y") { choix = "O"; }
                }

                if (choix.ToUpper() == "N") // Cas où l'on continue
                {
                    /// Faire timer ici
                    foreach (Joueur joueur in this.joueurs)
                    {
                        tourJoueur(joueur);
                    }
                    tableauScores();
                    Thread.Sleep(10000); // Fait une pause de 10s pour laisser scores affichés

                    if (this.difficulte < 5)
                    {
                        this.difficulte++;
                    }
                }
                else if (choix.ToUpper() == "O") // Choix sauvegarder
                {
                    sauvegarderJeu();
                    Environment.Exit(0);
                }
            }
            tableauScoresFinal();
        }


        /// <summary>
        /// Gère le tour du joueur en demandant si le plateau doit être généré ou récupéré d'un csv, lance le chrono, 
        /// affiche la matrice, teste les entrées du joueur, calcule et ajoute le score du joueur
        /// </summary>
        /// <param name="joueur"> joueur qui va jouer son tour </param>
        public void tourJoueur(Joueur joueur)
        {
            joueur.MotsTrouves.Clear(); // Vide la liste des mots trouvés par le joueur pour le nouveau round
            Console.Clear();
            Console.WriteLine("Round " + this.round);
            Console.WriteLine("Tour de " + joueur.Name + "\n");
            string choix;

            if (this.langue == "FR")
            {
                do
                {
                    Console.WriteLine("\nVoulez-vous générer un nouveau plateau ou en récupérer un à partir d'un fichier .CSV ? \"G\" ou \"C\"\n");
                    choix = Console.ReadLine();
                } while ((choix.ToUpper() != "G") && (choix.ToUpper() != "C"));
            }
            else
            {
                do
                {
                    Console.WriteLine("\nDo you want to generate a new tray or retrieve one from a .CSV file ? \"G\" or \"C\"\n");
                    choix = Console.ReadLine();
                } while ((choix.ToUpper() != "G") && (choix.ToUpper() != "C"));
            }
            Console.Clear();
            Plateau plateau;
            if (choix.ToUpper() == "C") // Cas "C" on récupère d'un .CSV
            {
                plateau = new Plateau(); // On instancie un plateau que l'on modifiera
                Console.WriteLine("Entrer le nom du fichier .csv contenant le plateau");
                //plateau.ToRead("Plateau_1_7_6_9.csv");
                plateau.ToRead(Console.ReadLine());
                Console.Clear();
            }
            else
            {
                plateau = new Plateau(this.difficulte, this.langue);
            }
            this.plateau = plateau;

            /// Lancer Timer et ajt temps pas écoulé au while

            float stop = setTime();
            /// debut correspond au moment de début du jeu
            DateTime debut = DateTime.Now;
            /// temps actuel
            DateTime actuel = DateTime.Now;
            /// durée écoulée
            TimeSpan interval = actuel - debut;
            /// On vérifie que le nombre de secondes écoulées est inférieur à la durée de jeu (en secondes aussi)
            while ((interval.TotalSeconds < stop) && (joueur.MotsTrouves.Count < plateau.MotsRecherches.Count))
            {
                Console.WriteLine(plateau.afficherPlateau());
                Console.WriteLine(joueur.ToString());
                Console.WriteLine("Tu as trouvé " + joueur.MotsTrouves.Count + " mots sur " + plateau.MotsRecherches.Count);
                string mot, direction;
                int ligne = -1, colonne = -1;
                Console.WriteLine("Le temps écoulé est supérieur à " + (int)interval.TotalSeconds / 60 + " minutes et " + (int)interval.TotalSeconds % 60 + " secondes");
                Console.WriteLine("Tu as " + (int)stop / 60 + " minutes et " + (int)stop % 60 + " secondes pour trouver tous les mots");
                
                /// Demande à l'utilisateur un mot une ligne une colonne une direction en vérifiant la cohérence des valeurs entrées
                if (this.langue == "FR")
                {
                    Console.WriteLine("\nEntrez un mot\n");
                    mot = Console.ReadLine();
                    
                    /// Petite blague, si l'utilisateur entre quoi, le jeu renvoie FEUR et se ferme.
                    if (mot.ToUpper() == "QUOI")
                    {
                        Console.Clear();
                        string fullName = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\feur.txt";
                        Console.WriteLine(File.ReadAllText(fullName));
                        Environment.Exit(0);  //permet d'arrêter le programme
                    }

                    Console.WriteLine("\nEntrez un numéro de ligne (position de la 1ère lettre du mot)\n");
                    do
                    {
                        try
                        {
                            ligne = int.Parse(Console.ReadLine()) - 1;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("\nLa valeur entrée n'est pas un entier\n");
                        }
                    } while ((ligne < 0) || (ligne > plateau.NbLignes - 1));

                    Console.WriteLine("\nEntrez un numéro de colonne (position de la 1ère lettre du mot)\n");
                    do
                    {
                        try
                        {
                            colonne = int.Parse(Console.ReadLine()) - 1;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("\nLa valeur entrée n'est pas un entier\n");
                        }                        
                    } while ((colonne < 0) || (colonne > plateau.NbColonnes - 1));

                    Console.WriteLine("\nEntrez une direction (N, S, E, O, NE, NO, SE, SO)\n");
                    direction = Console.ReadLine();
                }

                else // Idem en Anglais
                {
                    Console.WriteLine("\nEnter a word\n");
                    mot = Console.ReadLine().ToUpper();

                    Console.WriteLine("\nEnter a line number\n");
                    do
                    {
                        ligne = int.Parse(Console.ReadLine()) - 1;
                    } while ((ligne < 0) && (ligne > plateau.NbLignes - 1));

                    Console.WriteLine("\nEnter a column number\n");
                    do
                    {
                        colonne = int.Parse(Console.ReadLine()) - 1;
                    } while ((colonne < 0) && (colonne > plateau.NbColonnes - 1));

                    Console.WriteLine("\nEnter a direction (N, S, E, O, NE, NO, SE, SO)\n");
                    direction = Console.ReadLine().ToUpper();
                }
                /// Vérifie si mot appartient au plateau à la place indiquée par ligne, colonne et direction
                /// et si mot appartient aux mots recherchés (qui eux-mêmes appartiennent au Dictionnaire)
                if (plateau.testPlateau(mot.ToUpper(), ligne, colonne, direction.ToUpper()))
                {
                    if (this.langue == "FR")
                    {
                        Console.Clear();
                        Console.WriteLine("Bravo tu as trouvé le mot : " + mot + "\n");
                        joueur.MotsTrouves.Add(mot.ToUpper());
                        joueur.Add_Score(mot.Length);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Bravo you found the word : " + mot + "\n");
                        joueur.MotsTrouves.Add(mot.ToUpper());
                        joueur.Add_Score(mot.Length);
                    }
                }
                else
                {
                    if (this.langue == "FR")
                    {
                        Console.Clear();
                        Console.WriteLine("Le mot " + mot + " que tu as rentré n'est pas dans le plateau ou n'est pas à la bonne place\n");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("The word " + mot + " that you entered is not in the tray or the location is incorrect\n");
                    }
                }
                actuel = DateTime.Now;
                interval = actuel - debut;
            }
            
            /// Regarde pourquoi le round du joueur
            if (joueur.MotsTrouves.Count == plateau.MotsRecherches.Count) // Le joueur a trouvé tous les mots
            {
                if (this.langue == "FR") { Console.WriteLine("Bravo tu as trouvé tous les mots"); }
                else { Console.WriteLine("Bravo you found all the words"); }
                joueur.Add_Score(this.difficulte * joueur.MotsTrouves.Count); // Ajoute comme score difficulté x le nombre de mots trouvés
                if (interval.TotalSeconds < 60) // Challenge si fini en moins d'une minute
                {
                    joueur.Add_Score(10);
                    Console.WriteLine("Success achieved");
                }
            }
            else // Le temps est écoulé
            {
                foreach (string mot in joueur.MotsTrouves)
                {
                    joueur.Add_Score(mot.Length); // Ajoute comme score la longueur de tous les mots trouvés
                }
                if (langue == "FR") { Console.WriteLine("Le temps est écoulé"); }
                else { Console.WriteLine("Time's up"); }
            }
            if (langue == "FR") { Console.WriteLine("Score de fin de manche : " + joueur.Score); }
            else { Console.WriteLine("End of round score : " + joueur.Score); }
            this.platPrec.Add(plateau);
            Thread.Sleep(5000); // Fait une pause de 5s pour laisser scores affichés
        }


        /// <summary>
        /// Permet de définir la durée que le joueur a pour jouer en fonction de la difficulté, et de la vitesse de jeu définie au début.
        /// </summary>
        /// <returns> retourne un float qui correspond au nombre de secondes de durée de jeu </returns>
        public float setTime() // Permet d'avoir le temps de jeu en fonction de la difficulté et de la vitesse choisie par l'utilisateur
        {
            float time = 0;
            if (this.vitesse == 1) // Partie lente
            {
                switch (this.difficulte)
                {
                    case 1:
                        time = 120; // 2 minutes
                        break;

                    case 2:
                        time = 180; // 3 minutes
                        break;

                    case 3:
                        time = 240; // 4 minutes
                        break;

                    case 4:
                        time = 300; // 5 minutes
                        break;

                    case 5:
                        time = 360; // 6 minutes
                        break;
                }
            }
            else if (this.vitesse == 2) // Partie normale
            {
                switch (this.difficulte)
                {
                    case 1:
                        time = 60; // 1 minute
                        break;

                    case 2:
                        time = 120; // 2 minutes
                        break;

                    case 3:
                        time = 180; // 3 minutes
                        break;

                    case 4:
                        time = 240; // 4 minutes
                        break;

                    case 5:
                        time = 300; // 5 minutes
                        break;
                }
            }
            else // Partie rapide
            {
                switch (this.difficulte)
                {
                    case 1:
                        time = 45; // 45 secondes
                        break;

                    case 2:
                        time = 60; // 1 minute
                        break;

                    case 3:
                        time = 120; // 2 minutes
                        break;

                    case 4:
                        time = 180; // 3 minutes
                        break;

                    case 5:
                        time = 240; // 4 minutes
                        break;
                }
            }            
            return time;
        }


        /// <summary>
        /// Affiche le tableau des scores de chaque fin de round
        /// </summary>
        public void tableauScores() // Permet d'avoir le tableau des scores à chaque fin de round pour tous les joueurs
        {
            foreach (Joueur joueur in this.joueurs)
            {
                Console.WriteLine("\n" + joueur.Name + " a un score de " + joueur.Score);
            }
        }


        /// <summary>
        /// Affiche le gagnant et son score
        /// </summary>
        public void tableauScoresFinal() // Permet d'avoir le gagnant ainsi que son score
        {
            Console.Clear();
            Joueur gagnant = this.joueurs[0];
            foreach (Joueur joueur in this.joueurs)
            {
                if (joueur.Score > gagnant.Score)
                {
                    gagnant = joueur;
                }
            }
            Console.WriteLine("Le gagnant est " +  gagnant.Name + " avec un score de " + gagnant.Score);
        }


        /// <summary>
        /// Sauvegarde le jeu avec en 1ère ligne ses données (la langue, la difficulté, la vitesse, le numéro round actuel, 
        /// le nombre de rounds, le nombre de joueurs) puis chaque ligne suivante correspond à un joueur et son score.
        /// </summary>
        public void sauvegarderJeu()
        {
            if (this.langue == "FR")
            {
                Console.WriteLine("\nNomez votre fichier de sauvegarde (en .txt)\n");
            } else { Console.WriteLine("\nName your save file (it must be a .txt)\n"); }

            /// Récupère le dossier courant
            string fileName = Console.ReadLine();
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string fullName = String.Format(@"{0}\{1}", path, fileName);

            /// Ligne 1 on stocke toutes les données de la partie et ensuite les données de chaque joueur ligne par ligne d'ou nbJoueurs + 1
            string[] data = new string[this.nbJoueurs + 1];

            /// Sauvegarde des données du jeu
            data[0] = String.Format("{0};{1};{2};{3};{4};{5}", this.langue, this.difficulte, this.vitesse, this.round, this.nbRounds, this.nbJoueurs);
            
            /// Sauvegarde des joueurs            
            int count = 1;
            foreach (Joueur joueur in this.joueurs)
            {
                data[count] = joueur.Name + ";" + joueur.Score;
                count++;
            }

            File.WriteAllLines(fullName, data);
            if (File.Exists(fullName))
            {
                if (this.langue == "FR")
                {
                    Console.WriteLine("\nVotre jeu a bien été sauvegardé\n");
                }
                else
                {
                    Console.WriteLine("\nYour game has been saved successfully\n");
                }
            }
            else
            {
                if (this.langue == "FR")
                {
                    Console.WriteLine("\nIl y a eu une erreur lors de la sauvegarde, votre jeu n'a pas été sauvegardé\n");
                }
                else
                {
                    Console.WriteLine("\nThere was an error while saving, your game was not saved\n");
                }
            }
        }
    }
}
