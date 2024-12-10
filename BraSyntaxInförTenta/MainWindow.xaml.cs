using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;

namespace BraSyntaxInförTenta
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        //Denna är bra för alternativsrutor som i val
        public class Voter()
        { 
            int VoteA;

        
            if (OptionA.IsChecked==true)
            {
                voteA++;
            }
            else if (OptionB.IsChecked==true)
            {
                voteB++;
            }
            else
            {
                    voteC++;
            }
        }

        //Räknar röster i detta fall. Läggs in i en egen klass.
        class VoteCounter
        {
            public char ElectionWinner(int alternativeA, int alternativeB, int alternativeC)
            {
            if (alternativeA > alternativeB && alternativeA > alternativeC)
                {
                return 'A';
                }
            else if (alternativeB > alternativeA && alternativeB > alternativeC)
                {
                return 'B';
                }
            else if (alternativeC > alternativeA && alternativeC > alternativeB)
                {
                return 'C';
                }
            else
                {
                return 'X';
                }
            //Presenterar det vinnande alternativet
            VoteCounter voteCounter = new VoteCounter();
            char winner = voteCounter.ElectionWinner(voteA, voteB, voteC);

            //Metod som istället presenterar det vinnande alternativet i en string.
        if (winner.Equals('X'))
            {
                lblWinner.Content = $"Det går inte att avgöra vinnande alternativ";
            }
         else
            {
                lblWinner.Content = $"Alternativ {winner} fick flest röster";
            }        


        }

        static class VoteCounter //Array som loopar igenom partiers stolar i riksdagen som beskrivs av varje indexvärde.
        {
        private static int[] chairs = new int[]
        { 70, 31, 20, 22, 100, 28, 16, 62 };
        static public int CalculateMandate(bool[] checkedParties)
        {
            int sum = 0;
            // Loopar igenom en utav arrayerna.
            for (int i = 0; i < chairs.Count(); i++)
            {
            // Undersöker om partiet har markerats == true
            if (checkedParties[i])
                {
                sum += chairs[i];
                }
            }
        return sum; //returnerar summan av antalet stolar.
        }

        public static bool HasMajority(int mandate) //Boolsats som returnerar om man har majoritet eller inte.
        {
                if (mandate >= 175)
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }

        //returnerar en textruta(Content) om man har majoritet eller inte i en String.
        int sum = VoteCounter.CalculateMandate(checkedValues);
        lblResult.Content = $"Resultat: {sum} av 349";
        if (VoteCounter.HasMajority(sum))
        {
        lblMajority.Content = $"Majoritet? Ja";
        }
        else
        {
        lblMajority.Content = $"Majoritet? Nej";
        }

        bool[] checkedValues = new bool[] //Skapas när man klickar i rutor. Typ Bool.
        {
            (bool)chkModeraterna.IsChecked,
            (bool)chkCenterpartiet.IsChecked,
            (bool)chkLiberalerna.IsChecked,
            (bool)chkKristdemokraterna.IsChecked,
            (bool)chkSocialdemokraterna.IsChecked,
            (bool)chkVänsterpartiet.IsChecked,
            (bool)chkMiljöpartiet.IsChecked,
            (bool)chkSveriedemokraterna.IsChecked
        };

        Party party = new Party() //Skapar ett parti med förkortning som gör det till stora bokstäver med .Text.ToUpper().
        {
            Name = txtName.Text,
            Abbreviation = txtAbbreviation.Text.ToUpper()
        };

        listBoxParties.ItemsSource = election.Parties; //Syntax för ItemsSource i en List.
        party.NumberOfVotes = int.Parse(txtVotes.Text); //Parse översätter int till läsbar String.

            
        public Party GetPartyByAbbreviation(string abbreviation) //returnerar namnet på ett parti mha förkortningen.
                {
                    foreach (Party party in Parties)
                        {
                        if (party.Abbreviation.ToUpper().Equals(abbreviation.ToUpper()
                            {
                                return party;
                            }
                        }
                    return null;
                    }
        party = election.GetPartyByAbbreviation(txtAbbreviation.Text); //if-sats som returnerar ett meddelande om användaren skriver
        if (party == null)                                              //in fel.
            {
            lblPartyName.Content = "Partiet saknas";
            }
        else
            {
            lblPartyName.Content = party.Name;
            }
        public List<Party> GetRiksdagParties() // Lista som räknar ut om partiet har över 4% så lägger den till det riksdag.
                {
                    CalculatePercentage();
                    List < Party > riksdag = new List<Party>();
                    foreach (Party party in Parties)
                        {
                        if (party.Percentage >= 0.04)
                        {
                            riksdag.Add(party);
                        }
                    }
                return riksdag;
                }

        }

}