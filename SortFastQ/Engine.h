#pragma once
#include <boost/iostreams/filter/gzip.hpp>
#include <boost/iostreams/filtering_stream.hpp>
#include <boost/iostreams/filter/zlib.hpp>
#include <fstream>
#include <vector>
#include <string>
#include <iostream>
#include "dirent.h"
#include "FastQLine.h"
#include "ReadReadSingle.h"
#include <stdio.h>

using namespace std;

class Engine
{

private:
	std::ifstream file;
	boost::iostreams::filtering_stream<boost::iostreams::input> decompressor;
	std::ifstream file2;
	boost::iostreams::filtering_stream<boost::iostreams::input> decompressor2;

	std::ofstream fileOut;
	boost::iostreams::filtering_stream<boost::iostreams::output> compressor;
	/*std::ofstream fileOut2;
	boost::iostreams::filtering_stream<boost::iostreams::output> compressor2;*/

	vector<FastQLine> GetNextChunkOfData();
	/*vector<FastQLine> GetNextChunkOfDatas();*/
	vector<FastQLine> RemoveDuplicates(vector<FastQLine> &chunk);

	FastQLine GetNextRead();
	/*FastQLine GetNextReads();*/

	void SaveDataTooFile(string FileName, vector<FastQLine> &chunk);
	void RemoveFiles(vector<string> FileNames);

public:
	Engine() {};
	Engine(string FileName, string folder, string type);
	//Engine(string FileNameR1, string FileNameR2);
	~Engine();
};