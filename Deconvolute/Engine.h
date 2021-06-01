#pragma once
#include <iterator>
#include <algorithm>
#include <boost/iostreams/filter/gzip.hpp>
#include <boost/iostreams/filtering_stream.hpp>
#include <boost/iostreams/filter/zlib.hpp>
#include <boost/algorithm/string.hpp>
#include <fstream>
#include <vector>
#include <map>
#include "DataStore.h"

using namespace std;

class Engine
{
private:
	//aray of file objects
	DataStore * DataStores;

	//readcounts
	int InsertsRead;
	int InsertsCommon;
	int InsertsWithPrimersAndIndexs;
	int InsertsPassedTrimming;
	int InsertsLongerThanMinimum;
	int InsertsSaved;
	
	//cutoff and arguments
	int MinimumProductLength;
	int MinimumLength;
	int MinimumQuality;
	string R1Adaptor;
	string R2Adaptor;
	string baseFolder;
	vector<string> DataFileNames;
	bool NoConsole;

	//primers and indexes
	vector<string> primers;
	vector<string> OtherPrimer;
	vector<string> barcodes;

	//Sample data
	map<string, int> combinations;
	vector<string> AcceptedCombinations;
	map<string, int> FileIndexes;

	//reading files 
	std::ifstream fileR1;
	std::ifstream fileR2;

	boost::iostreams::filtering_stream<boost::iostreams::input> decompressorR1;
	boost::iostreams::filtering_stream<boost::iostreams::input> decompressorR2;

	//Open and shut data files
	void OpenFiles(vector<string> arguments);
	void CloseFiles();
	vector<string> MakeFiles(map<string, int> barcodes_Combinations);

	//Get the reads from stream
	vector<string> NextReadPair();

	//trim the data files
	bool testNames(string First, string Second);
	bool TrimQuality(string & Read, string & Quality, int minimumQuality);
	bool TrimAdaptor(string &Read, string &Quality, string Adaptor);

	//generic  matching functions
	bool InString(std::string First, std::string Second);
	bool InString(std::string First, std::string Second, float PercentMatch);
	bool InString_Wobble(std::string First, std::string Wobble, float PercentMatch);
	bool Match(char First, char wobble);
	
	//Get primer and index sequences in reads
	vector<string> PrimerIndexRead1(vector<string> Data);
	vector<string> PrimerIndexRead2(vector<string> Data);

	//get counts for each barcode combination in 100,000 reads
	map<string, int> GetCommonBarcodes();

	//make lists of primers and indexes
	vector<string> GetPrimerSequences(string FileName);
	vector<string> GetReversePrimerSet();
	vector<string> GetIndexes(string FileName);

	//combining steps
	bool IsRevesred(vector<string> data);
	std::vector<std::string> ReverseComplement(std::string Sequence, std::string Quality);
	std::vector<std::string> FindHomology(std::string name, std::string R1, std::string Q1, std::string R2, std::string Q2);
	std::vector<string> Combine(std::string name, std::string R1, std::string Q1, std::string R2, std::string Q2, int startPoint, int offest);
	void SaveTheData(vector<string>, string key, string FirstPrimer, string SecondPrimer);
	void CloseSaveDataFiles();
public:

	Engine() {};
	Engine(vector<string>);
	~Engine();
	void AnalyseData();
};