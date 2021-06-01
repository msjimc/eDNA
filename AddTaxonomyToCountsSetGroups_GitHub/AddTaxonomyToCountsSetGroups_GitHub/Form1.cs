using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AddTaxonomyToCountsSetGroups_GitHub
{
    public partial class Form1 : Form
    {
        private string xmlTitle = "";
        private string conString = "";
        private Dictionary<string, TaxonomicGroups> tgs = null;
        Dictionary<int, string[]> names = new Dictionary<int, string[]>();
        Dictionary<string, int> seman = new Dictionary<string, int>();
        Dictionary<int, string> dataLineAndLineage = new Dictionary<int, string>();
        Dictionary<string, string> done = new Dictionary<string, string>();
        Dictionary<int, string> lineage = new Dictionary<int, string>();
        List<string> missed = new List<string>();

        string xmlFile = null;
        string countsFile = null;

        int xmlSpeices = 0;
        int xmlLookedFor = 0;
        int xmlGenus = 0;

        public Form1()
        {
            InitializeComponent();
            string ServerName = Environment.MachineName;
            //conString = "Data Source=LIMM-PC1176X32;Initial Catalog=ITIS;User ID=bob;Password=AutoSNPa33";
            //conString = "Data Source=" + ServerName + "\\SQLEXPRESS;Initial Catalog=ITIS;Integrated Security=True";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            createConnectionString ccs = new createConnectionString();
            ccs.ShowDialog();
            conString = ccs.getConnectionString;
            if (string.IsNullOrEmpty(conString) == true) { Close(); }
        }

        private void btnCombine_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(xmlFile) == true)
            {
                MessageBox.Show("Please select the blast data file");
                return;
            }

            if (string.IsNullOrEmpty(countsFile) == true)
            {
                MessageBox.Show("Please select the read counts data file");
                return;
            }

            string starttext = Text;
            try
            {
                Text = "Creating list of species names from data base";
                setNameTSN();
                Text = "Getting Taxonomic data from data base";
                tgs = getTaxonomicData();
                Text = "Making the phylogenetic strings";
                lineage = MakeLineageStrings();
                tgs = null;
                Text = "Reading the blast data file";
                getXMLData();
                Text = "Reading the counts file and saving results to file";
                getReadCountData();
                Text = "Saving unasigned sequences to file";
                saveMissedToFile(xmlFile);
            }
            catch(Exception ex)
            {
                MessageBox.Show("When " + Text + " an error occured:\n " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            { Text = starttext;Application.DoEvents(); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            xmlFile = FileString.OpenAs("Select the aggergated BLAST xml data", "*.txt|*.txt");
            if (System.IO.File.Exists(xmlFile) == false) { return; }

            btnCounts.Enabled = true;
            lblBlastFile.Text = xmlFile.Substring(xmlFile.LastIndexOf("\\") + 1);

        }

        private void btnCounts_Click(object sender, EventArgs e)
        {
            countsFile = FileString.OpenAs("Select the counts file", "*.txt|*.txt");
            if (System.IO.File.Exists(countsFile) == false) { return; }

            btnCombine.Enabled = true;
            lblCounts.Text = countsFile.Substring(countsFile.LastIndexOf("\\") + 1);

        }

        private Dictionary<string, TaxonomicGroups> getTaxonomicData()
        {
            tgs = new Dictionary<string, TaxonomicGroups>();

            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString);
            if (con.State != ConnectionState.Open)
            { con.Open(); }

            string commandstringInfo = "select ITIS.dbo.taxonomic_units.complete_name, " +
                "ITIS.dbo.taxonomic_units.tsn, " +
                "ITIS.dbo.taxon_unit_types.rank_name, " +
                "ITIS.dbo.taxonomic_units.rank_id, " +
                "ITIS.dbo.hierarchy. hierarchy_string " +
                "from ITIS.dbo.taxonomic_units inner join ITIS.dbo.taxon_unit_types " +
                "on ITIS.dbo.taxonomic_units.rank_id = ITIS.dbo.taxon_unit_types.rank_id " +
                "inner join ITIS.dbo.hierarchy " +
                "on ITIS.dbo.hierarchy.tsn = ITIS.dbo.taxonomic_units.tsn " +
                "where ITIS.dbo.taxon_unit_types.rank_name = 'Species'";
            System.Data.SqlClient.SqlCommand com = new System.Data.SqlClient.SqlCommand(commandstringInfo, con);

            System.Data.SqlClient.SqlDataReader dr = com.ExecuteReader();

            int counter = 0;
            int found = 0;
            int lookedfor = 0;

            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    counter++;
                    string completeName = dr.GetString(0);
                    int tsn = dr.GetInt32(1);
                    string rankName = dr.GetString(2);
                    int rankId = dr.GetInt16(3);
                    string lineage = dr.GetString(4);

                    if (rankName.Trim().Equals("Species") == true)
                    {
                        if (string.IsNullOrEmpty(lineage) == false)// && lineage.StartsWith("202423-") == true)
                        {
                            string[] items = lineage.Split('-');
                            if (items.Length > 0)
                            {
                                lookedfor++;
                                if (tgs.ContainsKey(completeName) == false)
                                {
                                    found++;
                                    tgs.Add(completeName, new TaxonomicGroups(completeName, tsn)); 
                                }

                                for (int index = 0; index < items.Length; index++)
                                {
                                    if (names.ContainsKey(Convert.ToInt32(items[index])) == true)
                                    {
                                        string[] values = names[Convert.ToInt32(items[index])];
                                        tgs[completeName].addNewlevel(values[0], values[2], Convert.ToInt32(values[1]));
                                    }
                                }
                            }
                        }
                    }

                    if (counter > 9999)
                    {
                        counter = 0;
                        lblUpdate2.Text = "Getting Species and genus names: Found: " +
                            lookedfor.ToString("N0") +" name of which: " + found.ToString("N0") + " are novel.";
                        Application.DoEvents();
                    }

                }
            }

            dr.Close();

            commandstringInfo = "select ITIS.dbo.taxonomic_units.complete_name, " +
                "ITIS.dbo.taxonomic_units.tsn, " +
                "ITIS.dbo.taxon_unit_types.rank_name, " +
                "ITIS.dbo.taxonomic_units.rank_id, " +
                "ITIS.dbo.hierarchy. hierarchy_string " +
                "from ITIS.dbo.taxonomic_units inner join ITIS.dbo.taxon_unit_types " +
                "on ITIS.dbo.taxonomic_units.rank_id = ITIS.dbo.taxon_unit_types.rank_id " +
                "inner join ITIS.dbo.hierarchy " +
                "on ITIS.dbo.hierarchy.tsn = ITIS.dbo.taxonomic_units.tsn " +
                "where ITIS.dbo.taxon_unit_types.rank_name = 'Genus'";
            com = new System.Data.SqlClient.SqlCommand(commandstringInfo, con);

            dr = com.ExecuteReader();

            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    string completeName = dr.GetString(0);
                    int tsn = dr.GetInt32(1);
                    string rankName = dr.GetString(2);
                    int rankId = dr.GetInt16(3);
                    string lineage = dr.GetString(4);

                    if (rankName.Trim().Equals("Genus") == true)
                    {
                        if (string.IsNullOrEmpty(lineage) == false)// && lineage.StartsWith("202423-") == true)
                        {
                            string[] items = lineage.Split('-');
                            if (items.Length > 0)
                            {
                                lookedfor++;
                                if (tgs.ContainsKey(completeName) == false)
                                {
                                    found++;
                                    tgs.Add(completeName, new TaxonomicGroups(completeName, tsn));
                                }

                                for (int index = 0; index < items.Length; index++)
                                {
                                    if (names.ContainsKey(Convert.ToInt32(items[index])) == true)
                                    {
                                        string[] values = names[Convert.ToInt32(items[index])];
                                        tgs[completeName].addNewlevel(values[0], values[2], Convert.ToInt32(values[1]));
                                    }
                                }
                            }
                        }
                    }
                    if (counter > 9999)
                    {
                        counter = 0;
                        lblUpdate2.Text = "Getting species and Genus names: Found: " +
                            lookedfor.ToString("N0") + " name of which: " + found.ToString("N0") + " are novel.";
                        Application.DoEvents();

                    }
                }
            }

            dr.Close();
            lblUpdate2.Text = "Getting species and genus names: Found: " +
                            lookedfor.ToString("N0") + " name of which: " + found.ToString("N0") + " are novel.";
            Application.DoEvents();
            return tgs;
        }

        private void setNameTSN()
        {
            seman = new Dictionary<string, int>();
            names = new Dictionary<int, string[]>();


            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString);
            if (con.State != ConnectionState.Open)
            { con.Open(); }
            string commandstringNames = "select tsn, complete_name, ITIS.dbo.taxonomic_units.rank_id, ITIS.dbo.taxon_unit_types.rank_name  from ITIS.dbo.taxonomic_units inner join ITIS.dbo.taxon_unit_types on ITIS.dbo.taxon_unit_types.rank_id = ITIS.dbo.taxonomic_units.rank_id";
            System.Data.SqlClient.SqlCommand com = new System.Data.SqlClient.SqlCommand(commandstringNames, con);
            System.Data.SqlClient.SqlDataReader dr = com.ExecuteReader();
            if (dr.HasRows == true)
            {
                int counter = 0;
                int found = 0;
                while (dr.Read())
                {
                    counter++;
                    int tsn = dr.GetInt32(0);
                    string name = dr.GetString(1).ToLower();
                    int rankID = dr.GetInt16(2);
                    string rankName = dr.GetString(3);
                    if (names.ContainsKey(tsn) == false)
                    {
                        string[] data = { name, rankID.ToString(), rankName };
                        names.Add(tsn, data);
                        found++;
                        if (seman.ContainsKey(name) == false)
                        {
                            seman.Add(name, tsn);
                        }
                        else
                        { ;}
                    }
                    else
                    { ; }
                    if (counter > 999)
                    {
                        counter = 0;
                        lblUpdate.Text = "Getting taxonomic names: Found : " +
                            found.ToString("N0") + " novel names.";
                        Application.DoEvents();
                    }
                }
            }
            dr.Close();
            con.Close();

        }

        private Dictionary<int, string> MakeLineageStrings()
        {
            Dictionary<int, string> data = new Dictionary<int, string>();

            foreach (TaxonomicGroups tg in tgs.Values)
            {
                string lineage = tg.getLineage();
                if (data.ContainsKey(tg.getTSN) == false)
                { data.Add(tg.getTSN, lineage); }
            }

            return data;
        }

        private void getXMLData()
        {
            //string xmlFile = FileString.OpenAs("Select the aggergated BLAST xml data", "*.txt|*.txt");
            //if (System.IO.File.Exists(xmlFile) == false) { return; }

            System.IO.StreamReader fr = null;
            dataLineAndLineage = new Dictionary<int, string>();
            int counter = 0;
            xmlSpeices = 0;
            xmlLookedFor = 0;
            xmlGenus = 0;

            done = new Dictionary<string, string>();
            try
            {
                fr = new System.IO.StreamReader(xmlFile);

                string line = null;
                string[] items = null;
                string[] names = null;
                string name = null;
                xmlTitle = fr.ReadLine();
                missed = new List<string>();
                string lastID = "";

                while (fr.Peek() > 0)
                {
                    xmlLookedFor++;
                    counter++;

                    line = fr.ReadLine();
                    line = line.Replace("PREDICTED: ", "").Replace(" sp.", "").Replace("Uncultured ", "");
                    items = line.Split('\t');
                    names = items[items.GetUpperBound(0) -1].Split(' ');
                    names[0] = names[0].Substring(names[0].IndexOf(":") + 1).Trim();
                    if (string.IsNullOrEmpty(names[0]) == false)
                    { name = names[0] + " " + names[1]; }
                    else
                    { name = names[1] + " " + names[2]; }

                    if (lastID.Equals(items[0]) == false)
                    {
                        if (dataLineAndLineage.ContainsKey(Convert.ToInt32(items[0])) == false)
                        {
                            line += "\t" + GetLineage(name);
                            dataLineAndLineage.Add(Convert.ToInt32(items[0]), line);
                            lastID = items[0];
                        }
                        else
                        { }
                    }

                    line = null;
                    items = null;

                    if (counter > 49)
                    {
                        counter = 0;
                        lblUpdate3.Text = "Looked at: " + xmlLookedFor.ToString("N0") + " sequences and linked to " +
                            xmlSpeices.ToString("N0") + " different speices and " +
                            xmlGenus.ToString("N0") + " different genus.";
                        Application.DoEvents();
                    }
                }
                lblUpdate3.Text = "Looked at: " + xmlLookedFor.ToString("N0") + " sequences and linked to " +
                    xmlSpeices.ToString("N0") + " different speices and " +
                    xmlGenus.ToString("N0") + " different genus.";
                Application.DoEvents();

            }
            catch (Exception ex)
            { }
            finally
            {
                if (fr != null) { fr.Close(); }
            }
        }

        private string GetLineage(string name)
        {
            //StringBuilder sb = new StringBuilder();
            
            name = name.Replace(" sp.", "");
            name = name.Replace("PREDICTED: ", "");
            name = name.Replace("Uncultured ", "");

            if (name.Contains(":") == true)
            { name = name.Substring(name.LastIndexOf(":") + 1).Trim(); }

            if (name.Equals("Physeter catodon"))
            { ;}

             if (name.Contains("Human") == true)
            { name = "Homo sapiens"; }
             else if (name.StartsWith("O.orca") == true)
             {
                 name = "Orcinus orca";
             }
             else if (name.StartsWith("Kryptolebias") == true)
             {
                 name = "Rivulus marmoratus";
             }
             else if (name.StartsWith("Psetta") == true)//"Kryptolebias marmoratus"//O.orca
             {
                 name = "Scophthalmus maximus";
                 //done.Add(name, "animalia\tbilateria\tdeuterostomia\tchordata\tvertebrata\tgnathostomata\tactinopterygii\tteleostei\tacanthopterygii\tpleuronectiformes\tpleuronectoidei\tScophthalmidae\tscophthalmus\tscophthalmus maximus");
                 //sb.Append(done[name]);
             }
             else if (name.StartsWith("S.coeruleoalba") == true)
             {
                 name = "Stenella coeruleoalba";
                 //done.Add(name, "animalia\tbilateria\tdeuterostomia\tchordata\tvertebrata\tgnathostomata\ttetrapoda\tmammalia\ttheria\teutheria\tcetacea\todontoceti\tdelphinidae\tstenalla\tstene coeruleoalba");
                 //sb.Append(done[name]);
             }
             else if (name.StartsWith("Theragra") == true)
             {
                 name = "Gadus chalcogrammus";
                 //done.Add(name, "animalia\tbilateria\tdeuterostomia\tchordata\tvertebrata\tgnathostomata\tactinopterygii\tteleostei\tparacanthopterygii\tgadiformes\tgadidae\tgadinae\tgadus\tgadus chalcogrammus");
                 //sb.Append(done[name]);
             }

             string line = name + "\tNo info";

             if (done.ContainsKey(name) == true)
            {
                line = (done[name]);
            }
            else
            {
                 bool foundIt=false;
                if (seman.ContainsKey(name.ToLower()) == true)
                {
                    if (lineage.ContainsKey(seman[name.ToLower()]) == true)
                    {
                        line = lineage[seman[name.ToLower()]];
                        xmlSpeices++;
                        done.Add(name, line);
                        foundIt=true;
                    }                    
                    else
                    {
                    var myKey = names.FirstOrDefault(x => x.Value[0] == name.ToLower()).Key;
                    if (lineage.ContainsKey((int)myKey))
                    {
                         line = lineage[(int)myKey];
                        xmlSpeices++;
                        done.Add(name, line);
                        foundIt = true;
                    }
                    }
                }

                if (foundIt == false)
                {
                    string partname = name + " ";
                    partname = partname.Substring(0, partname.IndexOf(" ")).Trim();


                    if (seman.ContainsKey(partname.ToLower()) == true)
                    {
                        if (lineage.ContainsKey(seman[partname.ToLower()]) == true)
                        {
                            line = lineage[seman[partname.ToLower()]];
                            xmlGenus++;
                            done.Add(name, line);
                            foundIt = true;
                        }
                    }
                    else
                    {
                        var myKey2 = names.FirstOrDefault(x => x.Value[0] == partname.ToLower()).Key;
                        if (lineage.ContainsKey((int)myKey2))
                        {
                            line = lineage[(int)myKey2];
                            done.Add(name, line);
                            xmlGenus++;
                            foundIt = true;
                        }
                    }


                }
                if (foundIt == false)
                {
                    if (missed.Contains(name) == false)
                    { missed.Add(name); }
                    line = name + "\tNo info";
                    done.Add(name, line);
                }
           }
            return line;
        }

        private void saveMissedToFile(string fileName)
        {
            fileName = fileName.Substring(0, fileName.Length - 4) + "_Missed_Names.txt";
            System.IO.StreamWriter fw = null;
            try
            {
                fw = new System.IO.StreamWriter(fileName);
                foreach (string s in missed)
                { fw.Write(s + "\n"); }
            }
            catch (Exception ex) { }
            finally
            { if (fw != null) { fw.Close(); } }
        }
        
        private void getReadCountData()
        {
            //string countsFile = FileString.OpenAs("Select the counts file", "*.txt|*.txt");
            //if (System.IO.File.Exists(countsFile) == false) { return; }

            string outfile = countsFile.Substring(0, countsFile.LastIndexOf(".")) + "_annotated.xls";

            System.IO.StreamReader fr = null;
            System.IO.StreamWriter fw = null;
            try
            {
                string line = null;
                string[] items = null;
                string[] itemsStuff = null;

                fr = new System.IO.StreamReader(countsFile);
                fw = new System.IO.StreamWriter(outfile);
                TaxonomicGroups tg = new TaxonomicGroups("w", 1);
                string titles = tg.getTitles;
                string titleLine = fr.ReadLine();
                items = titleLine.Split('\t');
                for (int i = 0; i < items.Length; i++)
                {
                    items[i] = items[i].Replace(".fasta.gz", "");
                    items[i] = items[i].Substring(items[i].LastIndexOf("_") + 1);
                }
                fw.Write("Fasta name\t" + string.Join("\t", items) + "\tSequence\t" + xmlTitle + "\tSequence\t" + tg.getTitles + "\n");
                int counter =0;
                int lookedFor=0;
                int found = 0;

                while (fr.Peek() > 0)
                {
                    line = fr.ReadLine();
                    items = line.Split('\t');
                    counter++;
                    lookedFor++;
                    if (dataLineAndLineage.ContainsKey(Convert.ToInt32(items[0])) == true)
                    {                                           
                        string stuff = dataLineAndLineage[Convert.ToInt32(items[0])];
                        itemsStuff = stuff.Split('\t');
                        if (items[items.GetUpperBound(0)].Contains(itemsStuff[8].Replace("-", "")) == true)
                        {
                            fw.Write(line);
                            string lastword = itemsStuff[0]; ;
                            for (int index = 0; index < itemsStuff.Length; index++)
                            {
                                if (itemsStuff[index].Equals(".") == true)
                                { lastword += ".";    }
                                else
                                { lastword = itemsStuff[index]; }
                                fw.Write("\t" + lastword);
                            }
                            fw.Write("\n");
                            found++;
                        }
                        else
                        { }
                        stuff = null;
                    }
                    line = null;
                    items = null;
                    if (counter > 499)
                    {
                        counter = 0;
                        lblUpdate4.Text = "Read: " + lookedFor.ToString("N0") + " sequences and " +
                            "matched to " + found.ToString("N0") + " phylogentic linearages";
                        Application.DoEvents();
                    }

                }
                lblUpdate4.Text = "Read: " + lookedFor.ToString("N0") + " sequences and " +
                            "matched to " + found.ToString("N0") + " phylogentic linearages";
                Application.DoEvents();
            }
            catch (Exception ex)
            { }
            finally
            {
                if (fr != null) { fr.Close(); }
                if (fw != null) { fw.Close(); }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string exportFile = FileString.SaveAs("Enter name of file to save taxonomic data too", "Taxonomic data (*.tax)|*.tax");
            if (exportFile.Equals("Cancel") == true) { return; }

            System.IO.StreamWriter fw = null;

            try
            {
                fw = new System.IO.StreamWriter(exportFile);

                setNameTSN();

                fw.Write("names\nTSN\tNames\trankID\trankName\n");
                foreach (int key in names.Keys)
                {
                    fw.Write(key.ToString() + "\t" + names[key][0] + "\t" + names[key][1] + "\t" + names[key][2] + "\n");
                }

                fw.Write("seman\nNames\tTSN\n");
                foreach (string key in seman.Keys)
                {
                    fw.Write(key + "\t" + Convert.ToInt32(seman[key]) + "\n");
                }

                tgs = getTaxonomicData();
                lineage = MakeLineageStrings();

                fw.Write("lineage\nTSN\tLineage\n");                           

                foreach (int key in lineage.Keys)
                {
                    fw.Write(key.ToString() + ":" + lineage[key] + "\n");                    
                }
            }
            catch
            { MessageBox.Show("An error occured", "Error"); }
            finally
            {
                if (fw != null) { fw.Close(); }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string fileName = FileString.OpenAs("Select the taxonomy file", "*.tax|*.tax");
            if (System.IO.File.Exists(fileName) == false) { return; }

            System.IO.StreamReader fr = null;

            try
            {
                fr = new System.IO.StreamReader(fileName);

                string line = "";
                string[] items = null;
                names = new Dictionary<int, string[]>();
                seman = new Dictionary<string, int>();
                lineage = new Dictionary<int, string>();

                line = fr.ReadLine();
                if (line.Equals("names") == false)
                {
                    MessageBox.Show("An error occured", "Error");
                    return;
                }
                line = fr.ReadLine();
                if (line.Equals("TSN	Names	rankID	rankName") == false)
                {
                    MessageBox.Show("An error occured", "Error");
                    return;
                }


                while (fr.Peek() > 0)
                {
                    line = fr.ReadLine();
                    items = line.Split('\t');
                    if (items.Length == 4)
                    {
                        string[] value = { items[1], items[2], items[3] };
                        names.Add(Convert.ToInt32(items[0]), value);
                    }
                    else
                    { break; }
                }

                if (line.Equals("seman") == false)
                {
                    MessageBox.Show("An error occured", "Error");
                    return;
                }
                line = fr.ReadLine();
                if (line.Equals("Names\tTSN") == false)
                {
                    MessageBox.Show("An error occured", "Error");
                    return;
                }

                while (fr.Peek() > 0)
                {
                    line = fr.ReadLine();
                    items = line.Split('\t');
                    if (items.Length == 2)
                    {
                        seman.Add(items[0], Convert.ToInt32(items[1]));
                    }
                    else
                    { break; }
                }
                

                if (line.Equals("lineage") == false)
                { MessageBox.Show("An error occured", "Error"); }
                line = fr.ReadLine();
                if (line.Equals("TSN\tLineage") == false) { return; }
                while (fr.Peek() > 0)
                {
                    line = fr.ReadLine();
                    items = line.Split(':');
                    if (items.Length == 2)
                    {
                        lineage.Add(Convert.ToInt32(items[0]), items[1]);
                    }
                }

            }
            catch
            { MessageBox.Show("An error occured", "Error"); }
            finally
            { if (fr != null) { fr.Close(); } }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
