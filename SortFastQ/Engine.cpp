#include "Engine.h"

Engine::~Engine()
{ if (!file) { file.close(); }; }

Engine::Engine(string FileName, string folder, string type)
{
	cout << "Engine:" + type + "\n";
	
	size_t place = FileName.find_last_of("/");
	string NewFileName = folder + "/Filtered_" + FileName.substr(place + 1);
	
	if (type.compare("A") == 0)
	{ NewFileName = NewFileName.substr(0, NewFileName.length() - 4) + "a.gz"; }
	
	cout << NewFileName << "\n";

	file.exceptions(std::ios::failbit | std::ios::badbit);
	file.open(FileName.c_str(), std::ios_base::in | std::ios_base::binary);

	decompressor.push(boost::iostreams::gzip_decompressor());
	decompressor.push(file);

	vector<FastQLine> chunk;

	bool HasData = true;
	vector<string> FileNames;
	int count = 1;
	while (HasData == true)
	{
		chunk = GetNextChunkOfData();
		if (chunk.size() > 0)
		{				
			char buffer[50];			
			int n = sprintf(buffer, "%i", count);
			string str(buffer);			
			
			std::sort(chunk.begin(), chunk.end());
			chunk = RemoveDuplicates(chunk);
			SaveDataTooFile(FileName + str, chunk);
			FileNames.push_back(FileName + str);
			count++;
			chunk.clear();
		}
		else
		{
			HasData = false;
		}
	}

	if (FileNames.size() > 0)
	{
		cout << "multi\n";
		ReadReadSingle * RRSs;
		RRSs = new ReadReadSingle[FileNames.size()];
		for (int index = 0; index < FileNames.size(); index++)
		{
			RRSs[index].SetFileInformation(FileNames[index]);
		}
		
		string Name("X");
		string line;
		int indexToUse = 0;
		HasData = true;

		if (!fileOut) { fileOut.close(); }
		std::ofstream fileOut;
		fileOut.exceptions(std::ios::failbit | std::ios::badbit);
		fileOut.open(NewFileName.c_str(), std::ios_base::out | std::ios_base::binary);

		boost::iostreams::filtering_stream<boost::iostreams::output> compressor;
		compressor.push(boost::iostreams::gzip_compressor());
		compressor.push(fileOut);

		while (HasData == true)
		{
			Name = "X";
			for(int index = 0; index < FileNames.size(); index++)
			{
				if (RRSs[index].Name().compare(Name) < 0 )
				{
					indexToUse = index;
					Name = RRSs[index].Name();
					line = RRSs[index].GetRead().TextToSave(type);
				} 
			}
			for (int index = 0; index < FileNames.size(); index++)
			{
				if (RRSs[index].Name().compare(Name) == 0)
				{
					RRSs[index].GetNextRead();
				}
			}
			//save the line
			
			if (Name[0] == 'X')
			{ HasData = false; }
			else
			{				
				char* outR1 = (char*)line.c_str();
				if (line.length() > 0)
				{ compressor.write(outR1, strlen(outR1)); }
			}
		}

		for (int index = 0; index < FileNames.size(); index++)
		{ RRSs[index].CloseFile(); }

		RemoveFiles(FileNames);
	}
	else if (FileNames.size() == 1)
	{
		cout << "Single\n";
		string old(FileNames[0]);
		rename(old.c_str(), NewFileName.c_str());
	}
}

//Engine::Engine(string FileNameR1, string FileNameR2)
//{
//	size_t place = FileNameR1.find_last_of("\\");
//	string NewFileNameR1 = FileNameR1.substr(0, place + 1) + "Filtered_" + FileNameR1.substr(place + 1);
//	cout << NewFileNameR1 << "\n";
//	
//	place = FileNameR2.find_last_of("\\");
//	string NewFileNameR2 = FileNameR2.substr(0, place + 1) + "Filtered_" + FileNameR2.substr(place + 1);
//	cout << NewFileNameR2 << "\n";
//
//	file.exceptions(std::ios::failbit | std::ios::badbit);
//	file.open(FileNameR1, std::ios_base::in | std::ios_base::binary);
//
//	decompressor.push(boost::iostreams::gzip_decompressor());
//	decompressor.push(file);
//
//	file2.exceptions(std::ios::failbit | std::ios::badbit);
//	file2.open(FileNameR2, std::ios_base::in | std::ios_base::binary);
//
//	decompressor2.push(boost::iostreams::gzip_decompressor());
//	decompressor2.push(file2);
//
//	vector<FastQLine> chunk;
//
//	bool HasData = true;
//	vector<string> FileNames;
//	int count = 0;
//	while (HasData == true)
//	{
//		chunk.clear();
//		chunk = GetNextChunkOfData();
//		if (chunk.size() > 0)
//		{
//			std::sort(chunk.begin(), chunk.end());
//			chunk = RemoveDuplicates(chunk);
//			SaveDataTooFile(FileNameR1 + to_string(count), chunk);
//			FileNames.push_back(FileNameR1 + to_string(count));
//			count++;
//			chunk.clear();
//		}
//		else
//		{
//			HasData = false;
//		}
//	}
//	if (FileNames.size() > 1)
//	{
//		ReadReadSingle * RRSs;
//		RRSs = new ReadReadSingle[FileNames.size()];
//		for (int index = 0; index < FileNames.size(); index++)
//		{
//			RRSs[index].SetFileInformation(FileNames[index]);
//		}
//
//		string Name("X");
//		vector<string> line;
//		int indexToUse = 0;
//		HasData = true;
//
//		if (!fileOut) { fileOut.close(); }
//		std::ofstream fileOut;
//		fileOut.exceptions(std::ios::failbit | std::ios::badbit);
//		fileOut.open(NewFileNameR1.c_str(), std::ios_base::out | std::ios_base::binary);
//
//		boost::iostreams::filtering_stream<boost::iostreams::output> compressor;
//		compressor.push(boost::iostreams::gzip_compressor());
//		compressor.push(fileOut);
//
//		if (!fileOut2) { fileOut2.close(); }
//		std::ofstream fileOut2;
//		fileOut2.exceptions(std::ios::failbit | std::ios::badbit);
//		fileOut2.open(NewFileNameR2.c_str(), std::ios_base::out | std::ios_base::binary);
//
//		boost::iostreams::filtering_stream<boost::iostreams::output> compressor2;
//		compressor2.push(boost::iostreams::gzip_compressor());
//		compressor2.push(fileOut2);
//
//		while (HasData == true)
//		{
//			Name = "X";
//			for (int index = 1; index < FileNames.size(); index++)
//			{
//				if (RRSs[index].Name().compare(Name) < 0)
//				{
//					indexToUse = index;
//					Name = RRSs[index].Name();
//					line = RRSs[index].GetRead().TextToSave();
//				}
//			}
//			for (int index = 0; index < FileNames.size(); index++)
//			{
//				if (RRSs[index].Name().compare(Name) == 0)
//				{
//					RRSs[index].GetNextRead();
//				}
//			}
//			//save the line
//
//			if (Name[0] == 'X')
//			{
//				HasData = false;
//			}
//			else
//			{
//				char* outR1 = (char*)line[0].c_str();
//				if (line[0].length() > 0)
//				{
//					compressor.write(outR1, strlen(outR1));
//				}
//				outR1 = (char*)line[1].c_str();
//				if (line[1].length() > 0)
//				{
//					compressor2.write(outR1, strlen(outR1));
//				}
//			}
//		}
//
//		for (int index = 0; index < FileNames.size(); index++)
//		{
//			RRSs[index].CloseFile();
//		}
//
//		RemoveFiles(FileNames);
//	}
//	else if (FileNames.size() == 1)
//	{
//		/*cout << FileNames[0] << "\n";
//		string old(FileNames[0]);
//		cout << rename(old.c_str(), NewFileName.c_str()) << "\n";*/
//	}
//}

FastQLine Engine::GetNextRead()
{
	vector<string> data;
	string line;
	int tracker = 0;

	for (int index = 0; index < 4; index++)
	{
		if (getline(decompressor, line)) { tracker++; }
		data.push_back(line);
	}

	if (tracker != 4)
	{
		data.clear();
		FastQLine fl;
		return fl;
	}

	return FastQLine(data);

}

//FastQLine Engine::GetNextReads()
//{
//	vector<string> data;
//	string line;
//	int tracker = 0;
//
//	for (int index = 0; index < 4; index++)
//	{
//		if (getline(decompressor, line)) { tracker++; }
//		data.push_back(line);
//	}
//
//	for (int index = 0; index < 4; index++)
//	{
//		if (getline(decompressor2, line)) { tracker++; }
//		data.push_back(line);
//	}
//
//	if (tracker != 8)
//	{
//		data.clear();
//		FastQLine fl;
//		return fl;
//	}
//
//	return FastQLine(data);
//
//}

vector<FastQLine> Engine::GetNextChunkOfData()
{
	vector<FastQLine> FastQLines;
	bool NoData = false;
	
	while (NoData == false && FastQLines.size() < 100000)
	{		
		FastQLine fl(GetNextRead());
		if (fl.HasData() == true)
		{ FastQLines.push_back(fl); }
		else { NoData = true; }	
	}

	return FastQLines;
}

//vector<FastQLine> Engine::GetNextChunkOfDatas()
//{
//	vector<FastQLine> FastQLines;
//	bool NoData = false;
//
//	while (NoData == false && FastQLines.size() < 100000)
//	{
//		FastQLine fl(GetNextReads());
//		if (fl.HasData() == true)
//		{
//			FastQLines.push_back(fl);
//		}
//		else { NoData = true; }
//	}
//
//	return FastQLines;
//}

vector<FastQLine> Engine::RemoveDuplicates(vector<FastQLine> &chunk)
{
	vector<FastQLine> clean;
	if (chunk.size() > 1)
	{
		
		string lastName;
		lastName = chunk[0].Name();
		clean.push_back(chunk[0]);

		for (int index = 2; index < chunk.size(); index++)
		{
			if (lastName.compare(chunk[index].Name()) != 0)
			{
				clean.push_back(chunk[index]);
				lastName = chunk[index].Name();
			}
		}
	}
	else
	{
		clean = chunk;
	}

	return clean;
}

void Engine::SaveDataTooFile(string FileName, vector<FastQLine> &chunk)
{
	std::ofstream fileOut;
	fileOut.exceptions(std::ios::failbit | std::ios::badbit);
	fileOut.open(FileName.c_str(), std::ios_base::out | std::ios_base::binary);

	boost::iostreams::filtering_stream<boost::iostreams::output> compressor;
	compressor.push(boost::iostreams::gzip_compressor());
	compressor.push(fileOut);

	for (int index = 0; index < chunk.size(); index++)
	{
		string infor = chunk[index].TextToSave("Q");		
		char* outR1 = (char*)infor.c_str();
		if (infor.length() > 0)
		{
			compressor.write(outR1, strlen(outR1));
		}
		/*if (infor.size() > 1)
		{
			outR1 = (char*)infor[1].c_str();
			if (infor[1].length() > 0)
			{
				compressor.write(outR1, strlen(outR1));
			}
		}*/
	}
	try
	{
		if (!fileOut) { fileOut.close(); }
	}
	catch (exception ex)
	{
	}

}

void Engine::RemoveFiles(vector<string> FileNames)
{
	for (vector<string>::iterator it = FileNames.begin(); it != FileNames.end(); it++)
	{
		string fileName(*it);
		remove(fileName.c_str());
	}
}