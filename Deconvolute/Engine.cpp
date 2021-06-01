#include "Engine.h"

Engine::~Engine()
{
	try
	{
		if (!fileR1) { fileR1.close(); }
		if (!fileR2) { fileR2.close(); }
	}
	catch (exception ex)
	{
	}
}

Engine::Engine(vector<string> arguments)
{
	InsertsRead = 0;
	InsertsWithPrimersAndIndexs = 0;
	InsertsCommon = 0;
	InsertsPassedTrimming = 0;
	InsertsLongerThanMinimum = 0;
	InsertsSaved = 0;

	NoConsole = true;
	MinimumProductLength = 190;
	MinimumLength = 50;
	MinimumQuality = 20;
	R1Adaptor = "GATCGGAAGAG";
	R2Adaptor = "GATCGGAAGAG";
	primers = GetPrimerSequences(arguments[2]);
	barcodes = GetIndexes(arguments[3]);
	baseFolder = arguments[4];
	DataFileNames = arguments;

}

void Engine::CloseFiles()
{
	try
	{
		if (!fileR1) { fileR1.close(); }
		if (!fileR2) { fileR2.close(); }
	}
	catch (exception ex)
	{
	}
}

void Engine::OpenFiles(vector<string> arguments)
{
	fileR1.exceptions(std::ios::failbit | std::ios::badbit);
	fileR1.open(arguments[0].c_str(), std::ios_base::in | std::ios_base::binary);

	decompressorR1.push(boost::iostreams::gzip_decompressor());
	decompressorR1.push(fileR1);

	fileR2.exceptions(std::ios::failbit | std::ios::badbit);
	fileR2.open(arguments[1].c_str(), std::ios_base::in | std::ios_base::binary);

	decompressorR2.push(boost::iostreams::gzip_decompressor());
	decompressorR2.push(fileR2);

}

void Engine::AnalyseData()
{
	map<string, int> combinations = GetCommonBarcodes();
	AcceptedCombinations = MakeFiles(combinations);
	OtherPrimer = GetReversePrimerSet();

	vector<string> data = NextReadPair();
	int counter = 1;

	while (data.size() == 8)
	{
		InsertsRead++;
		if (testNames(data[0], data[4]) == true)
		{
			vector<string> indexR1 = PrimerIndexRead1(data);
			vector<string> indexR2 = PrimerIndexRead2(data);
			bool keepgoing = true;

			if (indexR1.size() == 2 && indexR2.size() == 2)
			{
				InsertsWithPrimersAndIndexs++;
				string key = indexR1[1] + "-" + indexR2[1];
				if (find(AcceptedCombinations.begin(), AcceptedCombinations.end(), key) != AcceptedCombinations.end())
				{
					InsertsCommon++;
					keepgoing = TrimQuality(data[1], data[3], MinimumQuality + 32);
					if (keepgoing == true)
					{
						keepgoing = TrimQuality(data[5], data[7], MinimumQuality + 32);
					}
					if (keepgoing == true)
					{
						keepgoing = TrimAdaptor(data[1], data[3], R1Adaptor);
					}
					if (keepgoing == true)
					{
						keepgoing = TrimAdaptor(data[5], data[7], R2Adaptor);
					}

					if (keepgoing == true)
					{
						InsertsPassedTrimming++;
						data = FindHomology(data[0], data[1], data[3], data[5], data[7]);//data size is now 4
						if (data.size() == 4 && data[1].length() >= MinimumProductLength)
						{
							InsertsLongerThanMinimum++;
							if (IsRevesred(data) == true)
							{
								vector<string> reversed = ReverseComplement(data[1], data[3]);
								data[1] = reversed[0];
								data[3] = reversed[1];
							}
							SaveTheData(data, key, indexR1[0], indexR2[0]);
						}
					}
				}
			}
			data = NextReadPair();
		}
		else
		{
			cout << "Read names seem to be out of order\n" << data[0] << "\n" << data[5] << "\n";
			data.clear();
		}
		if (NoConsole == false && counter > 9999)
		{
			cout << "Inserts processed: " << InsertsRead << "\tInserts saved: " << InsertsSaved << "\r";
			counter = 0;
		}
		counter++;
	}

	CloseSaveDataFiles();

	cout << "\nReads processed:                   " << InsertsRead << "\n";
	cout << "Reads with correct primer sequences" << InsertsWithPrimersAndIndexs << "\n";;
	cout << "Reads with accepted indexes        " << InsertsCommon << "\n";;
	cout << "Reads pairs passed trimming:       " << InsertsPassedTrimming << "\n";;
	cout << "Inserts longer than cutoff value:  " << InsertsLongerThanMinimum << "\n";;
	cout << "Inserts saved to files:            " << InsertsSaved << "\n";;
}

map<string, int> Engine::GetCommonBarcodes()
{
	OpenFiles(DataFileNames);

	vector<string> data = NextReadPair();
	int count = 0;
	while (data.size() == 8 && count < 100000)
	{
		count++;
		vector<string> indexR1 = PrimerIndexRead1(data);
		vector<string> indexR2 = PrimerIndexRead2(data);

		if (indexR1.size() == 2 && indexR2.size() == 2)
		{	string key = indexR1[1] + "-" + indexR2[1];
			if (IsRevesred(data) == true)
			{ key = indexR1[1] + "-" + indexR2[1]; }
			else
			{ key = indexR2[1] + "-" + indexR1[1]; }
				
				map<string, int>::iterator it = combinations.find(key);
				if (it == combinations.end())
				{
					if (find(barcodes.begin(), barcodes.end(), indexR1[1]) != barcodes.end() && find(barcodes.begin(), barcodes.end(), indexR2[1]) != barcodes.end())
					{
						combinations.insert(pair<string, int>(key, 1));
					}
				}
				else
				{
					it->second++;
				}					
		}

		data = NextReadPair();

	}

	cout << "Index combinations found in first 100,000 inserts \n";

	for (map<string, int>::iterator it = combinations.begin(); it != combinations.end(); it++)
	{
		cout << it->first << "\t" << it->second << "\n";
	}

	CloseFiles();

	return combinations;

}

vector<string> Engine::MakeFiles(map<string, int> barcodes_Combinations)
{
	vector<string> accepted;
	int NumberOfFiles = 0;
	for (map<string, int>::iterator it = barcodes_Combinations.begin(); it != barcodes_Combinations.end(); it++)
	{
		if (it->second > 10)
		{
			NumberOfFiles++;
		}
	}
	DataStores = new DataStore[NumberOfFiles];
	NumberOfFiles = 0;

	for (map<string, int>::iterator it = barcodes_Combinations.begin(); it != barcodes_Combinations.end(); it++)
	{
		if (it->second > 10)
		{
			string filename = baseFolder + "/" + it->first + ".fastq.gz";
			accepted.push_back(it->first);
			DataStores[NumberOfFiles].SetFileName(filename);
			FileIndexes.insert(pair<string, int>(it->first, NumberOfFiles));
			NumberOfFiles++;
		}
	}

	return accepted;
}

vector<string> Engine::NextReadPair()
{
	vector<std::string> nextReads;
	string line;
	int tracker = 0;

	for (int index = 0; index < 4; index++)
	{
		if (getline(decompressorR1, line)) { tracker++; }
		nextReads.push_back(line);
	}

	for (int index = 0; index < 4; index++)
	{
		if (getline(decompressorR2, line)) { tracker++; }
		nextReads.push_back(line);
	}

	if (tracker != 8)
	{
		nextReads.clear();
	}

	return nextReads;
}

bool Engine::testNames(string First, string Second)
{
	if (First.length() == Second.length())
	{
		int differences = 0;
		for (int index = 0; index < First.length(); index++)
		{
			if (First[index] != Second[index])
			{
				differences++;
			}
		}
		if (differences < 3)
		{
			return true;
		}
	}

	return false;

}

bool Engine::TrimQuality(string & Read, string & Quality, int minimumQuality)
{
	size_t len = Quality.length();
	int comparison = 0;
	int runningScore = 0;

	for (int index = 0; index < 6; index++)
	{
		runningScore += (int)Quality.at(index);
	}
	comparison = minimumQuality * 6;

	int endPoint = -1;

	for (size_t index = 6; index < len; index++)
	{
		runningScore += (int)Quality.at(index);
		runningScore -= (int)Quality.at(index - 6);
		if (comparison > runningScore)
		{
			endPoint = index - 6;
			index = len;
		}
	}

	if (endPoint > -1)
	{
		Read = Read.substr(0, endPoint);
		Quality = Quality.substr(0, endPoint);
	}

	if (Read.length() >= MinimumLength)
	{
		return true;
	}
	else
	{
		return false;
	}
}

bool Engine::TrimAdaptor(string & Read, string & Quality, string Adaptor)
{
	size_t Adaptor_Len = Adaptor.length();
	size_t Read_Len = Read.length();

	std::string Read_Piece;
	std::string Adaptor_Piece;

	int homology_Start = -1;

	if (Read_Len > Adaptor_Len)
	{
		for (int index = 6; index < Adaptor_Len; index++)
		{
			Read_Piece = Read.substr(Read_Len - index);
			Adaptor_Piece = Adaptor.substr(0, index);

			if (InString(Read_Piece, Adaptor_Piece) == true)
			{
				homology_Start = (int)Read_Len - index;
				index = (int)Adaptor_Len;
			}
		}

		if (homology_Start == -1)
		{
			for (int index = Read_Len - Adaptor_Len; index > -1; index--)
			{
				Read_Piece = Read.substr(index, Adaptor_Len);
				if (InString(Read_Piece, Adaptor) == true)
				{
					homology_Start = index;
					index = -1;
				}
			}
		}

		if (homology_Start > -1)
		{
			Read = Read.substr(0, homology_Start); /* + " " + Read.substr(homology_Start);*/
			Quality = Quality.substr(0, homology_Start);
		}

	}
	else
	{
		Read = "";
		Quality = "";
	}

	if (Read.length() >= MinimumLength)
	{
		return true;
	}
	else
	{
		return false;
	}

}

bool Engine::InString(std::string First, std::string Second)
{
	return InString(First, Second, 0.8f);
}

bool Engine::InString(std::string First, std::string Second, float PercentMatch)
{
	size_t len = First.length();
	if (len != Second.length())
	{
		return false;
	}

	int score = 0;
	for (int index = 0; index < len; index++)
	{
		if (First.at(index) == Second.at(index))
		{
			score++;
		}
	}

	float percent = (float)score;
	float minimum = (float)len * PercentMatch;
	if (minimum < percent)
	{
		return true;
	}
	else
	{
		return false;
	}
}

bool Engine::InString_Wobble(std::string First, std::string Wobble, float PercentMatch)
{
	size_t len = First.length();
	if (len != Wobble.length())
	{
		return false;
	}

	int score = 0;
	for (int index = 0; index < len; index++)
	{
		if (First.at(index) == Wobble.at(index))
		{
			score++;
		}
		else if (Match(First.at(index), Wobble.at(index)) == true)
		{
			score++;
		}
	}

	float percent = (float)score;
	float minimum = (float)len * PercentMatch;
	if (minimum < percent)
	{
		return true;
	}
	else
	{
		return false;
	}
}

bool Engine::Match(char First, char wobble)
{
	switch (First)
	{
	case 'A':
		switch (wobble)
		{
		case 'W':
			return true;
			break;
		case 'R':
			return true;
			break;
		}
		break;
	case 'C':
		switch (wobble)
		{
		case 'S':
			return true;
			break;
		case 'Y':
			return true;
			break;
		}
		break;
	case 'G':
		switch (wobble)
		{
		case 'S':
			return true;
			break;
		case 'R':
			return true;
			break;
		}
		break;
	case 'T':
		switch (wobble)
		{
		case 'W':
			return true;
			break;
		case 'Y':
			return true;
			break;
		}
		break;

	}

	return false;

}

//Sorting out primer and index data

vector<string> Engine::GetPrimerSequences(string FileName)
{
	vector<string> newPrimers;

	std::fstream primerFile;
	primerFile.exceptions(std::ios::failbit | std::ios::badbit);
	primerFile.open(FileName.c_str(), std::fstream::in);

	try
	{
		string sequence;
		while (!primerFile.eof())
		{
			std::getline(primerFile, sequence);
			boost::to_upper(sequence);
			newPrimers.push_back(sequence);
		}
	}
	catch (exception ex)
	{
		//cout << ex.what();
	}
	primerFile.close();

	return newPrimers;

}

vector<string> Engine::GetReversePrimerSet()
{
	vector<string> reversePrimers;
	for (int index = 1; index < primers.size(); index += 2)
	{
		reversePrimers.push_back(primers[index]);
	}

	return reversePrimers;

}

vector<string> Engine::GetIndexes(string FileName)
{
	vector<string> newPrimers;

	std::fstream primerFile;
	primerFile.exceptions(std::ios::failbit | std::ios::badbit);
	primerFile.open(FileName.c_str(), std::fstream::in);

	try
	{
		string sequence;
		while (!primerFile.eof())
		{
			std::getline(primerFile, sequence);
			boost::to_upper(sequence);
			newPrimers.push_back(sequence);
		}
	}
	catch (exception ex)
	{
		//cout << ex.what();
	}
	primerFile.close();

	return newPrimers;

}

//getting correct reads pairs

vector<string> Engine::PrimerIndexRead1(vector<string> data)
{
	vector<string> answer;
	string read = data.at(1);
	for (int primerIndex = 0; primerIndex < primers.size(); primerIndex++)
	{
		string p = primers.at(primerIndex);

		/*for (int index = 8; index < 19; index++)
		{*/
		int index = 12;
		if (InString_Wobble(read.substr(index, p.length()), p, 0.9f) == true)
		{
			answer.push_back(p);
			answer.push_back(read.substr(index - 8, 8));
			return answer;
		}
		//}
	}

	return answer;
}

vector<string> Engine::PrimerIndexRead2(vector<string> data)
{
	vector<string> answer;
	string read = data.at(5);

	for (int primerIndex = 0; primerIndex < primers.size(); primerIndex++)
	{
		string p = primers.at(primerIndex);
		/*for (int index = 8; index < 19; index++)
		{*/
		int index = 12;
		if (InString_Wobble(read.substr(index, p.length()), p, 0.9f) == true)
		{
			answer.push_back(p);
			answer.push_back(read.substr(index - 8, 8));
			return answer;
		}
		//}
	}

	return answer;
}

bool Engine::IsRevesred(vector<string> data)
{
	string read = data[1];

	for (int primerIndex = 0; primerIndex < OtherPrimer.size(); primerIndex++)
	{
		string p = OtherPrimer.at(primerIndex);

		int index = 12;
		if (InString_Wobble(read.substr(index, p.length()), p, 0.9f) == true)
		{
			return true;
		}
	}

	return false;

}

std::vector<string> Engine::Combine(std::string name, std::string R1, std::string Q1, std::string R2, std::string Q2, int startPoint, int Offset)
{
	string Sequence = "";
	string Quality = "";
	int lenR2 = R2.length();

	string temp1 = "";
	string temp2 = "";

	if (startPoint - Offset < 0)
	{
		int diff = startPoint - Offset;
		startPoint = startPoint - (Offset + diff);
	}


	if (startPoint > 0)
	{
		Sequence = R1.substr(0, startPoint);
		Quality = Q1.substr(0, startPoint);
		int index = 10;

		for (index = 0; index < R1.length() - startPoint; index++)
		{
			if (index < R2.length())
			{
				if (Q1[startPoint + index] == Q2[index])
				{
					temp1 += R1[startPoint + index];
					temp2 += R2[index];
					Sequence += R2[index];
					Quality += Q2[index];
				}
				else if ((int)Q1[startPoint + index] >(int)(Q2[index]))
				{
					temp1 += R1[startPoint + index];
					temp2 += R2[index];
					Sequence += R1[startPoint + index];
					Quality += Q1[startPoint + index];
				}
				else
				{
					temp1 += R1[startPoint + index];
					temp2 += R2[index];
					Sequence += R1[startPoint + index];
					Quality += Q1[startPoint + index];
				}
			}
			else
			{
				index++;
			}
		}


		if (index < R2.length())
		{
			Sequence += R2.substr(index);
			Quality += Q2.substr(index );
		}
		//if (index + 10 < R2.length())
		//{
		//	Sequence += R2.substr(index + 10);
		//	Quality += Q2.substr(index + 10);
		//}
	}

	vector<string> combined;

	if (Sequence.length() >= MinimumLength)
	{
		combined.push_back(name);
		combined.push_back(Sequence);
		combined.push_back("+");
		combined.push_back(Quality);
	}

	return combined;
}

std::vector<std::string> Engine::ReverseComplement(std::string Sequence, std::string Quality)
{
	std::string answer = "";
	std::string ytilauQ = "";
	std::string c, q;

	for (int index = Sequence.length() - 1; index > -1; index--)
	{
		c = Sequence.substr(index, 1);
		q = Quality.substr(index, 1);
		ytilauQ += q;

		if (c.compare("A") == 0 || c.compare("a") == 0)
		{
			answer += "T";
		}
		else if (c.compare("C") == 0 || c.compare("c") == 0)
		{
			answer += "G";
		}
		else if (c.compare("G") == 0 || c.compare("g") == 0)
		{
			answer += "C";
		}
		else if (c.compare("T") == 0 || c.compare("t") == 0)
		{
			answer += "A";
		}
		else
		{
			answer += "N";
		}
	}

	std::vector<std::string> reply;
	reply.push_back(answer);
	reply.push_back(ytilauQ);

	return reply;
}

std::vector<std::string> Engine::FindHomology(std::string name, std::string R1, std::string Q1, std::string R2, std::string Q2)
{
	std::vector<std::string> result;

	if (R2.length() > 70)
	{
		std::vector<std::string> inverted = ReverseComplement(R2, Q2);
		R2 = inverted[0];
		Q2 = inverted[1];

		std::size_t point = string::npos;
		int counter = 0;
		while (counter < 50 && point == string::npos)
		{
			point = R1.find(R2.substr(counter, 20));
			counter++;
		}

		if (point != string::npos)
		{
			//result = Combine(name, R1, Q1, R2, Q2, ((int)point + 10), (counter - 1) * 10);
			result = Combine(name, R1, Q1, R2, Q2, (int)point, (counter - 1) * 10);
		}

	}

	return result;
}

void Engine::SaveTheData(vector<string> data, string key, string FirstPrimer, string SecondPrimer)
{
	map<string, int>::iterator it = FileIndexes.find(key);
	if (it != FileIndexes.end())
	{
		string f = "@" + data[1].substr(0, 4) + "-" + data[1].substr(data[1].length() - 4, 4);
		DataStores[it->second].SaveData(f + ";" + FirstPrimer + ";" + SecondPrimer + ";" + "\n" + data[1] + "\n" + data[2] + "\n" + data[3] + "\n");

		InsertsSaved++;
	}
}

void Engine::CloseSaveDataFiles()
{
	for (map<string, int>::iterator it = FileIndexes.begin(); it != FileIndexes.end(); it++)
	{
		DataStores[it->second].CloseFile();
	}
}
