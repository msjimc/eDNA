#include "DataStore.h"

DataStore::~DataStore()
{
	CloseFile();
}

DataStore::DataStore(string FileName)
{
	count = 0;
	fileName = FileName;
	saveFile.exceptions(std::ios::failbit | std::ios::badbit);
	saveFile.open((fileName).c_str(), std::ios_base::app | std::ios_base::binary);
	}

void DataStore::SetFileName(string FileName)
{
	fileName = FileName;
	saveFile.exceptions(std::ios::failbit | std::ios::badbit);
	saveFile.open((fileName).c_str(), std::ios_base::out | std::ios_base::binary);
}

bool DataStore::SaveData(string Data)
{
	bool answer = true;
	count++;

	UnsavedData.push_back(Data);
	if (UnsavedData.size() > 999)
	{
		try
		{
			SaveDataToFile();
			UnsavedData.clear();
		}
		catch (exception ex)
		{ answer=false;}
	}
	
	return answer;
}

void DataStore::SaveDataToFile()
{
	if (UnsavedData.size() > 0)
	{
		boost::iostreams::filtering_stream<boost::iostreams::output> outStream;
		outStream.push(boost::iostreams::gzip_compressor());
		outStream.push(saveFile);

		for (vector<string>::iterator it = UnsavedData.begin(); it != UnsavedData.end(); it++)
		{
			try
			{
				string line(*it);
				char* outR1 = (char*)line.c_str();
				outStream.write(outR1, strlen(outR1));			
			}
			catch(exception ex)
			{ }
		}		
	}
}

void DataStore::SaveDataToFile(string line)
{
	try
	{
		int l = line.length();
		char* outR1 = (char*)line.c_str();
		saveFile.write(outR1, strlen(outR1));
	}
	catch (exception ex)
	{
	}
}

void DataStore::CloseFile()
{
	SaveDataToFile(); 
	saveFile.close();
	cout << fileName << ":\t" << count << "\n";
}
