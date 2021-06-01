#pragma once
#include <boost/iostreams/filter/gzip.hpp>
#include <boost/iostreams/filtering_stream.hpp>
#include <boost/iostreams/filter/zlib.hpp>
#include <fstream>
#include <vector>
#include "dirent.h"
#include <string>
#include <iostream>

using namespace std;

class DataStore
{
private:
	int count;
	string fileName;
	vector<string> UnsavedData;
	void SaveDataToFile();

	std::ofstream saveFile;
	
public:
	DataStore() {};
	~DataStore();
	DataStore(string FileName);
	void SetFileName(string Filename);
	bool SaveData(string Data);
	void SaveDataToFile(string line);
	void CloseFile();
};