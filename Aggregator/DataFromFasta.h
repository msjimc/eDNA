#pragma once
#include <iostream>
#include <iterator>
#include <algorithm>
#include <boost/algorithm/string.hpp>
#include <boost/iostreams/filter/gzip.hpp>
#include <boost/iostreams/filtering_stream.hpp>
#include <boost/iostreams/filter/zlib.hpp>
#include <boost/lexical_cast.hpp>
#include <fstream>
#include <vector>
#include <map>
#include <string>

using namespace std;

class DataFromFasta
{
private:
	int ProductSize;
	string fileName;
	std::ifstream file;
	boost::iostreams::filtering_stream<boost::iostreams::input> decompressor;



public:
	DataFromFasta() {}
	DataFromFasta(string fileName, int productSize);
	~DataFromFasta();

	map<string, map<int, int> > processData(map<string, map<int, int> > fileCounts, int fileIndex);

};