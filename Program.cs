using System;
using System.Collections.Generic;

namespace Galgenraten_Aufgabe
{
    internal class Program
    {
        private static List<string> worte = new List<string> { "DEUTSCHLAND", "POLEN", "SLOWAKEI", "SCHWEDEN", "TSCHECHIEN" };

        private static void Main(string[] args)
        {
            Random random = new Random();

            Wort wort = new Wort(worte[random.Next() % worte.Count]);
            int maxAnzahlProbe = 10;
            int verwendetProbe = 0;
            bool obGeloest = false;

            while (verwendetProbe < maxAnzahlProbe)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Das geheimes Lösungswort hat so viele Buchstaben ");
                    Console.WriteLine("                      " + wort.Entnehmen());

                    Console.WriteLine("Erraten Sie einen Buchstabe!    <" + verwendetProbe + " Versuche von 10 bereits verwendet>");
                    Console.WriteLine();

                    Console.WriteLine("Diese Buchstaben wurden bereits verwendet:  ");
                    foreach (char c in wort.verwendeteBuchstaben)
                    {
                        Console.Write(c + " ");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Das geheime Wort ist bereits so weit erraten: " + wort.Entnehmen());
                    
                    char benutzerBuchstabeAntwort = Convert.ToChar(Console.ReadLine().ToUpper());
                    if (wort.ErratenBuchstabe(benutzerBuchstabeAntwort) == false)
                        continue;
                    if (wort.ObErraten())
                        break;
                }
                catch { };

                Console.Clear();
                Console.WriteLine("Das geheime Wort ist bereits so weit erraten: " + wort.Entnehmen());
                Console.WriteLine();
                Console.WriteLine("Jetzt können Sie das ganze Wort versuchen zu erraten");
                Console.WriteLine("Wenn Sie scheitern, verlieren Sie!");
                Console.WriteLine();
                Console.WriteLine("Möchten Sie versuchen zu erraten? Tragen Sie ein:  ja / nein");
                string input = Console.ReadLine();
                if (input == "ja")
                {
                    Console.WriteLine("Sie haben <ja> gesagt. Geben Sie unten das vollständige Wort ein:");
                    string benutzerWortAntwort = Convert.ToString(Console.ReadLine().ToUpper());
                    obGeloest = benutzerWortAntwort == wort.EntnehmenOeffentlich();
                    if (benutzerWortAntwort == wort.EntnehmenOeffentlich())
                    {
                        Console.WriteLine("Bravo! Du hast das Wort erraten");
                        Console.ReadLine();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Es tut mir leid - Sie haben es nicht erraten!");
                        Console.ReadLine();
                        break;
                    }
                }

                verwendetProbe++;
                Console.Clear();
            }

            if (wort.ObErraten() || obGeloest)
            {
                Console.WriteLine("!!! You Win !!!");
                Console.WriteLine("Das geheimes Lösungswort ist " + wort.EntnehmenOeffentlich());
            }
            else
            {
                Console.WriteLine("You Lost the Game!");
                Console.WriteLine("Das geheimes Lösungswort war " + wort.EntnehmenOeffentlich());
            }

            Console.ReadLine();
        }

        public class Wort
        {
            public List<BuchstabeEinesWortes> ListeDerBuchstaben;
            public List<char> verwendeteBuchstaben;

            public Wort(string wort)
            {
                ListeDerBuchstaben = new List<BuchstabeEinesWortes>();

                verwendeteBuchstaben = new List<char>();

                foreach (char c in wort)
                {
                    BuchstabeEinesWortes a = new BuchstabeEinesWortes(c);
                    ListeDerBuchstaben.Add(a);
                }
            }

            public bool ObErraten()
            {
                foreach (BuchstabeEinesWortes a in ListeDerBuchstaben)
                {
                    if (a.ist_erraten == false)
                    {
                        return false;
                    }
                }
                return true;
            }

            public string Entnehmen()
            {
                string aufschrift = "";
                foreach (BuchstabeEinesWortes a in ListeDerBuchstaben)
                {
                    if (a.ist_erraten == true)
                    {
                        aufschrift = aufschrift + a.buchstabe;
                    }
                    else
                    {
                        aufschrift = aufschrift + "*";
                    }
                }
                return aufschrift;
            }

            public bool ErratenBuchstabe(char zeichen)
            {
                if (verwendeteBuchstaben.Contains(zeichen))
                {
                    return false;
                }

                verwendeteBuchstaben.Add(zeichen);

                foreach (BuchstabeEinesWortes eb_buchstabe in ListeDerBuchstaben)
                {
                    if (zeichen == eb_buchstabe.buchstabe)
                    {
                        eb_buchstabe.ist_erraten = true;
                    }
                }
                return true;
            }

            public string EntnehmenOeffentlich()
            {
                string aufschrift = "";
                foreach (BuchstabeEinesWortes eo_buchstabe in ListeDerBuchstaben)
                {
                    aufschrift = aufschrift + eo_buchstabe.buchstabe;
                }
                return aufschrift;
            }
        }

        public class BuchstabeEinesWortes
        {
            public char buchstabe;
            public bool ist_erraten;

            public BuchstabeEinesWortes(char bew_buchstabe)
            {
                this.buchstabe = bew_buchstabe;
                this.ist_erraten = false;
            }
        }
    }
}

/*
    public class Wort - eine Klasse mit Methoden
    Wort(string wort) - ein Konstruktor
    wort - ein Objekt der Klasse Wort
    Entnehmen()  - eine Methode der Klasse Wort
    ErratenBuchstabe() - eine Methode der Klasse Wort
    ObErraten() - eine Methode der Klasse Wort
    EntnehmenOeffentlch() - eine Methode der Klasse Wort

    BuchstabeEinesWortes - eine Klasse

    ListeDerBuchstaben - container

*/