using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace agenda
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
            CitireFisier pers = new CitireFisier(numeFisier);
            Persoana a = new Persoana("Ana", "Ana", "10.10.2000", 72832, "mere");
            Persoana b = new Persoana();
            pers.AddPersoana(a);
           // pers.Afisare();
            a.Afisare();
            b.Afisare();
        }
    }


    public class CitireFisier : Persoana
    {
        private const int NrMaxPersoane = 50;
        private string numeFisier;
        public  CitireFisier(string numeFisier)
        {
            this.numeFisier = numeFisier;
            //crearea sau deschiderea fisierului
            Stream streamFisierTxt = File.Open(numeFisier, FileMode.OpenOrCreate);
            streamFisierTxt.Close();
        }
        public void AddPersoana(Persoana persoana)
        {
            using (StreamWriter streamWriterFisierText = new StreamWriter(numeFisier, true))
            {
                streamWriterFisierText.WriteLine(persoana.ConversieLaSir_PentruFisier());
            }
        }

        public Persoana[] GetPersoana(out int nrPers)
        {
            Persoana[] pers = new Persoana[NrMaxPersoane];

            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;
                nrPers = 0;
                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    pers[nrPers++] = new Persoana(linieFisier);
                }
            }
            //return nrPers;
            return pers;
        }
    }


    public class Persoana
    {
        private const char SeparatorFisier = ';';
        private const int Tel = 0;
        string nume, prenume;
        string dataNastere;
        int nrTelefon;
        string Mail;
        private const int NUME = 1;
        private const int PRENUME = 2;
        private const int DATANASTERE = 3;
        private const int NRTEL = 4;
        private const int MAIL = 5;
        // constructor cu parametrii
        public Persoana(string _nume, string _prenume, string _dataNastere, int _nrTelefon, string _mail)
        {
            nume = _nume;
            prenume = _prenume;
            dataNastere = _dataNastere;
            nrTelefon = _nrTelefon;
            Mail = _mail;
        }
        // construcotr implicit
        public Persoana()
        {
            nume = "-";
            prenume = "-";
            dataNastere = "0.00.0000";
            nrTelefon = 0;
            Mail = "-";
        }
        public string ConversieLaSir_PentruFisier()
        {
            string obiectStudentPentruFisier = string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}",
                SeparatorFisier,
                //idStudent.ToString(),
                (nume ?? " NECUNOSCUT "),
                (prenume ?? " NECUNOSCUT "),
                (dataNastere ?? "NECUNOSCUT"),
                nrTelefon.ToString(),
                (Mail ?? "Nescucnoscut"));

            return obiectStudentPentruFisier;
        }
        public Persoana(string linieFisier)
        {
            var dateFisier = linieFisier.Split(SeparatorFisier);

            nrTelefon = Convert.ToInt32(dateFisier[Tel]);
            nume = dateFisier[NUME];
            prenume = dateFisier[PRENUME];
            dataNastere = dateFisier[DATANASTERE];
            //nrTelefon = dateFisier[NRTEL];
            Mail = dateFisier[MAIL];

        }
        public void SetNume(string nume)
        {
            this.nume = nume;
        }
        public void SetPrenume(string prenume)
        {
            this.prenume = prenume;
        }
        public void SetDataNastere(string dataNastere)
        {
            this.dataNastere = dataNastere;
        }
        public void SetMail(string Mail)
        {
            this.Mail = Mail;
        }
        public void SetNrTelefon(int nrTelefon)
        {
            this.nrTelefon = nrTelefon;
        }
        public string GetNume()
        {
            return nume;
        }
        public string GetPrenume()
        {
            return prenume;
        }
        public string GetMail()
        {
            return Mail;
        }
        public string GetDataNastere()
        {
            return dataNastere;
        }
        public int GetNrTelefon()
        {
            return nrTelefon;
        }
        public void Afisare()
        {
            Console.WriteLine(nume + " " + prenume + " " + dataNastere + " " + nrTelefon + " " + Mail);

        }
    }

}
