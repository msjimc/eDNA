#pragma once
#include <boost/iostreams/filter/gzip.hpp>
#include <boost/iostreams/filtering_stream.hpp>
#include <boost/iostreams/filter/zlib.hpp>
#include <fstream>
#include <vector>
#include <string>
#include <iostream>
#include "FastQLine.h"

using namespace std;

class ReadReadSingle
{
private:
	string fileName;
	std::ifstream file;
	boost::iostreams::filtering_stream<boost::iostreams::input> decompressor;
	FastQLine Read;

public:
	ReadReadSingle() {};
	ReadReadSingle(string FileName);
	~ReadReadSingle();
	void SetFileInformation(string FileName);
	void CloseFile();
	void GetNextRead();
	FastQLine GetRead();
	string Name() const { return Read.Name(); }
};

bool operator<(const ReadReadSingle &RRS1, const ReadReadSingle &RRS2);